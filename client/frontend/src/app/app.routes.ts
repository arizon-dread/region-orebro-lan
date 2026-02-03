import { Routes } from '@angular/router';
import { OrderList } from './components/order-list/order-list';
import { Information } from './components/information/information';

export const routes: Routes = [
    { path: "order-list", component: OrderList },
    { path: "information", component: Information }
];
