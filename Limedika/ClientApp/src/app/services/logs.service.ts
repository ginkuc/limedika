import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Log } from '../models/log';

@Injectable({
  providedIn: 'root'
})
export class LogsService {
  private logsController = 'logs';
  private baseUrl: string;

  constructor(
    private httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
      this.baseUrl = baseUrl + 'api/';
    }

  public getLogs(): Observable<Log[]> {
    return this.httpClient.get<Log[]>(this.baseUrl + this.logsController);
  }
}
