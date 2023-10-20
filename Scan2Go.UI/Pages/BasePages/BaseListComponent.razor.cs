using Scan2Go.UI.BaseClasses;

namespace Scan2Go.UI.Pages.BasePages
{
    public abstract partial class BaseListComponent
    {
        public PassingObject EntityListPassingObject { get; set; }
        public abstract string PageTitle { get; set; }
        public abstract Task HandleAddEntityClick();
        public abstract Task OpenEntityEditPopup(int entityId);
        public abstract void HandleEntitySaved();
    }
}
