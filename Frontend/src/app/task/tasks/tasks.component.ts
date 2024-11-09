import { Component, OnInit, inject } from "@angular/core";
import { ButtonModule } from "primeng/button";
import { CardModule } from "primeng/card";
import Assigment from "../../../types/Assigment";
import { ActivatedRoute, Router } from "@angular/router";
import { AssigmentService } from "../../../services/assigment.service";
import { PaginatorModule } from "primeng/paginator";
import { DataStorage } from "../../../services/dataStorage.service";

@Component({
  selector: "app-tasks",
  standalone: true,
  imports: [CardModule, ButtonModule, PaginatorModule],
  templateUrl: "./tasks.component.html",
  styleUrl: "./tasks.component.css",
})
export class TasksComponent implements OnInit {
  searchKeyword: string = "";
  sortBy: string = "id";
  sortDirection: string = "asc";
  currentPage: number = 0;
  totalRecords: number = 0;
  assigments: Assigment[] = [];
  loading: boolean = false;

  assigmentService = inject(AssigmentService);
  dataStorage = inject(DataStorage);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
  ) {}

  async ngOnInit() {
    this.getUserAssigments();
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

  async getUserAssigments() {
    let data = await this.assigmentService.getAllUserAssigments(this.dataStorage.getData("username"),
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

  viewAssigmentDetails(assigmentId: string) {
    this.router.navigate(["/view-assigment", assigmentId]);
  }
}
