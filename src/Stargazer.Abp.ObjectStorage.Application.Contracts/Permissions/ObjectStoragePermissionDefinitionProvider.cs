// using Volo.Abp.Authorization.Permissions;
//
// namespace Stargazer.Abp.ObjectStorage.Application.Contracts.Permissions
// {
//     public class ObjectStoragePermissionDefinitionProvider: PermissionDefinitionProvider
//     {
//         public override void Define(IPermissionDefinitionContext context)
//         {
//             var moduleGroup = context.AddGroup(ObjectStoragePermissions.GroupName, "对象存储");
//
//             var objectStorage =
//                 moduleGroup.AddPermission(ObjectStoragePermissions.ObjectStorage.Default, L("Permission:Category"));
//             objectStorage.AddChild(ObjectStoragePermissions.ObjectStorage.Manage, L("Permission:Manage"));
//             objectStorage.AddChild(ObjectStoragePermissions.ObjectStorage.Create, L("Permission:Create"));
//             objectStorage.AddChild(ObjectStoragePermissions.ObjectStorage.Update, L("Permission:Update"));
//             objectStorage.AddChild(ObjectStoragePermissions.ObjectStorage.Delete, L("Permission:Delete"));
//             objectStorage.AddChild(ObjectStoragePermissions.ObjectStorage.ShowHidden, L("Permission:ShowHidden"));
//         }
//     }
// }