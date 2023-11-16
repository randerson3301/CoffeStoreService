import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CustomerService } from '../shared/services/customer.service';
import { Router } from '@angular/router';
import { CustomerLogin } from '../shared/models/login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [CustomerService]
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup | any;
  public errorMessages: string[] = [];

  constructor(private fb: FormBuilder, private customerService: CustomerService, private router: Router) {
    this.loginForm = this.fb.group({
      email: '',
      password: ''
    });
  }

  ngOnInit(): void {
  }

  login(): void {
    
    const loginModel: CustomerLogin = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password,
    };

    this.customerService.login(loginModel).subscribe(
      (res) => {
        sessionStorage.setItem("name", res.name);
        sessionStorage.setItem("token", res.token);

        setTimeout(() => this.router.navigate(['/']), 1000);
      },
      res => {
        this.errorMessages = res.error.errorMessages;
        setTimeout(() => this.errorMessages = [], 2000);
      }
    );
  }

}
