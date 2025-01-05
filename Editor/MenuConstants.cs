namespace DGGLocalization.Editor
{
    public static class MenuConstants
    {
        #region Constants

        public const string Root = "Localization";
        
        public const string Settings = Root + "/Settings";
        public const string Statistics = Root + "/Statistics";
        
        public const string Import = Root + "/Import";
        public const string Export = Root + "/Export";

        public const int SettingsPriority = -10;
        
        public const int ImportPriority = 10;
        public const int ExportPriority = 10;

        #endregion
    }
}