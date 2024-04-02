import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Team, TeamRequest, TeamResponse } from '../_models';

@Injectable({
  providedIn: 'root'
})
export class TeamService
{
	private apiUrl: string = environment.apiUrl + 'Team';

	constructor(private http: HttpClient) { }

	public getAll(): Observable<TeamResponse[]>
	{
		return this.http.get<TeamResponse[]>(this.apiUrl);
	}

	public getById( teamId: number ): Observable<Team>
	{
		return this.http.get<Team>(`${this.apiUrl}/${teamId}`);
	}

	public create( teamRequest: TeamRequest ): Observable<TeamResponse>
	{
		return this.http.post<TeamResponse>(this.apiUrl, teamRequest);
	}

	public update( teamId: number, teamRequest: TeamRequest ): Observable<Team>
	{
		return this.http.put<Team>(`${this.apiUrl}/${teamId}`, teamRequest);
	}

	public delete( teamId: number, newTeamId: number ): Observable<Team>
	{
		return this.http.delete<Team>(`${this.apiUrl}/${teamId}/${newTeamId}`);
	}
}
