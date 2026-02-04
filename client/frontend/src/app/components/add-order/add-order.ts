import { HttpErrorResponse } from '@angular/common/http';
import { Component, signal, WritableSignal } from '@angular/core';
import { take } from 'rxjs';
import { Item } from '../../shared/models/item';
import { Order } from '../../shared/models/order';
import { OrderRow } from '../../shared/models/order-row';
import { CustomerService } from '../../shared/services/customer.service';
import { HttpService } from '../../shared/services/http.service';
import { OrderService } from '../../shared/services/order.service';

@Component({
  selector: 'app-add-order',
  imports: [],
  templateUrl: './add-order.html',
  styleUrl: './add-order.css',
})
export class AddOrder {
  public items: WritableSignal<Item[] | undefined> = signal(undefined);
  public itemAmounts: {[key: string]: number} = {};
  private order: Order | undefined;
  constructor(private httpService: HttpService, private customerService: CustomerService, private orderService: OrderService){
    
  }

  ngOnInit(){
    this.httpService.getItems().pipe(take(1)).subscribe({
      next: (data: Item[]) => {
        if (data) {
          this.items.set(data);
        }
      },
      error: (err: HttpErrorResponse) => {
        console.error(JSON.stringify(err));
      }
    });

  }

  updateAmount(itemId: string, amount: number){
    this.itemAmounts[itemId] = amount;
    console.log(amount);
    console.log(JSON.stringify(this.itemAmounts));
  }

  public addToOrder(item: Item){
    const amount = this.itemAmounts[item.id] || 0;
    var newRow = OrderRow.createNew(item, amount);
    this.order = this.orderService.getOrder();
    var customer = this.customerService.getCustomer()!;
    if(!this.order){
      this.order = Order.createNew(customer, 'Address 67', 'Townville', '12345');
    }
    var foundRow = this.order.orderRows.find(aRow => aRow.item.id === newRow.item.id);
    if(foundRow){
      foundRow.ammount += newRow.ammount;
    } else {
      this.order.orderRows.push(newRow);
    }
    this.orderService.addOrder(this.order);
  }
}
