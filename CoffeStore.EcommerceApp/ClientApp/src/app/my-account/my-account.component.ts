import { Component, OnInit, ViewChild } from '@angular/core';
import { CustomerService } from '../shared/services/customer.service';
import { Customer } from '../shared/models/customer.model';
import { jwtDecode } from "jwt-decode";
import { Router } from '@angular/router';
import { SessionService } from '../shared/services/session.service';
import { IconDefinition } from '@fortawesome/fontawesome-svg-core';
import { faBan, faCircleCheck } from '@fortawesome/free-solid-svg-icons';
import { CustomerAddress } from '../shared/models/address.model';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';

@Component({
  selector: 'app-my-account',
  templateUrl: './my-account.component.html',
  styleUrls: ['./my-account.component.css'],
  providers: [CustomerService, SessionService]
})
export class MyAccountComponent implements OnInit {
  @ViewChild('deleteSwal')
  public readonly deleteSwal!: SwalComponent;

  public customer: Customer | any;
  public faBan: IconDefinition = faBan;
  public faCircleCheck: IconDefinition = faCircleCheck;
  public addressText: string = "";
  public addressToBeRemoved: CustomerAddress | any;

  constructor(private customerService: CustomerService, private sessionService: SessionService, private router: Router) { }

  ngOnInit(): void {
    this.loadCustomer();
  }

  loadCustomer(): void {
    const userId = this.sessionService.getUserId();

    this.customerService.getCustomerById(userId).subscribe(customer => {
      this.customer = customer;
    });
  }

  logout(): void {
    this.sessionService.clearSession();

    setTimeout(() => this.router.navigate(['/']), 1000);
  }

  deleteAddress(): void {
    this.customerService.removeAddress(this.sessionService.getUserId(), this.addressToBeRemoved).subscribe(
      () => {
        this.loadCustomer();
      });
  }

  updatePossibleDeletedAddress(address: CustomerAddress): void {
    this.addressToBeRemoved = address;
    this.deleteSwal.text = `${address.address}, ${address.number}, ${address.neighborhood}, ${address.city}, ${address.state}`;
    this.deleteSwal.fire();
  }

}
