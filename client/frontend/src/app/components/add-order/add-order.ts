import { Component } from '@angular/core';
import { Item } from '../../shared/models/item';
import { OrderRow } from '../../shared/models/order-row';
import { HttpService } from '../../shared/services/http.service';

@Component({
  selector: 'app-add-order',
  imports: [],
  templateUrl: './add-order.html',
  styleUrl: './add-order.css',
})
export class AddOrder {
  public items: Item[] | undefined;
  constructor(httpService: HttpService){
    httpService.getItems().subscribe((data) => {
      this.items = data;
    });
  }

  public addToOrder(item: Item, amount: number){
    var row = OrderRow.createNew(item, amount);
    console.log(row);
  }
}
