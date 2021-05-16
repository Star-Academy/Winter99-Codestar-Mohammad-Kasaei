import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-search-area',
  templateUrl: './search-area.component.html',
  styleUrls: ['./search-area.component.scss']
})
export class SearchAreaComponent {

  @Output()
  public searchRequested: EventEmitter<string> = new EventEmitter<string>();

  onSearchBtnClicked(boxContent: string): void {
    this.searchRequested.emit(boxContent);
  }
}
