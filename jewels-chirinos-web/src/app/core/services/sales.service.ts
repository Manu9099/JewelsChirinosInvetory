import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateSaleRequest, SaleResponse } from '../../shared/models/sale.model';

@Injectable({
  providedIn: 'root'
})
export class SalesService {
  private http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5295/api/Sales';

  create(payload: CreateSaleRequest): Observable<SaleResponse> {
    return this.http.post<SaleResponse>(this.apiUrl, payload);
  }
}