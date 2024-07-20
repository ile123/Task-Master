import { Component, DoCheck, OnInit, Signal, inject } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { MenubarModule } from 'primeng/menubar';
import { MenuItem } from 'primeng/api';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { DataStorage } from '../services/dataStorage.service';
import { take } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MenubarModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'Task Master';
  dataChanged: Subscription;

  dataStorage = inject(DataStorage);

  constructor(private router: Router, private cookies: CookieService) {
    this.dataChanged = this.dataStorage.dataChanged$().pipe(
      take(1)
    ).subscribe(() => {
      switch(this.dataStorage.getData("role")) {
        case "Member":
          this.items = this.navigationItemsByRole.member;
          break;
        case "Admin":
          this.items = this.navigationItemsByRole.admin;
          break;
        default:
          this.items = this.navigationItemsByRole.notLoggedIn;
          break;
      }
    });
  }

  items: MenuItem[] = [];
  navigationItemsByRole: any = {
    notLoggedIn: [
      {
        label: 'Home',
        icon: 'pi pi-fw pi-home',
        routerLink: '/',
      },
      {
        label: 'Login',
        icon: 'pi pi-fw pi-unlock',
        routerLink: '/login',
      },
      {
        label: 'Register',
        icon: 'pi pi-fw pi-prime',
        routerLink: '/register',
      },
    ],
    member: [
      {
        label: 'Home',
        icon: 'pi pi-fw pi-home',
        routerLink: '/',
      },
      {
        label: 'Tasks',
        icon: 'pi pi-fw pi-prime',
        routerLink: '/',
      },
      {
        label: 'Profile',
        icon: 'pi pi-fw pi-id-card',
        routerLink: '/',
      },
      {
        label: 'Logout',
        icon: 'pi pi-fw pi-power-off',
        command: () => this.logoutUser(),
        routerLink: '/',
      }
    ],
    admin: [
      {
        label: 'Home',
        icon: 'pi pi-fw pi-home',
        routerLink: '/',
      },
      {
        label: 'Users',
        icon: 'pi pi-fw pi-user',
        routerLink: '/admin-user',
      },
      {
        label: 'Tasks',
        icon: 'pi pi-fw pi-book',
        routerLink: '/admin-task',
      },
      {
        label: 'Tags',
        icon: 'pi pi-fw pi-tag',
        routerLink: '/admin-tag',
      },
      {
        label: 'Profile',
        icon: 'pi pi-fw pi-id-card',
        routerLink: '/profile',
      },
      {
        label: 'Logout',
        icon: 'pi pi-fw pi-power-off',
        command: () => this.logoutUser(),
        routerLink: '/',
      },
    ],
  };

  ngOnInit() {
    this.items = this.navigationItemsByRole.notLoggedIn;
  }

  logoutUser() {
    localStorage.removeItem("token");
    this.cookies.delete("token");
    this.dataStorage.clearData("userId");
    this.dataStorage.clearData("username");
    this.dataStorage.clearData("role");
    this.router.navigate(['/']);
    location.reload();
  }
}
