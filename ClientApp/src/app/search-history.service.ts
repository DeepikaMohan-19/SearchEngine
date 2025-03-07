import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SearchHistoryService {
 private apiUrl = `${environment.apiBaseUrl}/airports`;

  constructor(private http: HttpClient) {}

  getSearchHistory(): Observable<string[]> {
    return this.http.get<string[]>(this.apiUrl);
  }
}
