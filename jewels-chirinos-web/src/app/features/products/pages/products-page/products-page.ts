import { CommonModule } from '@angular/common';
import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

import { ProductsService } from '../../../../core/services/products.service';
import { Product } from '../../../../shared/models/product.model';

@Component({
  selector: 'app-products-page',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './products-page.html',
  styleUrl: './products-page.scss'
})
export class ProductsPage implements OnInit {
  private productsService = inject(ProductsService);

  products = signal<Product[]>([]);
  loading = false;
  error = '';
  search = '';

  readonly totalProducts = computed(() => this.products().length);
  readonly lowStockCount = computed(() => this.products().filter((p) => p.stock > 0 && p.stock <= 5).length);
  readonly outOfStockCount = computed(() => this.products().filter((p) => p.stock <= 0).length);

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

  ngOnInit(): void {
    this.loadProducts();
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
        this.error = err?.error?.message ?? 'No se pudo cargar el inventario.';
      }
    });
  }

  getPrice(product: Product): number {
    return product.sellingPrice ?? product.salePrice ?? 0;
  }
}