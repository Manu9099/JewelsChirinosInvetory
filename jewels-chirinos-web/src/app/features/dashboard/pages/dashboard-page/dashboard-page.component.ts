import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-dashboard-page',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard-page.component.html',
  styleUrl: './dashboard-page.scss'
})
export class DashboardPageComponent {
  stats = [
    { label: 'Ventas hoy', value: 'S/ 0.00', hint: 'Próximo paso: datos reales' },
    { label: 'Productos activos', value: '0', hint: 'Conectaremos productos' },
    { label: 'Clientes', value: '0', hint: 'Conectaremos clientes' },
    { label: 'Tickets emitidos', value: '0', hint: 'Luego conectamos SUNAT' }
  ];

  shortcuts = [
    { label: 'Nueva venta', route: '/sales' },
    { label: 'Ver productos', route: '/products' },
    { label: 'Ver clientes', route: '/customers' }
  ];
}