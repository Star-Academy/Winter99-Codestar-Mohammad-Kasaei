import {Component} from '@angular/core';
import {DocumentsService} from '../services/documents.service';
import {DocumentModel} from '../shared/Document.model';

@Component({
  selector: 'app-add-document',
  templateUrl: './add-document.component.html',
  styleUrls: ['./add-document.component.scss']
})
export class AddDocumentComponent {
  public docID: string;
  public docContent: string;

  constructor(private documentsService: DocumentsService) {
  }

  onDocAddClicked(): void {
    this.documentsService
      .addDocument(new DocumentModel(this.docID, this.docContent))
      .subscribe(value => this.onDocumentReceived(value),
        error => this.onDocumentReceived(error)
      );
  }

  onDocumentReceived(event): void {
    if (event.status === 201) {
      alert('Document created successfully');
    } else {
      alert('Error happened while adding the document');
    }
  }

}
