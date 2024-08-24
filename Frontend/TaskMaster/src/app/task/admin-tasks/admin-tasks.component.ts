import { Component, inject } from "@angular/core";
import { CardModule } from "primeng/card";
import { PaginatorModule } from "primeng/paginator";
import Assigment from "../../../types/Assigment";
import { AssigmentService } from "../../../services/assigment.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-admin-tasks",
  standalone: true,
  imports: [CardModule, PaginatorModule],
  templateUrl: "./admin-tasks.component.html",
  styleUrl: "./admin-tasks.component.css",
})
export class AdminTasksComponent {
  searchKeyword: string = "";
  sortBy: string = "id";
  sortDirection: string = "asc";
  currentPage: number = 0;
  totalRecords: number = 0;
  assigments: Assigment[] = [];
  loading: boolean = false;

  assigmentService = inject(AssigmentService);

  constructor(private router: Router) {}

  goToEditAssigment(assigmentId: string) {
    this.router.navigate(["/edit-assigment", assigmentId]);
  }

  async deleteAssigment(assigmentId: string) {
    this.loading = true;
    await this.assigmentService.deleteAssigment(assigmentId);
    let data = await this.assigmentService.getAllAssigments(
      this.searchKeyword,
      this.sortBy,
      this.sortDirection,
      this.currentPage,
    );
    this.assigments = data.elements;
    this.totalRecords = data.totalElements;
    this.loading = false;
  }

  ngOnInit() {
    this.getAssigments();
  }

  async onPageChange(event: any) {
    this.loading = true;
    this.currentPage = event.page;
    let data = await this.assigmentService.getAllAssigments(
      this.searchKeyword,
      this.sortBy,
      this.sortDirection,
      this.currentPage,
    );
    this.assigments = data.elements;
    this.totalRecords = data.totalElements;
    this.loading = false;
  }

  async getAssigments() {
    let data = await this.assigmentService.getAllAssigments(
      this.searchKeyword,
      this.sortBy,
      this.sortDirection,
      this.currentPage,
    );
    this.assigments = data.elements;
    this.totalRecords = data.totalElements;
  }

  async sortAssigments(sortBy: string) {
    this.loading = true;
    this.sortBy = sortBy;
    if (this.sortDirection === "asc") this.sortDirection = "desc";
    else this.sortDirection = "asc";
    let data = await this.assigmentService.getAllAssigments(
      this.searchKeyword,
      this.sortBy,
      this.sortDirection,
      this.currentPage,
    );
    this.assigments = data.elements;
    this.totalRecords = data.totalElements;
    this.loading = false;
  }

  addNewAssigment() {
    this.router.navigate(["/new-task"]);
  }
}
