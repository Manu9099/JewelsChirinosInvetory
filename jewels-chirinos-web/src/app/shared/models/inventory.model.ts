export interface InventoryResponse {
  inventoryId: number;
  productId: number;
  productName?: string | null;
  availableStock: number;
  reservedStock: number;
  soldStock: number;
  damagedStock: number;
  totalStock: number;
  lastSaleDate?: string | null;
  lastPurchaseDate?: string | null;
}