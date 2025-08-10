import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ProjectUserListComponent } from './features/project-user-list/project-user-list.component';
import { StatsCardComponent } from './features/stats-card/stats-card.component';
import { UserTicketTableComponent } from './features/user-ticket-table/user-ticket-table.component';
import { HttpClientModule } from '@angular/common/http';
import { SidebarComponent } from './features/sidebar/sidebar.component';

@Component({
  selector: 'app-root',
  imports: [
    HttpClientModule,
    SidebarComponent,
    StatsCardComponent,
    RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ticketui';
}

