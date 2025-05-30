import { Component, OnInit } from '@angular/core';
import { GalleryService } from '../../services/gallery.service';
import { ArtworkService } from '../../services/artwork.service';
import { AuthService } from '../../auth/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-gallery-list',
  templateUrl: './gallery-list.component.html',
  styleUrls: ['./gallery-list.component.css'],
  imports: [CommonModule, FormsModule, RouterModule],
})
export class GalleryListComponent implements OnInit {
  galleries: any[] = [];
  allArtworks: any[] = [];  
  filteredArtworks: any[] = [];  
  selectedArtwork: { [galleryID: number]: number } = {};  
  loggedInUserId: string | null = null;
  searchQuery: string = '';
  userEmail:string = localStorage.getItem('email') || '';
  constructor(
    private galleryService: GalleryService,
    private artworkService: ArtworkService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loggedInUserId = this.authService.getUserId(); 
    this.fetchGalleries();
    this.fetchArtworks();
  }

  fetchGalleries(): void {
    this.galleryService.getAllGalleries().subscribe((galleries: any[]) => {
      this.galleries = galleries;

      this.galleries.forEach(gallery => {
        this.galleryService.getGalleryArtworks(gallery.galleryID).subscribe((artworks: any[]) => {
          gallery.artworks = artworks;
        });

        this.galleryService.getArtistById(gallery.artistId).subscribe(
          (artist) => {
            gallery.artistName = artist.name;
          },
          (error) => {
            console.error(`Error fetching artist for ID ${gallery.artistId}`, error);
            gallery.artistName = 'Unknown Artist';
          }
        );
      });
    });
  }

  fetchArtworks(): void {
    this.artworkService.getAllArtworks().subscribe((data: any[]) => {
      this.allArtworks = data;
      
      this.filteredArtworks = this.allArtworks.filter(artwork => artwork.artistID === this.loggedInUserId);
    });
  }

  isOwner(artistId: string): boolean {
    return this.loggedInUserId === artistId;
  }

  deleteGallery(galleryId: number): void {
    if (confirm('Are you sure you want to delete this gallery?')) {
      this.galleryService.deleteGallery(galleryId).subscribe(() => {
        this.fetchGalleries();
      });
    }
  }

  addArtwork(galleryId: number): void {
    const artworkId = this.selectedArtwork[galleryId];
  
    if (!artworkId) {
      alert('Please select an artwork.');
      return;
    }
  
    console.log(`Sending API request with artworkId: ${artworkId} and galleryId: ${galleryId}`);
  
    this.galleryService.addArtworkToGallery(artworkId, galleryId).subscribe({
      next: () => {
        alert('Artwork added successfully!');
        this.fetchGalleries();
      },
      error: (error) => {
        if(error.status === 400) {
          alert('Artwork already exists in the gallery!');
          return;
        }
        console.error('Error adding artwork:', error);
        alert(error.error?.message || 'Failed to add artwork');
      }
    });
  }

  searchGalleryArtworks(): void {
    if (!this.searchQuery.trim()) {
      this.fetchGalleries();
      return;
    }

    this.galleryService.searchGalleryArtworks(this.searchQuery).subscribe({
      next: (searchResults) => {
        this.galleries.forEach((gallery) => {
          gallery.artworks = searchResults.filter(artwork =>
            gallery.artworks.some((a: { artworkID: number }) => a.artworkID === artwork.artworkID)
          );
        });
      },
      error: (error) => {
        console.error('Error searching artworks:', error);
      }
    });
  }
}
