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

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
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
            ACPCore.Application = this.Application;
            ACPCore.LifecycleStart(null);
        }

        protected override void OnPause()
        {
            base.OnPause();
            ACPCore.LifecyclePause();
        }
    }
}