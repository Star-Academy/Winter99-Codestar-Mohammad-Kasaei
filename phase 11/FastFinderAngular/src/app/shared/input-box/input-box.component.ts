import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-input-box',
  templateUrl: './input-box.component.html',
  styleUrls: ['./input-box.component.scss']
})
export class InputBoxComponent {

  @Output()
  textChanged: EventEmitter<string> = new EventEmitter<string>();
  inputContent: string;
  @Input()
  placeHolder: string;

  onInputContentChanged(): void {
    this.textChanged.emit(this.inputContent);
  }
}
