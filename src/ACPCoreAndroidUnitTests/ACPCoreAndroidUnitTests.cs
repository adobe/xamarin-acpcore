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
using Com.Adobe.Marketing.Mobile;
using System.Collections.Generic;
using System.Threading;
using Android.Runtime;

namespace ACPCoreAndroidUnitTests
{
    [TestFixture]
    public class ACPCoreUnitTests
    {
        // CountDownEvent latch
        static CountdownEvent latch;

        // static vars to store data retrieved via callback
        static string retrievedPrivacyStatus;
        static string retrievedString;
        static string retrievedEcid;
        static string retrievedVisitorIdentifiers;

        [SetUp]
        public void Setup()
        {
            latch = null;
            retrievedPrivacyStatus = null;
            retrievedString = null;
            retrievedEcid = null;
            retrievedVisitorIdentifiers = null;
            ACPCore.SetPrivacyStatus(MobilePrivacyStatus.OptIn);
        }

        // ACPCore tests
        [Test]
        public void GetACPCoreExtensionVersion_Returns_CorrectVersion()
        {
            // verify
            Assert.That(ACPCore.ExtensionVersion(), Is.EqualTo("1.8.0-X"));
        }

        [Test]
        public void DispatchValidSDKEvent_Returns_True()
        {
            // setup
            var data = new Dictionary<string, Java.Lang.Object>();
            data.Add("testEvent", true);
            Event sdkEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
            // test
            var status = ACPCore.DispatchEvent(sdkEvent, new ErrorCallback());
            // verify
            Assert.That(status, Is.EqualTo(true));
        }

        [Test]
        public void DispatchValidSDKEventWithCallback_Returns_True()
        {
            // setup
            var data = new Dictionary<string, Java.Lang.Object>();
            data.Add("testEvent", true);
            Event sdkEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
            // test
            var status = ACPCore.DispatchEventWithResponseCallback(sdkEvent, new StringCallback(), new ErrorCallback());
            // verify
            Assert.That(status, Is.EqualTo(true));
        }

        [Test]
        public void DispatchValidRequestAndResponseSDKEvents_Returns_True()
        {
            // setup
            var data = new Dictionary<string, Java.Lang.Object>();
            data.Add("testDispatchResponseEvent", "true");
            Event requestEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
            Event responseEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
            // test
            var status = ACPCore.DispatchResponseEvent(responseEvent, requestEvent, new ErrorCallback());
            // verify
            Assert.That(status, Is.EqualTo(true));
        }

        [Test]
        public void SetPrivacyEqualToUnknown_GetPrivacyStatus_Returns_Unknown()
        {
            // setup
            latch = new CountdownEvent(1);
            string expectedPrivacyStatus = "UNKNOWN";
            ACPCore.SetPrivacyStatus(MobilePrivacyStatus.Unknown);
            // test
            ACPCore.GetPrivacyStatus(new PrivacyStatusCallback());
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.That(retrievedPrivacyStatus, Is.EqualTo(expectedPrivacyStatus));
        }

        [Test]
        public void SetPrivacyEqualToOptIn_GetPrivacyStatus_Returns_OptIn()
        {
            // setup
            latch = new CountdownEvent(1);
            string expectedPrivacyStatus = "OPT_IN";
            ACPCore.SetPrivacyStatus(MobilePrivacyStatus.OptIn);
            // test
            ACPCore.GetPrivacyStatus(new PrivacyStatusCallback());
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.That(retrievedPrivacyStatus, Is.EqualTo(expectedPrivacyStatus));
        }

        [Test]
        public void SetPrivacyEqualToOptOut_GetPrivacyStatus_Returns_OptOut()
        {
            // setup
            latch = new CountdownEvent(1);
            string expectedPrivacyStatus = "OPT_OUT";
            ACPCore.SetPrivacyStatus(MobilePrivacyStatus.OptOut);
            // test
            ACPCore.GetPrivacyStatus(new PrivacyStatusCallback());
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.That(retrievedPrivacyStatus, Is.EqualTo(expectedPrivacyStatus));
        }

        [Test]
        public void GetSDKIdentities_Returns_NonEmptyString()
        {
            // setup
            latch = new CountdownEvent(1);
            // test
            ACPCore.GetSdkIdentities(new StringCallback());
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.That(retrievedString.Length, Is.GreaterThan(0));
        }

        // ACPIdentity tests
        [Test]
        public void GetACPIdentityExtensionVersion_Returns_CorrectVersion()
        {
            // verify
            Assert.That(ACPIdentity.ExtensionVersion(), Is.EqualTo("1.2.2"));
        }

        [Test]
        public void TestAppendVisitorInfoForUrl_Returns_AppendedUrl()
        {
            // setup
            latch = new CountdownEvent(2);
            String url = "https://test.com";
            String orgid = "972C898555E9F7BC7F000101%40AdobeOrg";
            // test
            ACPIdentity.GetExperienceCloudId(new EcidCallback());
            ACPIdentity.AppendVisitorInfoForURL(url, new StringCallback());
            latch.Wait(1000);
            latch.Dispose();
            // verify
            Assert.That(retrievedString, Is.StringContaining(url));
            Assert.That(retrievedString, Is.StringContaining(retrievedEcid));
            Assert.That(retrievedString, Is.StringContaining(orgid));
        }

