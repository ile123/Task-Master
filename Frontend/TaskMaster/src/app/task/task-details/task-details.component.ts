import { Component, OnInit, inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { CardModule } from "primeng/card";
import { AssigmentService } from "../../../services/assigment.service";
import { ButtonModule } from "primeng/button";

@Component({
  selector: "app-task-details",
  standalone: true,
  imports: [CardModule, ButtonModule],
  templateUrl: "./task-details.component.html",
  styleUrl: "./task-details.component.css",
})
export class TaskDetailsComponent implements OnInit {
  assigmentId: string = "";
  loading: boolean = true;

  assigment: object = {};

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
        this.assigment = data;
        this.loading = false;
      })
      .catch((error) => {
        console.error("Error fetching assigment data:", error);
      });
  }

  changeAssigmentStatus() {
    this.assigmentService.changeAssigmentStatus(this.assigmentId);
    this.router.navigate(["/user-assigments"]);
  }
}
