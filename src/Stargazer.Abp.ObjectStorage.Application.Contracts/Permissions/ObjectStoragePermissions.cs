namespace Stargazer.Abp.ObjectStorage.Application.Contracts.Permissions
{
    public class ObjectStoragePermissions
    {
        public const string GroupName = "Stargazer.Abp.ObjectStorage";

        public class ObjectStorage
        {
            public const string Default = GroupName;
            public const string Manage = Default + ".Manage";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string View = Default + ".View";
        }
    }
}