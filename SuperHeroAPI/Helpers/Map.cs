namespace SuperHeroAPI.Helpers
{
    public static class Map
    {
        public static SuperHero SuperHeroRequestToSuperHero( SuperHeroRequest superHero )
        {
            return new SuperHero
            {
                Name = superHero.Name,
                FirstName = superHero.FirstName,
                LastName = superHero.LastName,
                TeamID = superHero.TeamID,
                Place = superHero.Place,
                Debut = superHero.Debut
            };
        }

        public static SuperHeroResponse SuperHeroToSuperHeroResponse( SuperHero superHero )
        {
            return new SuperHeroResponse
            {
                Id = superHero.SuperHeroID,
                Name = superHero.Name,
                FirstName = superHero.FirstName,
                LastName = superHero.LastName,
                Team = TeamToSuperHeroTeamResponse( superHero.Team ),
                Place = superHero.Place,
                Debut = superHero.Debut
            };
        }

        public static SuperHeroTeamResponse TeamToSuperHeroTeamResponse( Team team )
        {
            if(team == null)
            {
                return null;
            }

            return new SuperHeroTeamResponse
            {
                TeamID = team.TeamID,
                TeamName = team.TeamName
            };
        }

        public static TeamSuperHeroResponse SuperHeroToTeamSuperHeroResponse( SuperHero superHero )
        {
            if( superHero == null)
            {
                return null;
            }

            return new TeamSuperHeroResponse
            {
                Id = superHero.SuperHeroID,
                Name = superHero.Name,
                FirstName = superHero.FirstName,
                LastName = superHero.LastName,
                Place = superHero.Place,
                Debut = superHero.Debut
            };
        }

        public static TeamResponse TeamToTeamResponse( Team team )
        {
            return new TeamResponse
            {
                TeamID = team.TeamID,
                TeamName = team.TeamName,
                SuperHeroes = team.SuperHeroes?.Select( hero => SuperHeroToTeamSuperHeroResponse( hero ) ).ToList()
            };
        }

        public static Team TeamRequestToTeam( TeamRequest team )
        {
            return new Team
            {
                TeamName = team.TeamName,
                SuperHeroes = team.SuperHeroes.Select( hero => TeamSuperHeroRequestToSuperHero( hero ) ).ToList()
            };
        }

        public static SuperHero TeamSuperHeroRequestToSuperHero( TeamSuperHeroRequest superHero )
        {
            return new SuperHero
            {
                SuperHeroID = superHero.Id,
                Name = superHero.Name,
                FirstName = superHero.FirstName,
                LastName = superHero.LastName,
                Place = superHero.Place,
                Debut = superHero.Debut,
                TeamID = superHero.TeamID
            };
        }
    }
}
