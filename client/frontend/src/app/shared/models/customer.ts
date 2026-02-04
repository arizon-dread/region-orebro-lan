import { Order } from "./order";

export class Customer {
    id: string | undefined;
    version: number | undefined;
    status: string | undefined;
    createDate: Date | undefined;
    updateDate: Date | undefined;
    name: string;
    deliveryAddress: string | undefined;
    deliveryCity: string | undefined;
    deliveryPostalCode: string | undefined;
    active: boolean;
    orders: Order[] | undefined;

    constructor(id: string | undefined, version: number | undefined, status: string | undefined, createDate: Date | undefined, updateDate: Date | undefined, name: string, deliveryAddress: string | undefined, deliveryCity: string | undefined, deliveryPostalCode: string | undefined, active: boolean, orders: Order[] | undefined){
        this.id = id;
        this.version = version;
        this.status = status;
        this.createDate = createDate;
        this.updateDate = updateDate;
        this.name = name;
        this.deliveryAddress = deliveryAddress;
        this.deliveryCity = deliveryCity;
        this.deliveryPostalCode = deliveryPostalCode;
        this.active = active;
        this.orders = orders;
    }

    static createNew(name: string, deliveryAddress: string, deliveryCity: string, deliveryPostalCode: string): Customer {
        return new Customer(undefined, undefined, undefined, undefined, undefined, name, deliveryAddress, deliveryCity, deliveryPostalCode, true, undefined);
    }
}