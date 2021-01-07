// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace ACPCoreTestApp.tvOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel version { get; set; }

        [Action ("clickHandler:")]
        partial void clickHandler (UIKit.UIButton sender);

        [Action ("AppendVisitorInfoForUrl:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void AppendVisitorInfoForUrl (UIKit.UIButton sender);

        [Action ("DispatchEvent:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void DispatchEvent (UIKit.UIButton sender);

        [Action ("DispatchEventWithResponseCallback:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void DispatchEventWithResponseCallback (UIKit.UIButton sender);

        [Action ("DispatchResponseEvent:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void DispatchResponseEvent (UIKit.UIButton sender);

        [Action ("DownloadRules:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void DownloadRules (UIKit.UIButton sender);

        [Action ("GetExperienceCloudId:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void GetExperienceCloudId (UIKit.UIButton sender);

        [Action ("GetExtensionVersionCore:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void GetExtensionVersionCore (UIKit.UIButton sender);

        [Action ("GetExtensionVersionIdentity:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void GetExtensionVersionIdentity (UIKit.UIButton sender);

        [Action ("GetExtensionVersionLifecycle:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void GetExtensionVersionLifecycle (UIKit.UIButton sender);

        [Action ("GetExtensionVersionSignal:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void GetExtensionVersionSignal (UIKit.UIButton sender);

        [Action ("GetIdentifiers:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void GetIdentifiers (UIKit.UIButton sender);

        [Action ("GetPrivacyStatus:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void GetPrivacyStatus (UIKit.UIButton sender);

        [Action ("GetSDKIdentities:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void GetSDKIdentities (UIKit.UIButton sender);

        [Action ("GetUrlVariables:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void GetUrlVariables (UIKit.UIButton sender);

        [Action ("SetAdvertisingIdentifier:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SetAdvertisingIdentifier (UIKit.UIButton sender);

        [Action ("SetLogLevel:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SetLogLevel (UIKit.UIButton sender);

        [Action ("SetPrivacyStatus:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SetPrivacyStatus (UIKit.UIButton sender);

        [Action ("syncIdentifier:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void syncIdentifier (UIKit.UIButton sender);

        [Action ("SyncIdentifiers:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SyncIdentifiers (UIKit.UIButton sender);

        [Action ("SyncIdentifiersWithAuthState:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SyncIdentifiersWithAuthState (UIKit.UIButton sender);

        [Action ("TrackAction:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TrackAction (UIKit.UIButton sender);

        [Action ("TrackState:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TrackState (UIKit.UIButton sender);

        [Action ("UpdateConfig:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UpdateConfig (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (version != null) {
                version.Dispose ();
                version = null;
            }
        }
    }
}