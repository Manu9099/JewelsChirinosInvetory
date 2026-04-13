import { CommonModule } from '@angular/common';
import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AuthService } from '../../../../core/services/auth';
import { ProductsService } from '../../../../core/services/products.service';
import { forkJoin } from 'rxjs';
import { SuppliersService } from '../../../../core/services/suppliers.service';
import { Supplier } from '../../../../shared/models/supplier.model';

import {
  CreateProductRequest,
  Product,
  UpdateProductRequest
} from '../../../../shared/models/product.model';

type ProductForm = {
  code: string;
  barcode: string;
  name: string;
  description: string;
  categoryId: number | null;
  materialId: number | null;
  supplierId: number | null;
  costPrice: number | null;
  sellingPrice: number | null;
  weight: number | null;
  status: boolean;
};



@Component({
  selector: 'app-product-management-page',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-management-page.html',
  styleUrl: './product-management-page.scss'
})
export class ProductManagementPage implements OnInit {
  private authService = inject(AuthService);
  private productsService = inject(ProductsService);

  private readonly user = this.authService.getUser();

  private suppliersService = inject(SuppliersService);

 suppliers = signal<Supplier[]>([]);

  products = signal<Product[]>([]);
  loading = false;
  saving = false;
  error = '';
  success = '';
  search = '';

  editingProductId: number | null = null;

  readonly canManage = ['ADMIN', 'WAREHOUSE'].includes(this.user?.role ?? '');
  readonly canDelete = (this.user?.role ?? '') === 'ADMIN';

  form: ProductForm = this.buildEmptyForm();

  filteredProducts = computed(() => {
    const term = this.search.trim().toLowerCase();
    if (!term) return this.products();

    return this.products().filter((product) =>
      product.name?.toLowerCase().includes(term) ||
      product.code?.toLowerCase().includes(term) ||
      product.barcode?.toLowerCase()?.includes(term) ||
      product.categoryName?.toLowerCase()?.includes(term) ||
      product.materialName?.toLowerCase()?.includes(term) ||
      product.supplierName?.toLowerCase()?.includes(term)
    );
  });

  readonly knownCategories = computed(() => {
    const map = new Map<number, string>();
    for (const product of this.products()) {
      if (product.categoryId) {
        map.set(product.categoryId, product.categoryName || `Categoría ${product.categoryId}`);
      }
    }
    return Array.from(map.entries()).map(([id, name]) => ({ id, name }));
  });

  readonly knownMaterials = computed(() => {
    const map = new Map<number, string>();
    for (const product of this.products()) {
      if (product.materialId) {
        map.set(product.materialId, product.materialName || `Material ${product.materialId}`);
      }
    }
    return Array.from(map.entries()).map(([id, name]) => ({ id, name }));
  });

  readonly knownSuppliers = computed(() => {
    const map = new Map<number, string>();
    for (const product of this.products()) {
      if (product.supplierId) {
        map.set(product.supplierId, product.supplierName || `Proveedor ${product.supplierId}`);
      }
    }
    return Array.from(map.entries()).map(([id, name]) => ({ id, name }));
  });

  readonly isEditing = computed(() => this.editingProductId !== null);

ngOnInit(): void {
  if (!this.canManage && !this.canDelete) {
    this.error = 'No tienes permisos para gestionar productos.';
    return;
  }

  this.loadData();
}

  private buildEmptyForm(): ProductForm {
    return {
      code: '',
      barcode: '',
      name: '',
      description: '',
      categoryId: null,
      materialId: null,
      supplierId: null,
      costPrice: null,
      sellingPrice: null,
      weight: null,
      status: true
    };
  }

  loadData(forceRefresh = false): void {
  this.loading = true;
  this.error = '';

  forkJoin({
    products: this.productsService.getAll(forceRefresh),
    suppliers: this.suppliersService.getAll(forceRefresh)
  }).subscribe({
    next: ({ products, suppliers }) => {
      this.products.set(products ?? []);
      this.suppliers.set((suppliers ?? []).filter(x => x.status));
      this.loading = false;
    },
    error: (err) => {
      this.loading = false;
      this.error = err?.error?.message ?? 'No se pudieron cargar productos y proveedores.';
    }
  });
}

