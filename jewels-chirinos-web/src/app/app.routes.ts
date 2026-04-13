import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth-guard';

import { ShellComponent } from './layout/shell/shell.component';
import { LoginPageComponent } from './features/auth/pages/login-page/login-page.component';
import { DashboardPageComponent } from './features/dashboard/pages/dashboard-page/dashboard-page.component';
import { ProductsPage } from './features/products/pages/products-page/products-page';
import { ProductManagementPage } from './features/products/pages/product-management-page/product-management-page';
import { CustomersPage } from './features/customers/pages/customers-page/customers-page';
import { SalesPage } from './features/sales/pages/sales-page/sales-page';
import { SuppliersPage } from './features/suppliers/pages/suppliers-page/suppliers-page';

export const routes: Routes = [
  { path: 'login', component: LoginPageComponent },
  {
    path: '',
    component: ShellComponent,
    canActivate: [authGuard],
    children: [
      { path: 'dashboard', component: DashboardPageComponent },
      { path: 'inventory', component: ProductsPage },
      { path: 'products', pathMatch: 'full', redirectTo: 'inventory' },
      { path: 'product-management', component: ProductManagementPage },
      { path: 'customers', component: CustomersPage },
      { path: 'suppliers', component: SuppliersPage },
      { path: 'sales', component: SalesPage },
      { path: '', pathMatch: 'full', redirectTo: 'dashboard' }
    ]
  },
  { path: '**', redirectTo: '' }
];