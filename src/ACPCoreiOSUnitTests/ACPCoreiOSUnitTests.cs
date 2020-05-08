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
using NUnit.Framework;
using Foundation;
using Com.Adobe.Marketing.Mobile;
using System.Diagnostics;
using System.Threading;

namespace ACPCoreiOSUnitTests
{
    [TestFixture]
    public class ACPCoreUnitTests
    {
        [SetUp]
        public void Setup()
        {
            ACPCore.SetPrivacyStatus(ACPMobilePrivacyStatus.OptIn);
        }

        // ACPCore tests
        [Test]
        public void GetACPCoreExtensionVersion_Returns_CorrectVersion()
        {
            // verify
            Assert.True(ACPCore.ExtensionVersion == "2.6.1-XM");
        }

        [Test]
        public void DispatchValidSDKEvent_Returns_True()
        {
            // setup
            var data = new NSMutableDictionary<NSString, NSObject>
            {
                ["dispatchResponseEventKey"] = new NSString("dispatchResponseEventValue")
            };
            ACPExtensionEvent sdkEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
            // test
            var status = ACPCore.DispatchEvent(sdkEvent, out _);
            // verify
            Assert.True(status);
        }

        [Test]
        public void DispatchValidSDKEventWithCallback_Returns_True()
        {
            // setup
            NSError error;
            CountdownEvent latch = new CountdownEvent(1);
            var data = new NSMutableDictionary<NSString, NSObject>
            {
                ["dispatchResponseEventKey"] = new NSString("dispatchResponseEventValue")
            };
            ACPExtensionEvent sdkEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
            // test
            var status = ACPCore.DispatchEventWithResponseCallback(sdkEvent, responseEvent => {
                Console.WriteLine("Response event name: " + responseEvent.EventName.ToString() + " type: " + responseEvent.EventType.ToString() + " source: " + responseEvent.EventSource.ToString() + " data: " + responseEvent.EventData.ToString());
                latch.Signal();
            }, out error);
            latch.Wait(500);
            latch.Dispose();
            // verify
            Assert.True(status);
        }

        [Test]
        public void DispatchValidRequestAndResponseSDKEvents_Returns_True()
        {
            // setup
            NSError error;
            var data = new NSMutableDictionary<NSString, NSObject>
            {
                ["dispatchResponseEventKey"] = new NSString("dispatchResponseEventValue")
            };
            ACPExtensionEvent requestEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
            ACPExtensionEvent responseEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
            // test
            var status = ACPCore.DispatchResponseEvent(responseEvent, requestEvent, out error);
            // verify
            Assert.True(status);
        }

        [Test]
        public void SetPrivacyEqualToUnknown_GetPrivacyStatus_Returns_Unknown()
        {
            // setup
            ACPCore.SetPrivacyStatus(ACPMobilePrivacyStatus.Unknown);
            // test and verify
            ACPCore.GetPrivacyStatus(callback => {
                Assert.True(callback.ToString().Equals("Unknown"));
            });
        }

        [Test]
        public void SetPrivacyEqualToOptIn_GetPrivacyStatus_Returns_OptIn()
        {
            // setup
            ACPCore.SetPrivacyStatus(ACPMobilePrivacyStatus.OptIn);
            // test and verify
            ACPCore.GetPrivacyStatus(callback => {
                Assert.True(callback.ToString().Equals("OptIn"));
            });
        }

        [Test]
        public void SetPrivacyEqualToOptOut_GetPrivacyStatus_Returns_OptOut()
        {
            // setup
            ACPCore.SetPrivacyStatus(ACPMobilePrivacyStatus.OptOut);
            // test and verify
            ACPCore.GetPrivacyStatus(callback => {
                Assert.True(callback.ToString().Equals("OptOut"));
            });
        }

        [Test]
        public void GetSDKIdentities_Returns_NonEmptyString()
        {
            // verify
            ACPCore.GetSdkIdentities(callback => {
                Assert.True(callback.Length > 0);
            });
        }

