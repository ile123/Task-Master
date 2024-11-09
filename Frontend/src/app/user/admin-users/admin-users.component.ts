import { Component, inject, OnInit } from '@angular/core';
import { CardModule } from 'primeng/card';
import { PaginatorModule } from 'primeng/paginator';
import { UserService } from '../../../services/user.service';
import User from '../../../types/User';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-users',
  standalone: true,
  imports: [CardModule, PaginatorModule],
  templateUrl: './admin-users.component.html',
  styleUrl: './admin-users.component.css'
})
export class AdminUsersComponent implements OnInit {

  searchKeyword: string = "";
  sortBy: string = "id"
  sortDirection: string = "asc";
  currentPage: number = 0;
  totalRecords: number = 0;
  users: User[] = [];
  loading: boolean = false;

  userService = inject(UserService);

  constructor(private router: Router) {}

  goToEditUser(userId: string) {
    this.router.navigate(['/edit-user', userId]);
  }

  async deleteUser(userId: string) {
    this.loading = true;
    await this.userService.deleteUser(userId);
    let data = await this.userService.getAllUsers(this.searchKeyword, this.sortBy, this.sortDirection, this.currentPage);
    this.users = data.elements;
    this.totalRecords = data.totalElements;
    this.loading = false;
  }

  ngOnInit() {
    this.getUsers();
  }

  async onPageChange(event: any) {
    this.loading = true;
    this.currentPage = event.page;
    let data = await this.userService.getAllUsers(this.searchKeyword, this.sortBy, this.sortDirection, this.currentPage);
    this.users = data.elements;
    this.totalRecords = data.totalElements;
    this.loading = false;
  }

  async getUsers() {
    let data = await this.userService.getAllUsers(this.searchKeyword, this.sortBy, this.sortDirection, this.currentPage);
    this.users = data.elements;
    this.totalRecords = data.totalElements;
  }

  async sortUsers(sortBy: string) {
    this.loading = true;
    this.sortBy = sortBy;
    if(this.sortDirection === "asc") this.sortDirection = "desc";
    else this.sortDirection = "asc";
    let data = await this.userService.getAllUsers(this.searchKeyword, this.sortBy, this.sortDirection, this.currentPage);
    this.users = data.elements;
    this.totalRecords = data.totalElements;
    this.loading = false;
  }
}
