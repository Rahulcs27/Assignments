import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { HomeComponent } from './home/home.component';
import { AddArtworkComponent } from './artist/add-artwork/add-artwork.component';
import { GalleryListComponent } from './components/gallery-list/gallery-list.component';
import { FavoritesComponent } from './favorites/favorites.component';

export const routes: Routes = [
  // { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  // { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'favorites', component: FavoritesComponent },
  { path: '', component: GalleryListComponent },
  { path: 'add-artwork', component: AddArtworkComponent },
  { path: 'edit-artwork/:id', component: AddArtworkComponent }
];
