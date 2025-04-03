import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../auth/auth.service';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  imports: [CommonModule, RouterModule]
})
export class HeaderComponent {
  isLoggedIn = false;
  menuCollapsed: boolean = true;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.checkLoginStatus();
  }
  toggleMenu() {
    this.menuCollapsed = !this.menuCollapsed;
  }
  checkLoginStatus():boolean {
    if(localStorage.getItem('token') == null)
      return false;
    return true;
  }

  logout() {
    this.authService.logout();
    this.isLoggedIn = false;
    this.router.navigate(['/login']);
  }
}
