import { Component, inject, OnInit } from '@angular/core';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../../../services/user.service';
import { AuthService } from '../../../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import UpdateUser from '../../../types/UpdateUser';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CardModule, ButtonModule, DialogModule, ReactiveFormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  registerForm = new FormGroup({
    email: new FormControl(''),
    username: new FormControl(''),
    fullName: new FormControl(''),
    phoneNumber: new FormControl(''),
    profileUrl: new FormControl('')
  });
  userId: string = "";
  visible: boolean = false;
  buttonDisabled: boolean = false;
  buttonText: string = 'Submit';
  loading: boolean = true;
  errors: string[] = [];

  constructor(private cookie: CookieService, private router: Router) {}

  userService = inject(UserService);
  authService = inject(AuthService);

  ngOnInit(): void {
    this.authService.getUserByToken(this.cookie.get("token"))
    .then((data) => {
      this.userId = data?.result.id;
      this.registerForm.patchValue({
        email: data?.result.email,
        username: data?.result.username,
        fullName: data?.result.fullName,
        phoneNumber: data?.result.phoneNumber,
        profileUrl: data?.result.profileUrl
      });
      this.loading = false;
    })
    .catch((error) => {
      console.error('Error fetching user data:', error);
    });
  }

  async updateUser() {
    const formValues = {
      id: this.userId ?? '',
      username: this.registerForm.value.username ?? '',
      fullName: this.registerForm.value.fullName ?? '',
      phoneNumber: this.registerForm.value.phoneNumber ?? '',
      profileUrl: this.registerForm.value.profileUrl ?? '',
    };
    this.validateForm(formValues);
    if (this.errors.length > 0) {
      this.showDialog();
    } else {
      this.buttonText = 'Processing';
      this.buttonDisabled = true;
      await this.userService.updateUser(formValues)
      .then(() => {
        this.router.navigate(['/']);
      })
      .catch((error) => console.log(error));
    }
  }

  showDialog() {
    this.visible = true;
  }

  validateForm(values: UpdateUser) {
    this.errors = [];
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
    if (!/^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$/.test(values.profileUrl)) {
      this.errors.push('Profile url is invalid. Please re-enter a valid one.');
    }
  }

  registrationErrorHandler(errorText: string) {
    this.buttonText = 'Submit';
    this.buttonDisabled = false;
    this.errors.push(errorText);
    this.showDialog();
  }

  goToChangePassword() {
    this.router.navigate(['/change-password', this.userId]);
  }
}
