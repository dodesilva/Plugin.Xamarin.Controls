﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Plugin.Xamarin.Controls.IOS.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using static Plugin.Xamarin.Controls.PXC_PopupMenu;

[assembly: ResolutionGroupName("Plugin.Xamarin.Controls")]
[assembly: ExportEffect(typeof(PXC_PopupEffect), "PXC_PopupEffect")]
namespace Plugin.Xamarin.Controls.IOS.Controls
{
    public class PXC_PopupEffect: PlatformEffect
    {
        public static string UIAlertControllerCancelText { get; set; } = "Cancel";
        public static UIPopoverArrowDirection PermittedArrowDirections = UIPopoverArrowDirection.Up;

        InternalPopupEffect Effect;
        UIViewController RootViewController;

        public static void Init()
        {
            var now = DateTime.Now;
        }

        protected override void OnAttached()
        {
            Effect = (InternalPopupEffect)Element.Effects.FirstOrDefault(e => e is InternalPopupEffect);

            if (Effect != null)
                Effect.Parent.OnPopupRequest += OnPopupRequest;
        }

        void OnPopupRequest(View view)
        {
            if (Effect.Parent.ItemsSource == null)
                return;

            RootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            // Create a new Alert Controller
            UIAlertController actionSheetAlert = UIAlertController.Create(null, null, UIAlertControllerStyle.ActionSheet);

            // Add Actions
            foreach (var item in Effect.Parent.ItemsSource)
                actionSheetAlert.AddAction(UIAlertAction.Create(item.ToString(), UIAlertActionStyle.Default, (action) => Effect.Parent.InvokeItemSelected(item.ToString())));

            actionSheetAlert.AddAction(UIAlertAction.Create(UIAlertControllerCancelText, UIAlertActionStyle.Destructive, null));

            // Required for iPad - You must specify a source for the Action Sheet since it is
            // displayed as a popover
            if (Device.Idiom != TargetIdiom.Phone)
            {
                UIPopoverPresentationController presentationPopover = actionSheetAlert.PopoverPresentationController;
                if (presentationPopover != null)
                {
                    if (Control != null)
                    {
                        presentationPopover.SourceRect = Control.Frame;
                        presentationPopover.SourceView = Control;
                    }

                    else if (Container != null)
                    {
                        presentationPopover.SourceRect = Container.Frame;
                        presentationPopover.SourceView = Container;
                    }

                    else
                    {
                        presentationPopover.SourceRect = RootViewController.View.Frame;
                        presentationPopover.SourceView = RootViewController.View;
                    }

                    presentationPopover.PermittedArrowDirections = PermittedArrowDirections;
                }
            }

            // Present
            RootViewController.PresentViewController(actionSheetAlert, true, null);
        }

        protected override void OnDetached()
        {
            if (Effect != null)
                Effect.Parent.OnPopupRequest -= OnPopupRequest;
        }
    }
}