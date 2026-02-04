import { Injectable } from '@angular/core';
import { Order } from '../models/order';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private localStorageService: LocalStorageService) {
  }
  
  public addOrder(order: Order){
    this.localStorageService.setItem<Order>('order', order);
  }

  public getOrder(): Order | undefined{
    return this.localStorageService.getItem<Order>('order');
  }

  public clearOrder(){
    this.localStorageService.removeItem('order');
  }
}
