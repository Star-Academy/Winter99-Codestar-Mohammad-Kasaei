export class DocumentModel {
  public id: string;
  public content: string;

  public constructor(id: string = '', content: string = '') {
    this.id = id;
    this.content = content;
  }
}
