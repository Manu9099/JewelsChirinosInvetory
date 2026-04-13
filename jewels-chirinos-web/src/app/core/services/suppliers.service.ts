import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { shareReplay, tap } from 'rxjs/operators';
import {
  Supplier,
  CreateSupplierRequest,
  UpdateSupplierRequest
} from '../../shared/models/supplier.model';

@Injectable({ providedIn: 'root' })
export class SuppliersService {
  private http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5295/api/Suppliers';

  private suppliers$?: Observable<Supplier[]>;

  getAll(forceRefresh = false): Observable<Supplier[]> {
    if (!this.suppliers$ || forceRefresh) {
      this.suppliers$ = this.http.get<Supplier[]>(this.apiUrl).pipe(shareReplay(1));
    }

    return this.suppliers$;
  }

  create(payload: CreateSupplierRequest): Observable<Supplier> {
    return this.http.post<Supplier>(this.apiUrl, payload).pipe(
      tap(() => this.clearCache())
    );
  }

  update(id: number, payload: UpdateSupplierRequest): Observable<Supplier> {
    return this.http.put<Supplier>(`${this.apiUrl}/${id}`, payload).pipe(
      tap(() => this.clearCache())
    );
  }

  delete(id: number): Observable<{ message: string }> {
    return this.http.delete<{ message: string }>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.clearCache())
    );
  }

  clearCache(): void {
    this.suppliers$ = undefined;
  }
}