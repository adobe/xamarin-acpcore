/*
    Copyright 2020 Adobe
    All Rights Reserved.
    NOTICE: Adobe permits you to use, modify, and distribute this file in
    accordance with the terms of the Adobe license agreement accompanying
    it. If you have received this file from a source other than Adobe,
    then your use, modification, or distribution of it requires the prior
    written permission of Adobe.
    This file has been modified from its original form. The original
    license can be viewed in the NOTICES.txt file.
*/

using System.Reflection;
using System.Threading;
using Android.App;
using Android.OS;
using Xamarin.Android.NUnitLite;
using Com.Adobe.Marketing.Mobile;

namespace ACPCoreAndroidUnitTests
{
    [Activity(Label = "ACPCoreAndroidUnitTests", MainLauncher = true)]
    public class MainActivity : TestSuiteActivity
    {
        static CountdownEvent latch = new CountdownEvent(1);
        protected override void OnCreate(Bundle bundle)
        {
            // tests can be inside the main assembly
            AddTest(Assembly.GetExecutingAssembly());
            // or in any reference assemblies
            // AddTest (typeof (Your.Library.TestClass).Assembly);

            // Once you called base.OnCreate(), you cannot add more assemblies.
            base.OnCreate(bundle);

            // setup for all tests
            ACPCore.Application = this.Application;
            ACPCore.SetWrapperType(WrapperType.Xamarin);
            ACPCore.LogLevel = LoggingMode.Verbose;
            ACPIdentity.RegisterExtension();
            ACPSignal.RegisterExtension();
            ACPLifecycle.RegisterExtension();

            // start core
            ACPCore.Start(new CoreStartCompletionCallback());
            latch.Wait();
            latch.Dispose();
        }

        class CoreStartCompletionCallback : Java.Lang.Object, IAdobeCallback
        {
            public void Call(Java.Lang.Object callback)
            {
                ACPCore.ConfigureWithAppID("94f571f308d5/00fc543a60e1/launch-c861fab912f7-development");
                latch.Signal();
            }
        }
    }
}
