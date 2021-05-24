import {Injectable} from '@angular/core';
import * as configFile from 'src/assets/config.json';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationService {
  public getIndexName(): string {
    return configFile.indexName;
  }

  public getBaseUrl(): string {
    return configFile.baseApiUrl;
  }
}
