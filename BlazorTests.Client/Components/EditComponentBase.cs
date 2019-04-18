using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BlazorTests.Client.Controls;
using BlazorTests.Shared;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;
using Microsoft.AspNetCore.Blazor.Services;

namespace BlazorTests.Client
{
    public abstract class EditComponentBase : ComponentBase
    {
        [Parameter]
        private string IdAsString { get; set; }

        protected int? Id
        {
            get
            {
                if (string.IsNullOrWhiteSpace(IdAsString))
                {
                    return null;
                }

                return int.Parse(IdAsString);
            }
        }

        protected bool IsNew()
        {
            if (Id.HasValue == false)
            {
                return true;
            }

            return false;
        }

        protected async Task CheckAndSave()
        {
            IList<string> errors = CheckRequiredFields();
            if (errors.Count > 0)
            {
                await JavascriptFunctions.ShowAlert(string.Join("\r\n", errors));
            }
            else
            {
                await Save();
            }

            return;
        }

        protected abstract Task Save();
        protected abstract void Close();

        #region V�rification des champs obligatoires

        protected IList<string> CheckRequiredFields()
        {
            IList<string> errors = new List<string>();

            PropertyInfo[] infos = this.GetType().GetProperties();
            if (infos != null)
            {
                foreach (var info in infos)
                {
                    var requiredAttribute = info.GetCustomAttribute(typeof(RequiredExAttribute), true) as RequiredExAttribute;
                    if (requiredAttribute != null)
                    {
                        object o = info.GetValue(this);
                        if (CheckRequiredField(o) == false)
                        {
                            errors.Add($"Le champ {requiredAttribute.Label} est obligatoire");
                        }
                    }
                }
            }

            return errors;
        }

        private bool CheckRequiredField(object value)
        {
            if (value == null)
            {
                return false;
            }
            else
            {
                // Dans le cas d'une cha�ne, on v�rifie aussi qu'elle n'est pas vide
                if (value is string && string.IsNullOrWhiteSpace((string)value))
                {
                    return false;
                }

                // Dans le cas d'une liste, on v�rifie aussi qu'elle contient un �l�ment
                if (value is IList && ((IList)value).Count == 0)
                {
                    return false;
                }
            }

            return true;
        }

        protected IList<string> GetPropertyInfos()
        {
            IList<string> propertyNames = new List<string>();

            PropertyInfo[] infos = this.GetType().GetProperties(BindingFlags.Instance);
            if (infos != null)
            {
                foreach (PropertyInfo info in infos)
                {
                    propertyNames.Add(info.Name);
                }
            }

            return propertyNames;
        }

        #endregion
    }

    public class RequiredExAttribute : Attribute
    {
        public string Label { get; set; }

        public RequiredExAttribute(string label)
        {
            this.Label = label;
        }
    }
}