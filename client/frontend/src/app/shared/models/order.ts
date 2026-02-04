import { Customer } from "./customer";
import { OrderRow } from "./order-row";

export class Order {
    id: string | undefined;
    version: number | undefined;
    status: string | undefined;
    createDate: Date | undefined;
    updateDate: Date | undefined;
    deliveryAddress: string;
    deliveryCity: string;
    deliveryPostalCode: string;
    customer: Customer;
    orderRows: OrderRow[];
    private constructor(id: string | undefined, version: number | undefined, createDate: Date | undefined, updateDate: Date | undefined, customer: Customer, deliveryAddress: string, deliveryCity: string, deliveryPostalCode: string, orderRows: OrderRow[]){
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

    static createNew(customer: Customer, deliveryAddress: string, deliveryCity: string, deliveryPostalCode: string): Order {
        return new Order(undefined, undefined, undefined, undefined, customer, deliveryAddress, deliveryCity, deliveryPostalCode, []);
    }
}