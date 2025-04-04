// import { Component } from '@angular/core';
// import { Router, RouterModule } from '@angular/router';
// import { AuthService } from '../auth.service';
// import { FormsModule } from '@angular/forms';
// import { CommonModule } from '@angular/common';

// @Component({
//   selector: 'app-register',
//   standalone: true,
//   templateUrl: './register.component.html',
//   styleUrls: ['./register.component.css'],
//   imports: [FormsModule, RouterModule,CommonModule]
// })
// export class RegisterComponent {
//   email = '';
//   password = '';
//   role = 'User';
//   firstName = '';
//   lastName = '';
//   dateOfBirth: string = new Date().toString();

//   constructor(private authService: AuthService, private router: Router) {}

//   register() {
//     this.authService.register(this.email, this.password, this.role, this.firstName, this.lastName, this.dateOfBirth).subscribe(() => {
//       this.router.navigate(['/login']);
//     }, error => {
//       console.error('Registration failed', error);
//     });
//   }
// }
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../auth.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  imports: [FormsModule, RouterModule, CommonModule]
})
export class RegisterComponent {
  email = '';
  password = '';
  role = 'User';
  firstName = '';
  lastName = '';
  dateOfBirth: string = new Date().toString();
  errorMessage: string | null = null; 

  constructor(private authService: AuthService, private router: Router) {}

  register() {
    this.errorMessage = null;
  
    this.authService.register(this.email, this.password, this.role, this.firstName, this.lastName, this.dateOfBirth)
      .subscribe({
        next: () => {
          this.router.navigate(['/login']);
        },
        error: (error) => {
          console.error('Registration failed:', error);
  
          if (error.error && error.error.Message) {
            this.errorMessage = error.error.Message; 
          } else {
            this.errorMessage = 'Registration failed. Please try again.';
          }
        }
      });
  }
  
}
