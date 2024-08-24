import { Component, OnInit, inject } from "@angular/core";
import { FormControl, FormGroup, ReactiveFormsModule } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { ButtonModule } from "primeng/button";
import { CardModule } from "primeng/card";
import { DialogModule } from "primeng/dialog";
import UpdateUser from "../../../../types/UpdateUser";
import { AssigmentService } from "../../../../services/assigment.service";

@Component({
  selector: "app-edit-task",
  standalone: true,
  imports: [CardModule, ButtonModule, DialogModule, ReactiveFormsModule],
  templateUrl: "./edit-task.component.html",
  styleUrl: "./edit-task.component.css",
})
export class EditTaskComponent implements OnInit {
  registerForm = new FormGroup({
    title: new FormControl(""),
    description: new FormControl(""),
    tags: new FormControl(""),
    priority: new FormControl(""),
    status: new FormControl(""),
    dueDate: new FormControl(""),
  });
  assigmentId: string = "";
  visible: boolean = false;
  buttonDisabled: boolean = false;
  buttonText: string = "Submit";
  loading: boolean = true;
  errors: string[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
  ) {}

  assigmentService = inject(AssigmentService);

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id: string = params.get("id") ?? "";
      this.assigmentId = id;
    });
    this.assigmentService
      .getAssigmentById(this.assigmentId)
      .then((data) => {
        this.assigmentId = data?.id;
        this.registerForm.patchValue({
          title: data?.title,
          description: data?.description,
          tags: data?.tags,
          priority: data?.priority,
          status: data?.status,
          dueDate: data?.dueDate,
        });
        this.loading = false;
      })
      .catch((error) => {
        console.error("Error fetching assigment data:", error);
      });
  }

  async updateAssigment() {
    const formValues = {
      id: this.assigmentId ?? "",
      title: this.registerForm.value.title ?? "",
      description: this.registerForm.value.description ?? "",
      tags: this.registerForm.value.tags ?? "",
      priority: this.registerForm.value.priority ?? "",
      status: this.registerForm.value.status ?? "",
      dueDate: this.registerForm.value.dueDate ?? "",
    };
    this.buttonText = "Processing";
    this.buttonDisabled = true;
    await this.assigmentService
      .updateAssigment(formValues)
      .then(() => {
        this.router.navigate(["/admin-task"]);
      })
      .catch((error) => console.log(error));
  }

  showDialog() {
    this.visible = true;
  }

  registrationErrorHandler(errorText: string) {
    this.buttonText = "Submit";
    this.buttonDisabled = false;
    this.errors.push(errorText);
    this.showDialog();
  }
}
