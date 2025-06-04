import { Routes } from '@angular/router';
import { OrderListComponent } from './order-list/order-list.component';
import { OrderDetailComponent } from './order-detail/order-detail.component';
import { authGuard } from '../../core/guards/auth.guard';

export const orderRoutes: Routes = [
    {
        path: '',
        runGuardsAndResolvers: 'always', // Always check authGuard to make sure the user is still logged in
        canActivate: [authGuard],
        children: [
            { path: '', component: OrderListComponent }, // /orders
            { path: ':id', component: OrderDetailComponent } // /orders/123
        ]
    }
];
