export class Customer {
    id: string;
    version: number;
    status: string | undefined;
    createDate: Date;
    updateDate: Date;
    name: string;
    constructor(id: string, version: number, status: string | undefined, createDate: Date, updateDate: Date, name: string){
        this.id = id;
        this.version = version;
        this.status = status;
        this.createDate = createDate;
        this.updateDate = updateDate;
        this.name = name;
    }
}