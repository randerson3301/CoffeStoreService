import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Order } from "../models/order.model";
import { Observable } from "rxjs";

@Injectable()
export class OrderService {
  private apiUrl = "https://coffestoreecommerceappapi.azure-api.net/";

  constructor(private http: HttpClient) {
    this.apiUrl += 'api/order';
  }

  public add(newOrder: Order): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}`, newOrder);
  }

  public getOrdersByCustomer(customerId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/customer/${customerId}`);
  }
}
