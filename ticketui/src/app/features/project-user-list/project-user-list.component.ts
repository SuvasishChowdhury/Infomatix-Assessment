import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-project-user-list',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './project-user-list.component.html',
  styleUrls: ['./project-user-list.component.scss']
})
export class ProjectUserListComponent implements OnInit {
  projectsUserList = signal<any[]>([]);
  loading = signal(true);
  error = signal('');
  private dataTable: any;

  constructor(private api: ApiService) {}

 ngOnInit(): void {
    this.api.getUsers().subscribe({
      next: data => {
        this.projectsUserList.set(data);
        this.loading.set(false);
        this.loadDataTable();
      },
      error: err => {
        this.error.set('Failed to load projects');
        this.loading.set(false);
      }
    });
  }

  loadDataTable(){
    setTimeout(() => {
      if(this.dataTable){
        this.dataTable.destroy();
      }
      this.dataTable = ($('#projectUsersTable') as any).DataTable({
        paging: true,
        searching: true,
        ordering: false
      })
    }, 0);
  }

  ngOnDestroy(){
    if(this.dataTable){
      this.dataTable.destroy(true)
    }
  }
}
