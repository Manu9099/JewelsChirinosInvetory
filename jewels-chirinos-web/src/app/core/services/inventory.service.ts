import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { InventoryResponse } from '../../shared/models/inventory.model';

@Injectable({ providedIn: 'root' })
export class InventoryService {
  private http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5295/api/Inventory';

  getByProduct(productId: number): Observable<InventoryResponse> {
    return this.http.get<InventoryResponse>(`${this.apiUrl}/${productId}`);
  }
}