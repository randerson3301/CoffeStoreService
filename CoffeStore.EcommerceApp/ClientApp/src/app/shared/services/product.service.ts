import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Product } from "../models/product.model";
import { Observable } from "rxjs";

@Injectable()
export class ProductService{
    private apiUrl = '';

    constructor(private http: HttpClient){}

    public getProducts(): Observable<Product[]>{
        return this.http.get<Product[]>(`${this.apiUrl}/products`);
    }

    public getProductById(id: any): Observable<Product>{
        return this.http.get<Product>(`${this.apiUrl}/products/${id}`);
    }
}