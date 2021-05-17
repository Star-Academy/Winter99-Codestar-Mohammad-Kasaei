import {Component, Input, OnChanges, SimpleChanges} from '@angular/core';
import {DocumentModel} from '../../shared/Document.model';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.scss']
})
export class ResultsComponent implements OnChanges {
  @Input()
  public documentModels: DocumentModel[] = [];

  @Input()
  public loadedData = false;

  public showCouldNotFindMessage = false;

  public ngOnChanges(changes: SimpleChanges): void {
    this.showCouldNotFindMessage = this.documentModels.length === 0 && this.loadedData;
  }
}
