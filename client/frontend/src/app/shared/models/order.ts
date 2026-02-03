export class Order {
    id: string;
    orderDate: Date;
    customerId: string;
    deliveryAddress: string;
    deliveryCity: string;
    deliveryPostalCode: string;
    constructor(id: string, orderDate: Date, customerId: string, deliveryAddress: string, deliveryCity: string, deliveryPostalCode: string){
        this.id = id;
        this.orderDate = orderDate;
        this.customerId = customerId;
        this.deliveryAddress = deliveryAddress;
        this.deliveryCity = deliveryCity;
        this.deliveryPostalCode = deliveryPostalCode;
    }    
}