import { Component, OnInit } from '@angular/core';
import { faCartPlus } from '@fortawesome/free-solid-svg-icons';
import { Product } from '../shared/models/product.model';
import { ProductService } from '../shared/services/product.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
  providers: [ProductService]
})
export class ProductComponent implements OnInit {
  faCartPlus = faCartPlus;
  public product: Product | any;

  constructor(private productService: ProductService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe((paramId) => {
      // this.productService.getProductById(paramId).subscribe((product) => {
      //   // this.product = product;
      // });
      this.product = { id: paramId, price: 20.34, title: "Cafe bom", imagePath: "../assets/img/clipboard-image.png", rateNumber: 3.78, description: "um testezin basico" }

    });    
  }

}
