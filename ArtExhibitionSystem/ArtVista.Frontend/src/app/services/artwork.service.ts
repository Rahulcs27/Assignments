import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ArtworkService {
  private apiUrl = 'https://localhost:44357/api/Artwork';

  constructor(private http: HttpClient) { }

  getArtistById(userId: string): Observable<any> {
    const url = `https://localhost:44357/api/Gallery/GetArtistById?userId=${userId}`;
    return this.http.get<any>(url);
  }
  searchArtworks(query: string) {
    return this.http.get<any[]>(`https://localhost:44357/api/Artwork/search/${query}`);
  }  
  getAllArtworks(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
  addArtwork(artwork: any): Observable<string> { 
    return this.http.post(`${this.apiUrl}`, artwork, { responseType: 'text' });
  }
  updateArtwork(artworkID: number, artworkData: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${artworkID}`, artworkData);
  }
  getArtworkById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }
  deleteArtwork(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }  
  
}
