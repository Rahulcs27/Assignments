import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GalleryService {
  private baseUrl = 'https://localhost:44357/api';

  constructor(private http: HttpClient) {}
  
  getArtistById(userId: string): Observable<any> {
    const url = `https://localhost:44357/api/Gallery/GetArtistById?userId=${userId}`;
    return this.http.get<any>(url);
  }
  

  getAllGalleries(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Gallery/All`);
  }

  getGalleryArtworks(galleryId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/ArtworkGallery/gallery/${galleryId}`);
  }

  createGallery(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Gallery/create`, data);
  }

  updateGallery(galleryId: number, data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/Gallery/Update/${galleryId}`, data);
  }

  deleteGallery(galleryId: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Gallery/Delete/${galleryId}`);
  }
  addArtworkToGallery(artworkId: number, galleryId: number) {
    const url = `https://localhost:44357/api/ArtworkGallery/add?artworkId=${artworkId}&galleryId=${galleryId}`;
    console.log("Calling API:", url); 
  
    return this.http.post(url, {});
  }  
  

  removeArtworkFromGallery(galleryId: number, artworkId: number): Observable<any> {
    return this.http.request('DELETE', `${this.baseUrl}/ArtworkGallery/remove`, {
      body: { galleryId, artworkId }, 
    });
  }

}
