import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { take } from 'rxjs';
import { Order } from '../../shared/models/order';
import { HttpService } from '../../shared/services/http.service';
import { OrderService } from '../../shared/services/order.service';

@Component({
  standalone: true,
  selector: 'app-shopping-cart',
  imports: [],
  templateUrl: './shopping-cart.html',
  styleUrl: './shopping-cart.css',
})
export class ShoppingCart {
  public order: Order | undefined;
  constructor(private orderService: OrderService, private http: HttpService) {
  }

  ngOnInit() {
    this.order = this.orderService.getOrder() ?? undefined;
  }

  postOrder() {
    if (this.order) {
      this.http.postOrder(this.order).pipe(take(1)).subscribe({
        next: (data: Order) => {
          if (data) {
            this.orderService.clearOrder();
            this.order = data;
          }
        },
        error: (err: HttpErrorResponse) => {
          console.error(JSON.stringify(err));
        }
      });
    }
  }
}
