import { Component, inject } from '@angular/core';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { DataStorage } from '../../services/dataStorage.service';
import { DialogModule } from 'primeng/dialog';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CardModule, ButtonModule, DialogModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  constructor(private router: Router, private cookies: CookieService) {}

  visible: boolean = false;
  buttonDisabled: boolean = false;
  buttonText: string = 'Register';
  errors: string[] = [];
  loginForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl('')
  });
  authService = inject(AuthService);
  dataStorage = inject(DataStorage);

  showDialog() {
    this.visible = true;
  }

  loginUser() {
    const formValues = {
      email: this.loginForm.value.email ?? '',
      password: this.loginForm.value.password ?? ''
    };
    this.buttonText = 'Processing';
    this.buttonDisabled = true;
    this.authService.loginUser(formValues).then((data) => {
      if (data?.result.username == '') {
        this.loginErrorHandler('Invalid email or password.');
      } else {
        console.log(data?.result);
        this.dataStorage.addData("userId", data?.result.userId);
        this.dataStorage.addData("username", data?.result.username);
        this.dataStorage.addData("role", data?.result.role);
        this.cookies.set("token", data?.result.jwtToken);
        this.router.navigate(['/']);
      }
      });
    
  }

  loginErrorHandler(errorText: string) {
    this.buttonText = 'Login';
    this.buttonDisabled = false;
    this.errors.push(errorText);
    this.showDialog();
  }
}
