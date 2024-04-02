export interface SuperHero
{
	id: number;
	name: string;
	firstName: string;
	lastName: string;
	place: string;
	debut: number;
	team: SuperHeroTeamResponse;
}

export interface SuperHeroResponse
{
	id: number;
	name: string;
	firstName: string;
	lastName: string;
	place: string;
	debut: number;
	team: SuperHeroTeamResponse;
}

export interface SuperHeroTeamResponse
{
	teamID: number;
	teamName: string;
}

export interface SuperHeroRequest
{
	id: number;
	name: string;
	firstName: string;
	lastName: string;
	place: string;
	debut: number;
	teamID: number;
}

export function SuperHero_To_SuperHeroRequest( superHero: SuperHero ): SuperHeroRequest
{
	return {
		id: superHero.id,
		name: superHero.name,
		firstName: superHero.firstName,
		lastName: superHero.lastName,
		place: superHero.place,
		debut: superHero.debut,
		teamID: superHero.team.teamID
	} as SuperHeroRequest;
}

export function SuperHeroResponse_To_SuperHeroRequest(superHeroResponse: SuperHeroResponse): SuperHeroRequest
{
	return {
		id: superHeroResponse.id,
		name: superHeroResponse.name,
		firstName: superHeroResponse.firstName,
		lastName: superHeroResponse.lastName,
		place: superHeroResponse.place,
		debut: superHeroResponse.debut,
		teamID: superHeroResponse.team.teamID
	} as SuperHeroRequest;
}

export function SuperHeroResponses_To_SuperHeroRequests(superHeroResponses: SuperHeroResponse[]): SuperHeroRequest[]
{
	let output: SuperHeroRequest[] = [];
	for (let superHeroResponse of superHeroResponses)
	{
		output.push(SuperHeroResponse_To_SuperHeroRequest(superHeroResponse));
	}
	return output;
}
