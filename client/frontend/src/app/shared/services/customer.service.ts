import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { take } from 'rxjs';
import { Customer } from '../models/customer';
import { HttpService } from './http.service';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  private customer_key = 'customer';
  constructor(private localStorageService: LocalStorageService, private httpService: HttpService, private snackbar: MatSnackBar) {

  }

  public getCustomer(): Customer | undefined {
    var customer = this.localStorageService.getItem<Customer>(this.customer_key);
    if (!customer) {
      this.httpService.getCustomer('f5ec4386-6e19-4384-a1ea-8abe3b85fe71').pipe(take(1)).subscribe({
        next: (data: Customer) => {
          if (data) {
            this.localStorageService.setItem<Customer>(this.customer_key, data);
            customer = data;
          }
        },
        error: (err: HttpErrorResponse) => {
          this.snackbar.open('Kunde inte hämta kund.', 'OK', { duration: 3000 });
        }
      });
    }
    return customer;
  }
}
