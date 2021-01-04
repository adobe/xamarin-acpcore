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
using UIKit;
using Foundation;
using Com.Adobe.Marketing.Mobile;

namespace ACPCoreTestApp.tvOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        //ACPCore
        partial void GetExtensionVersionCore(UIButton sender)
        {            
            string extensionVersion = ACPCore.ExtensionVersion();
            Console.WriteLine("GetExtensionVersionCore: " + extensionVersion);

        }

        partial void DispatchEvent(UIButton sender)
        {
            NSError error;
            var data = new NSMutableDictionary<NSString, NSObject>
            {
                ["dispatchResponseEventKey"] = new NSString("dispatchResponseEventValue")
            };
            ACPExtensionEvent sdkEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
            bool result = ACPCore.DispatchEvent(sdkEvent, out error);
            if (error != null)
            {
                Console.WriteLine("DispatchEvent: Error: " + error.LocalizedDescription);
            }
            else
            {
                Console.WriteLine("DispatchEvent: " + result);
            }
        }        

        partial void DownloadRules(UIButton sender)
        {
            ACPCore.DownloadRules();
            Console.WriteLine("DownloadRules Completed");
        }        

        partial void SetAdvertisingIdentifier(UIButton sender)
        {
            ACPCore.SetAdvertisingIdentifier("testAdvertisingIdentifier");
            Console.WriteLine("SetAdvertisingIdentifier Completed");
        }        

        partial void DispatchEventWithResponseCallback(UIButton sender)
        {            
            NSError error;
            var data = new NSMutableDictionary<NSString, NSObject>
            {
                ["dispatchResponseEventKey"] = new NSString("dispatchResponseEventValue")
            };
            ACPExtensionEvent sdkEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
            Action<ACPExtensionEvent> callback = new Action<ACPExtensionEvent>(handleCallback);
            bool result = ACPCore.DispatchEventWithResponseCallback(sdkEvent, callback, out error);
            if (error != null)
            {
                Console.WriteLine("DispatchEventWithResponseCallback: Error: " + error.LocalizedDescription);
            }
            else {
                Console.WriteLine("DispatchEvent: " + result);
            }            
        }

        partial void DispatchResponseEvent(UIButton sender)
        {            
            NSError error;
            var data = new NSMutableDictionary<NSString, NSObject>
            {
                ["dispatchResponseEventKey"] = new NSString("dispatchResponseEventValue")
            };
            ACPExtensionEvent requestEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
            ACPExtensionEvent responseEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
            bool result = ACPCore.DispatchResponseEvent(responseEvent, requestEvent, out error);
            if (error != null)
            {
                Console.WriteLine("DispatchEventWithResponseCallback: Error: " + error.LocalizedDescription);
            }
            else {
                Console.WriteLine("DispatchResponseEvent: " + result);
            }            
        }

        partial void TrackAction(UIButton sender)
        {
            var data = new NSMutableDictionary<NSString, NSString>
            {
                ["key"] = new NSString("value")
            };
            ACPCore.TrackAction("action", data);
            Console.WriteLine("TrackAction Completed");
        }

        partial void GetSDKIdentities(UIButton sender)
        {
            var callback = new Action<NSString>(handleCallback);
            ACPCore.GetSdkIdentities(callback);
            Console.WriteLine("GetSDKIdentities Completed");            
        }

        partial void SetLogLevel(UIButton sender)
        {
            ACPCore.LogLevel = ACPMobileLogLevel.Verbose;
            Console.WriteLine("SetLogLevel Completed");
        }

        partial void GetPrivacyStatus(UIButton sender)
        {
            var callback = new Action<ACPMobilePrivacyStatus>(handleCallback);
            ACPCore.GetPrivacyStatus(callback);
            Console.WriteLine("GetPrivacyStatus Completed");
        }

        partial void TrackState(UIButton sender)
        {
            var data = new NSMutableDictionary<NSString, NSString>
            {
                ["key"] = new NSString("value")
            };
            ACPCore.TrackState("state", data);
            Console.WriteLine("TrackState Completed");
        }

        partial void UpdateConfig(UIButton sender)
        {
            var config = new NSMutableDictionary<NSString, NSObject>
            {
                ["someConfigKey"] = new NSString("configValue")
            };
            ACPCore.UpdateConfiguration(config);
            Console.WriteLine("UpdateConfig Completed");
        }

        partial void SetPrivacyStatus(UIButton sender)
        {
            ACPCore.SetPrivacyStatus(ACPMobilePrivacyStatus.OptIn);
            Console.WriteLine("SetPrivacyStatus Completed");
        }

        //ACPIdentity

        partial void GetExtensionVersionIdentity(UIButton sender)
        {
            string identityExtensionVersion = ACPIdentity.ExtensionVersion();
            Console.WriteLine("GetExtensionVersionIdentity: " + identityExtensionVersion);
        }

        partial void AppendVisitorInfoForUrl(UIButton sender)
        {
            var callback = new Action<NSUrl>(handleCallback);
            var url = new NSUrl("https://example.com");
            ACPIdentity.AppendToUrl(url, callback);
            Console.WriteLine("AppendVisitorInfoForUrl Completed");
        }

        partial void GetExperienceCloudId(UIButton sender)
        {
            var callback = new Action<NSString>(handleCallback);
            ACPIdentity.GetExperienceCloudId(callback);
            Console.WriteLine("GetExperienceCloudId Completed");
        }

        partial void GetIdentifiers(UIButton sender)
        {
            var callback = new Action<ACPMobileVisitorId[]>(handleCallback);
            ACPIdentity.GetIdentifiers(callback);
            Console.WriteLine("GetIdentifiers Completed");
        }

        partial void GetUrlVariables(UIButton sender)
        {
            var callback = new Action<NSString>(handleCallback);
            ACPIdentity.GetUrlVariables(callback);
            Console.WriteLine("GetUrlVariables Completed");
        }

        partial void SyncIdentifiers(UIButton sender)
        {
            var ids = new NSMutableDictionary<NSString, NSObject>
            {
                ["lastName"] = new NSString("doe"),
                ["age"] = new NSString("38"),
                ["zipcode"] = new NSString("94403")
            };
            ACPIdentity.SyncIdentifiers(ids);
            Console.WriteLine("SyncIdentifiers Completed");
        }

        partial void syncIdentifier(UIButton sender)
        {
            ACPIdentity.SyncIdentifier("name", "john", ACPMobileVisitorAuthenticationState.Authenticated);
            Console.WriteLine("SyncIdentifiers Completed");
        }

        partial void SyncIdentifiersWithAuthState(UIButton sender)
        {
            var ids = new NSMutableDictionary<NSString, NSObject>
            {
                ["lastName"] = new NSString("doe"),
                ["age"] = new NSString("38"),
                ["zipcode"] = new NSString("94403")
            };
            ACPIdentity.SyncIdentifiers(ids, ACPMobileVisitorAuthenticationState.LoggedOut);
            Console.WriteLine("SyncIdentifiersWithAuthState Completed");
        }

        //ACPLifecycle
        partial void GetExtensionVersionLifecycle(UIButton sender)
        {
            string extensionVersion = ACPLifecycle.ExtensionVersion();
            Console.WriteLine("GetExtensionVersionLifecycle: "+ extensionVersion);
        }

        //ACPSignal
        partial void GetExtensionVersionSignal(UIButton sender)
        {
            string extensionVersion = ACPSignal.ExtensionVersion();
            Console.WriteLine("GetExtensionVersionSignal: " + extensionVersion);
        }


        // callbacks
        private void handleCallback(ACPExtensionEvent responseEvent)
        {
            Console.WriteLine("Response event name: " + responseEvent.EventName.ToString() + " type: " + responseEvent.EventType.ToString() + " source: " + responseEvent.EventSource.ToString() + " data: " + responseEvent.EventData.ToString());
        }

        private void handleCallback(ACPMobilePrivacyStatus privacyStatus)
        {
            Console.WriteLine("Privacy status: " + privacyStatus.ToString());
        }

        private void handleCallback(NSString content)
        {
            Console.WriteLine("String callback: " + content);
        }

        private void handleCallback(NSUrl url)
        {
            Console.WriteLine("Appended url: " + url.ToString());
        }

        private void handleCallback(ACPMobileVisitorId[] ids)
        {
            String visitorIdsString = "[]";
            if (ids.Length != 0)
            {
                visitorIdsString = "";
                foreach (ACPMobileVisitorId id in ids)
                {
                    visitorIdsString = visitorIdsString + "[Id: " + id.Identifier + ", Type: " + id.IdType + ", Origin: " + id.IdOrigin + ", Authentication: " + id.AuthenticationState + "]";
                }
            }
            Console.WriteLine("Retrieved visitor ids: " + visitorIdsString);
        }        
    }
}

