export class Info {
    id: string;
    title: string;
    text: string;
    publishedDate: Date;
    unpublished: Date | undefined;
    constructor(id: string, title: string, text: string, publishedDate: Date, unpublished: Date | undefined){
        this.id = id;
        this.title = title;
        this.text = text;
        this.publishedDate = publishedDate;
        this.unpublished = unpublished;
    }
}