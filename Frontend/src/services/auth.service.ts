import { inject, Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import ResultResponse from '../types/ResultResponse';
import RegistrationValues from '../types/RegistrationValues';
import LoginValues from '../types/LoginValues';
import { DataStorage } from './dataStorage.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  url: string = 'http://localhost:5171/api/Auth';
  dataStorage = inject(DataStorage);

  constructor(private router: Router) {}

  async registerUser(
    values: RegistrationValues
  ): Promise<ResultResponse | undefined> {
    const data = await fetch(`${this.url}/register`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(values),
    });
    return data.json() ?? {};
  }

  async loginUser(values: LoginValues): Promise<ResultResponse | undefined> {
    const data = await fetch(`${this.url}/login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(values),
    });
    return data.json() ?? {};
  }

  async initializeAdmin(): Promise<void> {
    await fetch(`${this.url}/initialize-admin`, {
      method: 'POST',
    });
  }

  isTokenNotExpired(token: string): boolean {
    if (!token) return true;
    const decodedToken = jwtDecode(token) as { exp: number };
    if (!decodedToken || !decodedToken.exp) return true;
    return decodedToken.exp > (Date.now() / 1000);
  }

  async getUserByToken(token: string): Promise<ResultResponse | undefined> {
    const data = await fetch(`${this.url}/get-by-token`, {
      method: 'GET',
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return data.json() ?? {};
  }
}
