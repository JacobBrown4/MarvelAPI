using MarvelAPI.Models.Characters;

namespace MarvelAPI.Models.Teams
{
    public class TeamDetail
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Authority { get; set; }

        public string Alignment { get; set; }

        public IEnumerable<CharacterListItem> Members { get; set; }
    }
}