export class ItemInventory {
    id: string;
    itemId: string;
    inventory: number;
    constructor(id: string, itemId: string, inventory: number){
        this.id = id;
        this.itemId = itemId;
        this.inventory = inventory;
    }
}