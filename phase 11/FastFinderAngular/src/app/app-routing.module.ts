import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AddDocumentComponent} from './add-document/add-document.component';
import {SearchComponent} from './search/search.component';

const routes: Routes = [
  {path: '', redirectTo: 'search', pathMatch: 'full'},
  {path: 'search', component: SearchComponent},
  {path: 'add', component: AddDocumentComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
