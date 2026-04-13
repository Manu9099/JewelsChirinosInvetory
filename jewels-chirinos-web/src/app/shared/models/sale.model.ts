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
  customerId?: number | null;
  customerName?: string | null;
  subtotalAmount: number;
  taxAmount: number;
  discountAmount: number;
  totalAmount: number;
  paymentMethod?: string | null;
  saleStatus: string;
  invoiceNumber?: string | null;
  sunatTicketNumber?: string | null;
  createdAt: string;
}