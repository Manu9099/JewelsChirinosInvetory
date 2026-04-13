import { CommonModule } from '@angular/common';
import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { SuppliersService } from '../../../../core/services/suppliers.service';
import {
  Supplier,
  CreateSupplierRequest,
  UpdateSupplierRequest
} from '../../../../shared/models/supplier.model';

type SupplierForm = {
  name: string;
  rucDni: string;
  contactName: string;
  email: string;
  phone: string;
  address: string;
  status: boolean;
};

@Component({
  selector: 'app-suppliers-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './suppliers-page.html',
  styleUrl: './suppliers-page.scss'
})
export class SuppliersPage implements OnInit {
  private suppliersService = inject(SuppliersService);

  suppliers = signal<Supplier[]>([]);
  loading = false;
  saving = false;
  error = '';
  success = '';
  search = '';
  editingId: number | null = null;

  form: SupplierForm = this.buildEmptyForm();

  readonly isEditing = computed(() => this.editingId !== null);

  filteredSuppliers = computed(() => {
    const term = this.search.trim().toLowerCase();
    if (!term) return this.suppliers();

    return this.suppliers().filter((supplier) =>
      supplier.name?.toLowerCase().includes(term) ||
      supplier.rucDni?.toLowerCase()?.includes(term) ||
      supplier.contactName?.toLowerCase()?.includes(term) ||
      supplier.email?.toLowerCase()?.includes(term)
    );
  });

  ngOnInit(): void {
    this.loadSuppliers();
  }

  private buildEmptyForm(): SupplierForm {
    return {
      name: '',
      rucDni: '',
      contactName: '',
      email: '',
      phone: '',
      address: '',
      status: true
    };
  }

  loadSuppliers(forceRefresh = false): void {
    this.loading = true;
    this.error = '';

    this.suppliersService.getAll(forceRefresh).subscribe({
      next: (response) => {
        this.suppliers.set(response ?? []);
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        this.error = err?.error?.message ?? 'No se pudieron cargar los proveedores.';
      }
    });
  }

  startCreate(): void {
    this.editingId = null;
    this.form = this.buildEmptyForm();
    this.error = '';
    this.success = '';
  }

  startEdit(supplier: Supplier): void {
    this.editingId = supplier.supplierId;
    this.form = {
      name: supplier.name ?? '',
      rucDni: supplier.rucDni ?? '',
      contactName: supplier.contactName ?? '',
      email: supplier.email ?? '',
      phone: supplier.phone ?? '',
      address: supplier.address ?? '',
      status: supplier.status ?? true
    };
    this.error = '';
    this.success = '';
  }

  save(): void {
    if (!this.form.name.trim()) {
      this.error = 'El nombre es obligatorio.';
      return;
    }

    this.saving = true;
    this.error = '';
    this.success = '';

    if (this.editingId === null) {
      const payload: CreateSupplierRequest = {
        name: this.form.name.trim(),
        rucDni: this.form.rucDni.trim() || null,
        contactName: this.form.contactName.trim() || null,
        email: this.form.email.trim() || null,
        phone: this.form.phone.trim() || null,
        address: this.form.address.trim() || null
      };

      this.suppliersService.create(payload).subscribe({
        next: () => {
          this.saving = false;
          this.success = 'Proveedor creado correctamente.';
          this.startCreate();
          this.loadSuppliers(true);
        },
        error: (err) => {
          this.saving = false;
          this.error = err?.error?.message ?? 'No se pudo crear el proveedor.';
        }
      });

      return;
    }

    const payload: UpdateSupplierRequest = {
      name: this.form.name.trim(),
      rucDni: this.form.rucDni.trim() || null,
      contactName: this.form.contactName.trim() || null,
      email: this.form.email.trim() || null,
      phone: this.form.phone.trim() || null,
      address: this.form.address.trim() || null,
      status: this.form.status
    };

    this.suppliersService.update(this.editingId, payload).subscribe({
      next: () => {
        this.saving = false;
        this.success = 'Proveedor actualizado correctamente.';
        this.startCreate();
        this.loadSuppliers(true);
      },
      error: (err) => {
        this.saving = false;
        this.error = err?.error?.message ?? 'No se pudo actualizar el proveedor.';
      }
    });
  }

  remove(supplier: Supplier): void {
    const confirmed = window.confirm(`¿Desactivar "${supplier.name}"?`);
    if (!confirmed) return;

    this.suppliersService.delete(supplier.supplierId).subscribe({
      next: () => {
        this.success = 'Proveedor desactivado correctamente.';
        if (this.editingId === supplier.supplierId) this.startCreate();
        this.loadSuppliers(true);
      },
      error: (err) => {
        this.error = err?.error?.message ?? 'No se pudo desactivar el proveedor.';
      }
    });
  }
}