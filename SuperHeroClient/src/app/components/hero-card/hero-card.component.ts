import { Component, Input, OnInit } from '@angular/core';
import { SuperHero, Team } from 'src/app/_models';
import { SuperHeroService } from 'src/app/_services/super-hero.service';
import { TeamService } from 'src/app/_services/team.service';

@Component({
  selector: 'hero-card',
  templateUrl: './hero-card.component.html',
  styleUrls: ['./hero-card.component.css']
})
export class HeroCardComponent implements OnInit
{
	@Input() superHero: SuperHero = { id: 0, name: '', firstName: '', lastName: '', place: '', debut: 0, team: { teamID: 0, teamName: '' } };
	constructor(private superHeroService: SuperHeroService, private teamService: TeamService) { }

	ngOnInit(): void
	{
		// Data is of type SuperHero
		this.superHeroService.getById(this.superHero.id).subscribe(data => this.superHero = data );
  	}

}
