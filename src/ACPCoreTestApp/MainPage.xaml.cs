using System;
using System.IO;
using System.ComponentModel;
using Xamarin.Forms;


namespace ACPCoreTestApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // ACPCore API
        void OnCoreExtensionVersionButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().GetExtensionVersionCore().Task.Result;
            handleStringResult("GetExtensionVersionCore", result);
        }

        void OnDispatchEventButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().DispatchEvent().Task.Result;
            handleStringResult("DispatchEvent", result);

        }

        void OnDispatchEventWithResponseCallbackButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().DispatchEventWithResponseCallback().Task.Result;
            handleStringResult("DispatchEventWithResponseCallback", result);
        }

        void OnDispatchResponseEventButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().DispatchResponseEvent().Task.Result;
            handleStringResult("DispatchResponseEvent", result);

        }

        void OnGetPrivacyStatusButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().GetPrivacyStatus().Task.Result;
            handleStringResult("GetPrivacyStatus", result);
        }

        void OnGetSDKIdentitiesButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().GetSDKIdentities().Task.Result;
            handleStringResult("GetSDkIdentities", result);
        }

        void OnSetAdvertisingIdentifierButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().SetAdvertisingIdentifier().Task.Result;
            handleStringResult("SetAdvertisingIdentifier", result);
        }

        void OnSetLogLevelButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().SetLogLevel().Task.Result;
            handleStringResult("SetLogLevel", result);
        }

        void OnSetPrivacyStatusButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().SetPrivacyStatus().Task.Result;
            handleStringResult("SetPrivacyStatus", result);
        }

        void OnTrackActionButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().TrackAction().Task.Result;
            handleStringResult("TrackAction", result);
        }

        void OnTrackStateButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().TrackState().Task.Result;
            handleStringResult("TrackState", result);
        }

        void OnUpdateConfigurationButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().UpdateConfig().Task.Result;
            handleStringResult("UpdateConfig", result);
        }

        // ACPIdentity API
        void OnIdentityExtensionVersionButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().GetExtensionVersionIdentity().Task.Result;
            handleStringResult("GetExtensionVersionIdentity", result);
        }

        void OnAppendVisitorInformationButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().AppendVisitorInfoForUrl().Task.Result;
            handleStringResult("AppendVisitorInfoForUrl", result);
        }

        void OnGetExperienceCloudIdButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().GetExperienceCloudId().Task.Result;
            handleStringResult("GetExperienceCloudId", result);
        }

        void OnGetIdentifiersButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().GetIdentifiers().Task.Result;
            handleStringResult("GetIdentifiers", result);
        }

        void OnGetURLVariablesButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().GetURLVariables().Task.Result;
            handleStringResult("GetURLVariables", result);
        }

        void OnSyncIdentifierButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().SyncIdentifier().Task.Result;
            handleStringResult("SyncIdentifier", result);
        }

        void OnSyncIdentifiersButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().SyncIdentifiers().Task.Result;
            handleStringResult("SyncIdentifiers", result);
        }

        void OnSyncIdentifiersWithAuthStateButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().SyncIdentifiersWithAuthState().Task.Result;
            handleStringResult("SyncIdentifiersWithAuthState", result);
        }

        // ACPLifecycle API
        void OnLifecycleExtensionVersionButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().GetExtensionVersionLifecycle().Task.Result;
            handleStringResult("GetExtensionVersionLifecycle", result);
        }

        // ACPSignal API
        void OnSignalExtensionVersionButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPCoreExtensionService>().GetExtensionVersionSignal().Task.Result;
            handleStringResult("GetExtensionVersionSignal", result);
        }

        // helper methods
        private void handleStringResult(string apiName, string result)
        {
            if (result != null)
            {
                Console.WriteLine(apiName + ": " +result);
            }
        }
    }
}
