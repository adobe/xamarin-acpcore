# Adobe Experience Platform - Core plugin for Xamarin apps
![CI](https://github.com/adobe/xamarin-acpcore/workflows/CI/badge.svg)
[![ACPCore.Android](https://buildstats.info/nuget/Adobe.ACPCore.Android)](https://www.nuget.org/packages/Adobe.ACPCore.Android/)
[![ACPIdentity.Android](https://buildstats.info/nuget/Adobe.ACPIdentity.Android)](https://www.nuget.org/packages/Adobe.ACPIdentity.Android/)
[![ACPLifecycle.Android](https://buildstats.info/nuget/Adobe.ACPLifecycle.Android)](https://www.nuget.org/packages/Adobe.ACPLifecycle.Android/)
[![ACPSignal.Android](https://buildstats.info/nuget/Adobe.ACPSignal.Android)](https://www.nuget.org/packages/Adobe.ACPSignal.Android/)
[![ACPCore.iOS](https://buildstats.info/nuget/Adobe.ACPCore.iOS)](https://www.nuget.org/packages/Adobe.ACPCore.iOS/)
[![GitHub](https://img.shields.io/github/license/adobe/xamarin-acpcore)](https://github.com/adobe/xamarin-acpcore/blob/master/LICENSE)

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Running Tests](#running-tests)
- [Sample App](#sample-app)
- [Contributing](#contributing)
- [Licensing](#licensing)

## Prerequisites

Xamarin development requires the installation of [Microsoft Visual Studio](https://visualstudio.microsoft.com/downloads/). Information regarding installation for Xamarin development is available for [Mac](https://docs.microsoft.com/en-us/visualstudio/mac/installation?view=vsmac-2019) or [Windows](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2019).

 An [Apple developer account](https://developer.apple.com/programs/enroll/) and the latest version of Xcode (available from the App Store) are required if you are [building an iOS app](https://docs.microsoft.com/en-us/visualstudio/mac/installation?view=vsmac-2019).

## Installation

**Package Manager Installation**

The ACPCore Xamarin NuGet package for Android or iOS can be added to your project by right clicking the _"Packages"_ folder within the project you are working on then selecting _"Manage NuGet Packages"_. In the window that opens, ensure that your selected source is `nuget.org` and search for _"Adobe.ACP"_. After selecting the Xamarin AEP SDK packages that are required, click on the _"Add Packages"_ button. After exiting the _"Add Packages"_ menu, right click the main solution or the _"Packages"_ folder and select _"Restore"_ to ensure the added packages are downloaded.

**Manual installation**

Local ACPCore NuGet packages can be created via the included Makefile. If building for the first time, run:

```
make setup
```

followed by:

```
make release
```

The created NuGet packages can be found in the `bin` directory. This directory can be added as a local nuget source and packages within the directory can be added to a Xamarin project following the steps in the "Package Manager Installation" above.

## Usage

The ACPCore binding can be opened by loading the ACPCoreBinding.sln with Visual Studio. The following targets are available in the solution:

- Adobe.ACPCore.iOS - The ACPCore iOS bindings which includes ACPCore, ACPIdentity, ACPLifecycle, and ACPSignal.
- Adobe.ACPCore.Android - The ACPCore Android binding.
- Adobe.ACPCoreBridge.Android - The ACPCoreBridge Android binding. This is used by the Xamarin Android ACPGriffon binding to correctly start a Griffon session from within an app activity.
- Adobe.ACPCIdentity.Android - The ACPIdentity Android binding.
- Adobe.ACPLifecycle.Android - The ACPLifecycle Android binding.
- Adobe.ACPSIgnal.Android - The ACPSignal Android binding.
- ACPCoreTestApp - The Xamarin.Forms base app used by the iOS and Android test apps.
- ACPCoreTestApp.iOS - The Xamarin.Forms based iOS manual test app.
- ACPCoreTestApp.Android - The Xamarin.Forms based Android manual test app.
- ACPCoreiOSUnitTests - iOS unit test app.
- ACPCoreAndroidUnitTests - Android unit test app.

### [Core](https://aep-sdks.gitbook.io/docs/using-mobile-extensions/mobile-core)

#### Initialization

**iOS:**
```c#
// Import the SDK
using Com.Adobe.Marketing.Mobile;

public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
  global::Xamarin.Forms.Forms.Init();
  LoadApplication(new App());

  // set the wrapper type
  ACPCore.SetWrapperType(ACPMobileWrapperType.Xamarin);
  
  // set launch config
  ACPCore.ConfigureWithAppID("yourAppId");

  // register SDK extensions
  ACPIdentity.RegisterExtension();
  ACPLifecycle.RegisterExtension();
  ACPSignal.RegisterExtension();

  // start core
  ACPCore.Start(null);

  // register dependency service to link interface from App base project
  DependencyService.Register<IExtensionService, ExtensionService>();
  return base.FinishedLaunching(app, options);
}
```

**Android:**

```c#
// Import the SDK
using Com.Adobe.Marketing.Mobile;

protected override void OnCreate(Bundle savedInstanceState)
{
  TabLayoutResource = Resource.Layout.Tabbar;
  ToolbarResource = Resource.Layout.Toolbar;
  
  base.OnCreate(savedInstanceState);

  global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
  LoadApplication(new App());
  
  // set the wrapper type
  ACPCore.SetWrapperType(WrapperType.Xamarin);
  
  // set launch config
  ACPCore.ConfigureWithAppID("yourAppId");

  // register SDK extensions
  ACPCore.Application = this.Application;
  ACPIdentity.RegisterExtension();
  ACPLifecycle.RegisterExtension();
  ACPSignal.RegisterExtension();

  // start core
  ACPCore.Start(null);

  // register dependency service to link interface from App base project
  DependencyService.Register<IExtensionService, ExtensionService>();
}
```

#### Core methods

##### Getting Core version:

**iOS and Android**

```c#
Console.WriteLine(ACPCore.ExtensionVersion());
```

##### Updating the SDK configuration:

**iOS**

```c#
var config = new NSMutableDictionary<NSString, NSObject>
{
  ["someConfigKey"] = new NSString("configValue")
};
ACPCore.UpdateConfiguration(config);
```

**Android**

```c#
var config = new Dictionary<string, Java.Lang.Object>();
config.Add("someConfigKey", "configValue");
ACPCore.UpdateConfiguration(config);
```

##### Controlling the log level of the SDK:

**iOS**

```c#
ACPCore.LogLevel = ACPMobileLogLevel.Error;
ACPCore.LogLevel = ACPMobileLogLevel.Warning;
ACPCore.LogLevel = ACPMobileLogLevel.Debug;
ACPCore.LogLevel = ACPMobileLogLevel.Verbose;
```

**Android**

```c#
ACPCore.LogLevel = LoggingMode.Error;
ACPCore.LogLevel = LoggingMode.Warning;
ACPCore.LogLevel = LoggingMode.Debug;
ACPCore.LogLevel = LoggingMode.Verbose;
```

##### Getting the current privacy status:

**iOS**

```c#
var callback = new Action<ACPMobilePrivacyStatus>(handleCallback);
ACPCore.GetPrivacyStatus(callback);

private void handleCallback(ACPMobilePrivacyStatus privacyStatus)
{
  Console.WriteLine("Privacy status: " + privacyStatus.ToString());
}
```

**Android**

```c#
ACPCore.GetPrivacyStatus(new StringCallback());

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
```

##### Setting the privacy status:

**iOS**

```c#
ACPCore.SetPrivacyStatus(ACPMobilePrivacyStatus.OptIn);
```

**Android**

```c#
ACPCore.SetPrivacyStatus(MobilePrivacyStatus.OptIn);
```

##### Getting the SDK identities:

**iOS**

```c#
var callback = new Action<NSString>(handleCallback);
ACPCore.GetSdkIdentities(callback);

private void handleCallback(NSString content)
{
  Console.WriteLine("String callback: " + content);
}
```

**Android**

```c#
ACPCore.GetSdkIdentities(new StringCallback());

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
```

##### Dispatching an Event Hub event:

**iOS**

```c#
var data = new NSMutableDictionary<NSString, NSObject>
{
  ["dispatchResponseEventKey"] = new NSString("dispatchResponseEventValue")
};
ACPExtensionEvent sdkEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
ACPCore.DispatchEvent(sdkEvent, out error);
```

**Android**

```c#
var data = new Dictionary<string, Java.Lang.Object>();
data.Add("testEvent", true);
Event sdkEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
ACPCore.DispatchEvent(sdkEvent, new ErrorCallback());

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
```

##### Dispatching an Event Hub event with callback:

**iOS**

```c#
var data = new NSMutableDictionary<NSString, NSObject>
{
  ["dispatchResponseEventKey"] = new NSString("dispatchResponseEventValue")
};
ACPExtensionEvent sdkEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
Action<ACPExtensionEvent> callback = new Action<ACPExtensionEvent>(handleCallback);
ACPCore.DispatchEventWithResponseCallback(sdkEvent, callback, out error);

private void handleCallback(ACPExtensionEvent responseEvent)
{
  Console.WriteLine("Response event name: "+ responseEvent.EventName.ToString() + " type: " + responseEvent.EventType.ToString() + " source: " + responseEvent.EventSource.ToString() + " data: " + responseEvent.EventData.ToString());
}
```

**Android**

```c#
var data = new Dictionary<string, Java.Lang.Object>();
data.Add("testEvent", true);
Event sdkEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
ACPCore.DispatchEventWithResponseCallback(sdkEvent, new StringCallback(), new ErrorCallback());

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
```

##### Dispatching an Event Hub response event:

**iOS**

```c#
var data = new NSMutableDictionary<NSString, NSObject>
{
  ["dispatchResponseEventKey"] = new NSString("dispatchResponseEventValue")
};
ACPExtensionEvent requestEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
ACPExtensionEvent responseEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
ACPCore.DispatchResponseEvent(responseEvent, requestEvent, out error));
```

**Android**

```c#
var data = new Dictionary<string, Java.Lang.Object>();
data.Add("testDispatchResponseEvent", "true");
Event requestEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
Event responseEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
ACPCore.DispatchResponseEvent(responseEvent, requestEvent, new ErrorCallback());

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
```

##### Downloading the Rules

**iOS only**

```c#
ACPCore.DownloadRules();
```

##### Setting the advertising identifier:

**iOS and Android**

```c#
ACPCore.SetAdvertisingIdentifier("testAdvertisingIdentifier");
```

##### Calling track action

**iOS**

```c#
var data = new NSMutableDictionary<NSString, NSString>
{
  ["key"] = new NSString("value")
};
ACPCore.TrackAction("action", data);
```

**Android**

```c#
var data = new Dictionary<string, string>();
data.Add("key", "value");
ACPCore.TrackAction("action", data);
```

##### Calling track state

**iOS**

```c#
var data = new NSMutableDictionary<NSString, NSString>
{
  ["key"] = new NSString("value")
};
ACPCore.TrackState("state", data);
```

**Android**

```c#
var data = new Dictionary<string, string>();
data.Add("key", "value");
ACPCore.TrackState("state", data);
```

### [Identity](https://aep-sdks.gitbook.io/docs/using-mobile-extensions/mobile-core/identity)

##### Getting Identity version:

**iOS and Android**

```c#
Console.WriteLine(ACPIdentity.ExtensionVersion());
```

##### Sync Identifier:

**iOS**

```c#
ACPIdentity.SyncIdentifier("name", "john", ACPMobileVisitorAuthenticationState.Authenticated);
```

**Android**

```c#
ACPIdentity.SyncIdentifier("name", "john", VisitorID.AuthenticationState.Authenticated);
```

##### Sync Identifiers:

**iOS**

```c#
var ids = new NSMutableDictionary<NSString, NSObject>
{
  ["lastName"] = new NSString("doe"),
  ["age"] = new NSString("38"),
  ["zipcode"] = new NSString("94403")
};
ACPIdentity.SyncIdentifiers(ids);
```

**Android**

```c#
var ids = new Dictionary<string, string>();
ids.Add("lastname", "doe");
ids.Add("age", "38");
ids.Add("zipcode", "94403");
ACPIdentity.SyncIdentifiers(ids);
```

##### Sync Identifiers with Authentication State:

**iOS**

```c#
var ids = new NSMutableDictionary<NSString, NSObject>
{
  ["lastName"] = new NSString("doe"),
  ["age"] = new NSString("38"),
  ["zipcode"] = new NSString("94403")
};
ACPIdentity.SyncIdentifiers(ids, ACPMobileVisitorAuthenticationState.LoggedOut);
```

**Android**

```c#
var ids = new Dictionary<string, string>();
ids.Add("lastname", "doe");
ids.Add("age", "38");
ids.Add("zipcode", "94403");
ACPIdentity.SyncIdentifiers(ids, VisitorID.AuthenticationState.LoggedOut);
```

##### Append visitor data to a URL:

**iOS**

```c#
var callback = new Action<NSUrl>(handleCallback);
var url = new NSUrl("https://example.com");
ACPIdentity.AppendToUrl(url, callback);

private void handleCallback(NSString content)
{
  Console.WriteLine("String callback: " + content);
}
```

**Android**

```c#
ACPIdentity.AppendVisitorInfoForURL("https://example.com", new StringCallback());

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
```

##### Get visitor data as URL query parameter string:

**iOS**

```c#
var callback = new Action<NSString> (handleCallback);
ACPIdentity.GetUrlVariables(callback);

private void handleCallback(NSString content)
{
  Console.WriteLine("String callback: " + content);
}
```

**Android**

```c#
ACPIdentity.GetUrlVariables(new StringCallback());

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
```

##### Get Identifiers:

**iOS**

```c#
var callback = new Action<ACPMobileVisitorId[]>(handleCallback);
ACPIdentity.GetIdentifiers(callback);

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
```

**Android**

```c#
ACPIdentity.GetIdentifiers(new GetIdentifiersCallback());

class GetIdentifiersCallback : Java.Lang.Object, IAdobeCallback
{
  public void Call(Java.Lang.Object visitorIDs)
  {
    JavaList ids = null;
    System.String visitorIdsString = "[]";
    if (visitorIDs != null)
    {
      ids = (JavaList)visitorIDs;
      if (!ids.IsEmpty)
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
```

##### Get Experience Cloud IDs:

**iOS**

```c#
var callback = new Action<NSString>(handleCallback);
ACPIdentity.GetExperienceCloudId(callback);

private void handleCallback(NSString content)
{
  Console.WriteLine("String callback: " + content);
}
```

**Android**

```c#
ACPIdentity.GetExperienceCloudId(new StringCallback());

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
```

### [Lifecycle](https://aep-sdks.gitbook.io/docs/using-mobile-extensions/mobile-core/lifecycle)

##### Getting Lifecycle version:

**iOS and Android**

```c#
Console.WriteLine(ACPLifecycle.ExtensionVersion());
```

##### Starting a Lifecycle session

**iOS**

```c#
public override void OnActivated(UIApplication uiApplication)
{
  base.OnActivated(uiApplication);
  ACPCore.LifecycleStart(null);
}
```

**Android**

```c#
 protected override void OnResume()
 {
   base.OnResume();
   ACPCore.Application = this.Application;
   ACPCore.LifecycleStart(null);
 }
```

**Pausing a Lifecycle session**

**iOS**

```c#
public override void OnResignActivation(UIApplication uiApplication)
{
  base.OnResignActivation(uiApplication);
  ACPCore.LifecyclePause();
}
```

**Android**

```c#
 protected override void OnPause()
 {
   base.OnPause();
   ACPCore.LifecyclePause();
 }
```

### [Signal](https://aep-sdks.gitbook.io/docs/using-mobile-extensions/mobile-core/signals)

##### Getting Signal version:

**iOS and Android**

```c#
Console.WriteLine(ACPSignal.ExtensionVersion());
```

##### Running Tests

iOS and Android unit tests are included within the ACPCore binding solution. They must be built from within Visual Studio then manually triggered from the unit test app that is deployed to an iOS or Android device.

## Sample App

A Xamarin Forms sample app is provided in the Xamarin ACPCore solution file.

## Contributing
Looking to contribute to this project? Please review our [Contributing guidelines](.github/CONTRIBUTING.md) prior to opening a pull request.

We look forward to working with you!

## Licensing
This project is licensed under the Apache V2 License. See [LICENSE](LICENSE) for more information.
