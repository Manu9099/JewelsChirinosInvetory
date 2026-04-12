export interface CreateSaleDetailRequest {
  productId: number;
  quantity: number;
  unitPrice: number;
}

export interface CreateSaleRequest {
  customerId?: number | null;
  saleDetails: CreateSaleDetailRequest[];
  taxAmount: number;
  discountAmount: number;
  paymentMethod: string;
  observations?: string | null;
  createdBy: string;
}

export interface SaleResponse {
  saleId: number;
  saleNumber: string;
  totalAmount: number;
  paymentMethod: string;
  createdAt: string;
}