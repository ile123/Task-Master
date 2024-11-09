import { inject, Injectable } from '@angular/core';
import { DataStorage } from './dataStorage.service';
import { CookieService } from 'ngx-cookie-service';
import User from '../types/User';
import ChangePassword from '../types/ChangePassword';
import UpdateUser from '../types/UpdateUser';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  url: string = 'http://localhost:5171/api/User';

  constructor(private cookies: CookieService) { }

  async getAllUsers(keyword: string, sortBy: string, sortDirection: string, currentPage: number) {
    const response = await fetch(`${this.url}?keyword=${keyword}&sortBy=${sortBy}&sortDirection=${sortDirection}&pageNumber=${currentPage}&pageSize=10`, {
      method: 'GET',
      headers: {
        Authorization: `Bearer ${this.cookies.get("token")}`,
      },
    });
    const data = await response.json();
    return data.result ?? [];
  }

  async getUserById(userId: string) {
    const response = await fetch(`${this.url}/info/${userId}`, {
      method: 'GET',
      headers: {
        Authorization: `Bearer ${this.cookies.get("token")}`,
      },
    });
    const data = await response.json();
    return data.result ?? {};
  }

  async updateUser(userInfo: UpdateUser) {
    await fetch(`${this.url}/update-user/${userInfo.id}`, {
      method: 'PUT',
      body: JSON.stringify({
        Username: userInfo.username,
        Fullname: userInfo.fullName,
        PhoneNumber: userInfo.phoneNumber,
        ProfileUrl: userInfo.profileUrl
      }),
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${this.cookies.get("token")}`,
      },
    });
  }

  async changeUserPassword(userInfo: ChangePassword) {
    await fetch(`${this.url}/change-password/${userInfo.userId}`, {
      method: 'PATCH',
      body: JSON.stringify({
        NewPassword: userInfo.password
      }),
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${this.cookies.get("token")}`,
      },
    });
  }

  async deleteUser(userId: string) {
    await fetch(`${this.url}/${userId}`, {
      method: 'DELETE',
      headers: {
        Authorization: `Bearer ${this.cookies.get("token")}`,
      },
    });
  }
}
