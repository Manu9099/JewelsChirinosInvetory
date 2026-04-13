import { CommonModule } from '@angular/common';
import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { forkJoin, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthService } from '../../../../core/services/auth';
import { CustomersService } from '../../../../core/services/customers.service';
import { ProductsService } from '../../../../core/services/products.service';
import { SalesService } from '../../../../core/services/sales.service';
import { Customer } from '../../../../shared/models/customer.model';
import { Product } from '../../../../shared/models/product.model';
import { SaleResponse } from '../../../../shared/models/sale.model';

@Component({
  selector: 'app-dashboard-page',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard-page.component.html',
  styleUrl: './dashboard-page.scss'
})
export class DashboardPageComponent implements OnInit {
  private authService = inject(AuthService);
  private productsService = inject(ProductsService);
  private customersService = inject(CustomersService);
  private salesService = inject(SalesService);

  private readonly user = this.authService.getUser();

  loading = false;
  error = '';

  products = signal<Product[]>([]);
  customers = signal<Customer[]>([]);
  sales = signal<SaleResponse[]>([]);

  readonly canSeeSales = ['ADMIN', 'SELLER'].includes(this.user?.role ?? '');
  readonly canSeeCustomers = ['ADMIN', 'SELLER'].includes(this.user?.role ?? '');
  readonly canManageProducts = ['ADMIN', 'WAREHOUSE'].includes(this.user?.role ?? '');

  readonly todaySalesTotal = computed(() =>
    this.sales()
      .filter((sale) => this.isToday(sale.createdAt))
      .reduce((sum, sale) => sum + (sale.totalAmount ?? 0), 0)
  );

  readonly todayTickets = computed(() =>
    this.sales().filter((sale) => this.isToday(sale.createdAt)).length
  );

  readonly activeProducts = computed(() =>
    this.products().filter((product) => product.status !== false).length
  );

  readonly activeCustomers = computed(() =>
    this.customers().filter((customer) => customer.status !== false).length
  );

  readonly lowStockCount = computed(() =>
    this.products().filter((product) => product.stock > 0 && product.stock <= 5).length
  );

  readonly outOfStockCount = computed(() =>
    this.products().filter((product) => product.stock <= 0).length
  );

  ngOnInit(): void {
    this.loadData();
  }

  loadData(forceRefresh = false): void {
    this.loading = true;
    this.error = '';

    forkJoin({
      products: this.productsService.getAll(forceRefresh),
      customers: this.canSeeCustomers
        ? this.customersService.getAll(forceRefresh).pipe(catchError(() => of([])))
        : of([]),
      sales: this.canSeeSales
        ? this.salesService.getAll(forceRefresh).pipe(catchError(() => of([])))
        : of([])
    }).subscribe({
      next: ({ products, customers, sales }) => {
        this.products.set(products ?? []);
        this.customers.set(customers ?? []);
        this.sales.set(sales ?? []);
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.error = 'No se pudieron cargar los indicadores del dashboard.';
      }
    });
  }

  private isToday(value: string): boolean {
    const date = new Date(value);
    const today = new Date();

    return (
      date.getFullYear() === today.getFullYear() &&
      date.getMonth() === today.getMonth() &&
      date.getDate() === today.getDate()
    );
  }
}