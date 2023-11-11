import { Component, OnInit } from '@angular/core';
import { ProductService } from '../shared/services/product.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [ProductService]
})
export class HomeComponent implements OnInit {
  featureProductImagePaths: string[] = [];
  
  constructor(private productService : ProductService){}

  ngOnInit(): void {
    this.productService.getFeatureProductImagePaths().subscribe((images) => {
      this.featureProductImagePaths = images;
    });
  }
}
