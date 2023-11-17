import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CreateCustomer } from '../shared/models/create-customer.model';
import { CustomerService } from '../shared/services/customer.service';
import { IconDefinition, faCircleCheck } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [CustomerService]
})
export class RegisterComponent implements OnInit {

  public customerForm: FormGroup | any;
  public isConfirmPasswordEqualsPassword: boolean = false;
  public isCustomerAddedWithSuccess: boolean = false;
  public errorMessages: string[] = [];
  public faCircleCheck: IconDefinition = faCircleCheck;

  constructor(private fb: FormBuilder, private customerService: CustomerService, private router: Router) {
    this.customerForm = this.fb.group({
      name: '',
      birthDate: null,
      document: '',
      email: '',
      zipCode: '',
      address: '',
      number: 0,
      complement: '',
      neighborhood: '',
      city: '',
      state: '',
      password: '',
    });
  }

  ngOnInit(): void {
  }

  updatesPasswordEquality(newValue: boolean): void {
    this.isConfirmPasswordEqualsPassword = newValue;
  }

  register(): void {
    this.errorMessages = [];

    const formValue = this.customerForm.value;

    const newCustomer: CreateCustomer = {
      name: formValue.name,
      birthDate: formValue.birthDate,
      document: formValue.document,
      deliveryAddress: {
        zipCode: formValue.zipCode,
        address: formValue.address,
        number: formValue.number,
        complement: formValue.complement,
        neighborhood: formValue.neighborhood,
        city: formValue.city,
        state: formValue.state,
      },
      login: {
        email: formValue.email,
        password: formValue.password,
      }
    };

    if (this.isConfirmPasswordEqualsPassword) {
      this.customerService.add(newCustomer).subscribe(
        () => {
          this.isCustomerAddedWithSuccess = true;

          setTimeout(() => this.router.navigate(['/login']), 1500);
        },
        res => {
          this.errorMessages = res.error.errorMessages;
          setTimeout(() => this.errorMessages = [], 4000);

        }
      );
    }
  }
}
