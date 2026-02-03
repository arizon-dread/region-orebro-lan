import { Item } from "./item";

export class OrderRow {
    id: string | undefined; //undefined at creation, GUID when stored in db
    version: number;
    status: string | undefined;
    createDate: Date;
    updateDate: Date;
    item: Item;
    //felstavat, men det bjuder vi på
    ammount: number;
    private constructor(id: string | undefined, version: number | undefined, status: string | undefined, createDate: Date | undefined, updateDate: Date | undefined, item: Item, ammount: number){
        this.id = id;
        this.version = version ?? 0;
        this.status = status;
        var now =  new Date();
        this.createDate = createDate ?? now;
        this.updateDate = updateDate ?? now;
        this.item = item;
        this.ammount = ammount;
    }

    static createNew(item: Item, amount: number): OrderRow {
        return new OrderRow(undefined, undefined, undefined, undefined, undefined, item, amount);
    }
}