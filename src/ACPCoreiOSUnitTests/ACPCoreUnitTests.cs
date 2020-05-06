
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

        [Test]
        public void GetACPCoreExtensionVersion_Returns_CorrectVersion()
        {
            Assert.True(ACPCore.ExtensionVersion == "2.6.1-XM");
        }

        [Test]
        public void DispatchValidSDKEvent_Returns_True()
        {
            NSError error;
            var data = new NSMutableDictionary<NSString, NSObject>
            {
                ["dispatchResponseEventKey"] = new NSString("dispatchResponseEventValue")
            };
            ACPExtensionEvent sdkEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out error);
            var status = ACPCore.DispatchEvent(sdkEvent, out error);
            Assert.True(status);
        }

        [Test]
        public void DispatchValidSDKEventWithCallback_Returns_True()
        {
            NSError error;
            CountdownEvent latch = new CountdownEvent(1);
            var data = new NSMutableDictionary<NSString, NSObject>
            {
                ["dispatchResponseEventKey"] = new NSString("dispatchResponseEventValue")
            };
            ACPExtensionEvent sdkEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
            var status = ACPCore.DispatchEventWithResponseCallback(sdkEvent, responseEvent => {
                Console.WriteLine("Response event name: " + responseEvent.EventName.ToString() + " type: " + responseEvent.EventType.ToString() + " source: " + responseEvent.EventSource.ToString() + " data: " + responseEvent.EventData.ToString());
                latch.Signal();
            }, out error);
            latch.Wait(500);
            latch.Dispose();
            Assert.True(status);
        }

        [Test]
        public void DispatchValidRequestAndResponseSDKEvents_Returns_True()
        {
            NSError error;
            var data = new NSMutableDictionary<NSString, NSObject>
            {
                ["dispatchResponseEventKey"] = new NSString("dispatchResponseEventValue")
            };
            ACPExtensionEvent requestEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
            ACPExtensionEvent responseEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
            var status = ACPCore.DispatchResponseEvent(responseEvent, requestEvent, out error);
            Assert.True(status);
        }

        [Test]
        public void SetPrivacyEqualToUnknown_GetPrivacyStatus_Returns_Unknown()
        {
            ACPCore.SetPrivacyStatus(ACPMobilePrivacyStatus.Unknown);
            ACPCore.GetPrivacyStatus(callback => {
                Assert.True(callback.ToString().Equals("Unknown"));
            });
        }

        [Test]
        public void SetPrivacyEqualToOptIn_GetPrivacyStatus_Returns_OptIn()
        {
            ACPCore.SetPrivacyStatus(ACPMobilePrivacyStatus.OptIn);
            ACPCore.GetPrivacyStatus(callback => {
                Assert.True(callback.ToString().Equals("OptIn"));
            });
        }

        [Test]
        public void SetPrivacyEqualToOptOut_GetPrivacyStatus_Returns_OptOut()
        {
            ACPCore.SetPrivacyStatus(ACPMobilePrivacyStatus.OptOut);
            ACPCore.GetPrivacyStatus(callback => {
                Assert.True(callback.ToString().Equals("OptOut"));
            });
        }

        [Test]
        public void GetSDKIdentities_Returns_NonEmptyString()
        {
            ACPCore.GetSdkIdentities(callback => {
                Assert.True(callback.Length > 0);
            });
        }
    }
}
