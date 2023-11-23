import { Component, OnInit, ViewChild } from '@angular/core';
import { CustomerAddress } from '../shared/models/address.model';
import { Customer } from '../shared/models/customer.model';
import { SessionService } from '../shared/services/session.service';
import { CustomerService } from '../shared/services/customer.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Order } from '../shared/models/order.model';
import { Card } from './model/card.model';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import { OrderService } from '../shared/services/order.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pre-order',
  templateUrl: './pre-order.component.html',
  styleUrls: ['./pre-order.component.css'],
  providers: [SessionService, CustomerService, OrderService]
})
export class PreOrderComponent implements OnInit {
  addressSelect: CustomerAddress | any;
  public customer: Customer = new Customer();
  public orderForm: FormGroup | any;
  public order: Order = new Order();  

  @ViewChild('validationError')
  public readonly validationError!: SwalComponent;

  @ViewChild('successOrder')
  public readonly successOrder!: SwalComponent;

  constructor(private sessionService: SessionService, private customerService: CustomerService, private fb: FormBuilder, private orderService: OrderService, private router: Router) {
    this.orderForm = this.fb.group({
      selectAddress: '',
      cardNumber: '',
      cardFullName: '',
      cardExpiration: '',
      cardCvv: ''
    });

    this.order = JSON.parse(this.sessionService.getSessionOrder());
  }

  ngOnInit(): void {
    const userId = this.sessionService.getUserId();

    this.customerService.getCustomerById(userId).subscribe(customer => {
      this.customer = customer;
    });
  }

  closeOrder(): void {
    if (this.orderForm.status === "INVALID") {
      this.validationError.fire();
      return;
    }

    const formValue = this.orderForm.value;

    this.order.addressToDeliver = this.customer.addresses.find(a => a.zipCode === formValue.selectAddress);
    this.order.paymentCard = { fullName: formValue.cardFullName, cardNumber: formValue.cardNumber, cvv: formValue.cardCvv, expiration: formValue.cardExpiration };

    this.orderService.add(this.order).subscribe(
      () => {
        this.successOrder.fire();
      });
  }

  endOrder(): void {
    sessionStorage.removeItem("order");
    setTimeout(() => this.router.navigate(['/']), 1500);
  }

}
