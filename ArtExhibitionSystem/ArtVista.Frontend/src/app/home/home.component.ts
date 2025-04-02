import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ArtworkService } from '../services/artwork.service';
import { FavoriteService } from '../services/favorite.service';
import { AuthService } from '../auth/auth.service';
import { RouterModule } from '@angular/router';

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
      },
      error: (err) => {
        console.error('Error fetching artworks:', err);
      },
    });
  }

  loadFavorites() {
    const userId = this.authService.getUserId() || ''; // Provide a default empty string
    if (!userId) {
      console.error('User ID is null. Cannot fetch favorites.');
      return; // Stop execution if userId is not available
    }
  
    this.favoriteService.getFavoriteArtworks(userId).subscribe({
      next: (favorites) => {
        this.favoriteArtworks = new Set(favorites.map(fav => fav.artworkID));
      },
      error: (err) => console.error('Error fetching favorites:', err)
    });
  }
  

  isFavorite(artworkID: number): boolean {
    return this.favoriteArtworks.has(artworkID);
  }

  toggleFavorite(artworkID: number) {
    this.favoriteService.toggleFavorite(artworkID).subscribe({
      next: () => {
        this.loadFavorites(); 
      },
      error: (err) => console.error('Error toggling favorite:', err)
    });
  }
}
