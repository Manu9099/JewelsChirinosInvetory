import { CommonModule } from '@angular/common';
import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
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
  private customersService = inject(CustomersService);

  customers = signal<Customer[]>([]);
  loading = false;
  error = '';
  search = '';

  filteredCustomers = computed(() => {
    const term = this.search.trim().toLowerCase();
    if (!term) return this.customers();

    return this.customers().filter((customer) =>
      customer.fullName?.toLowerCase().includes(term) ||
      customer.email?.toLowerCase().includes(term) ||
      customer.phone?.toLowerCase().includes(term) ||
      customer.documentNumber?.toLowerCase().includes(term)
    );
  });

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.loading = true;
    this.error = '';

    this.customersService.getAll().subscribe({
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