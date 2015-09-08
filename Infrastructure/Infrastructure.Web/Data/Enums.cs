using System;

namespace Infrastructure.Web.Data
{
    public enum PageLoadingProgress
    {
        Always, Never, RootOnly, NotCachedAlways, NotCachedRootOnly
    }

    public enum ScriptsVersionType
    {
        None, Fixed, Hours, Day, Week, Month, AssemblyVersion, Instance, Unique
    }

    [Flags]
    public enum Engines
    {
        WebFormViewEngine = 0x01, RazorViewEngine = 0x02, PrecompiledViewEngine = 0x04
    }

    public enum Library
    {
        JQuery, KendoUi, RequireJs, Bootstrap
    }
}