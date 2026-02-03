import { Component } from '@angular/core';
import { Order } from '../../shared/models/order';
import { HttpService } from '../../shared/services/http.service';
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
  constructor(private localStorageService: LocalStorageService, private http: HttpService){
  }

  ngAfterViewInit(){
    this.order = this.localStorageService.getItem<Order>('shopping-cart') ?? undefined;
  }

  postOrder() {
    if(this.order){
      this.http.postOrder(this.order).subscribe((response) => {
        this.localStorageService.removeItem('shopping-cart');
        this.order = response;
      });
    }
  }
}
