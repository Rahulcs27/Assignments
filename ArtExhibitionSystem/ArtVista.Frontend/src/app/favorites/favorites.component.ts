import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { FavoriteService } from '../services/favorite.service';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-favorites',
  standalone: true,
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.css'],
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule]
})
export class FavoritesComponent implements OnInit {
  favoriteArtworks: any[] = [];

  constructor(private favoriteService: FavoriteService, private authService: AuthService) {}

  ngOnInit(): void {
    this.loadFavoriteArtworks();
  }

  loadFavoriteArtworks() {
    const userId = this.authService.getUserId();
    if (!userId) {
      console.error('User ID is missing. Cannot fetch favorites.');
      return;
    }

    this.favoriteService.getFavoriteArtworks(userId).subscribe({
      next: (favorites) => {
        this.favoriteArtworks = favorites;
        console.log(this.favoriteArtworks);
      },
      error: (err) => console.error('Error fetching favorite artworks:', err)
    });
  }

  toggleFavorite(artworkID: number) {
    this.favoriteService.toggleFavorite(artworkID).subscribe({
      next: () => {
        this.favoriteArtworks = this.favoriteArtworks.filter(artwork => artwork.artworkID !== artworkID);
      },
      error: (err) => console.error('Error toggling favorite:', err)
    });
  }
}
