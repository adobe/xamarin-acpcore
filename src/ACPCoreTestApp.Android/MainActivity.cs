/*
 Copyright 2020 Adobe. All rights reserved.
 This file is licensed to you under the Apache License, Version 2.0 (the "License");
 you may not use this file except in compliance with the License. You may obtain a copy
 of the License at http://www.apache.org/licenses/LICENSE-2.0
 Unless required by applicable law or agreed to in writing, software distributed under
 the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR REPRESENTATIONS
 OF ANY KIND, either express or implied. See the License for the specific language
 governing permissions and limitations under the License.
*/

using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Adobe.Marketing.Mobile;
using Xamarin.Forms;

namespace ACPCoreTestApp.Droid
{
    class CoreStartCompletionCallback : Java.Lang.Object, IAdobeCallback
    {
        public void Call(Java.Lang.Object callback)
        {
            // set launch config
            ACPCore.ConfigureWithAppID("launch-ENf8ed5382efc84d5b81a9be8dcc231be1-development");
        }
    }

    [Activity(Label = "ACPCoreTestApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            // set the wrapper type
            ACPCore.SetWrapperType(WrapperType.Xamarin);

            // set log level
            ACPCore.LogLevel = LoggingMode.Verbose;

            // register SDK extensions
            ACPCore.Application = this.Application;
            ACPIdentity.RegisterExtension();
            ACPLifecycle.RegisterExtension();
            ACPSignal.RegisterExtension();

            // start core
            ACPCore.Start(new CoreStartCompletionCallback());

            // register dependency service to link interface from ACPCoreTestApp base project
            DependencyService.Register<IACPCoreExtensionService, ACPCoreExtensionService>();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnResume()
        {
            base.OnResume();
            ACPCore.LifecycleStart(null);
        }

        protected override void OnPause()
        {
            base.OnPause();
            ACPCore.LifecyclePause();
        }
    }
}