﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware.Camera2;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Plugin.Xamarin.Controls.Droid.Classes;
using Plugin.Xamarin.Controls.Droid.Controls;

namespace Plugin.Xamarin.Controls.Droid.Listner
{
    public class CameraCaptureListener : CameraCaptureSession.CaptureCallback
    {
        private readonly CameraDroidView owner;

        public CameraCaptureListener(CameraDroidView owner)
        {
            if (owner == null)
                throw new System.ArgumentNullException("owner");
            this.owner = owner;
        }

        public override void OnCaptureCompleted(CameraCaptureSession session, CaptureRequest request, TotalCaptureResult result)
        {
            Process(result);
        }

        public override void OnCaptureProgressed(CameraCaptureSession session, CaptureRequest request, CaptureResult partialResult)
        {
            Process(partialResult);
        }

        private void Process(CaptureResult result)
        {
            switch (owner.mState)
            {
                case CameraDroidView.STATE_PREVIEW:
                    {
                        // We have nothing to do when the camera preview is working normally.
                        break;
                    }
                case CameraDroidView.STATE_WAITING_LOCK:
                    {
                        Integer afState = (Integer)result.Get(CaptureResult.ControlAfState);
                        if (afState == null)
                        {
                            owner.mState = CameraDroidView.STATE_PICTURE_TAKEN; // avoids multiple picture callbacks
                            owner.CaptureStillPicture();
                        }

                        else if ((((int)ControlAFState.FocusedLocked) == afState.IntValue()) || (((int)ControlAFState.NotFocusedLocked) == afState.IntValue()) || (((int)ControlAFState.Inactive) == afState.IntValue()))
                        {
                            // ControlAeState can be null on some devices
                            Integer aeState = (Integer)result.Get(CaptureResult.ControlAeState);
                            if (aeState == null ||
                                    aeState.IntValue() == ((int)ControlAEState.Converged))
                            {
                                owner.mState = CameraDroidView.STATE_PICTURE_TAKEN;
                                owner.CaptureStillPicture();
                            }
                            else
                            {
                                owner.RunPrecaptureSequence();
                            }
                        }
                        break;
                    }
                case CameraDroidView.STATE_WAITING_PRECAPTURE:
                    {
                        // ControlAeState can be null on some devices
                        Integer aeState = (Integer)result.Get(CaptureResult.ControlAeState);
                        if (aeState == null ||
                                aeState.IntValue() == ((int)ControlAEState.Precapture) ||
                                aeState.IntValue() == ((int)ControlAEState.FlashRequired))
                        {
                            owner.mState = CameraDroidView.STATE_WAITING_NON_PRECAPTURE;
                        }
                        break;
                    }
                case CameraDroidView.STATE_WAITING_NON_PRECAPTURE:
                    {
                        // ControlAeState can be null on some devices
                        Integer aeState = (Integer)result.Get(CaptureResult.ControlAeState);
                        if (aeState == null || aeState.IntValue() != ((int)ControlAEState.Precapture))
                        {
                            owner.mState = CameraDroidView.STATE_PICTURE_TAKEN;
                            owner.CaptureStillPicture();
                        }
                        break;
                    }
            }
        }
    }
}