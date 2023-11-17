import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreateCustomer } from "../models/create-customer.model";
import { CustomerLogin } from "../models/login.model";
import { Customer } from "../models/customer.model";
import { CustomerAddress } from "../models/address.model";

@Injectable()
export class CustomerService { 
  @Inject('BASE_URL') private apiUrl = "";

  constructor(private http: HttpClient) {
    this.apiUrl += 'api/customer';
  }

  public add(newCustomer: CreateCustomer): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}`, newCustomer);
  }

  public login(loginModel: CustomerLogin): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, loginModel);
  }

  public getCustomerById(id: any): Observable<Customer> {
    return this.http.get<Customer>(`${this.apiUrl}/${id}`);
  }

  public addAddress(id: any, newAddress: CustomerAddress): Observable<Customer> {
    return this.http.post<Customer>(`${this.apiUrl}/${id}/address`, newAddress);
  }

  public removeAddress(id: any, removedAddress: CustomerAddress): Observable<any> {
    return this.http.patch<any>(`${this.apiUrl}/${id}/address`, removedAddress);
  }
}
