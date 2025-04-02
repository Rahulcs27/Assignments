import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ArtworkService {
  private apiUrl = 'https://localhost:44357/api/Artwork';

  constructor(private http: HttpClient) { }

  getAllArtworks(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
  addArtwork(artwork: any): Observable<string> { 
    return this.http.post(`${this.apiUrl}`, artwork, { responseType: 'text' });
  }
  
}
