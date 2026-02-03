export class Customer {
    id: string;
    deliveryAddress: string;
    deliveryCity: string;
    deliveryPostalCode: string;
    constructor(id: string, deliveryAddress: string, deliveryCity: string, deliveryPostalCode: string){
        this.id = id;
        this.deliveryAddress = deliveryAddress;
        this.deliveryCity = deliveryCity;
        this.deliveryPostalCode = deliveryPostalCode;
    }
}