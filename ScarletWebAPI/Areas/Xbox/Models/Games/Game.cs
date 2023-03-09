namespace ScarletWebAPI.Areas.Xbox.Models.Games
{
    public class Game
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
        public string DisplayImage { get; set; }
        public int MaxGameScore { get; set; }
        public int CurrentGameScore { get; set; }
    }
}