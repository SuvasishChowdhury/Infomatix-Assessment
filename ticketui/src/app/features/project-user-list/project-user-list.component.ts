import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-project-user-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './project-user-list.component.html',
  styleUrls: ['./project-user-list.component.scss']
})
export class ProjectUserListComponent implements OnInit {
  projectsUserList = signal<any[]>([]);
  loading = signal(true);
  error = signal('');

  constructor(private api: ApiService) {}

 ngOnInit(): void {
    this.api.getUsers().subscribe({
      next: data => {
        this.projectsUserList.set(data);
        this.loading.set(false);
      },
      error: err => {
        this.error.set('Failed to load projects');
        this.loading.set(false);
      }
    });
  }
}
