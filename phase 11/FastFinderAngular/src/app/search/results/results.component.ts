import {Component, Input} from '@angular/core';
import {DocumentModel} from '../../shared/Document.model';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.scss']
})
export class ResultsComponent {
  @Input()
  public documentModels: DocumentModel[] = [];

  @Input()
  public loadedData = false;

  public showCouldNotFindMessage(): boolean {
    return this.documentModels.length === 0 && this.loadedData;
  }
}
