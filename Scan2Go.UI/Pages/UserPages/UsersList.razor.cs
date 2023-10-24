using Microsoft.AspNetCore.Components;
using Scan2Go.Mapper.Models.UserModels;
using Scan2Go.UI.BaseClasses;
using Scan2Go.UI.Pages.BasePages;

namespace Scan2Go.UI.Pages.UserPages
{
    public partial class UsersList : BaseListComponent
    {
        public int _clickedUserId;
        private EditUserPopup editUserPopup;
        public override string PageTitle { get; set; }
        [Parameter] public int SetHashCode { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }

        protected override async Task OnParametersSetAsync()
        {
            EntityListPassingObject = StateContainer.GetRoutingObjectParameter<PassingObject>(SetHashCode);
            PageTitle = EntityListPassingObject.EntityName + " List";
        }

        #region BaseListComponent Members

        public override async Task HandleAddEntityClick()
        {
            await editUserPopup.SetUser(new UsersModel());
        }

        public override void HandleEntitySaved()
        {
            StateHasChanged();
        }

        public override async Task OpenEntityEditPopup(int userId)
        {
            _clickedUserId = userId;
            await editUserPopup.SetUserId(_clickedUserId);
        }

        #endregion BaseListComponent Members
    }
}