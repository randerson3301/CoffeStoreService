import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { GetAddressService } from './services/get-address.service';
import * as EventEmitter from 'events';

@Component({
  selector: 'app-form-user-address',
  templateUrl: './form-user-address.component.html',
  styleUrls: ['./form-user-address.component.css'],
  providers: [GetAddressService]
})
export class FormUserAddressComponent implements OnInit {

  @Input() userForm: FormGroup | any;
  
  street: string = "";
  neighborhood: string = "";
  city: string = "";
  state: string = "";


  constructor(private addressService: GetAddressService) { }

  ngOnInit(): void {

  }

  getAddressByZipCode(zipCode: string): void {
    if (zipCode.length === 9) {
      this.addressService.getAddressByZipCode(zipCode).subscribe(response => {
        this.street = response.logradouro;
        this.neighborhood = response.bairro;
        this.city = response.localidade;
        this.state = response.uf;               
      });
    }
  }

}
