/*
Copyright 2020 Adobe
All Rights Reserved.

NOTICE: Adobe permits you to use, modify, and distribute this file in
accordance with the terms of the Adobe license agreement accompanying
it. If you have received this file from a source other than Adobe,
then your use, modification, or distribution of it requires the prior
written permission of Adobe. (See LICENSE-MIT in the root folder for details)
*/

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
        TaskCompletionSource<string> DownloadRules();
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
        TaskCompletionSource<string> GetUrlVariables();
        TaskCompletionSource<string> SyncIdentifier();
        TaskCompletionSource<string> SyncIdentifiers();
        TaskCompletionSource<string> SyncIdentifiersWithAuthState();
        // ACPLifecycle API
        TaskCompletionSource<string> GetExtensionVersionLifecycle();
        // ACPSignal API
        TaskCompletionSource<string> GetExtensionVersionSignal();
    }
}
