import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LoginService } from '../../services/login.service';
import { LoginDto } from '../../Domain/account';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  constructor(private accountService: LoginService, private router: Router) {}

  onSubmit() {
    console.log('Email:', this.username);
    console.log('Password:', this.password);
    var loginDto = new LoginDto(this.username, this.password);
    this.accountService.Login(loginDto).subscribe({
      next: (data) => {
        this.router.navigate(['/rooms']);
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
  }
}
