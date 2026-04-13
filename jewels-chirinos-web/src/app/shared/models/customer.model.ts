export interface Customer {
  customerId: number;
  firstName: string;
  lastName?: string | null;
  fullName: string;
  email: string;
  phone?: string | null;
  address?: string | null;
  rucDni?: string | null;
  documentNumber?: string | null;
  status: boolean;
  createdAt: string;
}