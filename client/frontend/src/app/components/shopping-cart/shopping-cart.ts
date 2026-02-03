import { Component } from '@angular/core';
import { Order } from '../../shared/models/order';
import { LocalStorageService } from '../../shared/services/local-storage.service';

@Component({
  standalone: true,
  selector: 'app-shopping-cart',
  imports: [],
  templateUrl: './shopping-cart.html',
  styleUrl: './shopping-cart.css',
})
export class ShoppingCart {
  public order: Order | undefined;
  constructor(private localStorageService: LocalStorageService){
  }
  ngAfterViewInit(){
    this.order = this.localStorageService.getItem<Order>('shopping-cart') ?? undefined;
  }
}
