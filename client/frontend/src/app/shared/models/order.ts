import { OrderRow } from "./order-row";

export class Order {
    id: string;
    orderDate: Date;
    customerId: string;
    deliveryAddress: string;
    deliveryCity: string;
    deliveryPostalCode: string;
    orderRows: OrderRow[];
    constructor(id: string, orderDate: Date, customerId: string, deliveryAddress: string, deliveryCity: string, deliveryPostalCode: string, orderRows: OrderRow[]){
        this.id = id;
        this.orderDate = orderDate;
        this.customerId = customerId;
        this.deliveryAddress = deliveryAddress;
        this.deliveryCity = deliveryCity;
        this.deliveryPostalCode = deliveryPostalCode;
        this.orderRows = orderRows;
    }    
}