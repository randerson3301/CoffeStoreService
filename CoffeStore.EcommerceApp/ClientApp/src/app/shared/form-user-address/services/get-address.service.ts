import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class GetAddressService {

  constructor(private http: HttpClient) { }

  getAddressByZipCode(zipCode: string): Observable<any> {
    zipCode = zipCode.replace("-", "");
    return this.http.get<any>(`https://viacep.com.br/ws/${zipCode}/json/`);
  }
}
