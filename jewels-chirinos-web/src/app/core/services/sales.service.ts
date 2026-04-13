import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { shareReplay, tap } from 'rxjs/operators';
import { CreateSaleRequest, SaleResponse } from '../../shared/models/sale.model';

@Injectable({ providedIn: 'root' })
export class SalesService {
  private http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5295/api/Sales';

  private sales$?: Observable<SaleResponse[]>;

  getAll(forceRefresh = false): Observable<SaleResponse[]> {
    if (!this.sales$ || forceRefresh) {
      this.sales$ = this.http.get<SaleResponse[]>(this.apiUrl).pipe(shareReplay(1));
    }

    return this.sales$;
  }

  create(payload: CreateSaleRequest): Observable<SaleResponse> {
    return this.http.post<SaleResponse>(this.apiUrl, payload).pipe(
      tap(() => {
        this.sales$ = undefined;
      })
    );
  }

  clearCache(): void {
    this.sales$ = undefined;
  }
}