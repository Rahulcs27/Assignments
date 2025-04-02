import { Component, OnInit } from '@angular/core';
import { ArtworkService } from '../services/artwork.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    imports: [CommonModule, RouterModule]
})
export class HomeComponent implements OnInit {
    artworks: any[] = [];

    constructor(private artworkService: ArtworkService) {}

    ngOnInit(): void {
        this.fetchArtworks();
    }

    fetchArtworks() {
        this.artworkService.getAllArtworks().subscribe({
            next: (data) => this.artworks = data,
            error: (err) => console.error('Error fetching artworks:', err)
        });
    }

    handleImageError(event: any) {
        console.error('Image failed to load:', event);
    }
}
