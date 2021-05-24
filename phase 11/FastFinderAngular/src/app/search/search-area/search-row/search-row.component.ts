import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-search-row',
  templateUrl: './search-row.component.html',
  styleUrls: ['./search-row.component.scss']
})
export class SearchRowComponent {

  @Output()
  public searchBtnClicked: EventEmitter<string> = new EventEmitter<string>();
  private boxContent: string;

  public onSearchBtnClicked(): void {
    this.searchBtnClicked.emit(this.boxContent);
  }

  public searchInputTextChanged(newValue): void {
    this.boxContent = newValue;
  }

  public onKeyPress(event: any): void {
    if (event.key === 'Enter') {
      this.onSearchBtnClicked();
    }
  }
}
