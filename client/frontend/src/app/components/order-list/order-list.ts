import { Component } from '@angular/core';
import { HttpService } from '../../shared/services/http.service';

@Component({
  standalone: true,
  selector: 'app-order-list',
  imports: [],
  templateUrl: './order-list.html',
  styleUrl: './order-list.css',
})
export class OrderList {
  constructor(private http: HttpService){

  }

  ngOnInit(){
    this.http.getOrders().subscribe((data) => {
      console.log(data);
    });
  }
}
