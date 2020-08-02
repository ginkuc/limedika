import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ParsedLocation } from '../models/parsed-location';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  private locationController = 'locations';
  private baseUrl: string;

  constructor(
    private httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
      this.baseUrl = baseUrl + 'api/';
    }

  public getLocations(): Observable<Location[]> {
    return this.httpClient.get<Location[]>(this.baseUrl + this.locationController);
  }

  public updatePostCodes(): Observable<any> {
    return this.httpClient.patch(this.baseUrl + this.locationController, {});
  }

  public importLocations(parsedLocations: ParsedLocation[]): Observable<any> {
    return this.httpClient.post(this.baseUrl + this.locationController, parsedLocations);
  }
}
