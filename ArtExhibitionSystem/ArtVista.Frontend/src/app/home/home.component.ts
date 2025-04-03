import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ArtworkService } from '../services/artwork.service';
import { FavoriteService } from '../services/favorite.service';
import { AuthService } from '../auth/auth.service';
import { RouterModule } from '@angular/router';
import { GalleryService } from '../services/gallery.service';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, RouterModule],
})
export class HomeComponent implements OnInit {
  artworks: any[] = [];
  favoriteArtworks: Set<number> = new Set();

  constructor(
    private artworkService: ArtworkService,
    private galleryService: GalleryService,
    private cdr: ChangeDetectorRef,
    private authService: AuthService,
    private favoriteService: FavoriteService
  ) {}

  ngOnInit(): void {
    this.fetchArtworks();
    this.loadFavorites();
  }

  fetchArtworks() {
    this.artworkService.getAllArtworks().subscribe({
      next: (data) => {
        this.artworks = data;

        this.artworks.forEach((artwork) => {
          if (!artwork.artistID) {
            artwork.artistName = 'Unknown Artist';
            return;
          }

          this.galleryService.getArtistById(artwork.artistID).subscribe(
            (artist) => {
              artwork.artistName = artist?.name || 'Unknown Artist';
            },
            () => {
              artwork.artistName = 'Unknown Artist';
            }
          );
        });
      },
      error: () => {
        this.artworks = [];
      },
    });
  }

  loadFavorites() {
    const userId = this.authService.getUserId();
    if (!userId) return;

    this.favoriteService.getFavoriteArtworks(userId).subscribe({
      next: (favorites) => {
        this.favoriteArtworks = new Set(favorites.map((fav) => fav.artworkID));
      },
    });
  }

  isFavorite(artworkID: number): boolean {
    return this.favoriteArtworks.has(artworkID);
  }

  toggleFavorite(artworkID: number) {
    this.favoriteService.toggleFavorite(artworkID).subscribe({
      next: () => this.loadFavorites(),
    });
  }
}
