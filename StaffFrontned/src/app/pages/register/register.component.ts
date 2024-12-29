import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LoginService } from '../../services/login.service';
import { LoginDto, RegisterDto } from '../../Domain/account';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  username: string = '';
  email: string = '';
  password: string = '';
  constructor(private accountService: LoginService, private router: Router) {}

  onSubmit() {
    var registerDto = new RegisterDto(this.username, this.email, this.password);
    this.accountService.Register(registerDto).subscribe({
      next: (data) => {
        if (data == true) this.router.navigate(['/rooms']);
      },
      error: (err) => {
        console.error('Error fetching rooms:', err);
      },
    });
  }
}
