import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Info } from '../models/info';
import { Item } from '../models/item';
import { Order } from '../models/order';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  
  private apiPath: string = "https://localhost:7089";
  private http = inject(HttpClient);

  get(){
    this.http.get<string>(this.apiPath + '/ready').subscribe((res) => {
       console.log('Response: ', res);
    });
  }

  getItems(): Observable<Item[]> {
    return this.http.get<Item[]>(this.apiPath + '/api/v1/item/all');
  }

  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.apiPath + '/api/v1/orders');
  }

  postOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(this.apiPath + 'api/v1/orders', order);
  }
  
  getInformationList() : Observable<Info[]> {
    return this.http.get<Info[]>(this.apiPath + '/api/v1/info');
  }
  saveInformation(info: Info) {
    return this.http.post<Info>(this.apiPath + '/api/v1/info', info);
  }
}
