using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTests.Client.Controls
{
    public class SelectListItem<T>
    {
        public T Id { get; set; }
        public string Text { get; set; }
    }

    public class SelectListItem : SelectListItem<int>
    {
    }

    public class SelectListItems
    {
        public static IList<SelectListItem> Convert<T>(IList<T> data, Action<T, SelectListItem> transformation, bool addEmptyLine = true)
        {
            IList<SelectListItem> selectListItems = new List<SelectListItem>();

            if (addEmptyLine)
            {
                selectListItems.Add(new SelectListItem { Id = (int)Constants.Null, Text = string.Empty });
                //   selectListItems.Add(new SelectListItem { Id = default(int), Text = string.Empty });
            }

            foreach (T datum in data)
            {
                SelectListItem item = new SelectListItem();
                transformation.Invoke(datum, item);
                selectListItems.Add(item);
            }

            return selectListItems;
        }
    }
}
