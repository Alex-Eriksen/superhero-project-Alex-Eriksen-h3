import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { SuperHeroComponent } from './admin/super-hero/super-hero.component';
import { HeaderComponent } from './components/header/header.component';
import { HeroCardComponent } from './components/hero-card/hero-card.component';
import { TeamsComponent } from './admin/teams/teams.component';
import { TeamCardComponent } from './admin/teams/team-card/team-card.component';
import { NotificationHandlerComponent } from './components/notification-handler/notification-handler.component';
import { NotificationComponent } from './components/notification-handler/notification/notification.component';

@NgModule({
  	declarations: [
    	AppComponent,
    	FrontpageComponent,
     	SuperHeroComponent,
    	HeaderComponent,
     	HeroCardComponent,
     	TeamsComponent,
     	TeamCardComponent,
     	NotificationHandlerComponent,
      NotificationComponent
  	],
  	imports: [
    	BrowserModule,
	  	AppRoutingModule,
		HttpClientModule,
		FormsModule
  	],
  	providers: [],
  	bootstrap: [AppComponent]
})
export class AppModule { }
