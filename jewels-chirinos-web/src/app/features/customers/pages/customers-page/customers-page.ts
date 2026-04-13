import { CommonModule } from '@angular/common';
import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AuthService } from '../../../../core/services/auth';
import { CustomersService } from '../../../../core/services/customers.service';
import { Customer } from '../../../../shared/models/customer.model';

@Component({
  selector: 'app-customers-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './customers-page.html',
  styleUrl: './customers-page.scss'
})
export class CustomersPage implements OnInit {
  private authService = inject(AuthService);
  private customersService = inject(CustomersService);

  private readonly user = this.authService.getUser();

  customers = signal<Customer[]>([]);
  loading = false;
  error = '';
  search = '';

  readonly canAccess = ['ADMIN', 'SELLER'].includes(this.user?.role ?? '');

  filteredCustomers = computed(() => {
    const term = this.search.trim().toLowerCase();
    if (!term) return this.customers();

    return this.customers().filter((customer) =>
      customer.fullName?.toLowerCase().includes(term) ||
      customer.email?.toLowerCase().includes(term) ||
      customer.phone?.toLowerCase()?.includes(term) ||
      customer.documentNumber?.toLowerCase()?.includes(term)
    );
  });

  ngOnInit(): void {
    if (!this.canAccess) {
      this.error = 'No tienes permisos para ver clientes.';
      return;
    }

    this.loadCustomers();
  }

  loadCustomers(forceRefresh = false): void {
    this.loading = true;
    this.error = '';

    this.customersService.getAll(forceRefresh).subscribe({
      next: (response) => {
        this.customers.set(response ?? []);
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        this.error = err?.error?.message ?? 'No se pudieron cargar los clientes.';
      }
    });
  }
}