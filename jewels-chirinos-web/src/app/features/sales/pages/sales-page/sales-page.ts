import { CommonModule } from '@angular/common';
import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { forkJoin } from 'rxjs';

import { AuthService } from '../../../../core/services/auth';
import { CustomersService } from '../../../../core/services/customers.service';
import { ProductsService } from '../../../../core/services/products.service';
import { SalesService } from '../../../../core/services/sales.service';
import { Customer } from '../../../../shared/models/customer.model';
import { Product } from '../../../../shared/models/product.model';
import { CreateSaleRequest } from '../../../../shared/models/sale.model';

interface CartItem {
  productId: number;
  name: string;
  code: string;
  unitPrice: number;
  quantity: number;
  stock: number;
}

@Component({
  selector: 'app-sales-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './sales-page.html',
  styleUrl: './sales-page.scss'
})
export class SalesPage implements OnInit {
  private productsService = inject(ProductsService);
  private customersService = inject(CustomersService);
  private salesService = inject(SalesService);
  private authService = inject(AuthService);

  private readonly user = this.authService.getUser();

  products = signal<Product[]>([]);
  customers = signal<Customer[]>([]);
  cart = signal<CartItem[]>([]);

  search = '';
  selectedCustomerId: number | null = null;
  paymentMethod = 'EFECTIVO';
  discountAmount = 0;
  taxAmount = 0;
  observations = '';
  loading = false;
  saving = false;
  error = '';
  success = '';

  readonly canAccess = ['ADMIN', 'SELLER'].includes(this.user?.role ?? '');

  filteredProducts = computed(() => {
    const term = this.search.trim().toLowerCase();
    if (!term) return this.products().slice(0, 12);

    return this.products().filter((p) =>
      p.name?.toLowerCase().includes(term) ||
      p.code?.toLowerCase().includes(term) ||
      p.barcode?.toLowerCase()?.includes(term)
    );
  });

  subtotal = computed(() =>
    this.cart().reduce((sum, item) => sum + item.unitPrice * item.quantity, 0)
  );

  total = computed(() =>
    Math.max(0, this.subtotal() - this.discountAmount + this.taxAmount)
  );

  ngOnInit(): void {
    if (!this.canAccess) {
      this.error = 'No tienes permisos para registrar ventas.';
      return;
    }

    this.loadData();
  }

  loadData(forceRefresh = false): void {
    this.loading = true;
    this.error = '';

    forkJoin({
      products: this.productsService.getAll(forceRefresh),
      customers: this.customersService.getAll(forceRefresh)
    }).subscribe({
      next: ({ products, customers }) => {
        this.products.set(products ?? []);
        this.customers.set(customers ?? []);
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        this.error = err?.error?.message ?? 'No se pudo cargar la información.';
      }
    });
  }

  trackByProductId = (_: number, product: Product) => product.productId;
  trackByCustomerId = (_: number, customer: Customer) => customer.customerId;
  trackByCartItem = (_: number, item: CartItem) => item.productId;

  getPrice(product: Product): number {
    return product.sellingPrice ?? product.salePrice ?? 0;
  }

  addToCart(product: Product): void {
    if (product.stock <= 0) return;

    const current = [...this.cart()];
    const existing = current.find((x) => x.productId === product.productId);

    if (existing) {
      if (existing.quantity >= existing.stock) return;
      existing.quantity += 1;
    } else {
      current.push({
        productId: product.productId,
        name: product.name,
        code: product.code,
        unitPrice: this.getPrice(product),
        quantity: 1,
        stock: product.stock
      });
    }

    this.cart.set(current);
  }

  increase(item: CartItem): void {
    const current = [...this.cart()];
    const target = current.find((x) => x.productId === item.productId);
    if (!target) return;
    if (target.quantity >= target.stock) return;

    target.quantity += 1;
    this.cart.set(current);
  }

  decrease(item: CartItem): void {
    const current = [...this.cart()];
    const target = current.find((x) => x.productId === item.productId);
    if (!target) return;

    target.quantity -= 1;
    this.cart.set(current.filter((x) => x.quantity > 0));
  }

  remove(item: CartItem): void {
    this.cart.set(this.cart().filter((x) => x.productId !== item.productId));
  }

  submitSale(): void {
    if (this.cart().length === 0) {
      this.error = 'Agrega al menos un producto.';
      return;
    }

    if (!this.user) {
      this.error = 'No se encontró el usuario logueado.';
      return;
    }

    this.saving = true;
    this.error = '';
    this.success = '';

    const payload: CreateSaleRequest = {
      customerId: this.selectedCustomerId,
      saleDetails: this.cart().map((item) => ({
        productId: item.productId,
        quantity: item.quantity,
        unitPrice: item.unitPrice
      })),
      taxAmount: this.taxAmount,
      discountAmount: this.discountAmount,
      paymentMethod: this.paymentMethod,
      observations: this.observations,
      createdBy: this.user.username
    };

    this.salesService.create(payload).subscribe({
      next: (response) => {
        this.saving = false;
        this.success = `Venta registrada correctamente. N° ${response.saleNumber}`;
        this.cart.set([]);
        this.discountAmount = 0;
        this.taxAmount = 0;
        this.observations = '';
        this.selectedCustomerId = null;
        this.loadData(true);
      },
      error: (err) => {
        this.saving = false;
        this.error = err?.error?.message ?? 'No se pudo registrar la venta.';
      }
    });
  }
}