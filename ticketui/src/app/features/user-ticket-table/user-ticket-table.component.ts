import { Component, OnInit, OnDestroy, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';

import $ from 'jquery';
import 'datatables.net';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-user-ticket-table',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './user-ticket-table.component.html',
  styleUrls: ['./user-ticket-table.component.scss']
})
export class UserTicketTableComponent implements OnInit, OnDestroy {
  tickets = signal<any[]>([]);
  private dataTable: any;

  constructor(private api: ApiService) {}

  ngOnInit() {
    this.api.getUsersAssignedTickets().subscribe(data => {
      this.tickets.set(data);
      this.loadDataTable();
    });
  }

  loadDataTable() {
    setTimeout(() => {
      if (this.dataTable) {
        this.dataTable.destroy(); // destroy old instance if reloaded
      }
      this.dataTable = ($('#ticketTable') as any).DataTable({
        paging: true,
        searching: true,
        ordering: true
      });
    }, 0);
  }

  ngOnDestroy() {
    if (this.dataTable) {
      this.dataTable.destroy(true);
    }
  }
}
