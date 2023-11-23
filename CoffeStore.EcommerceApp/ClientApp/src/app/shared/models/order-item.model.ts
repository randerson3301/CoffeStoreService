import { Product } from "./product.model";

export class OrderItem {
  public product: Product = new Product();
  public quantity: number = 1;
  public subtotal: number = 0.0;
}