  loadProducts(forceRefresh = false): void {
    this.loading = true;
    this.error = '';

    this.productsService.getAll(forceRefresh).subscribe({
      next: (response) => {
        this.products.set(response ?? []);
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        this.error = err?.error?.message ?? 'No se pudieron cargar los productos.';
      }
    });
  }

  startCreate(): void {
    this.editingProductId = null;
    this.form = this.buildEmptyForm();
    this.error = '';
    this.success = '';
  }

  startEdit(product: Product): void {
    this.editingProductId = product.productId;

    this.form = {
      code: product.code ?? '',
      barcode: product.barcode ?? '',
      name: product.name ?? '',
      description: product.description ?? '',
      categoryId: product.categoryId ?? null,
      materialId: product.materialId ?? null,
      supplierId: product.supplierId ?? null,
      costPrice: product.costPrice ?? null,
      sellingPrice: product.sellingPrice ?? product.salePrice ?? null,
      weight: product.weight ?? null,
      status: product.status ?? true
    };

    this.error = '';
    this.success = '';
  }

  save(): void {
    if (!this.canManage) {
      this.error = 'No tienes permisos para guardar productos.';
      return;
    }

    if (!this.form.name.trim()) {
      this.error = 'El nombre es obligatorio.';
      return;
    }

    if (!this.form.categoryId) {
      this.error = 'El categoryId es obligatorio.';
      return;
    }

    if (!this.form.supplierId) {
      this.error = 'El supplierId es obligatorio.';
      return;
    }

    if (this.form.costPrice === null || this.form.costPrice < 0) {
      this.error = 'El costPrice es obligatorio.';
      return;
    }

    if (this.form.sellingPrice === null || this.form.sellingPrice < 0) {
      this.error = 'El sellingPrice es obligatorio.';
      return;
    }

    this.saving = true;
    this.error = '';
    this.success = '';

    if (this.editingProductId === null) {
      if (!this.form.code.trim()) {
        this.error = 'El código es obligatorio.';
        this.saving = false;
        return;
      }

      const payload: CreateProductRequest = {
        code: this.form.code.trim(),
        barcode: this.form.barcode.trim() || null,
        name: this.form.name.trim(),
        description: this.form.description.trim() || null,
        categoryId: Number(this.form.categoryId),
        materialId: this.form.materialId ? Number(this.form.materialId) : null,
        supplierId: Number(this.form.supplierId),
        costPrice: Number(this.form.costPrice),
        sellingPrice: Number(this.form.sellingPrice),
        weight: this.form.weight !== null ? Number(this.form.weight) : null,
        certificate: null,
        imageUrl: null,
        sku: null
      };

      this.productsService.create(payload).subscribe({
        next: () => {
          this.saving = false;
          this.success = 'Producto creado correctamente.';
          this.startCreate();
          this.loadData(true);
        },
        error: (err) => {
          this.saving = false;
          this.error = err?.error?.message ?? 'No se pudo crear el producto.';
        }
      });

      return;
    }

    const payload: UpdateProductRequest = {
      name: this.form.name.trim(),
      description: this.form.description.trim() || null,
      categoryId: Number(this.form.categoryId),
      materialId: this.form.materialId ? Number(this.form.materialId) : null,
      supplierId: Number(this.form.supplierId),
      costPrice: Number(this.form.costPrice),
      sellingPrice: Number(this.form.sellingPrice),
      weight: this.form.weight !== null ? Number(this.form.weight) : null,
      status: this.form.status
    };

    this.productsService.update(this.editingProductId, payload).subscribe({
      next: () => {
        this.saving = false;
        this.success = 'Producto actualizado correctamente.';
        this.startCreate();
        this.loadData(true);
      },
      error: (err) => {
        this.saving = false;
        this.error = err?.error?.message ?? 'No se pudo actualizar el producto.';
      }
    });
  }

  remove(product: Product): void {
    if (!this.canDelete) {
      this.error = 'Solo ADMIN puede eliminar productos.';
      return;
    }

    const confirmed = window.confirm(`¿Eliminar "${product.name}"?`);
    if (!confirmed) return;

    this.productsService.delete(product.productId).subscribe({
      next: () => {
        this.success = 'Producto eliminado correctamente.';
        if (this.editingProductId === product.productId) {
          this.startCreate();
        }
        this.loadData(true);
      },
      error: (err) => {
        this.error = err?.error?.message ?? 'No se pudo eliminar el producto.';
      }
    });
  }
}