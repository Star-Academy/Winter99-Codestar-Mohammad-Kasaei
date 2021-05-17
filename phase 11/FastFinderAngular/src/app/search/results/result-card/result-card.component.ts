import {Component, Input} from '@angular/core';
import {DocumentModel} from '../../../shared/Document.model';

@Component({
  selector: 'app-result-card',
  templateUrl: './result-card.component.html',
  styleUrls: ['./result-card.component.scss']
})
export class ResultCardComponent {
  @Input()
  public document: DocumentModel;
}
