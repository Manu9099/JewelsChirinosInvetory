export interface Customer {
  customerId: number;
  fullName: string;
  email?: string | null;
  phone?: string | null;
  documentNumber?: string | null;
}