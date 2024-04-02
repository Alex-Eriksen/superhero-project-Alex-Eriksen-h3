import { SuperHeroRequest, SuperHeroResponses_To_SuperHeroRequests, SuperHero_To_SuperHeroRequest } from "./superHero";

export interface Team
{
	teamID: number;
	teamName: string;
}

export interface TeamResponse
{
	teamID: number;
	teamName: string;
	superHeroes: TeamSuperHeroResponse[];
}

export interface TeamRequest
{
	teamName: string;
	superHeroes: SuperHeroRequest[];
}

export interface TeamSuperHeroResponse
{
	id: number;
	name: string;
	firstName: string;
	lastName: string;
	place: string;
	debut: number;
}

export function TeamResponse_To_Team( teamResponse: TeamResponse )
{
	return {
		teamID: teamResponse.teamID,
		teamName: teamResponse.teamName
	} as Team;
}

export function TeamResponse_To_TeamRequest(teamResponse: TeamResponse)
{
	return {
		teamName: teamResponse.teamName
	} as TeamRequest;
}
