using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Xamarin.Controls.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Plugin.Xamarin.Controls.PXC_PopupMenu;

[assembly: ResolutionGroupName("Plugin.Xamarin.Controls")]
[assembly: ExportEffect(typeof(PXC_PopupEffect), "PXC_PopupEffect")]
namespace Plugin.Xamarin.Controls.Droid.Controls
{
    public class PXC_PopupEffect: PlatformEffect
    {
        PopupMenu ToggleMenu;
        InternalPopupEffect Effect;
        private Context mContext;

        protected override void OnAttached()
        {
            Effect = (InternalPopupEffect)Element.Effects.FirstOrDefault(e => e is InternalPopupEffect);
            mContext = Android.App.Application.Context;
            if (Effect != null)
                Effect.Parent.OnPopupRequest += OnPopupRequest;
        }

        private void OnPopupRequest(global::Xamarin.Forms.View view)
        {
            if (Effect.Parent.ItemsSource == null)
                return;

            ToggleMenu = new PopupMenu(mContext, Control);
            ToggleMenu.MenuItemClick += MenuItemClick;
            //// Clear Old
            ToggleMenu.Menu.Clear();
          
            //// Add New
            foreach (var item in Effect.Parent.ItemsSource)
            {

                ToggleMenu.Menu.Add(item.ToString());
            }

            ToggleMenu.Show();
        }

        protected override void OnDetached()
        {
            if (ToggleMenu != null)
                ToggleMenu.MenuItemClick -= MenuItemClick;

            if (Effect != null)
                Effect.Parent.OnPopupRequest -= OnPopupRequest;
        }

        void MenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            Effect?.Parent.InvokeItemSelected(e.Item.ToString());
        }
    }
}