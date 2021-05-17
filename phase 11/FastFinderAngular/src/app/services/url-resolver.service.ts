import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UrlResolverService {
  private baseUrl = 'https://localhost:5001/api/v1/Document/';

  public getPing(): string {
    return `${this.baseUrl}ping`;
  }

  public getQueryWithParam(indexName: string, queryString): string {
    return `${this.baseUrl}${indexName}?query=${queryString}`;
  }

  public getAddDocument(indexName: string): string {
    return `${this.baseUrl}${indexName}`;
  }
}
