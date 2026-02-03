export class Info {
    id: string;
    title: string;
    text: string;
    publishDate: Date;
    unpublished: Date | undefined;
    constructor(id: string, title: string, text: string, publishDate: Date, unpublished: Date | undefined){
        this.id = id;
        this.title = title;
        this.text = text;
        this.publishDate = publishDate;
        this.unpublished = unpublished;
    }
}