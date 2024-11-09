import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AssigmentService {

  url: string = 'http://localhost:5171/api/Assigment';

  constructor(private cookies: CookieService) { }

  async getAllAssigments(keyword: string, sortBy: string, sortDirection: string, currentPage: number) {
    const response = await fetch(`${this.url}?keyword=${keyword}&sortBy=${sortBy}&sortDirection=${sortDirection}&pageNumber=${currentPage}&pageSize=10`, {
      method: 'GET',
      headers: {
        Authorization: `Bearer ${this.cookies.get("token")}`,
      },
    });
    const data = await response.json();
    return data.result ?? [];
  }

  async getAllUserAssigments(username: string, keyword: string, sortBy: string, sortDirection: string, currentPage: number) {
    const response = await fetch(`${this.url}/assignments-by-user/${username}?keyword=${keyword}&sortBy=${sortBy}&sortDirection=${sortDirection}&pageNumber=${currentPage}&pageSize=10`, {
      method: 'GET',
      headers: {
        Authorization: `Bearer ${this.cookies.get("token")}`,
      },
    });
    const data = await response.json();
    return data.result ?? [];
  }

  async getAssigmentById(assigmentId: string) {
    const response = await fetch(`${this.url}/${assigmentId}`, {
      method: 'GET',
      headers: {
        Authorization: `Bearer ${this.cookies.get("token")}`,
      },
    });
    const data = await response.json();
    return data.result ?? {};
  }

  async createAssigment(assigmentDto: any) {
    await fetch(`${this.url}`, {
      method: 'POST',
      body: JSON.stringify({
        title: assigmentDto.title,
        description: assigmentDto.description,
        tags: assigmentDto.tags,
        priority: assigmentDto.priority,
        status: assigmentDto.status,
        dueDate: assigmentDto.dueDate,
        username: assigmentDto.username
      }),
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${this.cookies.get("token")}`,
      },
    });
  }

  async updateAssigment(assigmentDto: any) {
    await fetch(`${this.url}/${assigmentDto.id}`, {
      method: 'PUT',
      body: JSON.stringify({
        title: assigmentDto.title,
        description: assigmentDto.description,
        tags: assigmentDto.tags,
        priority: assigmentDto.priority,
        status: assigmentDto.status,
        dueDate: assigmentDto.dueDate,
        username: assigmentDto.username
      }),
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${this.cookies.get("token")}`,
      },
    });
  }

  async changeAssigmentStatus(assigmentId: string) {
    await fetch(`${this.url}/change-assigment-status/${assigmentId}`, {
      method: 'POST',
      headers: {
        Authorization: `Bearer ${this.cookies.get("token")}`,
      },
    });
  }

  async deleteAssigment(assigmentId: string) {
    await fetch(`${this.url}/${assigmentId}`, {
      method: 'DELETE',
      headers: {
        Authorization: `Bearer ${this.cookies.get("token")}`,
      },
    });
  }
}

//new Date().toISOString().split('T')[0] -> this is hte date format I will be using