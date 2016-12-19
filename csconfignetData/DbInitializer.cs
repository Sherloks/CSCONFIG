namespace Data
{
    public static class DbInitializer
    {
        
        public static void Initialize(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            //// Look for any settings. 
            //if (!context.Settings.Any())
            //{
            //    // Database has not been seeded yet
            //    var settings = new Setting[]
            //    {
            //        new Setting()
            //        {
            //            GameDirectory = "C:\\Program Files (x86)\\Steam",
            //            PreferedLanguage = Setting.Language.FR,
            //            PreferedTheme = Setting.Theme.Default
            //        },
            //        new Setting()
            //        {
            //            GameDirectory = "C:\\Program Files (x86)\\Steam",
            //            PreferedLanguage = Setting.Language.EN,
            //            PreferedTheme = Setting.Theme.Default
            //        },
            //        new Setting()
            //        {
            //            GameDirectory = "D:\\Steam",
            //            PreferedLanguage = Setting.Language.FR,
            //            PreferedTheme = Setting.Theme.Default
            //        }
            //    };

            //    foreach (Setting setting in settings)
            //    {
            //        context.Settings.Add(setting);
            //    }

            //    context.SaveChanges();
            //}

            //if (!context.Users.Any())
            //{
            //    var users = new User[]
            //    {
            //        new User()
            //        {
            //            UserName= "Sherloks",
            //            DateCreated = DateTime.Parse("2016-09-01"),
            //            Email = "charlesantoinep@outlook.com",
            //            LastLogin = DateTime.Today,
            //            SettingID = 1
            //        },
            //        new User()
            //        {
            //            UserName = "bYox",
            //            DateCreated = DateTime.Parse("2016-09-14"),
            //            Email = "byoxgaming@outlook.com",
            //            LastLogin = DateTime.Today,
            //            SettingID = 2
            //        },
            //        new User()
            //        {
            //            UserName = "Yamashi",
            //            DateCreated = DateTime.Parse("2016-10-04"),
            //            Email = "yamashi@outlook.com",
            //            SettingID = 3
            //        }
            //    };

            //    foreach (User user in users)
            //    {
            //        context.Users.Add(user);
            //    }

            //    context.SaveChanges();

        }
        
    }
}
