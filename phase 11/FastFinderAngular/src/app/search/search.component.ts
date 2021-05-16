import {Component, Input} from '@angular/core';
import {DocumentsService} from '../services/documents.service';
import {DocumentModel} from '../shared/Document.model';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent {

  @Input()
  public documents: DocumentModel[] = [];

  public loadedData = false;

  constructor(private documentsService: DocumentsService) {
  }

  onSearchRequested(query: string): void {
    this.documentsService
      .queryDocuments(query)
      .subscribe(value => {
        this.documents = value;
        this.loadedData = true;
      }, error => {
        if (error.status === 404) {
          this.documents = [];
          this.loadedData = true;
        }
      });
  }
}
