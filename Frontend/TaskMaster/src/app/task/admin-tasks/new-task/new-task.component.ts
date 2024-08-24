import { Component, inject } from "@angular/core";
import { FormControl, FormGroup, ReactiveFormsModule } from "@angular/forms";
import { Router } from "@angular/router";
import { ButtonModule } from "primeng/button";
import { CardModule } from "primeng/card";
import { DialogModule } from "primeng/dialog";
import { AssigmentService } from "../../../../services/assigment.service";
import { DataStorage } from "../../../../services/dataStorage.service";

@Component({
  selector: "app-new-task",
  standalone: true,
  imports: [CardModule, ButtonModule, DialogModule, ReactiveFormsModule],
  templateUrl: "./new-task.component.html",
  styleUrl: "./new-task.component.css",
})
export class NewTaskComponent {
  constructor(private router: Router) {}

  visible: boolean = false;
  buttonDisabled: boolean = false;
  buttonText: string = "Submit";
  errors: string[] = [];
  registerForm = new FormGroup({
    title: new FormControl(""),
    description: new FormControl(""),
    tags: new FormControl(""),
    priority: new FormControl(""),
    status: new FormControl(""),
    dueDate: new FormControl(""),
  });
  assigmentService = inject(AssigmentService);
  dataStorage = inject(DataStorage);

  showDialog() {
    this.visible = true;
  }

  createNewAssigment() {
    const formValues = {
      title: this.registerForm.value.title ?? "",
      description: this.registerForm.value.description ?? "",
      tags: this.registerForm.value.tags ?? "",
      priority: this.registerForm.value.priority ?? "",
      status: this.registerForm.value.status ?? "",
      dueDate: this.registerForm.value.dueDate ?? "",
      username: this.dataStorage.getData("username") ?? "",
    };
    this.buttonText = "Processing";
    this.buttonDisabled = true;
    this.assigmentService.createAssigment(formValues).then((data) => {
      this.router.navigate(["/admin-task"]);
    });
  }

  registrationErrorHandler(errorText: string) {
    this.buttonText = "Submit";
    this.buttonDisabled = false;
    this.errors.push(errorText);
    this.showDialog();
  }
}
