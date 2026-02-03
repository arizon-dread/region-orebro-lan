export class Info {
    id: string;
    title: string;
    text: string;
    publishDate?: Date;
    unpublished?: Date;
    status?: string;
    version: number;
    constructor(id: string, title: string, text: string, publishDate: Date, unpublished: Date | undefined, status: string, version: number){
        this.id = id;
        this.title = title;
        this.text = text;
        this.publishDate = publishDate;
        this.unpublished = unpublished;
        this.status = status;
        this.version = version;
    }
}