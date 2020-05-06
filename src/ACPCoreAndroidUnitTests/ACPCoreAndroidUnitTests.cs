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
            Assert.True(ACPCore.ExtensionVersion() == "1.5.3-XM");
        }

        [Test]
        public void DispatchValidSDKEvent_Returns_True()
        {
            // setup
            IDictionary<string, Java.Lang.Object> data = new Dictionary<string, Java.Lang.Object>();
            data.Add("testEvent", true);
            Event sdkEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
            // test
            var status = ACPCore.DispatchEvent(sdkEvent, new ErrorCallback());
            // verify
            Assert.True(status);
        }

        [Test]
        public void DispatchValidSDKEventWithCallback_Returns_True()
        {
            // setup
            IDictionary<string, Java.Lang.Object> data = new Dictionary<string, Java.Lang.Object>();
            data.Add("testEvent", true);
            Event sdkEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
            // test
            var status = ACPCore.DispatchEventWithResponseCallback(sdkEvent, new StringCallback(), new ErrorCallback());
            // verify
            Assert.True(status);
        }

        [Test]
        public void DispatchValidRequestAndResponseSDKEvents_Returns_True()
        {
            // setup
            IDictionary<string, Java.Lang.Object> data = new Dictionary<string, Java.Lang.Object>();
            data.Add("testDispatchResponseEvent", "true");
            Event requestEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
            Event responseEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
            // test
            var status = ACPCore.DispatchResponseEvent(responseEvent, requestEvent, new ErrorCallback());
            // verify
            Assert.True(status);
        }

        [Test]
        public void SetPrivacyEqualToUnknown_GetPrivacyStatus_Returns_Unknown()
        {
            // setup
            string expectedPrivacyStatus = "UNKNOWN";
            ACPCore.SetPrivacyStatus(MobilePrivacyStatus.Unknown);
            // test
            ACPCore.GetPrivacyStatus(new PrivacyStatusCallback());
            // verify
            Assert.True(retrievedPrivacyStatus == expectedPrivacyStatus);
        }

        [Test]
        public void SetPrivacyEqualToOptIn_GetPrivacyStatus_Returns_OptIn()
        {
            // setup
            string expectedPrivacyStatus = "OPT_IN";
            ACPCore.SetPrivacyStatus(MobilePrivacyStatus.OptIn);
            // test
            ACPCore.GetPrivacyStatus(new PrivacyStatusCallback());
            // verify
            Assert.True(retrievedPrivacyStatus == expectedPrivacyStatus);
        }

        [Test]
        public void SetPrivacyEqualToOptOut_GetPrivacyStatus_Returns_OptOut()
        {
            // setup
            string expectedPrivacyStatus = "OPT_OUT";
            ACPCore.SetPrivacyStatus(MobilePrivacyStatus.OptOut);
            // test
            ACPCore.GetPrivacyStatus(new PrivacyStatusCallback());
            // verify
            Assert.True(retrievedPrivacyStatus == expectedPrivacyStatus);
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
            Assert.True(retrievedString.Length > 0);
        }

        // ACPIdentity tests
        [Test]
        public void GetACPIdentityExtensionVersion_Returns_CorrectVersion()
        {
            // verify
            Assert.True(ACPIdentity.ExtensionVersion() == "1.2.0");
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
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.True(retrievedString.Contains(url));
            Assert.True(retrievedString.Contains(retrievedEcid));
            Assert.True(retrievedString.Contains(orgid));
        }

        [Test]
        public void TestGetIdentifiers_Returns_SyncedIdentifiers()
        {
            // setup
            ACPIdentity.SyncIdentifier("id1", "value1", VisitorID.AuthenticationState.Authenticated);
            Dictionary<string, string> ids = new Dictionary<string, string>();
            ids.Add("id2", "value2");
            ids.Add("id3", "value3");
            ACPIdentity.SyncIdentifiers(ids);
            Dictionary<string, string> ids2 = new Dictionary<string, string>();
            ids.Add("id4", "value4");
            ids.Add("id5", "value5");
            ACPIdentity.SyncIdentifiers(ids2, VisitorID.AuthenticationState.LoggedOut);
            // test
            ACPIdentity.GetIdentifiers(new GetIdentifiersCallback());
            // verify
            Assert.True(retrievedVisitorIdentifiers.Contains("[Id: id1, Type: value1, Origin: d_cid_ic, Authentication: Authenticated]"));
            Assert.True(retrievedVisitorIdentifiers.Contains("[Id: id2, Type: value2, Origin: d_cid_ic, Authentication: Unknown]"));
            Assert.True(retrievedVisitorIdentifiers.Contains("[Id: id3, Type: value3, Origin: d_cid_ic, Authentication: Unknown]"));
            Assert.True(retrievedVisitorIdentifiers.Contains("[Id: id4, Type: value4, Origin: d_cid_ic, Authentication: LoggedOut]"));
            Assert.True(retrievedVisitorIdentifiers.Contains("[Id: id5, Type: value5, Origin: d_cid_ic, Authentication: LoggedOut]"));
        }

        [Test]
        public void TestGetUrlVariables_Returns_UrlVariables()
        {
            // setup
            latch = new CountdownEvent(2);
            String orgid = "972C898555E9F7BC7F000101%40AdobeOrg";
            // test
            ACPIdentity.GetExperienceCloudId(new EcidCallback());
            ACPIdentity.GetUrlVariables(new StringCallback());
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.True(retrievedString.Contains(retrievedEcid));
            Assert.True(retrievedString.Contains(orgid));
        }

        // ACPLifecycle tests
        [Test]
        public void GetACPLifecycleExtensionVersion_Returns_CorrectVersion()
        {
            // verify
            Assert.True(ACPLifecycle.ExtensionVersion() == "1.0.4");
        }

        // ACPSignal tests
        [Test]
        public void GetACPSignalExtensionVersion_Returns_CorrectVersion()
        {
            // verify
            // todo: this should be 1.0.3 but the current signal aar returns the wrong version
            Assert.True(ACPSignal.ExtensionVersion() == "1.0.2");
        }

        // callbacks
        class StringCallback : Java.Lang.Object, IAdobeCallback
        {
            public void Call(Java.Lang.Object stringContent)
            {
                if (stringContent != null)
                {
                    retrievedString = stringContent.ToString();
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
                if (latch.IsSet)
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
            }
        }

        class GetIdentifiersCallback : Java.Lang.Object, IAdobeCallback
        {
            public void Call(Java.Lang.Object visitorIDs)
            {
                JavaList ids = null;
                retrievedVisitorIdentifiers = "[]";
                if (visitorIDs != null)
                {
                    ids = (JavaList)visitorIDs;
                    if (!ids.IsEmpty)
                    {
                        retrievedVisitorIdentifiers = "";
                        foreach (VisitorID id in ids)
                        {
                            retrievedVisitorIdentifiers = retrievedVisitorIdentifiers + "[Id: " + id.Id + ", Type: " + id.IdType + ", Origin: " + id.IdOrigin + ", Authentication: " + id.GetAuthenticationState() + "]";
                        }
                    }
                }
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