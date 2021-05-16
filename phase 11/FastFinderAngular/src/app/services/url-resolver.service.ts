import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UrlResolverService {
  private baseUrl = 'https://localhost:5001/api/v1/Document/';

  getPing(): string {
    return `${this.baseUrl}ping`;
  }

  getQueryWithParam(indexName: string, queryString): string {
    return `${this.baseUrl}${indexName}?query=${queryString}`;
  }

  getAddDocument(indexName: string): string {
    return `${this.baseUrl}${indexName}`;
  }
}
