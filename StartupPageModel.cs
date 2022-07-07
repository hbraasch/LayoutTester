using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LayoutTester
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    internal class StartupPageModel
    {


        public bool IsButton1Visible { get; set; }
        public bool IsButton2Visible { get; set; }
        public bool IsButton3Visible { get; set; }
        public bool IsButtonsVisible { get; set; }
        public bool IsButton1OnlyButton { get; set; }
        public bool IsPlusButtonVisible { get; set; }

        public bool IsActivityIndicatorOn { get; set; }

        public class DisplayItem
        {
            public string Description { get;set; }
        }

        public List<DisplayItem> DisplayItems { get; set; } = new();
        public DisplayItem SelectedDisplayItem { get; set; } = null;

        public StartupPageModel()
        {
            IsButton1Visible = true;
            IsButton2Visible = true;
            IsButton3Visible = true;
            IsButtonsVisible = true;
            IsButton1OnlyButton = false;
            IsPlusButtonVisible = true;
            IsActivityIndicatorOn = false;
        }

        public ICommand OnAppearing => new Command(() => {

            var list = new List<DisplayItem>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new DisplayItem { Description = $"Item {i}"});
            }
            DisplayItems = list;

            IsButton1Visible = true;
            IsButton2Visible = true;
            IsButton3Visible = true;
            IsButtonsVisible = false;
            IsButton1OnlyButton = false;
            IsPlusButtonVisible = true;

            SelectedDisplayItem = DisplayItems.Last();
        });

        public ICommand ActivateMenu => new Command(async () => {

            var action = await Application.Current.MainPage.DisplayActionSheet("Menu", "Cancel", null,
                    "MakeButtonsVisible",
                    "MakeButtonsInVisible",
                    "MakeButton1Visible",
                    "MakeButton1InVisible",
                    "MakeButton1SpanAll",
                    "MakeButton1Normal",
                    "MakePlusButtonVisible",
                    "MakePlusButtonInVisible",
                    "ActivityIndicatorOn",
                    "ActivityIndicatorOff",
                    "SelectFirstListItem",
                    "UnSelectFirstListItem"
            );
            if (action == "Cancel") return;
            switch (action)
            {
                case "MakeButtonsVisible":
                    IsButtonsVisible = true;
                    break;
                case "MakeButtonsInVisible":
                    IsButtonsVisible = false;
                    break;
                case "MakeButton1Visible":
                    IsButton1Visible = true;
                    break;
                case "MakeButton1InVisible":
                    IsButton1Visible = false;
                    break;
                case "MakeButton1SpanAll":
                    IsButton1OnlyButton = true;
                    break;
                case "MakeButton1Normal":
                    IsButton1OnlyButton = false;
                    break;
                case "MakePlusButtonVisible":
                    IsPlusButtonVisible = true;
                    break;
                case "MakePlusButtonInVisible":
                    IsPlusButtonVisible = false;
                    break;
                case "ActivityIndicatorOn":
                    IsActivityIndicatorOn = true;
                    break;
                case "ActivityIndicatorOff":
                    IsActivityIndicatorOn = false;
                    break;
                case "SelectFirstListItem":
                    SelectedDisplayItem = DisplayItems.First();
                    break;
                case "UnSelectFirstListItem":
                    SelectedDisplayItem = null;
                    break;
                default:
                    break;
            }

        });

    }
}
