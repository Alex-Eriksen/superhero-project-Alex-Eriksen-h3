namespace SuperHeroAPI.DTOs
{
    public class TeamResponse
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public List<TeamSuperHeroResponse> SuperHeroes { get; set; } = new();
    }

    public class TeamSuperHeroResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public short Debut { get; set; } = 0;
    }
}
