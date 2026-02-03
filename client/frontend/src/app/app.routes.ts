import { Routes } from '@angular/router';
import { OrderList } from './components/order-list/order-list';

export const routes: Routes = [
    {
        path: "order-list",
        component: OrderList
    },
    { path: 'cart', loadComponent: () => import('./components/shopping-cart/shopping-cart').then(m => m.ShoppingCart) },
];
