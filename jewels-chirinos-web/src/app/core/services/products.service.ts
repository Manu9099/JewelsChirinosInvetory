import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin, of } from 'rxjs';
import { catchError, map, shareReplay, switchMap, tap } from 'rxjs/operators';

import {
  Product,
  CreateProductRequest,
  UpdateProductRequest
} from '../../shared/models/product.model';
import { InventoryResponse } from '../../shared/models/inventory.model';
import { InventoryService } from './inventory.service';

@Injectable({ providedIn: 'root' })
export class ProductsService {
  private http = inject(HttpClient);
  private inventoryService = inject(InventoryService);

  private readonly apiUrl = 'http://localhost:5295/api/Products';
  private products$?: Observable<Product[]>;

  getAll(forceRefresh = false): Observable<Product[]> {
    if (!this.products$ || forceRefresh) {
      this.products$ = this.http.get<Product[]>(this.apiUrl).pipe(
        switchMap((products) => {
          const items = products ?? [];
          if (!items.length) return of([]);

          return forkJoin(
            items.map((product) =>
              this.inventoryService.getByProduct(product.productId).pipe(
                catchError(() => of(null)),
                map((inventory) => this.mapProduct(product, inventory))
              )
            )
          );
        }),
        shareReplay(1)
      );
    }

    return this.products$;
  }

  create(payload: CreateProductRequest): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, payload).pipe(
      tap(() => this.clearCache())
    );
  }

  update(id: number, payload: UpdateProductRequest): Observable<Product> {
    return this.http.put<Product>(`${this.apiUrl}/${id}`, payload).pipe(
      tap(() => this.clearCache())
    );
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.clearCache())
    );
  }

  clearCache(): void {
    this.products$ = undefined;
  }

  private mapProduct(product: Product, inventory: InventoryResponse | null): Product {
    return {
      ...product,
      salePrice: product.salePrice ?? product.sellingPrice ?? 0,
      sellingPrice: product.sellingPrice ?? product.salePrice ?? 0,
      stock: inventory?.availableStock ?? 0,
      reservedStock: inventory?.reservedStock ?? 0,
      soldStock: inventory?.soldStock ?? 0,
      damagedStock: inventory?.damagedStock ?? 0,
      totalStock: inventory?.totalStock ?? 0
    };
  }
}