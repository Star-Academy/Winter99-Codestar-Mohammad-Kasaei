import {Injectable} from '@angular/core';
import {DocumentModel} from '../shared/Document.model';
import {HttpClient, HttpResponse} from '@angular/common/http';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {UrlResolverService} from './url-resolver.service';


@Injectable({
  providedIn: 'root'
})
export class DocumentsService {
  private indexName = 'search-app';

  constructor(private urlResolver: UrlResolverService, private http: HttpClient) {
  }

  checkConnection(): Observable<boolean> {
    return this.http
      .get(this.urlResolver.getPing(), {observe: 'response'})
      .pipe(
        map((value) => {
            return value.status === 200;
          }
        )
      );
  }

  queryDocuments(queryString: string): Observable<DocumentModel[]> {
    return this.http
      .get(this.urlResolver.getQueryWithParam(this.indexName, queryString), {responseType: 'json', observe: 'response'})
      .pipe(map(value => {
        console.log('I am service and receiving the result');
        if (value.status === 200) {
          return value.body;
        } else {
          return '';
        }
      }))
      .pipe(map(docsRaw => {
          return (docsRaw as DocumentModel[]);
        }
      ));
  }

  addDocument(documentModel: DocumentModel): Observable<HttpResponse<any>> {
    return this.http
      .post(
        this.urlResolver.getAddDocument(this.indexName),
        documentModel, {observe: 'response'}
      );
  }
}
