import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:44357/api/auth';  // Ensure this is your actual API URL

  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, { email, password });
  }

  register(email: string, password: string, role: string, firstName: string, lastName: string, dateOfBirth: string): Observable<any> {
    const payload = {
      email,
      password,
      role,
      firstName,
      lastName,
      dateOfBirth
    };

    return this.http.post<any>(`${this.apiUrl}/register`, payload);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
  getUserId(): string | null {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken: any = jwtDecode(token);
      return decodedToken.userId || null;
    }
    return null;
  }
  getArtistID(): string | null {
    const token = localStorage.getItem('token'); 
    if (token) {
        const decodedToken: any = jwtDecode(token);
        return decodedToken.userId || null; 
    }
    return null;
  }
  getUserRole(): string | null {
    const token = localStorage.getItem('token'); 
    if (token) {
      try {
        const decodedToken: any = jwtDecode(token);
        return decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || null;
      } catch (error) {
        console.error('Error decoding JWT token', error);
        return null;
      }
    }
    return null;
  }
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('email');
  }
}
