﻿using Simple.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Simple
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlaxPage : ContentPage
    {
        public FlaxPage()
        {
            InitializeComponent();
            BindingContext = new MultiSelectViewModel();
        }
    }
}