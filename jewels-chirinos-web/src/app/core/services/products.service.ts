import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { shareReplay } from 'rxjs/operators';
import { Product } from '../../shared/models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5295/api/Products';

  private products$?: Observable<Product[]>;

  getAll(forceRefresh = false): Observable<Product[]> {
    if (!this.products$ || forceRefresh) {
      this.products$ = this.http
        .get<Product[]>(this.apiUrl)
        .pipe(shareReplay(1));
    }

    return this.products$;
  }

  clearCache(): void {
    this.products$ = undefined;
  }
}