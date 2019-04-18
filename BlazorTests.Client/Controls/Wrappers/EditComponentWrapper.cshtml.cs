using System;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorTests.Client.Controls
{
    public class EditComponentWrapperComponent : BlazorComponent
    {
        #region Propriétés bindées

        [Parameter]
        protected RenderFragment ChildContent { get; set; }

        [Parameter]
        protected string Title { get; set; }

        [Parameter]
        private Action CheckAndSaveAction { get; set; }

        [Parameter]
        private Action CloseAction { get; set; }

        #endregion

        #region  Sauvegarde / Fermeture

        protected void CheckAndSave()
        {
            CheckAndSaveAction.Invoke();
        }

        protected void Close()
        {
            CloseAction.Invoke();
        }

        #endregion
    }
}
