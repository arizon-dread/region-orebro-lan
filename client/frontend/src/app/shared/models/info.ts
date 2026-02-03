export class Info {
    id: string;
    title: string;
    text: string;
    publishDate: Date;
    unpublished?: Date | undefined;
    status?: string;
    constructor(id: string, title: string, text: string, publishDate: Date, unpublished: Date | undefined, status: string){
        this.id = id;
        this.title = title;
        this.text = text;
        this.publishDate = publishDate;
        this.unpublished = unpublished;
        this.status = status;
    }
}