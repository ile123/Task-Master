import { Component, OnInit, inject } from "@angular/core";
import { ButtonModule } from "primeng/button";
import { CardModule } from "primeng/card";
import Assigment from "../../../types/Assigment";
import { ActivatedRoute, Router } from "@angular/router";
import { AssigmentService } from "../../../services/assigment.service";

@Component({
  selector: "app-tasks",
  standalone: true,
  imports: [CardModule, ButtonModule],
  templateUrl: "./tasks.component.html",
  styleUrl: "./tasks.component.css",
})
export class TasksComponent implements OnInit {
  assigments: Assigment[] = [];
  searchKeyword: string = "";
  sortBy: string = "id";
  sortDirection: string = "asc";
  currentPage: number = 0;
  totalRecords: number = 0;

  assigmentService = inject(AssigmentService);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
  ) {}

  async ngOnInit() {
    let data = await this.assigmentService.getAllAssigments(
      this.searchKeyword,
      this.sortBy,
      this.sortDirection,
      this.currentPage,
    );
    this.assigments = data.elements;
    this.totalRecords = data.totalElements;
  }

  async getAssigments() {}

  viewAssigmentDetails(assigmentId: string) {
    this.router.navigate(["/view-assigment", assigmentId]);
  }
}
