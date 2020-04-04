using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.MultySelectable;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls.ViewCells
{
    [ContentProperty(nameof(DataView))]
    public class SelectableViewCell : ViewCell
    {
        private Grid rootGrid;
        private View dataView;
        private View checkView;

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(SelectableViewCell));

        public static readonly BindableProperty ViewTypeProperty =
            BindableProperty.Create(nameof(TypesView), typeof(TypesOfView), typeof(SelectableViewCell), TypesOfView.None);

        public static readonly BindableProperty TextToChangeProperty =
        BindableProperty.Create(nameof(TextToChange), typeof(string), typeof(SelectableViewCell), string.Empty);

        public static readonly BindableProperty BackColorToChangeProperty =
            BindableProperty.Create(nameof(BackColorToChange), typeof(Color), typeof(SelectableViewCell), Color.Transparent);

        public static readonly BindableProperty ColorToChangeProperty =
            BindableProperty.Create(nameof(ColorToChange), typeof(Color), typeof(SelectableViewCell), Color.Transparent);

        public static readonly BindableProperty IconProperty =
       BindableProperty.Create(nameof(Icon), typeof(string), typeof(SelectableViewCell), string.Empty);
        public static readonly BindableProperty FontIconNameProperty =
       BindableProperty.Create(nameof(FontIconName), typeof(Fonts), typeof(PXC_Button), Fonts.Material);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public Fonts FontIconName
        {
            get { return (Fonts)GetValue(FontIconNameProperty); }
            set { SetValue(FontIconNameProperty, value); }
        }
        public TypesOfView TypesView
        {
            get { return (TypesOfView)GetValue(ViewTypeProperty); }
            set { SetValue(ViewTypeProperty, value); }
        }

        public string TextToChange
        {
            get { return (string)GetValue(TextToChangeProperty); }
            set { SetValue(TextToChangeProperty, value); }
        }
        public Color BackColorToChange
        {
            get { return (Color)GetValue(BackColorToChangeProperty); }
            set { SetValue(BackColorToChangeProperty, value); }
        }
        public Color ColorToChange
        {
            get { return (Color)GetValue(ColorToChangeProperty); }
            set { SetValue(ColorToChangeProperty, value); }
        }

        public Color ColorOriginal { get; set; }
        public SelectableViewCell()
        {
            rootGrid = new Grid
            {
                Padding = new Thickness(10, 7),
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Auto }
                }
            };

            View = rootGrid;

            var btn = new BoxView
            {
                BackgroundColor = Color.LightSkyBlue,
                WidthRequest = 12,
                HeightRequest = 12,
            };
            CheckView = btn;

            var text = new Label();
            text.SetBinding(Label.TextProperty, "Insert your view hier");
            DataView = text;
        }

        public View CheckView
        {
            get { return checkView; }
            set
            {
                if (checkView == value || View != rootGrid)
                    return;

                OnPropertyChanging();

                if (checkView != null)
                {
                    if (TypesView == TypesOfView.Button)
                        checkView.RemoveBinding(Button.CommandParameterProperty);
                    else if (TypesView == TypesOfView.CheckBox)
                        checkView.RemoveBinding(TapGestureRecognizer.CommandParameterProperty);
                    else if (TypesView == TypesOfView.Switch)
                        checkView.RemoveBinding(TapGestureRecognizer.CommandParameterProperty);

                    rootGrid.Children.Remove(checkView);
                }

                checkView = value;

                if (checkView != null)
                {
                    if (TypesView == TypesOfView.Button)
                    {
                        checkView.SetBinding(Button.CommandParameterProperty, nameof(SelectableItem.Data));
                        var btn = (PXC_Button)checkView;
                        btn.Clicked += Text_Clicked;
                    }
                    else if (TypesView == TypesOfView.CheckBox)
                    {
                        var tgr = new TapGestureRecognizer();
                        tgr.SetBinding(TapGestureRecognizer.CommandParameterProperty, nameof(SelectableItem.Data));
                        var checkedbox = (CheckBox)checkView;
                        ColorOriginal = checkedbox.Color;
                        checkedbox.GestureRecognizers.Add(tgr);
                        checkedbox.CheckedChanged += Checkedbox_CheckedChanged;
                    }
                    else if (TypesView == TypesOfView.Switch)
                    {
                        var tgr = new TapGestureRecognizer();
                        tgr.SetBinding(TapGestureRecognizer.CommandParameterProperty, nameof(SelectableItem.Data));
                        var switchs = (Switch)checkView;
                        ColorOriginal = switchs.ThumbColor;
                        switchs.GestureRecognizers.Add(tgr);
                        switchs.Toggled += Switchs_Toggled;
                    }

                    Grid.SetColumn(checkView, 1);
                    Grid.SetColumnSpan(checkView, 1);
                    Grid.SetRow(checkView, 0);
                    Grid.SetRowSpan(checkView, 1);
                    checkView.HorizontalOptions = LayoutOptions.End;
                    checkView.VerticalOptions = LayoutOptions.Center;
                    rootGrid.Children.Add(checkView);
                }
                OnPropertyChanged();
            }
        }

        private void Checkedbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var radiobtn = (CheckBox)sender;
            if (ColorToChange != null)
                radiobtn.Color = e.Value ? ColorToChange : ColorOriginal;

            if (radiobtn.GestureRecognizers.Count > 0)
            {
                var gesture = (TapGestureRecognizer)radiobtn.GestureRecognizers[0];
                if (Command != null && Command.CanExecute(gesture.CommandParameter))
                {
                    Command.Execute(gesture.CommandParameter);
                }
            }
        }

        private void Switchs_Toggled(object sender, ToggledEventArgs e)
        {
            var switchs = (Switch)sender;
            if (ColorToChange != null)
                switchs.ThumbColor = e.Value ? ColorToChange : ColorOriginal;
            if (switchs.GestureRecognizers.Count > 0)
            {
                var gesture = (TapGestureRecognizer)switchs.GestureRecognizers[0];
                if (Command != null && Command.CanExecute(gesture.CommandParameter))
                {
                    Command.Execute(gesture.CommandParameter);
                }
            }
        }

        private void Text_Clicked(object sender, EventArgs e)
        {
            var btn = (PXC_Button)sender;
            if (!string.IsNullOrEmpty(TextToChange))
            {
                if (!string.IsNullOrEmpty(btn.Text))
                    btn.Text = TextToChange;
            }

            if (!string.IsNullOrEmpty(Icon))
                btn.Icon = Icon;
            if (FontIconName != Fonts.None)
                btn.FontIconName = FontIconName;
            if (btn.BorderColor != null)
                btn.BorderColor = ColorToChange;
            if (ColorToChange != null)
                btn.TextColor = ColorToChange;
            if (BackColorToChange != null)
                btn.BackgroundColor = BackColorToChange;

            btn.IsEnabled = false;

            if (Command != null && Command.CanExecute(btn.CommandParameter))
            {
                Command.Execute(btn.CommandParameter);
            }
        }

        public View DataView
        {
            get { return dataView; }
            set
            {
                if (dataView == value || View != rootGrid)
                    return;

                OnPropertyChanging();

                if (dataView != null)
                {
                    dataView.RemoveBinding(BindingContextProperty);
                    rootGrid.Children.Remove(dataView);
                }

                dataView = value;

                if (dataView != null)
                {
                    dataView.SetBinding(BindingContextProperty, nameof(SelectableItem.Data));
                    Grid.SetColumn(dataView, 0);
                    Grid.SetColumnSpan(dataView, 1);
                    Grid.SetRow(dataView, 0);
                    Grid.SetRowSpan(dataView, 1);
                    dataView.HorizontalOptions = LayoutOptions.StartAndExpand;
                    dataView.VerticalOptions = LayoutOptions.FillAndExpand;
                    rootGrid.Children.Add(dataView);
                }

                OnPropertyChanged();
            }
        }
    }
}