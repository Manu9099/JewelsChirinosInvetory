export interface Product {
  productId: number;
  code: string;
  barcode?: string | null;
  qrCode?: string | null;
  name: string;
  description?: string | null;

  categoryId?: number;
  categoryName?: string | null;

  materialId?: number | null;
  materialName?: string | null;

  supplierId?: number;
  supplierName?: string | null;

  costPrice?: number;
  sellingPrice?: number;
  salePrice?: number; // compat temporal

  marginPercentage?: number;
  weight?: number | null;
  status?: boolean;

  stock: number;
  reservedStock?: number;
  soldStock?: number;
  damagedStock?: number;
  totalStock?: number;
}

export interface CreateProductRequest {
  code: string;
  barcode?: string | null;
  name: string;
  description?: string | null;
  categoryId: number;
  materialId?: number | null;
  supplierId: number;
  costPrice: number;
  sellingPrice: number;
  weight?: number | null;
  certificate?: string | null;
  imageUrl?: string | null;
  sku?: string | null;
}

export interface UpdateProductRequest {
  name?: string;
  description?: string | null;
  categoryId?: number;
  materialId?: number | null;
  supplierId?: number;
  costPrice?: number;
  sellingPrice?: number;
  weight?: number | null;
  status?: boolean;
}