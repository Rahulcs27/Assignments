import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../auth/auth.service';


@Injectable({
  providedIn: 'root' 
})
export class FavoriteService {
  private apiUrl = 'https://localhost:44357/api/FavoriteArtwork';

  constructor(private http: HttpClient, private authService: AuthService) { }

  getFavoriteArtworks(userId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/${userId}`);
  }  


  toggleFavorite(artworkID: number): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}/ToggleFavorite?artworkId=${artworkID}`, {});
  }  
}
