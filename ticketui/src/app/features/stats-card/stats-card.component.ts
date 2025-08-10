import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-stats-card',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './stats-card.component.html',
  styleUrls: ['./stats-card.component.scss']
})
export class StatsCardComponent implements OnInit {
  totalProjects = signal<number>(0);
  totalTickets = signal<number>(0);

  constructor(private api: ApiService) {}

  ngOnInit() {
    this.api.getProjects().subscribe(data => this.totalProjects.set(data.length));
    this.api.getAssignedTickets().subscribe(data => this.totalTickets.set(data.length));
  }
}

