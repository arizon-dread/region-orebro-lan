export class OrderRow {
    id: string;
    orderId: string;
    itemId: string;
    amount: number;
    constructor(id: string, orderId: string, itemId: string, amount: number){
        this.id = id;
        this.orderId = orderId;
        this.itemId = itemId;
        this.amount = amount;
    }
}