using proggame.BackEnd.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace proggame.BackEnd.Permissions;

public class BackEndPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BackEndPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(BackEndPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BackEndResource>(name);
    }
}
