import { Component, OnInit } from '@angular/core';
import { ArtworkService } from '../services/artwork.service';
import { GalleryService } from '../services/gallery.service';
import { FavoriteService } from '../services/favorite.service';
import { AuthService } from '../auth/auth.service';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, RouterModule, FormsModule]

})
export class HomeComponent implements OnInit {
  artworks: any[] = [];
  favoriteArtworks: Set<number> = new Set();
  searchQuery: string = '';

  constructor(
    private artworkService: ArtworkService,
    private galleryService: GalleryService,
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
        this.assignArtistNames();
      },
      error: (err) => console.error('Error fetching artworks:', err),
    });
  }

  searchArtworks() {
    if (!this.searchQuery.trim()) {
      this.fetchArtworks();
      return;
    }

    this.artworkService.searchArtworks(this.searchQuery).subscribe({
      next: (data) => {
        this.artworks = data;
        this.assignArtistNames();
      },
      error: (err) => console.error('Error searching artworks:', err),
    });
  }

  assignArtistNames() {
    this.artworks.forEach((artwork) => {
      if (!artwork.artistID) {
        artwork.artistName = 'Unknown Artist';
        return;
      }
      this.galleryService.getArtistById(artwork.artistID).subscribe(
        (artist) => (artwork.artistName = artist?.name || 'Unknown Artist'),
        () => (artwork.artistName = 'Unknown Artist')
      );
    });
  }

  loadFavorites() {
    const userId = this.authService.getUserId();
    if (!userId) return;

    this.favoriteService.getFavoriteArtworks(userId).subscribe({
      next: (favorites) => {
        this.favoriteArtworks = new Set(favorites.map((fav) => fav.artworkID));
      },
      error: (err) => console.error('Error fetching favorites:', err),
    });
  }

  isFavorite(artworkID: number): boolean {
    return this.favoriteArtworks.has(artworkID);
  }

  toggleFavorite(artworkID: number) {
    this.favoriteService.toggleFavorite(artworkID).subscribe({
      next: () => this.loadFavorites(),
      error: (err) => console.error('Error toggling favorite:', err),
    });
  }
}
