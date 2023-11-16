import { CustomerAddress } from "./address.model";

export class Customer {
  public id: string = "";
  public name: string = "";
  public birthDate: string = "";
  public document: string = "";
  public email: string = "";
  public addresses: CustomerAddress[] = [];
}
