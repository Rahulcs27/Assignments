import { Component } from '@angular/core';
import { ArtworkService } from '../../services/artwork.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-add-artwork',
  templateUrl: './add-artwork.component.html',
  styleUrls: ['./add-artwork.component.css'],
  imports: [CommonModule, RouterModule,FormsModule],
  standalone: true
})
export class AddArtworkComponent {
  artwork = {
    title: '',
    description: '',
    imageURL: '',
    artistID: '' 
  };
  constructor(private artworkService: ArtworkService, private router: Router, private authService: AuthService) {}
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

}
