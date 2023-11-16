import { CustomerAddress } from "../../shared/models/address.model";
import { CustomerLogin } from "../../shared/models/login.model";

export class CreateCustomer {
  public name: string = "";
  public birthDate: Date = new Date();
  public document: string = "";
  public deliveryAddress: CustomerAddress | any;
  public login: CustomerLogin | any;
}
