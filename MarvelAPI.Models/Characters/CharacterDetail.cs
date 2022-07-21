namespace MarvelAPI.Models.Characters
{
    public class CharacterDetail
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Age { get; set; }

        public string Location { get; set; }
        public string Origin { get; set; }
        public string Abilities { get; set; }
        public string AbilitiesOrigin { get; set; }
        // add list of movie list objects
        // use display models not data
    }
}