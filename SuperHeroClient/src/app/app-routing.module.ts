import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SuperHeroComponent } from './admin/super-hero/super-hero.component';
import { TeamsComponent } from './admin/teams/teams.component';
import { FrontpageComponent } from './frontpage/frontpage.component';

const routes: Routes = [
	{ path: 'home', component: FrontpageComponent },
	{ path: 'admin/super-hero', component: SuperHeroComponent },
	{ path: 'admin/teams', component: TeamsComponent },

	{ path: '**', redirectTo: 'home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
