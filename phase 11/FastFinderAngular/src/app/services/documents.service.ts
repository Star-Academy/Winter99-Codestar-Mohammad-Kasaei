import {Injectable} from '@angular/core';
import {DocumentModel} from '../shared/Document.model';
import {HttpClient, HttpResponse} from '@angular/common/http';
import {Observable, Subject} from 'rxjs';
import {map} from 'rxjs/operators';
import {UrlResolverService} from './url-resolver.service';
import {ConfigurationService} from './configuration.service';


@Injectable({
  providedIn: 'root'
})
export class DocumentsService {
  private readonly indexName: string;
  private readonly documentsSubject: Subject<DocumentModel[]> = new Subject<DocumentModel[]>();

  public constructor(private configurationService: ConfigurationService,
                     private urlResolver: UrlResolverService,
                     private http: HttpClient) {
    this.indexName = configurationService.getIndexName();
  }


  public get documents(): Observable<DocumentModel[]> {
    return this.documentsSubject;
  }

  public checkConnection(): Observable<boolean> {
    return this.http
      .get(this.urlResolver.getPing(), {observe: 'response'})
      .pipe(
        map((value) => {
            return value.status === 200;
          }
        )
      );
  }

  public queryDocuments(queryString: string): void {
    const requestObservable = this.http
      .get(this.urlResolver.getQueryWithParam(this.indexName, queryString), {responseType: 'json', observe: 'response'})
      .pipe(map(value => {
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
    requestObservable.subscribe(value => {
      this.documentsSubject.next(value);
    }, () => {
      this.documentsSubject.next([]);
    });
  }

  public addDocument(documentModel: DocumentModel): Observable<HttpResponse<any>> {
    return this.http
      .post(
        this.urlResolver.getAddDocument(this.indexName),
        documentModel, {observe: 'response'}
      );
  }
}
