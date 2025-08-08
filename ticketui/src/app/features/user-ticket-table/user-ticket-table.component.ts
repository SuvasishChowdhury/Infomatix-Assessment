import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-user-ticket-table',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-ticket-table.component.html',
  styleUrls: ['./user-ticket-table.component.scss']
})
export class UserTicketTableComponent implements OnInit {
  tickets = signal<any[]>([]);

  constructor(private api: ApiService) {}

  ngOnInit() {
    this.api.getAssignedTickets().subscribe(data => this.tickets.set(data));
  }
}
