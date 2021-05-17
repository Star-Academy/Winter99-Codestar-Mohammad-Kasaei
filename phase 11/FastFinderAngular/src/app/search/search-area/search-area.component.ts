import {Component} from '@angular/core';
import {DocumentsService} from '../../services/documents.service';

@Component({
  selector: 'app-search-area',
  templateUrl: './search-area.component.html',
  styleUrls: ['./search-area.component.scss']
})
export class SearchAreaComponent {
  public constructor(private documentsService: DocumentsService) {
  }

  public onSearchBtnClicked(boxContent: string): void {
    this.documentsService.queryDocuments(boxContent);
  }
}
