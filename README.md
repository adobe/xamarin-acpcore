# Adobe Experience Platform - Core plugin for Xamarin apps
[![CI](https://github.com/adobe/xamarin-acpcore/workflows/CI/badge.svg)](https://github.com/adobe/xamarin-acpcore/actions)
[![npm](https://img.shields.io/npm/v/@adobe/xamarin-acpcore)](https://www.npmjs.com/package/@adobe/xamarin-acpcore)
[![GitHub](https://img.shields.io/github/license/adobe/xamarin-acpcore)](https://github.com/adobe/xamarin-acpcore/blob/master/LICENSE)

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Running Tests](#running-tests)
- [Sample App](#sample-app)
- [Contributing](#contributing)
- [Licensing](#licensing)

## Prerequisites

Xamarin development requires the installation of [Microsoft Visual Studio](https://visualstudio.microsoft.com/downloads/).

## Installation

# TODO

To start using the AEP SDK for Cordova, navigate to the directory of your Cordova app and install the plugin:
```
cordova plugin add https://github.com/adobe/cordova-acpgriffon.git
```
Check out the documentation for help with APIs

## Usage

### [Core](https://aep-sdks.gitbook.io/docs/using-mobile-extensions/mobile-core)

The following usage instructions assume [Xamarin Forms](https://dotnet.microsoft.com/apps/xamarin/xamarin-forms) is being used to develop a multiplatform mobile app. Some API calls have differences between iOS and Android. For these cases, the usage example is given for each platform.

#### Initialization

**iOS:**
```c#
// Import the SDK
using Com.Adobe.Marketing.Mobile;

public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
  global::Xamarin.Forms.Forms.Init();
  LoadApplication(new App());

  // set launch config
  ACPCore.ConfigureWithAppId("yourAppId");

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
  
  // set launch config
  ACPCore.ConfigureWithAppId("yourAppId");

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
public TaskCompletionSource<string> GetExtensionVersionCore()
{
  stringOutput = new TaskCompletionSource<string>();
  stringOutput.SetResult(ACPCore.ExtensionVersion());
  return stringOutput;
}
```

##### Updating the SDK configuration:

**iOS**

```c#
public TaskCompletionSource<string> UpdateConfig()
{
  stringOutput = new TaskCompletionSource<string>();
  var config = new NSMutableDictionary<NSString, NSObject>
  {
    ["someConfigKey"] = new NSString("configValue")
  };
  ACPCore.UpdateConfiguration(config);
  stringOutput.SetResult("completed");
  return stringOutput;
}
```

**Android**

```c#
public TaskCompletionSource<string> UpdateConfig()
{
  stringOutput = new TaskCompletionSource<string>();
  Dictionary<string, Java.Lang.Object> config = new Dictionary<string, Java.Lang.Object>();
  config.Add("someConfigKey", "configValue");
  ACPCore.UpdateConfiguration(config);
  stringOutput.SetResult("completed");
  return stringOutput;
}
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
public TaskCompletionSource<string> GetPrivacyStatus()
{
  stringOutput = new TaskCompletionSource<string>();
  Action<ACPMobilePrivacyStatus> callback = new Action<ACPMobilePrivacyStatus>(handleCallback);
  ACPCore.GetPrivacyStatus(callback);
  stringOutput.SetResult("completed");
  return stringOutput;
}

private void handleCallback(ACPMobilePrivacyStatus privacyStatus)
{
  Console.WriteLine("Privacy status: " + privacyStatus.ToString());
}
```

**Android**

```c#
public TaskCompletionSource<string> GetPrivacyStatus()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPCore.GetPrivacyStatus(new StringCallback());
  stringOutput.SetResult("completed");
  return stringOutput;
}

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
public TaskCompletionSource<string> SetPrivacyStatus()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPCore.SetPrivacyStatus(ACPMobilePrivacyStatus.OptIn);
  stringOutput.SetResult("completed");
  return stringOutput;
}
```

**Android**

```c#
public TaskCompletionSource<string> SetPrivacyStatus()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPCore.SetPrivacyStatus(MobilePrivacyStatus.OptIn);
  stringOutput.SetResult("completed");
  return stringOutput;
}
```

##### Getting the SDK identities:

**iOS**

```c#
public TaskCompletionSource<string> GetSDKIdentities()
{
  stringOutput = new TaskCompletionSource<string>();
  Action<NSString> callback = new Action<NSString>(handleCallback);
  ACPCore.GetSdkIdentities(callback);
  stringOutput.SetResult("completed");
  return stringOutput;
}

private void handleCallback(NSString content)
{
  Console.WriteLine("String callback: " + content);
}
```

**Android**

```c#
public TaskCompletionSource<string> GetSDKIdentities()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPCore.GetSdkIdentities(new StringCallback());
  stringOutput.SetResult("completed");
  return stringOutput;
}

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
public TaskCompletionSource<string> DispatchEvent()
{
  stringOutput = new TaskCompletionSource<string>();
  NSError error;
  var data = new NSMutableDictionary<NSString, NSObject>
  {
    ["dispatchResponseEventKey"] = new NSString("dispatchResponseEventValue")
  };
  ACPExtensionEvent sdkEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
  stringOutput.SetResult(ACPCore.DispatchEvent(sdkEvent, out error).ToString());
  if (error != null)
  {
    stringOutput.SetResult(error.LocalizedDescription);
  }
  return stringOutput;
}
```

**Android**

```c#
public TaskCompletionSource<string> DispatchEvent()
{
  stringOutput = new TaskCompletionSource<string>();
  IDictionary<string, Java.Lang.Object> data = new Dictionary<string, Java.Lang.Object>();
  data.Add("testEvent", true);
  Event sdkEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
  stringOutput.SetResult(ACPCore.DispatchEvent(sdkEvent, new ErrorCallback()).ToString());
  return stringOutput;
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

##### Dispatching an Event Hub event with callback:

**iOS**

```c#
public TaskCompletionSource<string> DispatchEventWithResponseCallback()
{
  stringOutput = new TaskCompletionSource<string>();
  NSError error;
  var data = new NSMutableDictionary<NSString, NSObject>
  {
    ["dispatchResponseEventKey"] = new NSString("dispatchResponseEventValue")
  };
  ACPExtensionEvent sdkEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
  Action<ACPExtensionEvent> callback = new Action<ACPExtensionEvent>(handleCallback);
  stringOutput.SetResult(ACPCore.DispatchEventWithResponseCallback(sdkEvent, callback, out error).ToString());
  if(error != null)
  {
    stringOutput.SetResult(error.LocalizedDescription);
  }
  return stringOutput;
}

private void handleCallback(ACPExtensionEvent responseEvent)
{
  Console.WriteLine("Response event name: "+ responseEvent.EventName.ToString() + " type: " + responseEvent.EventType.ToString() + " source: " + responseEvent.EventSource.ToString() + " data: " + responseEvent.EventData.ToString());
}
```

**Android**

```c#
public TaskCompletionSource<string> DispatchEventWithResponseCallback()
{
  stringOutput = new TaskCompletionSource<string>();
  IDictionary<string, Java.Lang.Object> data = new Dictionary<string, Java.Lang.Object>();
  data.Add("testEvent", true);
  Event sdkEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
  stringOutput.SetResult(ACPCore.DispatchEventWithResponseCallback(sdkEvent, new StringCallback(), new ErrorCallback()).ToString());
  return stringOutput;
}

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
public TaskCompletionSource<string> DispatchResponseEvent()
{
  stringOutput = new TaskCompletionSource<string>();
  NSError error;
  var data = new NSMutableDictionary<NSString, NSObject>
  {
    ["dispatchResponseEventKey"] = new NSString("dispatchResponseEventValue")
  };
  ACPExtensionEvent requestEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
  ACPExtensionEvent responseEvent = ACPExtensionEvent.ExtensionEventWithName("eventName", "eventType", "eventSource", data, out _);
  stringOutput.SetResult(ACPCore.DispatchResponseEvent(responseEvent, requestEvent, out error).ToString());
  if (error != null)
  {
    stringOutput.SetResult(error.LocalizedDescription);
  }
  return stringOutput;
}
```

**Android**

```c#
public TaskCompletionSource<string> DispatchResponseEvent()
{
  stringOutput = new TaskCompletionSource<string>();
  IDictionary<string, Java.Lang.Object> data = new Dictionary<string, Java.Lang.Object>();
  data.Add("testDispatchResponseEvent", "true");
  Event requestEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
  Event responseEvent = new Event.Builder("eventName", "eventType", "eventSource").SetEventData(data).Build();
  stringOutput.SetResult(ACPCore.DispatchResponseEvent(responseEvent, requestEvent, new ErrorCallback()).ToString());
  return stringOutput;
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

##### Downloading the Rules

**iOS only**

```c#
public TaskCompletionSource<string> DownloadRules()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPCore.DownloadRules();
  stringOutput.SetResult("completed");
  return stringOutput;
}
```

##### Setting the advertising identifier:

**iOS and Android**

```c#
public TaskCompletionSource<string> SetAdvertisingIdentifier()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPCore.SetAdvertisingIdentifier("testAdvertisingIdentifier");
  stringOutput.SetResult("completed");
  return stringOutput;
}
```

##### Calling track action

**iOS**

```c#
public TaskCompletionSource<string> TrackAction()
{
  stringOutput = new TaskCompletionSource<string>();
  var data = new NSMutableDictionary<NSString, NSString>
  {
    ["key"] = new NSString("value")
  };
  ACPCore.TrackAction("action", data);
  stringOutput.SetResult("completed");
  return stringOutput;
}
```

**Android**

```c#
public TaskCompletionSource<string> TrackAction()
{
  stringOutput = new TaskCompletionSource<string>();
  Dictionary<string, string> data = new Dictionary<string, string>();
  data.Add("key", "value");
  ACPCore.TrackAction("action", data);
  stringOutput.SetResult("completed");
  return stringOutput;
}
```

##### Calling track state

**iOS**

```c#
public TaskCompletionSource<string> TrackState()
{
  stringOutput = new TaskCompletionSource<string>();
  var data = new NSMutableDictionary<NSString, NSString>
  {
    ["key"] = new NSString("value")
  };
  ACPCore.TrackState("state", data);
  stringOutput.SetResult("completed");
  return stringOutput;
}
```

**Android**

```c#
public TaskCompletionSource<string> TrackState()
{
  stringOutput = new TaskCompletionSource<string>();
  Dictionary<string, string> data = new Dictionary<string, string>();
  data.Add("key", "value");
  ACPCore.TrackState("state", data);
  stringOutput.SetResult("completed");
  return stringOutput;
}
```

### [Identity](https://aep-sdks.gitbook.io/docs/using-mobile-extensions/mobile-core/identity)

##### Getting Identity version:

##### **iOS and Android**

```c#
public TaskCompletionSource<string> GetExtensionVersionIdentity()
{
  stringOutput = new TaskCompletionSource<string>();
  stringOutput.SetResult(ACPIdentity.ExtensionVersion());
  return stringOutput;
}
```

##### Sync Identifier:

**iOS**

```c#
public TaskCompletionSource<string> SyncIdentifier()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPIdentity.SyncIdentifier("name", "john", ACPMobileVisitorAuthenticationState.Authenticated);
  stringOutput.SetResult("completed");
  return stringOutput;
}
```

**Android**

```c#
public TaskCompletionSource<string> SyncIdentifier()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPIdentity.SyncIdentifier("name", "john", VisitorID.AuthenticationState.Authenticated);
  stringOutput.SetResult("completed");
  return stringOutput;
}
```

##### Sync Identifiers:

**iOS**

```c#
public TaskCompletionSource<string> SyncIdentifiers()
{
  stringOutput = new TaskCompletionSource<string>();
  var ids = new NSMutableDictionary<NSString, NSObject>
  {
    ["lastName"] = new NSString("doe"),
    ["age"] = new NSString("38"),
    ["zipcode"] = new NSString("94403")
  };
  ACPIdentity.SyncIdentifiers(ids);
  stringOutput.SetResult("completed");
  return stringOutput;
}
```

**Android**

```c#
public TaskCompletionSource<string> SyncIdentifiers()
{
  stringOutput = new TaskCompletionSource<string>();
  Dictionary<string, string> ids = new Dictionary<string, string>();
  ids.Add("lastname", "doe");
  ids.Add("age", "38");
  ids.Add("zipcode", "94403");
  ACPIdentity.SyncIdentifiers(ids);
  stringOutput.SetResult("completed");
  return stringOutput;
}
```

##### Sync Identifiers with Authentication State:

**iOS**

```c#
public TaskCompletionSource<string> SyncIdentifiers()
{
  stringOutput = new TaskCompletionSource<string>();
  var ids = new NSMutableDictionary<NSString, NSObject>
  {
    ["lastName"] = new NSString("doe"),
    ["age"] = new NSString("38"),
    ["zipcode"] = new NSString("94403")
  };
  ACPIdentity.SyncIdentifiers(ids, ACPMobileVisitorAuthenticationState.LoggedOut);
  stringOutput.SetResult("completed");
  return stringOutput;
}
```

**Android**

```c#
public TaskCompletionSource<string> SyncIdentifiers()
{
  stringOutput = new TaskCompletionSource<string>();
  Dictionary<string, string> ids = new Dictionary<string, string>();
  ids.Add("lastname", "doe");
  ids.Add("age", "38");
  ids.Add("zipcode", "94403");
  ACPIdentity.SyncIdentifiers(ids, VisitorID.AuthenticationState.LoggedOut);
  stringOutput.SetResult("completed");
  return stringOutput;
}
```

##### Append visitor data to a URL:

**iOS**

```c#
public TaskCompletionSource<string> AppendVisitorInfoForUrl()
{
  stringOutput = new TaskCompletionSource<string>();
  Action<NSUrl> callback = new Action<NSUrl>(handleCallback);
  var url = new NSUrl("https://example.com");
  ACPIdentity.AppendToUrl(url, callback);
  stringOutput.SetResult("");
  return stringOutput;
}

private void handleCallback(NSString content)
{
  Console.WriteLine("String callback: " + content);
}
```

**Android**

```c#
public TaskCompletionSource<string> AppendVisitorInfoForUrl()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPIdentity.AppendVisitorInfoForURL("https://example.com", new StringCallback());
  stringOutput.SetResult("");
  return stringOutput;
}

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
public TaskCompletionSource<string> GetUrlVariables()
{
  stringOutput = new TaskCompletionSource<string>();
  Action<NSString> callback = new Action<NSString> (handleCallback);
  ACPIdentity.GetUrlVariables(callback);
  stringOutput.SetResult("");
  return stringOutput;
}

private void handleCallback(NSString content)
{
  Console.WriteLine("String callback: " + content);
}
```

**Android**

```c#
public TaskCompletionSource<string> GetUrlVariables()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPIdentity.GetUrlVariables(new StringCallback());
  stringOutput.SetResult("");
  return stringOutput;
}

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
public TaskCompletionSource<string> GetIdentifiers()
{
  stringOutput = new TaskCompletionSource<string>();
  Action<ACPMobileVisitorId[]> callback = new Action<ACPMobileVisitorId[]>(handleCallback);
  ACPIdentity.GetIdentifiers(callback);
  stringOutput.SetResult("");
  return stringOutput;
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
```

**Android**

```c#
public TaskCompletionSource<string> GetIdentifiers()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPIdentity.GetIdentifiers(new GetIdentifiersCallback());
  stringOutput.SetResult("");
  return stringOutput;
}

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
public TaskCompletionSource<string> GetExperienceCloudId()
{
  stringOutput = new TaskCompletionSource<string>();
  Action<NSString> callback = new Action<NSString>(handleCallback);
  ACPIdentity.GetExperienceCloudId(callback);
  stringOutput.SetResult("");
  return stringOutput;
}

private void handleCallback(NSString content)
{
  Console.WriteLine("String callback: " + content);
}
```

**Android**

```c#
public TaskCompletionSource<string> GetExperienceCloudId()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPIdentity.GetExperienceCloudId(new StringCallback());
  stringOutput.SetResult("");
  return stringOutput;
}

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

### [Lifecycle](https://aep-sdks.gitbook.io/docs/using-mobile-extensions/mobile-core/lifecycle)

##### Getting Lifecycle version:

**iOS and Android**

```c#
public TaskCompletionSource<string> GetExtensionVersionLifecycle()
{
  stringOutput = new TaskCompletionSource<string>();
  stringOutput.SetResult(ACPLifecycle.ExtensionVersion());
  return stringOutput;
}
```

**Starting a Lifecycle session**

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
public TaskCompletionSource<string> GetExtensionVersionSignal()
{
  stringOutput = new TaskCompletionSource<string>();
  stringOutput.SetResult(ACPSignal.ExtensionVersion());
  return stringOutput;
}
```

## Running Tests

# TODO

Install cordova-paramedic `https://github.com/apache/cordova-paramedic`
```bash
npm install -g cordova-paramedic
```
Run the tests
```
cordova-paramedic --platform ios --plugin . --verbose
```
```
cordova-paramedic --platform android --plugin . --verbose
```

## Sample App

A Xamarin Forms sample app is provided in the Xamarin ACPCore solution file.

## Contributing
Looking to contribute to this project? Please review our [Contributing guidelines](.github/CONTRIBUTING.md) prior to opening a pull request.

We look forward to working with you!

## Licensing
This project is licensed under the Apache V2 License. See [LICENSE](LICENSE) for more information.
