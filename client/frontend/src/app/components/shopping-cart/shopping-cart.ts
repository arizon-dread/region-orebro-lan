import { HttpErrorResponse } from '@angular/common/http';
import { Component, signal, WritableSignal } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
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
  public order: WritableSignal<Order | undefined> = signal(undefined);
  constructor(private orderService: OrderService, private http: HttpService, private snackbar: MatSnackBar) {
  }

  ngOnInit() {
    this.order.set(this.orderService.getOrder() ?? undefined);
  }

  clearOrder() {
    this.orderService.clearOrder();
    this.order.set(undefined);
    this.snackbar.open('Varukorgen rensad', 'OK', { duration: 3000 });
  }

  postOrder() {
    if (this.order()) {
      this.http.postOrder(this.order()!).pipe(take(1)).subscribe({
        next: (data: Order) => {
          if (data) {
            this.orderService.clearOrder();
            this.order.set(undefined);
            this.snackbar.open('Beställningen lagd!', 'OK', { duration: 3000 });
          }
        },
        error: (err: HttpErrorResponse) => {
          console.error(JSON.stringify(err));
        }
      });
    }
  }
}
