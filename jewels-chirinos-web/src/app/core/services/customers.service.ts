import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { Customer } from '../../shared/models/customer.model';

@Injectable({ providedIn: 'root' })
export class CustomersService {
  private http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5295/api/Customers';

  private customers$?: Observable<Customer[]>;

  getAll(forceRefresh = false): Observable<Customer[]> {
    if (!this.customers$ || forceRefresh) {
      this.customers$ = this.http.get<Customer[]>(this.apiUrl).pipe(
        map((customers) =>
          (customers ?? []).map((customer) => ({
            ...customer,
            fullName: [customer.firstName, customer.lastName].filter(Boolean).join(' ').trim(),
            documentNumber: customer.rucDni ?? null
          }))
        ),
        shareReplay(1)
      );
    }

    return this.customers$;
  }

  clearCache(): void {
    this.customers$ = undefined;
  }
}