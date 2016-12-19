namespace Data.Models
{
    public class Setting
    {
        public enum Language
        {
            FR = 0,
            EN = 1
        }

        public enum Theme
        {
            Default = 0
        }

        public int ID { get; set; }
        public string GameDirectory { get; set; }
        public Language PreferedLanguage { get; set; }
        public Theme PreferedTheme { get; set; }

        //public User User { get; set; }
    }
}
