import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {ToolbarComponent} from './toolbar/toolbar.component';
import {ToolbarButtonComponent} from './toolbar/button/toolbar-button.component';
import {InputBoxComponent} from './shared/input-box/input-box.component';
import {AddDocumentComponent} from './add-document/add-document.component';
import {SearchComponent} from './search/search.component';
import {SearchAreaComponent} from './search/search-area/search-area.component';
import {TitleComponent} from './search/search-area/search-row/title/title.component';
import {SearchIconComponent} from './search/search-area/search-row/search-icon/search-icon.component';
import {SearchRowComponent} from './search/search-area/search-row/search-row.component';
import {ResultsComponent} from './search/results/results.component';
import {ResultCardComponent} from './search/results/result-card/result-card.component';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import { ButtonComponent } from './shared/button/button.component';

@NgModule({
  declarations: [
    AppComponent,
    ToolbarComponent,
    ToolbarButtonComponent,
    SearchAreaComponent,
    TitleComponent,
    SearchIconComponent,
    SearchRowComponent,
    InputBoxComponent,
    AddDocumentComponent,
    SearchComponent,
    ResultsComponent,
    ResultCardComponent,
    ResultsComponent,
    ButtonComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
