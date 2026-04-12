export interface Product {
  productId: number;
  name: string;
  code: string;
  barcode?: string | null;
  salePrice: number;
  stock: number;
  categoryName?: string | null;
  materialName?: string | null;
}