namespace DTI.Puzzle.WebApp.Models
{
    public class GlossaryIndexViewModel
    {
        public int CurrentPageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalAvailabeItems { get; set; }
        public string Query { get; set; }
        public bool IsSearchModeEnabled { get; set; }
        public IList<GlossaryViewModel> GlossaryItems { get; set; }
        public IList<int> AvailabePageNumbers { get; set; }
    }
}
