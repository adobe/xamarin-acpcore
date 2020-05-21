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
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Android.Runtime;
using Com.Adobe.Marketing.Mobile;
using System.Collections;

namespace ACPCoreTestApp.Droid
{
    public class ACPCoreExtensionService : IACPCoreExtensionService
    {
        TaskCompletionSource<string> stringOutput;

        public ACPCoreExtensionService()
        {
        }        

        // ACPCore methods
        public TaskCompletionSource<string> GetExtensionVersionCore()
        {
            stringOutput = new TaskCompletionSource<string>();
            stringOutput.SetResult(ACPCore.ExtensionVersion());
            return stringOutput;
        }

        public TaskCompletionSource<string> DispatchEvent()
        {
            stringOutput = new TaskCompletionSource<string>();
            var data = new Dictionary<string, Java.Lang.Object>();
            data.Add("testEvent", true);
            Event sdkEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
            stringOutput.SetResult(ACPCore.DispatchEvent(sdkEvent, new ErrorCallback()).ToString());
            return stringOutput;
        }

        public TaskCompletionSource<string> DispatchEventWithResponseCallback()
        {
            stringOutput = new TaskCompletionSource<string>();
            var data = new Dictionary<string, Java.Lang.Object>();
            data.Add("testEvent", true);
            Event sdkEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
            stringOutput.SetResult(ACPCore.DispatchEventWithResponseCallback(sdkEvent, new StringCallback(), new ErrorCallback()).ToString());
            return stringOutput;
        }

        public TaskCompletionSource<string> DispatchResponseEvent()
        {
            stringOutput = new TaskCompletionSource<string>();
            var data = new Dictionary<string, Java.Lang.Object>();
            data.Add("testDispatchResponseEvent", "true");
            Event requestEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
            Event responseEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
            stringOutput.SetResult(ACPCore.DispatchResponseEvent(responseEvent, requestEvent, new ErrorCallback()).ToString());
            return stringOutput;
        }

        public TaskCompletionSource<string> DownloadRules()
        {
            // TODO: this method is not implemented on Android
            stringOutput = new TaskCompletionSource<string>();
            stringOutput.SetResult("");
            return stringOutput;
        }

        public TaskCompletionSource<string> GetPrivacyStatus()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPCore.GetPrivacyStatus(new StringCallback());
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> GetSDKIdentities()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPCore.GetSdkIdentities(new StringCallback());
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> SetAdvertisingIdentifier()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPCore.SetAdvertisingIdentifier("testAdvertisingIdentifier");
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> SetLogLevel()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPCore.LogLevel = LoggingMode.Verbose;
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> SetPrivacyStatus()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPCore.SetPrivacyStatus(MobilePrivacyStatus.OptIn);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> TrackAction()
        {
            stringOutput = new TaskCompletionSource<string>();
            var data = new Dictionary<string, string>();
            data.Add("key", "value");
            ACPCore.TrackAction("action", data);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> TrackState()
        {
            stringOutput = new TaskCompletionSource<string>();
            var data = new Dictionary<string, string>();
            data.Add("key", "value");
            ACPCore.TrackState("state", data);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> UpdateConfig()
        {
            stringOutput = new TaskCompletionSource<string>();
            var config = new Dictionary<string, Java.Lang.Object>();
            config.Add("someConfigKey", "configValue");
            ACPCore.UpdateConfiguration(config);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        // ACPIdentity methods
        public TaskCompletionSource<string> GetExtensionVersionIdentity()
        {
            stringOutput = new TaskCompletionSource<string>();
            stringOutput.SetResult(ACPIdentity.ExtensionVersion());
            return stringOutput;
        }

        public TaskCompletionSource<string> AppendVisitorInfoForUrl()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPIdentity.AppendVisitorInfoForURL("https://example.com", new StringCallback());
            stringOutput.SetResult("");
            return stringOutput;
        }

        public TaskCompletionSource<string> GetExperienceCloudId()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPIdentity.GetExperienceCloudId(new StringCallback());
            stringOutput.SetResult("");
            return stringOutput;
        }

        public TaskCompletionSource<string> GetIdentifiers()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPIdentity.GetIdentifiers(new GetIdentifiersCallback());
            stringOutput.SetResult("");
            return stringOutput;
        }

        public TaskCompletionSource<string> GetUrlVariables()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPIdentity.GetUrlVariables(new StringCallback());
            stringOutput.SetResult("");
            return stringOutput;
        }

        public TaskCompletionSource<string> SyncIdentifier()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPIdentity.SyncIdentifier("name", "john", VisitorID.AuthenticationState.Authenticated); 
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> SyncIdentifiers()
        {
            stringOutput = new TaskCompletionSource<string>();
            var ids = new Dictionary<string, string>();
            ids.Add("lastname", "doe");
            ids.Add("age", "38");
            ids.Add("zipcode", "94403");
            ACPIdentity.SyncIdentifiers(ids);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> SyncIdentifiersWithAuthState()
        {
            stringOutput = new TaskCompletionSource<string>();
            var ids = new Dictionary<string, string>();
            ids.Add("lastname", "doe");
            ids.Add("age", "38");
            ids.Add("zipcode", "94403");
            ACPIdentity.SyncIdentifiers(ids, VisitorID.AuthenticationState.LoggedOut);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        // ACPLifecycle methods
        public TaskCompletionSource<string> GetExtensionVersionLifecycle()
        {
            stringOutput = new TaskCompletionSource<string>();
            stringOutput.SetResult(ACPLifecycle.ExtensionVersion());
            return stringOutput;
        }

        // ACPSignal methods
        public TaskCompletionSource<string> GetExtensionVersionSignal()
        {
            stringOutput = new TaskCompletionSource<string>();
            stringOutput.SetResult(ACPSignal.ExtensionVersion());
            return stringOutput;
        }

        // callbacks
        class StringCallback : Java.Lang.Object, IAdobeCallback
        {
            public void Call(Java.Lang.Object stringContent)
            {
                if (stringContent != null)
                {
                    Console.WriteLine(stringContent);
                }
                else
                {
                    Console.WriteLine("null content in string callback");
                }
            }
        }

        class GetIdentifiersCallback : Java.Lang.Object, IAdobeCallback
        {
            public void Call(Java.Lang.Object retrievedIds)
            {
                System.String visitorIdsString = "[]";
                if (retrievedIds != null)
                {
                    var ids = GetObject<JavaList>(retrievedIds.Handle, JniHandleOwnership.DoNotTransfer);
                    if (ids != null && ids.Count > 0)
                    {
                        visitorIdsString = "";
                        foreach (VisitorID id in ids)
                        {
                            visitorIdsString = visitorIdsString + "[Id: " + id.Id + ", Type: " + id.IdType + ", Origin: " + id.IdOrigin + ", Authentication: " + id.GetAuthenticationState() + "]";
                        }
                    }
                }
                Console.WriteLine("Retrieved visitor ids: " + visitorIdsString);
            }
        }

        class ErrorCallback : Java.Lang.Object, IExtensionErrorCallback
        {

            public void Call(Java.Lang.Object sdkEvent)
            {
                Console.WriteLine("AEP SDK event data: " + sdkEvent.ToString());
            }

            public void Error(Java.Lang.Object error)
            {
                Console.WriteLine("AEP SDK Error: " + error.ToString());
            }

        }
    }

}
