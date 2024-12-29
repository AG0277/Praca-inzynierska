import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';
import { Inject, PLATFORM_ID } from '@angular/core';
import { CustomJwtPayload } from '../../Domain/jwtPayload';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-navbar',
  imports: [RouterModule, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  constructor(@Inject(PLATFORM_ID) private platformId: Object) {}
  navItems = [
    { label: 'Pokoje', link: '/rooms' },
    { label: 'Kursy', link: '/courses' },
    { label: 'Instruktorzy', link: '/instructors' },
    { label: 'Daty kursów', link: '/assignements' },
    { label: 'Rezerwacje', link: '/reservations' },
  ];
  get isAuthenticated(): boolean {
    if (isPlatformBrowser(this.platformId))
      return !!localStorage.getItem('jwtToken');
    return false;
  }

  get isAdmin(): boolean {
    if (!isPlatformBrowser(this.platformId)) return false;
    const token = localStorage.getItem('jwtToken');
    if (token == null) return false;
    var payload = jwtDecode<CustomJwtPayload>(token);
    if (!payload.role) return false;
    if (payload.role == 'Admin') return true;
    return false;
  }

  LogOut(): void {
    localStorage.removeItem('jwtToken');
    window.location.reload();
  }
}
