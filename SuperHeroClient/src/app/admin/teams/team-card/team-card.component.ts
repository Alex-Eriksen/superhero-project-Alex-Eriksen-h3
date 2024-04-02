import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TeamResponse, TeamSuperHeroResponse } from 'src/app/_models';

@Component({
  selector: 'team-card',
  templateUrl: './team-card.component.html',
  styleUrls: ['./team-card.component.css']
})
export class TeamCardComponent implements OnInit
{
	@Input() team: TeamResponse = { teamID: 0, teamName: '', superHeroes: [] };
	@Output() editEmitter: EventEmitter<TeamResponse> = new EventEmitter();
	@Output() deleteEmitter: EventEmitter<TeamResponse> = new EventEmitter();
	@Output() moveHeroEmitter: EventEmitter<TeamSuperHeroResponse> = new EventEmitter();

  	constructor() { }

  	ngOnInit(): void { }

	public edit(): void
	{
		this.editEmitter.emit(this.team);
	}

	public delete(): void
	{
		if (confirm(`Er du sikker på du vil slette "${this.team.teamName.toUpperCase()}" holdet?\nOBS: Dette vil også slette alle Superhelte som er medlem af holdet!`))
		{
			this.deleteEmitter.emit(this.team);
		}
	}

	public moveHero( superHero: TeamSuperHeroResponse ): void
	{
		this.moveHeroEmitter.emit(superHero);
	}

	public addHero(): void
	{

	}

	public removeHero( superHero: TeamSuperHeroResponse ): void
	{
		if (confirm(`Er du sikker på du vil fjerne "${superHero.name.toUpperCase()}" fra holdet "${this.team.teamName.toUpperCase()}"`))
		{

		}
	}
}
