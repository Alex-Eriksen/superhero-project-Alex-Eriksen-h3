import { Component, OnInit } from '@angular/core';
import { Guid } from 'guid-typescript';
import { SuperHero, SuperHero_To_SuperHeroRequest, Team } from 'src/app/_models';
import { NotificationService } from 'src/app/_services/notification.service';
import { SuperHeroService } from 'src/app/_services/super-hero.service';
import { TeamService } from 'src/app/_services/team.service';

@Component({
  selector: 'app-super-hero',
  templateUrl: './super-hero.component.html',
  styleUrls: ['./super-hero.component.css']
})
export class SuperHeroComponent implements OnInit
{
	superHeroes: SuperHero[] = [];
	teams: Team[] = [];

	team: Team = { teamID: 0, teamName: '' };
	superHero: SuperHero = { id: 0, name: '', firstName: '', lastName: '', place: '', debut: 0, team: this.team };

  	constructor(private superHeroService: SuperHeroService, private teamService: TeamService, private notificationService: NotificationService) { }

	ngOnInit(): void
	{
		// Data is of type SuperHero[]
		this.superHeroService.getAll().subscribe(data =>
		{
			for (let hero of data)
			{
				if (hero.team == null)
				{
					hero.team = { teamID: 0, teamName: '' };
				}
			}

			this.superHeroes = data;
		});

		// Data is of type Team[]
		this.teamService.getAll().subscribe(data => this.teams = data);
	}

	public edit(superHero: SuperHero): void
	{
		this.superHero = superHero;
	}

	public delete(superHero: SuperHero): void
	{
		if (confirm('Er du sikker på du vil slette?'))
		{
			this.superHeroService.delete(superHero.id).subscribe((hero) =>
			{
				this.superHeroes = this.superHeroes.filter(hero => hero.id != superHero.id);
				this.notificationService.AddNotification(2500, `Slettede ${hero.name.toUpperCase()}!`);
			});
		}
	}

	public cancel(): void
	{
		this.superHero = { id: 0, name: '', firstName: '', lastName: '', place: '', debut: 0, team: this.team };
	}

	public save(): void
	{
		if (this.superHero.id == 0)
		{
			this.superHeroService.create(SuperHero_To_SuperHeroRequest(this.superHero)).subscribe({
				next: (hero) =>
				{
					this.superHeroes.push(hero);
					this.notificationService.AddNotification(2500, `Oprettede ny Superhelt "${hero.name.toUpperCase()}".`);
					this.cancel();
				},
				error: (err) =>
				{
					this.notificationService.AddNotification(5000, Object.values(err.error.errors).join(', '));
				}
			});
		}
		else
		{
			this.superHeroService.update(SuperHero_To_SuperHeroRequest(this.superHero)).subscribe({
				error: (err) =>
				{
					this.notificationService.AddNotification(5000, Object.values(err.error.errors).join(', '));
				},
				complete: () =>
				{
					this.notificationService.AddNotification(2500, `Opdaterede "${this.superHero.name.toUpperCase()}".`);
					this.cancel();
				}
			});
		}
	}
}
