using System.ComponentModel.DataAnnotations;

namespace DTI.Puzzle.WebApp.Models
{
    public class GlossaryUpsertViewModel
    {
        public int? Id { get; set; }
        [MinLength(1)]
        public string Description { get; set; } = string.Empty;
        [MinLength(2)]
        public string Term { get; set; }
    }
}
