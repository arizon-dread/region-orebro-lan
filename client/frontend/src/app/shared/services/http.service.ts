import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer';
import { Info } from '../models/info';
import { Item } from '../models/item';
import { Order } from '../models/order';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private apiPath: string = "https://localhost:7089/api/v1";
  private http = inject(HttpClient);

  getItems(): Observable<Item[]> {
    return this.http.get<Item[]>(this.apiPath + '/item/all');
  }

  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.apiPath + '/order/all');
  }

  postOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(this.apiPath + '/order', order);
  }
  
  getInformationList() : Observable<Info[]> {
    return this.http.get<Info[]>(this.apiPath + '/info');
  }

  saveInformation(info: Info) {
    return this.http.post<Info>(this.apiPath + '/info', info);
  }

  getCustomer(customerId: string) {
    return this.http.get<Customer>(this.apiPath + '/customer/' + customerId);
  }

  getAllCustomers() {
    return this.http.get<Customer[]>(this.apiPath + '/customer/all');
  }

  saveCustomer(customer: Customer) {
    return this.http.post<Customer>(this.apiPath + '/customer', customer);
  }
}
