import { Component, OnInit, DoCheck, inject } from '@angular/core';
import { CardModule } from 'primeng/card';
import { AuthService } from '../../services/auth.service';
import { DataStorage } from '../../services/dataStorage.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CardModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit, DoCheck{

  constructor(private cookieService: CookieService) {}

  welcomeText = "You are not logged in.";
  token = '';
  authService = inject(AuthService);
  dataStorage = inject(DataStorage);

  ngOnInit(): void {
    this.authService.initializeAdmin();
    this.token = this.cookieService.get("token") ?? '';
    if(this.token !== '' && this.authService.isTokenNotExpired(this.token)) {
      this.authService.getUserByToken(this.token)
      .then((data) => {
        this.dataStorage.addData("userId", data?.result.userId);
        this.dataStorage.addData("username", data?.result.username);
        this.dataStorage.addData("role", data?.result.role);
        this.welcomeText = "Welcome " + data?.result.username;
      })
    }
  }

  ngDoCheck(): void {
    if(!this.authService.isTokenNotExpired(this.token)) {
      this.cookieService.delete("token");
      window.location.reload();
    }
  }
}
