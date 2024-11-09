import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { AssigmentService } from '../../../../services/assigment.service';
import { DataStorage } from '../../../../services/dataStorage.service';
import { UserService } from '../../../../services/user.service';
import User from '../../../../types/User';

@Component({
  selector: 'app-new-task',
  standalone: true,
  imports: [
    CardModule,
    ButtonModule,
    DropdownModule,
    DialogModule,
    ReactiveFormsModule,
  ],
  templateUrl: './new-task.component.html',
  styleUrls: ['./new-task.component.css'],
})
export class NewTaskComponent implements OnInit{
  visible: boolean = false;
  buttonDisabled: boolean = false;
  loading: boolean = true;
  buttonText: string = 'Submit';
  errors: string[] = [];
  usernames: string[] = [];
  registerForm = new FormGroup({
    title: new FormControl(''),
    description: new FormControl(''),
    username: new FormControl(''),
    priority: new FormControl(''),
    tags: new FormControl(''),
    dueDate: new FormControl(''),
  });

  assigmentService = inject(AssigmentService);
  userService = inject(UserService);
  dataStorage = inject(DataStorage);

  constructor(
    private router: Router,
  ) {}

  showDialog() {
    this.visible = true;
  }

  createNewAssigment() {
    const formValues = {
      title: this.registerForm.value.title ?? '',
      description: this.registerForm.value.description ?? '',
      tags: this.registerForm.value.tags ?? '',
      priority: this.registerForm.value.priority ?? 'Low',
      status: 'Todo',
      dueDate: this.registerForm.value.dueDate ?? '',
      username: this.registerForm.value.username ?? "Admin123",
    };
    this.buttonText = 'Processing';
    this.buttonDisabled = true;
    this.assigmentService.createAssigment(formValues).then(() => {
      this.router.navigate(['/admin-assigment']);
    }).catch((error) => {
      this.registrationErrorHandler(error.message);
    });
  }

  registrationErrorHandler(errorText: string) {
    this.buttonText = 'Submit';
    this.buttonDisabled = false;
    this.errors.push(errorText);
    this.showDialog();
  }

  async ngOnInit() {
    let users = await this.userService.getAllUsers("", "id", "asc", 0);
    users.elements.forEach((user: User) => {
      this.usernames.push(user.username);
    });
    this.loading = false;
  }
}
