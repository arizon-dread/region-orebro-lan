import { Customer } from "./customer";
import { OrderRow } from "./order-row";

export class Order {
    id: string;
    version: number;
    statis: string | undefined;
    createDate: Date;
    updateDate: Date;
    deliveryAddress: string;
    deliveryCity: string;
    deliveryPostalCode: string;
    customer: Customer;
    orderRows: OrderRow[];
    constructor(id: string, version: number, createDate: Date, updateDate: Date, customer: Customer, deliveryAddress: string, deliveryCity: string, deliveryPostalCode: string, orderRows: OrderRow[]){
        this.id = id;
        this.version = version;
        this.createDate = createDate;
        this.updateDate = updateDate;
        this.customer = customer;
        this.deliveryAddress = deliveryAddress;
        this.deliveryCity = deliveryCity;
        this.deliveryPostalCode = deliveryPostalCode;
        this.orderRows = orderRows;
    }    
}