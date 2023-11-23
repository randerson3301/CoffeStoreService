import { Card } from "../../pre-order/model/card.model";
import { CustomerAddress } from "./address.model";
import { OrderItem } from "./order-item.model";

export class Order {
  public orderItems: OrderItem[] = []
  public total: number = 0.0;
  public customerId: string = "";
  public addressToDeliver: CustomerAddress | any;
  public paymentCard: Card | any
}


