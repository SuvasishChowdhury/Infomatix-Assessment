import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'https://localhost:7053/v1';

  constructor(private http: HttpClient) {}

  getProjects(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/projects`);
  }

  getUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/projects/users`);
  }

  getAssignedTickets(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/tickets/assigned`);
  }

  getCounts(): Observable<{ projects: number, tickets: number }> {
    return forkJoin({
      projects: this.getProjects(),
      tickets: this.getAssignedTickets()
    }).pipe(
      map(({ projects, tickets }) => ({
        projects: projects.length,
        tickets: tickets.length
      }))
    );
  }
}
