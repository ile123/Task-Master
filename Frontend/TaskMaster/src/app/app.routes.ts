import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { NotFoundComponent } from './not-found/not-found.component';

export const routes: Routes = [
    {
        path: '',
        component: HomeComponent,
        title: 'Task Master'
    },
    {
        path: 'profile/:id',
        component: ProfileComponent,
        title: 'Task Master'
    },
    {
        path: 'login',
        component: LoginComponent,
        title: 'Task Master'
    },
    {
        path: 'register',
        component: RegisterComponent,
        title: 'Task Master'
    },
    {
        path: '**',
        component: NotFoundComponent,
        title: 'Task Master'
    }
];
