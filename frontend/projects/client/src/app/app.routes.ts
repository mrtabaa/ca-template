import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { LoginComponent } from './features/auth/login/login.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent // ✅ eagerly loaded
  },
  {
    path: 'login',
    component: LoginComponent // ✅ eagerly loaded
  },
  {
    path: 'order',
    loadChildren: () =>
      import('./features/order/routes').then(m => m.orderRoutes) // ✅ lazy loaded
  }
];
