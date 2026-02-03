import { Routes } from '@angular/router';
import { AddOrder } from './components/add-order/add-order';
import { Information } from './components/information/information';
import { OrderList } from './components/order-list/order-list';
import { ShoppingCart } from './components/shopping-cart/shopping-cart';

export const routes: Routes = [
    { path: "add-order", component: AddOrder},
    { path: "order-list", component: OrderList },
    { path: "information", component: Information },
    { path: 'cart', component: ShoppingCart },

];
