import { Component, OnInit } from '@angular/core';
import { SuperHeroRequest, TeamRequest, TeamResponse, TeamResponse_To_TeamRequest } from 'src/app/_models';
import { NotificationService } from 'src/app/_services/notification.service';
import { TeamService } from 'src/app/_services/team.service';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css']
})
export class TeamsComponent implements OnInit
{
	teams: TeamResponse[] = [];
	superHeroesToUpdate: SuperHeroRequest[] = [];

	team: TeamResponse = { teamID: 0, teamName: '', superHeroes: [] };
	creating: boolean = false;
	newTeamName: string = '';
  	constructor(private teamService: TeamService, private notificationService: NotificationService) { }

	ngOnInit(): void
	{
		// Data is of type TeamResponse[]
		this.teamService.getAll().subscribe(data => this.teams = data);
	}

	public create(): void
	{
		this.creating = true;
	}

	public createNew(): void
	{
		if (this.newTeamName === '') { return; }

		this.teamService.create({ teamName: this.newTeamName, superHeroes: [] }).subscribe({
			next: (newTeam) =>
			{
				this.teams.push(newTeam);
				this.notificationService.AddNotification(2500, `Oprettede det nye hold "${newTeam.teamName.toUpperCase()}".`);
				this.cancel();
			},
			error: (err) =>
			{
				this.notificationService.AddNotification(2500, Object.values(err.error.errors).join(', '));
			}
		})
	}

	public cancel(): void
	{
		this.team = { teamID: 0, teamName: '', superHeroes: [] };
		this.creating = false;
	}

	public edit( team: TeamResponse ): void
	{
		this.superHeroesToUpdate = [];
		this.team = team;
		for (let superHero of team.superHeroes)
		{
			this.superHeroesToUpdate.push({
				id: superHero.id,
				name: superHero.name,
				firstName: superHero.firstName,
				lastName: superHero.lastName,
				place: superHero.place,
				debut: superHero.debut,
				teamID: team.teamID
			} as SuperHeroRequest);
		}
	}

	public save(): void
	{
		let teamRequest: TeamRequest = TeamResponse_To_TeamRequest(this.team);
		teamRequest.superHeroes = this.superHeroesToUpdate;

		this.teamService.update(this.team.teamID, teamRequest).subscribe(data1 =>
		{
			this.teamService.getAll().subscribe(data => this.teams = data);
			this.notificationService.AddNotification(2500, `Opdaterede holdet "${data1.teamName.toUpperCase()}".`);
		});
		this.cancel();
	}

	public delete( team: TeamResponse ): void
	{
		this.teams = this.teams.filter(e => e.teamID != team.teamID);
		this.teamService.delete(team.teamID, this.teams[ this.teams.length - 1 ].teamID).subscribe({
			next: (t) =>
			{
				this.notificationService.AddNotification(2500, `Slettede holdet "${t.teamName.toUpperCase()}".`);
			},
			error: (err) =>
			{
				this.notificationService.AddNotification(5000, Object.values(err.error.errors).join(', '));
			}
		});
	}
}
