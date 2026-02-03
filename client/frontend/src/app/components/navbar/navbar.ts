import { Component } from '@angular/core';
import { CustomerService } from '../../shared/services/customer.service';

@Component({
  standalone: true,
  selector: 'app-navbar',
  imports: [],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
  constructor(protected customerService: CustomerService){
  }
}
