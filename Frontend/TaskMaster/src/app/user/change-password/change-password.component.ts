import { Component, inject, OnInit } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-change-password',
  standalone: true,
  imports: [CardModule, ButtonModule, DialogModule, ReactiveFormsModule],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css',
})
export class ChangePasswordComponent implements OnInit {
  constructor(private route: ActivatedRoute, private router: Router) {}

  visible: boolean = false;
  buttonDisabled: boolean = false;
  buttonText: string = 'Submit';
  errors: string[] = [];
  userId: string = '';
  loading: boolean = true;
  registerForm = new FormGroup({
    password: new FormControl(''),
    repeatPassword: new FormControl(''),
  });
  userService = inject(UserService);

  showDialog() {
    this.visible = true;
  }

  changePassword() {
    const formValues = {
      id: this.userId,
      password: this.registerForm.value.password ?? '',
      repeatPassword: this.registerForm.value.repeatPassword ?? '',
    };
    this.validateForm(formValues.password, formValues.repeatPassword);
    if (this.errors.length > 0) {
      this.showDialog();
    } else {
      this.buttonText = 'Processing';
      this.buttonDisabled = true;
      this.userService
        .changeUserPassword({
          userId: formValues.id,
          password: formValues.password,
        })
        .then(() => {
          this.router.navigate(['/']);
        });
    }
  }

  validateForm(password: string, repeatPassword: string) {
    if (!/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$/.test(password)) {
      this.errors.push(
        'Password is invalid. Please re-enter the password so it has at least one lower and upper case character, one digit and minimum lenght is 8.'
      );
    } else if (
      !/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$/.test(
        repeatPassword ?? ''
      )
    ) {
      this.errors.push(
        'Repeat password is invalid. Please re-enter the repeat password so it has at least one lower and upper case character, one digit and minimum lenght is 8.'
      );
    } else if (password != repeatPassword) {
      this.errors.push('Password and repeat password do not match.');
    }
  }

  registrationErrorHandler(errorText: string) {
    this.buttonText = 'Submit';
    this.buttonDisabled = false;
    this.errors.push(errorText);
    this.showDialog();
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id: string = params.get('id') ?? '';
      this.userId = id;
    });
    this.loading = false;
  }
}
