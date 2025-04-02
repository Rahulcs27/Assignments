import { Component, OnInit } from '@angular/core';
import { GalleryService } from '../../services/gallery.service';
import { AuthService } from '../../auth/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from 'express';
import { RouterModule } from '@angular/router';
@Component({
  selector: 'app-gallery-list',
  templateUrl: './gallery-list.component.html',
  styleUrls: ['./gallery-list.component.css'],
  imports: [CommonModule,FormsModule,RouterModule],
})
export class GalleryListComponent implements OnInit {
  galleries: any[] = [];
  loggedInUserId: string | null = null;

  constructor(private galleryService: GalleryService, private authService: AuthService) {}

  ngOnInit(): void {
    this.loggedInUserId = this.authService.getUserId(); 
    this.fetchGalleries();
  }

  fetchGalleries(): void {
    this.galleryService.getAllGalleries().subscribe((data: any[]) => {
      this.galleries = data;
      
      this.galleries.forEach(gallery => {
        this.galleryService.getGalleryArtworks(gallery.galleryID).subscribe((artworks: any[]) => {
          gallery.artworks = artworks;
          console.log(gallery.artworks); 
        });
      });
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
}
// import { Component, OnInit } from '@angular/core';
// import { GalleryService } from '../../services/gallery.service';
// import { AuthService } from '../../auth/auth.service';
// import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';
// import { RouterModule } from '@angular/router';
// import { forkJoin } from 'rxjs'; // Import forkJoin for batch API calls

// @Component({
//   selector: 'app-gallery-list',
//   templateUrl: './gallery-list.component.html',
//   styleUrls: ['./gallery-list.component.css'],
//   imports: [CommonModule, FormsModule, RouterModule],
// })
// export class GalleryListComponent implements OnInit {
//   galleries: any[] = [];
//   loggedInUserId: string | null = null;

//   constructor(private galleryService: GalleryService, private authService: AuthService) {}

//   ngOnInit(): void {
//     console.log("Fetching galleries...");
//     this.loggedInUserId = this.authService.getUserId(); 
//     this.fetchGalleries();
//   }

//   fetchGalleries(): void {
//     this.galleryService.getAllGalleries().subscribe((galleries: any[]) => {
//       console.log("API Response: ", galleries); // Check if API returns duplicates
//       this.galleries = galleries;

//       if (!galleries.length) return; // Exit if there are no galleries

//       const artworkRequests = galleries.map(gallery =>
//         this.galleryService.getGalleryArtworks(gallery.galleryID)
//       );

//       // Fetch all artworks in a single batch
//       forkJoin(artworkRequests).subscribe(artworksArray => {
//         this.galleries.forEach((gallery, index) => {
//           gallery.artworks = artworksArray[index]; // Assign artworks to the respective gallery
//         });
//       });
//     });
//   }

//   isOwner(artistId: string): boolean {
//     return this.loggedInUserId === artistId;
//   }

//   deleteGallery(galleryId: number): void {
//     if (confirm('Are you sure you want to delete this gallery?')) {
//       this.galleryService.deleteGallery(galleryId).subscribe(() => {
//         console.log(`Gallery ${galleryId} deleted`);
//         this.fetchGalleries(); // Refresh the gallery list
//       });
//     }
//   }
// }

