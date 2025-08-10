import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter, Routes } from '@angular/router';
import { ProjectUserListComponent } from './app/features/project-user-list/project-user-list.component';
import { UserTicketTableComponent } from './app/features/user-ticket-table/user-ticket-table.component';
import { StatsCardComponent } from './app/features/stats-card/stats-card.component';

  const routes : Routes = [
    { path: '', component:StatsCardComponent},
    { path: 'project-user-list', component: ProjectUserListComponent},
    { path: 'user-ticket-table', component: UserTicketTableComponent}
  ]
  bootstrapApplication(AppComponent, {
    providers: [
      provideHttpClient(),
      provideRouter(routes)
    ]
  });