import { Component, signal, WritableSignal } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Order } from '../../shared/models/order';
import { HttpService } from '../../shared/services/http.service';

@Component({
  standalone: true,
  selector: 'app-order-list',
  imports: [],
  templateUrl: './order-list.html',
  styleUrl: './order-list.css',
})
export class OrderList {
  protected orders: WritableSignal<Order[] | undefined> = signal(undefined);

  constructor(private http: HttpService, private snackbar: MatSnackBar){

  }

  ngOnInit(){
    this.http.getOrders().subscribe((data) => {
      this.snackbar.open(`Hämtade ${data.length} beställningar från servern`, 'OK', { duration: 3000 });
      console.log(JSON.stringify(data))
      this.orders.set(data);
    });
  }
}
