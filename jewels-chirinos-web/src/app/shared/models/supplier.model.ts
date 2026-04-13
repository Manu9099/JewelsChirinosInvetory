export interface Supplier {
  supplierId: number;
  name: string;
  rucDni?: string | null;
  contactName?: string | null;
  email?: string | null;
  phone?: string | null;
  address?: string | null;
  status: boolean;
  createdAt: string;
}

export interface CreateSupplierRequest {
  name: string;
  rucDni?: string | null;
  contactName?: string | null;
  email?: string | null;
  phone?: string | null;
  address?: string | null;
}

export interface UpdateSupplierRequest {
  name?: string;
  rucDni?: string | null;
  contactName?: string | null;
  email?: string | null;
  phone?: string | null;
  address?: string | null;
  status?: boolean;
}