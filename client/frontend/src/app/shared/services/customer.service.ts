import { Injectable } from '@angular/core';
import { Customer } from '../models/customer';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  private customer_key = 'customer';
  constructor(private localStorageService: LocalStorageService) {
    
  }

  public getCustomer(): Customer | undefined {
    var customer = this.localStorageService.getItem<Customer>(this.customer_key);
    if(!customer){
      customer = new Customer(crypto.randomUUID(), 1, undefined, new Date(), new Date(), "Cool Cat");
      this.localStorageService.setItem(this.customer_key, customer);
    }
    return customer;
  }
}
