import { Component } from '@angular/core';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-admin-tasks',
  standalone: true,
  imports: [CardModule],
  templateUrl: './admin-tasks.component.html',
  styleUrl: './admin-tasks.component.css'
})
export class AdminTasksComponent {

}
