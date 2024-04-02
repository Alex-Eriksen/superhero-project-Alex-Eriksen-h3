import { Component, OnInit } from '@angular/core';
import { SuperHero } from '../_models';
import { SuperHeroService } from '../_services/super-hero.service';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit
{
	superHeroes: SuperHero[] = [];

  	constructor(private superHeroService: SuperHeroService) { }

	ngOnInit(): void
	{
		// Data is of type SuperHero[]
		this.superHeroService.getAll().subscribe(data => this.superHeroes = data);
  	}
}
