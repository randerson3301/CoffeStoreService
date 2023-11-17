import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CustomerService } from '../shared/services/customer.service';
import { CustomerAddress } from '../shared/models/address.model';
import { SessionService } from '../shared/services/session.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-new-address',
  templateUrl: './new-address.component.html',
  styleUrls: ['./new-address.component.css'],
  providers: [CustomerService, SessionService]
})
export class NewAddressComponent implements OnInit {

  public addressForm: FormGroup | any;
  public errorMessages: string[] = [];
  public isCustomerAddressAddedWithSuccess: boolean = false;
  public successMessage: string = "Novo endereço adicionado com sucesso!";

  constructor
  (
    private fb: FormBuilder,
    private customerService: CustomerService,
    private sessionService: SessionService,
    private router: Router
  )
  {
    this.addressForm = this.fb.group({
      zipCode: '',
      address: '',
      number: 0,
      complement: '',
      neighborhood: '',
      city: '',
      state: '',
    })
  }

  ngOnInit(): void {
  }

  addAddress(): void {
    const address: CustomerAddress = this.addressForm.value as CustomerAddress;

    this.customerService.addAddress(this.sessionService.getUserId(), address).subscribe(
      () => {
        this.isCustomerAddressAddedWithSuccess = true;

        setTimeout(() => this.router.navigate(['/my-account']), 1500);
      },
      res => {
        this.errorMessages = res.error.errorMessages;
        setTimeout(() => this.errorMessages = [], 4000);
      });
  }
}

