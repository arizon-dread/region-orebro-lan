import { Injectable } from '@angular/core';
import { take } from 'rxjs';
import { Customer } from '../models/customer';
import { HttpService } from './http.service';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  private customer_key = 'customer';
  constructor(private localStorageService: LocalStorageService, private httpService: HttpService) {

  }

  public getCustomer(): Customer | undefined {
    var customer = this.localStorageService.getItem<Customer>(this.customer_key);
    if (!customer) {
      customer = Customer.createNew("Cool kund", "Gata 67", "Townsville", "12345");
      this.httpService.saveCustomer(customer).pipe(take(1)).subscribe({
        next: (data: Customer) => {
          if (data) {
            customer = data;
            this.localStorageService.setItem(this.customer_key, customer!);
          }
        },
        error: (err: any) => {
          console.error(JSON.stringify(err));
        }
      });
      this.localStorageService.setItem(this.customer_key, customer);
    }
    return customer;
  }
}