        // ACPIdentity tests
        [Test]
        public void GetACPIdentityExtensionVersion_Returns_CorrectVersion()
        {
            // verify
            Assert.True(ACPIdentity.ExtensionVersion == "2.2.1");
        }

        [Test]
        public void TestAppendVisitorInfoForUrl_Returns_AppendedUrl()
        {
            // setup
            CountdownEvent latch = new CountdownEvent(1);
            // test
            NSUrl url = new NSUrl("https://test.com");
            ACPIdentity.AppendToUrl(url, callback => {
                var urlString = callback.ToString();
                NSString ecid = null;
                NSString orgid = new NSString("972C898555E9F7BC7F000101%40AdobeOrg");
                ACPIdentity.GetExperienceCloudId(ecidCallback =>
                {
                    ecid = ecidCallback;
                    latch.Signal();
                });
                latch.Wait();
                latch.Dispose();
                // verify
                Assert.True(urlString.Contains(url.ToString()));
                Assert.True(urlString.Contains(ecid));
                Assert.True(urlString.Contains(orgid));
            });
        }

        [Test]
        public void TestGetIdentifiers_Returns_SyncedIdentifiers()
        {
            // setup
            ACPIdentity.SyncIdentifier("id1", "value1", ACPMobileVisitorAuthenticationState.Authenticated);
            var ids = new NSMutableDictionary<NSString, NSObject>
            {
                ["id2"] = new NSString("value2"),
                ["id3"] = new NSString("value3"),
            };
            ACPIdentity.SyncIdentifiers(ids);
            var ids2 = new NSMutableDictionary<NSString, NSObject>
            {
                ["id4"] = new NSString("value4"),
                ["id5"] = new NSString("value5"),
            };
            ACPIdentity.SyncIdentifiers(ids, ACPMobileVisitorAuthenticationState.LoggedOut);
            // test
            ACPIdentity.GetIdentifiers(callback => {
                String visitorIdsString = "";
                foreach (ACPMobileVisitorId id in callback)
                {
                    visitorIdsString = visitorIdsString + "[Id: " + id.Identifier + ", Type: " + id.IdType + ", Origin: " + id.IdOrigin + ", Authentication: " + id.AuthenticationState + "]";
                }
                // verify
                Assert.True(visitorIdsString.Contains("[Id: id1, Type: value1, Origin: d_cid_ic, Authentication: Authenticated]"));
                Assert.True(visitorIdsString.Contains("[Id: id2, Type: value2, Origin: d_cid_ic, Authentication: Unknown]"));
                Assert.True(visitorIdsString.Contains("[Id: id3, Type: value3, Origin: d_cid_ic, Authentication: Unknown]"));
                Assert.True(visitorIdsString.Contains("[Id: id4, Type: value4, Origin: d_cid_ic, Authentication: LoggedOut]"));
                Assert.True(visitorIdsString.Contains("[Id: id5, Type: value5, Origin: d_cid_ic, Authentication: LoggedOut]"));
            });
        }

        [Test]
        public void TestGetUrlVariables_Returns_UrlVariables()
        {
            // setup
            CountdownEvent latch = new CountdownEvent(1);
            // test
            ACPIdentity.GetUrlVariables(callback => {
                var urlString = callback.ToString();
                NSString ecid = null;
                NSString orgid = new NSString("972C898555E9F7BC7F000101%40AdobeOrg");
                ACPIdentity.GetExperienceCloudId(ecidCallback =>
                {
                    ecid = ecidCallback;
                    latch.Signal();
                });
                latch.Wait();
                latch.Dispose();
                // verify
                Assert.True(urlString.Contains(ecid));
                Assert.True(urlString.Contains(orgid));
            });
        }

        // ACPLifecycle tests
        [Test]
        public void GetACPLifecycleExtensionVersion_Returns_CorrectVersion()
        {
            // verify
            Assert.True(ACPLifecycle.ExtensionVersion == "2.0.4");
        }

        // ACPSignal tests
        [Test]
        public void GetACPSignalExtensionVersion_Returns_CorrectVersion()
        {
            // verify
            Assert.True(ACPSignal.ExtensionVersion == "2.0.4");
        }
    }
}
