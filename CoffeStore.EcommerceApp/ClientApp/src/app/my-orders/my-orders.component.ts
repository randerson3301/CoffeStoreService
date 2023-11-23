import { Component, OnInit } from '@angular/core';
import { OrderPlaced } from '../shared/models/order-placed.model';
import { OrderService } from '../shared/services/order.service';
import { SessionService } from '../shared/services/session.service';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css'],
  providers: [OrderService]
})
export class MyOrdersComponent implements OnInit {
  myOrders: OrderPlaced[] = [];

  constructor(private orderService: OrderService, private sessionService: SessionService) { }

  ngOnInit(): void {
    this.orderService.getOrdersByCustomer(this.sessionService.getUserId() || "").subscribe((orders) => {
      console.log(orders);

      for (let order of orders) {
        let items: string = "";

        for (let item of order.orderItems) {
          items += `${item.productItem.title}(${item.quantity}x), `;
        }

        this.myOrders.push({ items, total: order.amount, orderedIn: order.createdAt, status: this.getStatusDesc(order.deliveryStatus) })
      }
    })
  }

  getStatusDesc(status: number): string {
    switch (status) {
      case 0:
        return "Recebido";
      case 1:
        return "Em Trânsito";
      case 2:
        return "Entregue";
      default:
        return "";
    }
  }
}
