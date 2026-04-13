import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth-guard';

import { ShellComponent } from './layout/shell/shell.component';
import { LoginPageComponent } from './features/auth/pages/login-page/login-page.component';
import { DashboardPageComponent } from './features/dashboard/pages/dashboard-page/dashboard-page.component';
import { ProductsPage } from './features/products/pages/products-page/products-page';
import { CustomersPage } from './features/customers/pages/customers-page/customers-page';
import { SalesPage } from './features/sales/pages/sales-page/sales-page';

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
      { path: 'customers', component: CustomersPage },
      { path: 'sales', component: SalesPage },
      { path: '', pathMatch: 'full', redirectTo: 'dashboard' }
    ]
  },
  { path: '**', redirectTo: '' }
];