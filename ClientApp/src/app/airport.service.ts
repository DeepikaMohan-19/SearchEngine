import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root', // This makes sure the service is available globally
})
export class AirportService {
  getFilters() {
    throw new Error('Method not implemented.');
  }
 private apiUrl = `${environment.apiBaseUrl}/airports`;

  constructor(private http: HttpClient) {}

  searchAirports(
    searchTerm: string,
    city?: string,
    country?: string,
    iata?: string,
    icao?: string,
    latitude?: number | null,
    longitude?: number | null,
    elevation?: number | null,
    runwayLength?: string,
    type?: string,
    timezone?: string,
    airportType?: string,
    source?: string,
    name?: string
  ): Observable<any> {
    let params = new HttpParams().set('searchTerm', searchTerm);

    if (city) params = params.set('city', city);
    if (country) params = params.set('country', country);
    if (iata) params = params.set('iata', iata);
    if (icao) params = params.set('icao', icao);
    if (latitude) params = params.set('latitude', latitude.toString());
    if (longitude) params = params.set('longitude', longitude.toString());
    if (elevation) params = params.set('elevation', elevation.toString());
    if (runwayLength) params = params.set('runwayLength', runwayLength);
    if (type) params = params.set('type', type);
    if (timezone) params = params.set('timezone', timezone);
    if (airportType) params = params.set('airportType', airportType);
    if (source) params = params.set('source', source);
    if (name) params = params.set('name', name);

    return this.http.get<any[]>(this.apiUrl, { params });
  }
}
