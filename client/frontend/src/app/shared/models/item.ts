export class Item {
    id: string; //GUID
    manufacturer: string;
    name: string;
    price: number;
    constructor(id: string, manufacturer: string, name: string, price: number){
        this.id = id;
        this.manufacturer = manufacturer;
        this.name = name;
        this.price = price;
    }
}