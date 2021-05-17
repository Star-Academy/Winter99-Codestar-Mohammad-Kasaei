import {Component, OnInit} from '@angular/core';
import {DocumentModel} from '../../shared/Document.model';
import {DocumentsService} from '../../services/documents.service';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.scss']
})
export class ResultsComponent implements OnInit {
  public documentModels: DocumentModel[] = [];
  public loadedData = false;

  public showCouldNotFindMessage = false;

  public constructor(private documentsService: DocumentsService) {
  }

  public ngOnInit(): void {
    this.documentsService
      .documents
      .subscribe(value => {
        this.documentModels = value;
        this.loadedData = true;
        this.showCouldNotFindMessage = this.documentModels.length === 0 && this.loadedData;
      });
  }
}
