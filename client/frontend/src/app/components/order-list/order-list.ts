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

  testGet(){
    this.http.get();
  }
}
