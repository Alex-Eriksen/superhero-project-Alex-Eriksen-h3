import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SuperHero, SuperHeroRequest } from '../_models';

@Injectable({
  providedIn: 'root'
})
export class SuperHeroService
{
	private apiUrl: string = environment.apiUrl + 'SuperHero';

	constructor(private http: HttpClient) { }

	public getAll(): Observable<SuperHero[]>
	{
		return this.http.get<SuperHero[]>(this.apiUrl);
	}

	public getById( superHeroId: number ): Observable<SuperHero>
	{
		return this.http.get<SuperHero>(`${this.apiUrl}/${superHeroId}`);
	}

	public create( superHeroRequest: SuperHeroRequest ): Observable<SuperHero>
	{
		return this.http.post<SuperHero>(this.apiUrl, superHeroRequest);
	}

	public update( superHero: SuperHeroRequest ): Observable<SuperHero>
	{
		return this.http.put<SuperHero>(`${this.apiUrl}/${superHero.id}`, superHero);
	}

	public delete( superHeroId: number ): Observable<SuperHero>
	{
		return this.http.delete<SuperHero>(`${this.apiUrl}/${superHeroId}`);
	}
}
