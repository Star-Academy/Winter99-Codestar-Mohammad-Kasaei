import {Injectable} from '@angular/core';
import {ConfigurationService} from './configuration.service';

@Injectable({
  providedIn: 'root'
})
export class UrlResolverService {
  private readonly baseUrl: string;

  public constructor(private configurationService: ConfigurationService) {
    this.baseUrl = this.configurationService.getBaseUrl();
  }

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
