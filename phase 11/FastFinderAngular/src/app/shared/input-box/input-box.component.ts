import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-input-box',
  templateUrl: './input-box.component.html',
  styleUrls: ['./input-box.component.scss']
})
export class InputBoxComponent {

  @Output()
  public textChanged: EventEmitter<string> = new EventEmitter<string>();
  public inputContent: string;
  @Input()
  public placeHolder: string;

  public onInputContentChanged(): void {
    this.textChanged.emit(this.inputContent);
  }
}
