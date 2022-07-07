using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayoutTester
{
    internal class StartupPage : ContentPage
    {
        StartupPageModel viewModel;
        public StartupPage(StartupPageModel viewModel)
        {
            this.viewModel = viewModel;
            BindingContext = viewModel;

            var button1 = new Button { Text = "Button1", Margin = 5 };
            button1.SetBinding(Button.IsVisibleProperty, new Binding(nameof(StartupPageModel.IsButton1Visible), BindingMode.OneWay));

            var button2 = new Button { Text = "Button2", Margin = 5 };
            button2.SetBinding(Button.IsVisibleProperty, new Binding(nameof(StartupPageModel.IsButton2Visible), BindingMode.OneWay));

            var button3 = new Button { Text = "Button3", Margin = 5 };
            button3.SetBinding(Button.IsVisibleProperty, new Binding(nameof(StartupPageModel.IsButton3Visible), BindingMode.OneWay));

            var buttonsGrid = new Grid();
            buttonsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            buttonsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            buttonsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            buttonsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            
            buttonsGrid.Add(button2, 1, 0);
            buttonsGrid.Add(button3, 2, 0);
            buttonsGrid.Add(button1, 0, 0);
            button1.SetBinding(Grid.ColumnSpanProperty, new Binding(nameof(StartupPageModel.IsButton1OnlyButton), BindingMode.OneWay, converter: new SpanConverter()));
            buttonsGrid.SetBinding(Grid.IsVisibleProperty, new Binding(nameof(StartupPageModel.IsButtonsVisible), BindingMode.OneWay));

            var listView = new CollectionView
            {
                SelectionMode = SelectionMode.Single
            };
            listView.ItemTemplate = new DataTemplate(() =>
            {

                var label = new Label();
                label.SetBinding(Label.TextProperty, new Binding(nameof(StartupPageModel.DisplayItem.Description)));

                var cellGrid = new Grid();
                cellGrid.Add(label);

                #region *// Code that implements change of selection color
                var vsg = new VisualStateGroup() { Name = "CommonStates" };
                var vsNormal = new VisualState { Name = "Normal" };
                var vsSelected = new VisualState { Name = "Selected" };
                var vsFocused = new VisualState { Name = "Focused" };

                vsNormal.Setters.Add(new Setter
                {
                    Property = Grid.BackgroundColorProperty,
                    Value = Colors.Transparent
                });

                vsSelected.Setters.Add(new Setter
                {
                    Property = Grid.BackgroundColorProperty,
                    Value = Colors.LightBlue
                });

                vsFocused.Setters.Add(new Setter
                {
                    Property = Grid.BackgroundColorProperty,
                    Value = Colors.LightBlue
                });

                vsg.States.Add(vsNormal);
                vsg.States.Add(vsSelected);
                vsg.States.Add(vsFocused);

                VisualStateManager.GetVisualStateGroups(cellGrid).Add(vsg);
                #endregion 

                return cellGrid;

            });
            listView.SetBinding(CollectionView.ItemsSourceProperty, new Binding(nameof(StartupPageModel.DisplayItems)));
            listView.SetBinding(CollectionView.SelectedItemProperty, new Binding(nameof(StartupPageModel.SelectedDisplayItem)));

            var mainGrid = new Grid();
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            mainGrid.Add(buttonsGrid, 0, 0);
            mainGrid.Add(listView, 0, 1);

            var backgroundImage = new Image { Source = "ic_background.png", Aspect = Aspect.AspectFill };

            var plusButton = new ImageButton { Source = "ic_plus.png", BackgroundColor = Colors.Transparent };
            plusButton.SetBinding(IsVisibleProperty, new Binding(nameof(StartupPageModel.IsPlusButtonVisible), BindingMode.OneWay));

            var activityIndicator = new ActivityIndicator();
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, new Binding(nameof(StartupPageModel.IsActivityIndicatorOn), BindingMode.OneWay));

            var centeredLayout = new AbsoluteLayout
            {
                Children = { backgroundImage, mainGrid, activityIndicator, plusButton  },
            };
            AbsoluteLayout.SetLayoutFlags(backgroundImage, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(backgroundImage, new Rect(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(mainGrid, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(mainGrid, new Rect(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(activityIndicator, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(activityIndicator, new Rect(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(plusButton, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(plusButton, new Rect(0.95, 0.95, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            Content = centeredLayout;

            ToolbarItems.Add(new ToolbarItem("", "ic_hamburger.png", () => { viewModel.ActivateMenu.Execute(null); }, ToolbarItemOrder.Primary));

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing.Execute(null);
        }

        internal class SpanConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return ((bool)value) ? 3 : 1;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
