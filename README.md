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

# TODO

Cordova is distributed via [Node Package Management](https://www.npmjs.com/) (aka - `npm`).  

In order to install and build Cordova applications you will need to have `Node.js` installed. [Install Node.js](https://nodejs.org/en/).  

Once Node.js is installed, you can install the Cordova framework from terminal:  

```  
sudo npm install -g cordova  
```  

## Installation

# TODO

To start using the AEP SDK for Cordova, navigate to the directory of your Cordova app and install the plugin:
```
cordova plugin add https://github.com/adobe/cordova-acpgriffon.git
```
Check out the documentation for help with APIs

## Usage

# TODO

### [Core](https://aep-sdks.gitbook.io/docs/using-mobile-extensions/mobile-core)

#### Initialization

**iOS:**
```objective-c
// Import the SDK
#import "ACPCore.h"
#import "ACPLifecycle.h"
#import "ACPIdentity.h"
#import "ACPSignal.h"
- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {  
  // make sure to set the wrapper type at the beginning of initialization
  [ACPCore setWrapperType:ACPMobileWrapperTypeCordova];

  //...
  [ACPCore configureWithAppId:@"yourAppId"];
  [ACPIdentity registerExtension];
  [ACPLifecycle registerExtension];
  [ACPSignal registerExtension];
  // Register any additional extensions

  [ACPCore start:nil];
}
```

**Android:**
```java
// Import the SDK
import com.adobe.marketing.mobile.MobileCore;
import com.adobe.marketing.mobile.Identity;
import com.adobe.marketing.mobile.Lifecycle;
import com.adobe.marketing.mobile.Signal;
import com.adobe.marketing.mobile.WrapperType;

@Override
public void onCreate() {
  //...
  MobileCore.setApplication(this);
  MobileCore.configureWithAppID("yourAppId");

  // make sure to set the wrapper type at the beginning of initialization
  MobileCore.setWrapperType(WrapperType.CORDOVA);

  try {
    Identity.registerExtension();
    Lifecycle.registerExtension();
    Signal.registerExtension();

    // Register any additional extensions
  } catch (Exception e) {
    // handle exception
  }

  MobileCore.start(null);
}
```

#### Core methods

##### Getting Core version:
```js
ACPCore.extensionVersion(function(version) {
    console.log(version);
}, function(error) {
    console.log(error);
});
```

##### Updating the SDK configuration:
```js
ACPCore.updateConfiguration({"newConfigKey":"newConfigValue"}, successCallback, errorCallback);
```

##### Controlling the log level of the SDK:
```js
ACPCore.setLogLevel(ACPCore.ACPMobileLogLevelError, successCallback, errorCallback);
ACPCore.setLogLevel(ACPCore.ACPMobileLogLevelWarning, successCallback, errorCallback);
ACPCore.setLogLevel(ACPCore.ACPMobileLogLevelDebug, successCallback, errorCallback);
ACPCore.setLogLevel(ACPCore.ACPMobileLogLevelVerbose, successCallback, errorCallback);
```

##### Getting the current privacy status:
```js
ACPCore.getPrivacyStatus(function(privacyStatus) {
    console.log(privacyStatus);
}, function(error) {
    console.log(error);
});
```

##### Setting the privacy status:
```js
ACPCore.setPrivacyStatus(ACPCore.ACPMobilePrivacyStatusOptIn, successCallback, errorCallback);
ACPCore.setPrivacyStatus(ACPCore.ACPMobilePrivacyStatusOptOut, successCallback, errorCallback);
ACPCore.setPrivacyStatus(ACPCore.ACPMobilePrivacyStatusUnknown, successCallback, errorCallback);
```

##### Getting the SDK identities:
```js
ACPCore.getSdkIdentities(function(sdkIdentities) {
    console.log(sdkIdentities);
}, function(error) {
    console.log(error);
});
```

##### Dispatching an Event Hub event:
```js
var e = ACPCore.createEvent("eventName", "eventType", "eventSource", {"key":"value"});
ACPCore.dispatchEvent(e, successCallback, errorCallback);
```

##### Dispatching an Event Hub event with callback:
```js
var e = ACPCore.createEvent("eventName", "eventType", "eventSource", {"key":"value"});
ACPCore.dispatchEventWithResponseCallback(e, successCallback, errorCallback);
```

##### Dispatching an Event Hub response event:
```js
var e1 = ACPCore.createEvent("eventName", "eventType", "eventSource", {"key":"value"});
var e2 = ACPCore.createEvent("eventName2", "eventType", "eventSource", {"key":"value"});
ACPCore.dispatchResponseEvent(e1, e2, successCallback, errorCallback);
```

##### Downloading the Rules
```js
ACPCore.downloadRules(successCallback, errorCallback);
```

##### Setting the advertising identifier:
```js
ACPCore.setAdvertisingIdentifier("someAdid", successCallback, errorCallback);
```

##### Calling track action
```js
ACPCore.trackAction("cordovaAction", {"cordovaKey":"cordovaValue"}, successCallback, errorCallback);
```

##### Calling track state
```js
ACPCore.trackState("cordovaState", {"cordovaKey":"cordovaValue"}, successCallback, errorCallback);
```

### [Identity](https://aep-sdks.gitbook.io/docs/using-mobile-extensions/mobile-core/identity)

##### Getting Identity version:
```js
ACPIdentity.extensionVersion(function(version) {
    console.log(version);
}, function(error) {
    console.log(error);
});
```

##### Sync Identifier:
```js
ACPIdentity.syncIdentifier("id1", "value1", ACPIdentity.ACPMobileVisitorAuthenticationStateUnknown, successCallback, errorCallback);
```

##### Sync Identifiers:
```js
ACPIdentity.syncIdentifiers({"id2":"value2", "id3":"value3", "id4":"value4"}, successCallback, errorCallback);
```
##### Sync Identifiers with Authentication State:
```js
ACPIdentity.syncIdentifiers({"id2":"value2", "id3":"value3", "id4":"value4"}, ACPIdentity.ACPMobileVisitorAuthenticationStateLoggedOut, successCallback, errorCallback);
ACPIdentity.syncIdentifiers({"id2":"value2", "id3":"value3", "id4":"value4"}, ACPIdentity.ACPMobileVisitorAuthenticationStateAuthenticated, successCallback, errorCallback);
ACPIdentity.syncIdentifiers({"id2":"value2", "id3":"value3", "id4":"value4"}, ACPIdentity.ACPMobileVisitorAuthenticationStateUnknown, successCallback, errorCallback);
```

##### Append visitor data to a URL:
```js
ACPIdentity.appendVisitorInfoForUrl("https://www.adobe.com", successCallback, errorCallback);
```

##### Get visitor data as URL query parameter string:
```js
ACPIdentity.getUrlVariables(function(variables) {
    console.log(variables);
}, function(error) {
    console.log(error);
});
```

##### Get Identifiers:
```js
ACPIdentity.getIdentifiers(function(ids) {
    console.log(ids);
}, function(error) {
    console.log(error);
});
```

##### Get Experience Cloud IDs:
```js
ACPIdentity.getExperienceCloudId(function(cloudId) {
    console.log(cloudId);
}, function(error) {
    console.log(error);
});
```

### [Lifecycle](https://aep-sdks.gitbook.io/docs/using-mobile-extensions/mobile-core/lifecycle)
> Note: We recommend implementing Lifecycle in native [Android and iOS code](https://aep-sdks.gitbook.io/docs/using-mobile-extensions/mobile-core/lifecycle).

##### Getting Lifecycle version:
```js
ACPLifecycle.extensionVersion(function(version) {
    console.log(version);
}, function(error) {
    console.log(error);
});
 ```

### [Signal](https://aep-sdks.gitbook.io/docs/using-mobile-extensions/mobile-core/signals)

##### Getting Signal version:
```js
ACPSignal.extensionVersion(function(version) {
    console.log(version);
}, function(error) {
    console.log(error);
});
```

## Running Tests
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

A Cordova app for testing the plugin is located in the `https://github.com/adobe/cordova-acpsample`. The app is configured for both iOS and Android platforms.  

## Contributing
Looking to contribute to this project? Please review our [Contributing guidelines](.github/CONTRIBUTING.md) prior to opening a pull request.

We look forward to working with you!

## Licensing
This project is licensed under the Apache V2 License. See [LICENSE](LICENSE) for more information.
