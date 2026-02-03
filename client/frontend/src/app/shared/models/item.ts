export class Item {
    id: string; //GUID
    version: number;
    status: string | undefined;
    createDate: Date;
    updateDate: Date;
    manufacturer: string;
    name: string;
    price: number;
    constructor(id: string, version: number, status: string | undefined, createDate: Date, updateDate: Date, manufacturer: string, name: string, price: number){
        this.id = id;
        this.version = version;
        this.status = status;
        this.createDate = createDate;
        this.updateDate = updateDate;
        this.manufacturer = manufacturer;
        this.name = name;
        this.price = price;
    }
}