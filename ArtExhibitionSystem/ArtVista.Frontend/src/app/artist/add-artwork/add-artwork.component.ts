import { Component } from '@angular/core';
import { ArtworkService } from '../../services/artwork.service';
import { GalleryService } from '../../services/gallery.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-add-artwork',
  templateUrl: './add-artwork.component.html',
  styleUrls: ['./add-artwork.component.css'],
  imports: [CommonModule, RouterModule, FormsModule],
  standalone: true
})
export class AddArtworkComponent {
  artwork = {
    title: '',
    description: '',
    imageURL: '',
    artistID: ''
  };
  

  gallery = {
    name: '',
    description: '',
    location: '',
    artistId: ''
  };

  message: string = '';

  constructor(
    private artworkService: ArtworkService,
    private galleryService: GalleryService,
    private router: Router,
    private authService: AuthService
  ) {}

  addArtwork() {
    const artistID = this.authService.getArtistID();

    if (!artistID) {
      console.error('ArtistID not found!');
      return;
    }

    const artworkData = {
      title: this.artwork.title,
      description: this.artwork.description,
      imageURL: this.artwork.imageURL,
      artistID: artistID
    };

    this.artworkService.addArtwork(artworkData).subscribe({
      next: () => {
        alert('Artwork added successfully!');
        this.router.navigate(['/home']);
      },
      error: (err) => console.error('Error adding artwork:', err)
    });
  }

  createGallery() {
    const artistID = this.authService.getArtistID();

    if (!artistID) {
      this.message = 'Artist ID is missing!';
      return;
    }

    if (!this.gallery.name.trim() || !this.gallery.description.trim() || !this.gallery.location.trim()) {
      this.message = 'All fields are required!';
      return;
    }

    const galleryData = {
      name: this.gallery.name,
      description: this.gallery.description,
      location: this.gallery.location,
      artistId: artistID
    };

    this.galleryService.createGallery(galleryData).subscribe({
      next: () => {
        this.message = 'Gallery created successfully!';
        this.gallery = { name: '', description: '', location: '', artistId: '' }; 
      },
      error: (err) => {
        console.error('Error creating gallery:', err);
        this.message = err.error?.message || 'Failed to create gallery.';
      }
    });
  }
}
