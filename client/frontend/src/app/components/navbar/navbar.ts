import { Component } from '@angular/core';
import { CustomerService } from '../../shared/services/customer.service';
import { ClientServer } from "../client-server/client-server";

@Component({
  standalone: true,
  selector: 'app-navbar',
  imports: [ClientServer],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
  constructor(protected customerService: CustomerService){
  }
}
