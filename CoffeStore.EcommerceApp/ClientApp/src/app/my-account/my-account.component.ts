import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../shared/services/customer.service';
import { Customer } from '../shared/models/customer.model';
import { jwtDecode } from "jwt-decode";
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-account',
  templateUrl: './my-account.component.html',
  styleUrls: ['./my-account.component.css'],
  providers: [CustomerService]
})
export class MyAccountComponent implements OnInit {
  public customer: Customer | any;

  constructor(private customerService: CustomerService, private router: Router) { }

  ngOnInit(): void {
    const token: string = sessionStorage.getItem("token") || "";

    const decode = jwtDecode<any>(token);

    this.customerService.getCustomerById(decode.UserId).subscribe(customer => {
      this.customer = customer;
    });


  }

  logout(): void {
    sessionStorage.removeItem("name");
    sessionStorage.removeItem("token");

    setTimeout(() => this.router.navigate(['/']), 1000);
  }

}
