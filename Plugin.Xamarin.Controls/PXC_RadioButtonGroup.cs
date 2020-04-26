﻿using Plugin.Xamarin.Controls.EnumFiles;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_RadioButtonGroup : StackLayout
    { /// <summary>
      /// The items
      /// </summary>
        public ObservableCollection<PXC_RadioButton> Items;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableRadioGroup"/> class.
        /// </summary>
        public PXC_RadioButtonGroup()
        {
            Items = new ObservableCollection<PXC_RadioButton>();
        }

        /// <summary>
        /// The items source property
        /// </summary>
        public static BindableProperty ItemsSourceProperty =
                    BindableProperty.Create(nameof(ItemsSource),typeof(IEnumerable),typeof(PXC_RadioButtonGroup), default(IEnumerable), propertyChanged: OnItemsSourceChanged);

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var radButtons = bindable as PXC_RadioButtonGroup;


            foreach (var item in radButtons.Items)
            {
                item.CheckedChanged -= radButtons.OnCheckedChanged;
            }

            radButtons.Children.Clear();

            var radIndex = 0;

            foreach (var item in radButtons.ItemsSource)
            {
                var button = new PXC_RadioButton
                {
                    Text = item.ToString(),
                    Id = radIndex++,
                    TextColor = radButtons.TextColor,
                    CheckedColor=radButtons.CheckedColor,
                    UnCheckedColor=radButtons.UnCheckedColor,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, radButtons),
                    FontName = radButtons.FontName,

                };

                button.CheckedChanged += radButtons.OnCheckedChanged;

                radButtons.Items.Add(button);

                radButtons.Children.Add(button);
            }
        }

        /// <summary>
        /// The selected index property
        /// </summary>
        public static BindableProperty SelectedIndexProperty =
            BindableProperty.Create(nameof(SelectedIndex),typeof(int),typeof(PXC_RadioButtonGroup), -1, BindingMode.TwoWay,propertyChanged: OnSelectedIndexChanged);

        private static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((int)newValue == -1)
            {
                return;
            }

            var bindableRadioGroup = bindable as PXC_RadioButtonGroup;

            if (bindableRadioGroup == null)
            {
                return;
            }

            foreach (var button in bindableRadioGroup.Items.Where(button => button.Id == bindableRadioGroup.SelectedIndex))
            {
                button.Checked = true;
            }
        }


        /// <summary>
        /// The text color property
        /// </summary>
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor),typeof(Color),typeof(PXC_RadioButtonGroup), Color.Black);

        /// <summary>
        /// The font size property
        /// </summary>
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize),typeof(double),typeof(PXC_RadioButtonGroup));

        /// <summary>
        /// The font name property.
        /// </summary>
        public static readonly BindableProperty FontNameProperty =
           BindableProperty.Create(nameof(FontName),typeof(FontType),typeof(PXC_RadioButtonGroup), FontType.OpenSans_Regular);

        /// <summary>
        ///     The default color checked property.
        /// </summary>
        public static readonly BindableProperty CheckedColorProperty = BindableProperty.Create(nameof(CheckedColor), 
            typeof(Color), typeof(PXC_RadioButton), Color.Default);

        /// <summary>
        ///     The default color unchecked property.
        /// </summary>
        public static readonly BindableProperty UnCheckedColorProperty = BindableProperty.Create(nameof(TextColor),
            typeof(Color), typeof(PXC_RadioButton), Color.Default);

        public Color CheckedColor
        {
            get { return (Color)GetValue(CheckedColorProperty); }

            set { SetValue(CheckedColorProperty, value); }
        }

        public Color UnCheckedColor
        {
            get { return (Color)GetValue(UnCheckedColorProperty); }

            set { SetValue(UnCheckedColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }


        /// <summary>
        /// Gets or sets the index of the selected.
        /// </summary>
        /// <value>The index of the selected.</value>
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        /// <value>The size of the font.</value>
        public double FontSize
        {
            get
            {
                return (double)GetValue(FontSizeProperty);
            }
            set
            {
                SetValue(FontSizeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the font.
        /// </summary>
        /// <value>The name of the font.</value>
        public FontType FontName
        {
            get
            {
                return (FontType)GetValue(FontNameProperty);
            }
            set
            {
                SetValue(FontNameProperty, value);
            }
        }

        /// <summary>
        /// Occurs when [checked changed].
        /// </summary>
        public event EventHandler<int> CheckedChanged;

        private void OnCheckedChanged(object sender, bool e)
        {
            if (e == false)
            {
                return;
            }

            var selectedItem = sender as PXC_RadioButton;

            if (selectedItem == null)
            {
                return;
            }

            foreach (var item in Items)
            {
                if (!selectedItem.Id.Equals(item.Id))
                {
                    item.Checked = false;
                }
                else
                {
                    SelectedIndex = selectedItem.Id;
                    if (CheckedChanged != null)
                    {
                        CheckedChanged.Invoke(sender, item.Id);
                    }
                }
            }
        }
    }
}
