import { Component, OnInit } from '@angular/core';
import { faCartPlus } from '@fortawesome/free-solid-svg-icons';
import { Product } from '../shared/models/product.model';
import { ProductService } from '../shared/services/product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Order } from '../shared/models/order.model';
import { SessionService } from '../shared/services/session.service';
import { OrderItem } from '../shared/models/order-item.model';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
  providers: [ProductService]
})
export class ProductComponent implements OnInit {
  faCartPlus = faCartPlus;

  public isAddedToCart: boolean = false;
  public successMessage: string = "Produto adicionado com sucesso!";
  public product: Product | any;
  public order: Order = new Order();
  private userId: string | null = "";
  constructor(private productService: ProductService, private route: ActivatedRoute, private sessionService: SessionService, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe((paramId) => {
      this.productService.getProductById(paramId.id).subscribe((product) => {
          this.product = product;
       });
    });

    this.userId = this.sessionService.getUserId();
    this.order = JSON.parse(this.sessionService.getSessionOrder());

    if (this.userId && !this.order) {
      this.order = { orderItems: [], addressToDeliver: null, customerId: this.sessionService.getUserId() || "", total: 0.0, paymentCard: null };
      this.sessionService.setSessionOrder(this.order);
    }  
  }

  addToCart() {
    if (this.userId == "") {
      this.router.navigate(['/login']);
      return;
    }

    const orderItem: OrderItem = { product: this.product, quantity: 1, subtotal: this.product.price };    
    this.order.orderItems.push(orderItem);
    this.isAddedToCart = true;
    this.sessionService.setSessionOrder(this.order);
  }

}