        [Test]
        public void TestGetIdentifiers_Returns_SyncedIdentifiers()
        {
            // setup
            latch = new CountdownEvent(1);
            ACPIdentity.SyncIdentifier("id1", "value1", VisitorID.AuthenticationState.Authenticated);
            var ids = new Dictionary<string, string>();
            ids.Add("id2", "value2");
            ids.Add("id3", "value3");
            ACPIdentity.SyncIdentifiers(ids);
            var ids2 = new Dictionary<string, string>();
            ids2.Add("id4", "value4");
            ids2.Add("id5", "value5");
            ACPIdentity.SyncIdentifiers(ids2, VisitorID.AuthenticationState.LoggedOut);
            // test
            ACPIdentity.GetIdentifiers(new GetIdentifiersCallback());
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.That(retrievedVisitorIdentifiers, Is.StringContaining("[Id: value1, Type: id1, Origin: d_cid_ic, Authentication: AUTHENTICATED]"));
            Assert.That(retrievedVisitorIdentifiers, Is.StringContaining("[Id: value2, Type: id2, Origin: d_cid_ic, Authentication: UNKNOWN]"));
            Assert.That(retrievedVisitorIdentifiers, Is.StringContaining("[Id: value3, Type: id3, Origin: d_cid_ic, Authentication: UNKNOWN]"));
            Assert.That(retrievedVisitorIdentifiers, Is.StringContaining("[Id: value4, Type: id4, Origin: d_cid_ic, Authentication: LOGGED_OUT]"));
            Assert.That(retrievedVisitorIdentifiers, Is.StringContaining("[Id: value5, Type: id5, Origin: d_cid_ic, Authentication: LOGGED_OUT]"));
        }

        [Test]
        public void TestGetUrlVariables_Returns_UrlVariables()
        {
            // setup
            latch = new CountdownEvent(2);
            string orgid = "972C898555E9F7BC7F000101%40AdobeOrg";
            // test
            ACPIdentity.GetExperienceCloudId(new EcidCallback());
            ACPIdentity.GetUrlVariables(new StringCallback());
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.That(retrievedString, Is.StringContaining(retrievedEcid));
            Assert.That(retrievedString, Is.StringContaining(orgid));
        }

        // ACPLifecycle tests
        [Test]
        public void GetACPLifecycleExtensionVersion_Returns_CorrectVersion()
        {
            // verify
            Assert.That(ACPLifecycle.ExtensionVersion(), Is.EqualTo("1.0.7"));
        }

        // ACPSignal tests
        [Test]
        public void GetACPSignalExtensionVersion_Returns_CorrectVersion()
        {
            // verify
            // todo: this should be 1.0.3 but the current signal aar returns the wrong version
            Assert.That(ACPSignal.ExtensionVersion(), Is.EqualTo("1.0.2"));
        }

        // callbacks
        class StringCallback : Java.Lang.Object, IAdobeCallback
        {
            public void Call(Java.Lang.Object stringContent)
            {
                if (stringContent != null)
                {
                    retrievedString = stringContent.ToString();
                    Console.WriteLine("string retrieved: " + retrievedString);
                }
                else
                {
                    Console.WriteLine("null content in string callback");
                }
                if (latch != null)
                {
                    latch.Signal();
                }
            }
        }

        class EcidCallback : Java.Lang.Object, IAdobeCallback
        {
            public void Call(Java.Lang.Object ecid)
            {
                if (ecid != null)
                {
                    retrievedEcid = ecid.ToString();
                }
                else
                {
                    Console.WriteLine("null content in ecid callback");
                }
                if (latch != null)
                {
                    latch.Signal();
                }
            }
        }

        class PrivacyStatusCallback : Java.Lang.Object, IAdobeCallback
        {
            public void Call(Java.Lang.Object privacyStatus)
            {
                if (privacyStatus != null)
                {
                    retrievedPrivacyStatus = privacyStatus.ToString();
                }
                else
                {
                    Console.WriteLine("null privacy status retrieved");
                }
                if (latch != null)
                {
                    latch.Signal();
                }
            }
        }

        class GetIdentifiersCallback : Java.Lang.Object, IAdobeCallback
        {
            public void Call(Java.Lang.Object retrievedIds)
            {
                retrievedVisitorIdentifiers = "[]";
                if (retrievedIds != null)
                {
                    var ids = GetObject<JavaList>(retrievedIds.Handle, JniHandleOwnership.DoNotTransfer);
                    if (!ids.IsEmpty)
                    {
                        retrievedVisitorIdentifiers = "";
                        foreach (VisitorID id in ids)
                        {
                            retrievedVisitorIdentifiers = retrievedVisitorIdentifiers + "[Id: " + id.Id + ", Type: " + id.IdType + ", Origin: " + id.IdOrigin + ", Authentication: " + id.GetAuthenticationState().ToString() + "]";
                        }
                    }
                }
                if (latch != null)
                {
                    latch.Signal();
                }
                Console.WriteLine("retrieved visitor identifiers string: " + retrievedVisitorIdentifiers);
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