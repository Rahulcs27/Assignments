import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GalleryService {
  private baseUrl = 'https://localhost:44357/api';

  constructor(private http: HttpClient) {}

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
}
