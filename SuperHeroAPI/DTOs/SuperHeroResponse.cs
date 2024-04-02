namespace SuperHeroAPI.DTOs
{
    public class SuperHeroResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public short Debut { get; set; } = 0;
        public SuperHeroTeamResponse Team { get; set; }
    }

    public class SuperHeroTeamResponse
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; } = string.Empty;
    }
}
