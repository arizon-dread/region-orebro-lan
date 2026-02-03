import { Routes } from '@angular/router';
import { Information } from './components/information/information';
import { OrderList } from './components/order-list/order-list';
import { ShoppingCart } from './components/shopping-cart/shopping-cart';

export const routes: Routes = [
    { path: "order-list", component: OrderList },
    { path: "information", component: Information },
    { path: 'cart', component: ShoppingCart },

];
