import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Product } from "../models/product.model";
import { Observable } from "rxjs";

@Injectable()
export class ProductService{    
    @Inject('BASE_URL') private apiUrl = "";

    constructor(private http: HttpClient){
        this.apiUrl += 'api/product';
    }

    public getProducts(): Observable<Product[]>{
        return this.http.get<Product[]>(`${this.apiUrl}`);
    }

    public getProductById(id: any): Observable<Product>{
        return this.http.get<Product>(`${this.apiUrl}/${id}`);
    }

    public getFeatureProductImagePaths(): Observable<string[]> {
        return this.http.get<string[]>(`${this.apiUrl}/featured`);
    }
}