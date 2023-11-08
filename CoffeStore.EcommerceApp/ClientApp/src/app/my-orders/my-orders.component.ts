import { Component, OnInit } from '@angular/core';
import { OrderPlaced } from '../shared/models/order-placed.model';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css']
})
export class MyOrdersComponent implements OnInit {
  myOrders: OrderPlaced[] = [];

  constructor() { }

  ngOnInit(): void {
    this.myOrders.push({items: "cafezin mil grau(2x), cafezinho bom(1x), cafe delicia(3x)", total: 25.67, orderedIn: "10/10/2023 20:58", rate: 0})
  }

}
