using Plugin.Xamarin.Controls.EventArgsFile;
using Plugin.Xamarin.Controls.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plugin.Xamarin.Controls
{
    public class PXC_PopupMenu : BindableObject
    {
        #region events and delegates
        public delegate void PopupShowRequestDelegate(View view);
        public event PopupShowRequestDelegate OnPopupRequest;

        public delegate void ItemSelectedDelegate(object sender, PopupDataChangedEventArgs e);
        public event ItemSelectedDelegate OnItemSelected;
        #endregion

        #region fields
        InternalPopupEffect _internalEffect;

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(PXC_PopupMenu), default(IEnumerable));
        public static BindableProperty PopupCommandProperty = BindableProperty.Create(nameof(PopupCommand), typeof(ICommand),typeof(PXC_PopupMenu), null, BindingMode.OneWay);
        public static BindableProperty BaseContextProperty = BindableProperty.Create(nameof(BaseContext), typeof(object),
             typeof(PXC_Button), null, BindingMode.OneWay);
        #endregion

        #region properties
        public IEnumerable ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public ICommand PopupCommand
        {
            get => (ICommand)GetValue(PopupCommandProperty);
            set => SetValue(PopupCommandProperty, value);
        }
        public object BaseContext
        {
            get => GetValue(BaseContextProperty);
            set => SetValue(BaseContextProperty, value);
        }

        public InternalPopupEffect InternalEffect
        {
            get { return _internalEffect; }
        }
        #endregion

        #region construction
        public PXC_PopupMenu()
        {
            _internalEffect = new InternalPopupEffect(this);
        }
        #endregion

        #region methods
        public void ShowPopup(View sender)
        {
            var effects = sender.Effects.Where(c => c is InternalPopupEffect).ToList();

            // Remove all old popups
            if (effects.Count > 0 && (effects[0] != InternalEffect))
                foreach (var effect in effects)
                    sender.Effects.Remove(effect);

            // Add new popup
            sender.Effects.Add(InternalEffect);

            // Invoke
            OnPopupRequest?.Invoke(sender);
        }

        public void InvokeItemSelected(string item)
        {
            var data = new DataModel { Item = item, Data = BaseContext };
            if (OnItemSelected != null)
                OnItemSelected?.Invoke(this,new PopupDataChangedEventArgs(data));
            if (PopupCommand != null && PopupCommand.CanExecute(data))
                PopupCommand.Execute(data);
        }
        #endregion

        #region classes
        /// <summary>
        /// INTERNAL USE ONLY.
        /// This is used by the Popup Menu as routing effects can not normally be bound, this provides the routing effect whereas the PopupMenu provides the bindable.
        /// </summary>
        public sealed class InternalPopupEffect : RoutingEffect
        {
            public PXC_PopupMenu Parent;
            public InternalPopupEffect(PXC_PopupMenu menu) : base("Plugin.Xamarin.Controls.PXC_PopupEffect")
            {
                Parent = menu;
            }
        }
        #endregion
    }
}