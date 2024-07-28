import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './user/profile/profile.component';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { NotFoundComponent } from './error/not-found/not-found.component';
import { AdminUsersComponent } from './user/admin-users/admin-users.component';
import { AdminTasksComponent } from './task/admin-tasks/admin-tasks.component';
import { EditUserComponent } from './user/admin-users/edit-user/edit-user.component';
import { ChangePasswordComponent } from './user/change-password/change-password.component';

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
        path: 'profile',
        component: ProfileComponent,
        title: 'Task Master'
    },
    {
        path: 'admin-user',
        component: AdminUsersComponent,
        title: 'Task Master'
    },
    {
        path: 'edit-user/:id',
        component: EditUserComponent,
        title: 'Task Master'
    },
    {
        path: 'change-password/:id',
        component: ChangePasswordComponent,
        title: 'Task Master'
    },
    {
        path: 'admin-task',
        component: AdminTasksComponent,
        title: 'Task Master'
    },
    {
        path: '**',
        component: NotFoundComponent,
        title: 'Task Master'
    }
];
