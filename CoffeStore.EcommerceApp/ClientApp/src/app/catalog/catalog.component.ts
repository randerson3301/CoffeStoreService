import { Component, OnInit } from '@angular/core';
import { faStar } from '@fortawesome/free-solid-svg-icons';
import { Product } from '../shared/models/product.model';
import { ProductService } from '../shared/services/product.service';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css'],
  providers: [ProductService]
})
export class CatalogComponent implements OnInit {
  faStar = faStar;
  public products: Product[] = []

  constructor(private productService : ProductService) { }

  ngOnInit(): void {
    // this.productService.getProducts().subscribe((products) => {
    //   // this.products = products;
    // });
    this.products.push({ id: 1, price: 20.34, title: "Cafe bom", imagePath: "../assets/img/COFFE-BG.jpg", rateNumber: 3.78, description: "" });

  }

}
