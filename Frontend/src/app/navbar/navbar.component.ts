import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-navbar',
  imports: [RouterModule, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  navItems = [
    { label: 'Home', link: '/' },
    { label: 'Pokoje', link: '/booking' },
    { label: 'Kursy', link: '/kursy' },
    { label: 'harmonogram', link: '/harmonogram' },
    { label: 'regulamin', link: '/regulamin' },
    { label: 'Zarezerwuj', link: '/payU' },
  ];
}
