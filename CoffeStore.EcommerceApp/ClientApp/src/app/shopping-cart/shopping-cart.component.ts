import { Component, OnInit } from '@angular/core';
import { faMinusCircle, faPlusCircle } from '@fortawesome/free-solid-svg-icons';
import { Order } from '../shared/models/order.model';
import { Product } from '../shared/models/product.model';
import { OrderItem } from '../shared/models/order-item.model';
import { SessionService } from '../shared/services/session.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {
  faMinusCircle = faMinusCircle;
  faPlusCircle = faPlusCircle;

  public order: Order = new Order();  

  constructor(private sessionService: SessionService) {

  }

  ngOnInit(): void {
    this.order = JSON.parse(this.sessionService.getSessionOrder());

    this.changeOrderTotal();
  }

  dropItemQuantity(item: OrderItem): void {
    if (item.quantity === 1) {
      this.order.orderItems = this.order.orderItems.filter(_item => _item !== item);
      this.changeOrderTotal();
      return;
    }

    item.quantity--;

    this.changeSubtotal(item);
  }

  raiseItemQuantity(item: OrderItem): void {
    item.quantity++;

    this.changeSubtotal(item);
  }

  private changeSubtotal(item: OrderItem): void {
    item.subtotal = item.product.price * item.quantity;
    this.changeOrderTotal();
  }

  private changeOrderTotal(): void {
    this.order.total = 0;

    for (let item of this.order.orderItems) {
      this.order.total += item.subtotal; 
    }

    this.sessionService.setSessionOrder(this.order);
  }
}
