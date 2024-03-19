import { Component, inject } from '@angular/core';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import RegistrationValues from '../../types/RegistrationValues';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CardModule, ButtonModule, DialogModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  constructor(private router: Router) {}

  visible: boolean = false;
  buttonDisabled: boolean = false;
  buttonText: string = 'Register';
  errors: string[] = [];
  registerForm = new FormGroup({
    email: new FormControl(''),
    username: new FormControl(''),
    fullName: new FormControl(''),
    phoneNumber: new FormControl(''),
    profileUrl: new FormControl(''),
    password: new FormControl(''),
    repeatPassword: new FormControl(''),
  });
  authService = inject(AuthService);

  showDialog() {
    this.visible = true;
  }

  registerUser() {
    const formValues = {
      email: this.registerForm.value.email ?? '',
      username: this.registerForm.value.username ?? '',
      fullName: this.registerForm.value.fullName ?? '',
      phoneNumber: this.registerForm.value.phoneNumber ?? '',
      profileUrl: this.registerForm.value.profileUrl ?? '',
      password: this.registerForm.value.password ?? '',
      repeatPassword: this.registerForm.value.repeatPassword ?? '',
    };
    this.validateForm(formValues);
    if (this.errors.length > 0) {
      this.showDialog();
    } else {
      this.buttonText = 'Processing';
      this.buttonDisabled = true;
      this.authService.registerUser(formValues).then((data) => {
        if (data?.result == 'Email already in use') {
          this.registrationErrorHandler('Email already in use.');
        } else if (data?.result == 'Phone number already in use') {
          this.registrationErrorHandler('Phone number already in use.');
        } else {
          this.router.navigate(['/login']);
        }
      });
    }
  }

  validateForm(values: RegistrationValues) {
    this.errors = [];
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(values.email)) {
      this.errors.push(
        'Email is invalid. Please re-enter the email so it looks like this(example@example.com.)'
      );
    }
    if (!/^\(?(\d{3})\)?[- ]?(\d{3})[- ]?(\d{4})$/.test(values.phoneNumber)) {
      this.errors.push(
        'Phone number is invalid. Please re-enter the phone number so it follows a valid format.'
      );
    }
    if (!/^[a-zA-Z0-9_-]{3,20}$/.test(values.username)) {
      this.errors.push(
        'Username is invalid. Please re-enter the username so it has between 3-20 characters.'
      );
    }
    if (!/^[a-zA-Z\s.'-]{2,50}$/.test(values.fullName)) {
      this.errors.push(
        'Full name is invalid. Please re-enter the full name so it has between 2-50 characters.'
      );
    }
    if (
      !/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$/.test(values.password)
    ) {
      this.errors.push(
        'Password is invalid. Please re-enter the password so it has at least one lower and upper case character, one digit and minimum lenght is 8.'
      );
    } else if (
      !/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$/.test(
        values.repeatPassword ?? ''
      )
    ) {
      this.errors.push(
        'Repeat password is invalid. Please re-enter the repeat password so it has at least one lower and upper case character, one digit and minimum lenght is 8.'
      );
    } else if (values.password != values.repeatPassword) {
      this.errors.push('Password and repeat password do not match.');
    }
    if (!/^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$/.test(values.profileUrl)) {
      this.errors.push('Profile url is invalid. Please re-enter a valid one.');
    }
  }

  registrationErrorHandler(errorText: string) {
    this.buttonText = 'Register';
    this.buttonDisabled = false;
    this.errors.push(errorText);
    this.showDialog();
  }
}
