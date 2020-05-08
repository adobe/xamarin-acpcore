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

using System;
using System.Linq;
using System.Collections.Generic;

using Foundation;
using UIKit;
using MonoTouch.NUnit.UI;
using Com.Adobe.Marketing.Mobile;
using System.Threading;

namespace ACPCoreiOSUnitTests
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("UnitTestAppDelegate")]
    public partial class UnitTestAppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
        TouchRunner runner;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // create a new window instance based on the screen size
            window = new UIWindow(UIScreen.MainScreen.Bounds);
            runner = new TouchRunner(window);

            // register every tests included in the main application/assembly
            runner.Add(System.Reflection.Assembly.GetExecutingAssembly());

            window.RootViewController = new UINavigationController(runner.GetViewController());

            // make the window visible
            window.MakeKeyAndVisible();

            // setup for all tests
            CountdownEvent latch = new CountdownEvent(1);
            ACPCore.SetWrapperType(ACPMobileWrapperType.Xamarin);
            ACPCore.LogLevel = ACPMobileLogLevel.Verbose;
            ACPIdentity.RegisterExtension();
            ACPSignal.RegisterExtension();
            ACPLifecycle.RegisterExtension();

            // start core
            ACPCore.Start(() =>
            {
                // set config from launch (org: OBUMobile5, app: ryan-xamarin)
                ACPCore.ConfigureWithAppId("94f571f308d5/00fc543a60e1/launch-c861fab912f7-development");
                latch.Signal();
            });
            latch.Wait();
            latch.Dispose();

            return true;
        }
    }
}
