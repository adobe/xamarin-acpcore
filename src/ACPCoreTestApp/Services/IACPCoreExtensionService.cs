using System.IO;
using System.Threading.Tasks;

namespace ACPCoreTestApp
{
    public interface IACPCoreExtensionService
    {
        // ACPCore API
        TaskCompletionSource<string> GetExtensionVersionCore();
        TaskCompletionSource<string> DispatchEvent();
        TaskCompletionSource<string> DispatchEventWithResponseCallback();
        TaskCompletionSource<string> DispatchResponseEvent();
        TaskCompletionSource<string> GetPrivacyStatus();
        TaskCompletionSource<string> GetSDKIdentities();
        TaskCompletionSource<string> SetAdvertisingIdentifier();
        TaskCompletionSource<string> SetLogLevel();
        TaskCompletionSource<string> SetPrivacyStatus();
        TaskCompletionSource<string> TrackAction();
        TaskCompletionSource<string> TrackState();
        TaskCompletionSource<string> UpdateConfig();
        // ACPIdentity API
        TaskCompletionSource<string> GetExtensionVersionIdentity();
        TaskCompletionSource<string> AppendVisitorInfoForUrl();
        TaskCompletionSource<string> GetExperienceCloudId();
        TaskCompletionSource<string> GetIdentifiers();
        TaskCompletionSource<string> GetURLVariables();
        TaskCompletionSource<string> SyncIdentifier();
        TaskCompletionSource<string> SyncIdentifiers();
        TaskCompletionSource<string> SyncIdentifiersWithAuthState();
        // ACPLifecycle API
        TaskCompletionSource<string> GetExtensionVersionLifecycle();
        // ACPSignal API
        TaskCompletionSource<string> GetExtensionVersionSignal();
    }
}
