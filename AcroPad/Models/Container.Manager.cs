namespace AcroPad.Models
{
    public static class ContainerManager
    {
        public static void Load()
        {
            SettingContainer.Load();
            LanguageContainer.Load();
            HistoryContainer.Load();
            LayoutContainer.Load();
        }

        public static void Save()
        {
            SettingContainer.Instance.Save();
            LanguageContainer.Instance.Save();
            HistoryContainer.Instance.Save();
        }
    }
}
