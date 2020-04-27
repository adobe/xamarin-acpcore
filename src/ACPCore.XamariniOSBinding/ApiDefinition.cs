using System;
using Foundation;
using ObjCRuntime;

namespace Com.Adobe.Marketing.Mobile
{
	// @interface ACPCore : NSObject
	[BaseType(typeof(NSObject))]
	interface ACPCore
	{
		// +(void)configureWithAppId:(NSString * _Nullable)appid;
		[Static]
		[Export("configureWithAppId:")]
		void ConfigureWithAppId([NullAllowed] string appid);

		// +(void)configureWithFileInPath:(NSString * _Nullable)filepath;
		[Static]
		[Export("configureWithFileInPath:")]
		void ConfigureWithFileInPath([NullAllowed] string filepath);

		// +(void)getSdkIdentities:(void (^ _Nullable)(NSString * _Nullable))callback;
		[Static]
		[Export("getSdkIdentities:")]
		void GetSdkIdentities([NullAllowed] Action<NSString> callback);

		// +(void)getSdkIdentitiesWithCompletionHandler:(void (^ _Nullable)(NSString * _Nullable, NSError * _Nullable))completionHandler;
		[Static]
		[Export("getSdkIdentitiesWithCompletionHandler:")]
		void GetSdkIdentitiesWithCompletionHandler([NullAllowed] Action<NSString, NSError> completionHandler);

		// +(void)registerURLHandler:(BOOL (^ _Nonnull)(NSString * _Nullable))callback;
		[Static]
		[Export("registerURLHandler:")]
		void RegisterURLHandler(Func<NSString, bool> callback);

		// +(void)getPrivacyStatus:(void (^ _Nonnull)(ACPMobilePrivacyStatus))callback;
		[Static]
		[Export("getPrivacyStatus:")]
		void GetPrivacyStatus(Action<ACPMobilePrivacyStatus> callback);

		// +(void)getPrivacyStatusWithCompletionHandler:(void (^ _Nonnull)(ACPMobilePrivacyStatus, NSError * _Nullable))completionHandler;
		[Static]
		[Export("getPrivacyStatusWithCompletionHandler:")]
		void GetPrivacyStatusWithCompletionHandler(Action<ACPMobilePrivacyStatus, NSError> completionHandler);

		// +(NSString * _Nonnull)extensionVersion;
		[Static]
		[Export("extensionVersion")]
		string ExtensionVersion { get; }

		// +(void)setAppGroup:(NSString * _Nullable)appGroup;
		[Static]
		[Export("setAppGroup:")]
		void SetAppGroup([NullAllowed] string appGroup);

		// +(void)setPrivacyStatus:(ACPMobilePrivacyStatus)status;
		[Static]
		[Export("setPrivacyStatus:")]
		void SetPrivacyStatus(ACPMobilePrivacyStatus status);

		// +(void)start:(void (^ _Nullable)(void))callback;
		[Static]
		[Export("start:")]
		void Start([NullAllowed] Action callback);

		// +(void)updateConfiguration:(NSDictionary * _Nullable)config;
		[Static]
		[Export("updateConfiguration:")]
		void UpdateConfiguration([NullAllowed] NSDictionary config);

		// +(BOOL)registerExtension:(Class _Nonnull)extensionClass error:(NSError * _Nullable * _Nullable)error;
		[Static]
		[Export("registerExtension:error:")]
		bool RegisterExtension(Class extensionClass, [NullAllowed] out NSError error);

		// +(void)collectPii:(NSDictionary<NSString *,NSString *> * _Nonnull)data;
		[Static]
		[Export("collectPii:")]
		void CollectPii(NSDictionary<NSString, NSString> data);

		// +(void)lifecyclePause;
		[Static]
		[Export("lifecyclePause")]
		void LifecyclePause();

		// +(void)lifecycleStart:(NSDictionary<NSString *,NSString *> * _Nullable)additionalContextData;
		[Static]
		[Export("lifecycleStart:")]
		void LifecycleStart([NullAllowed] NSDictionary<NSString, NSString> additionalContextData);

		// +(void)setAdvertisingIdentifier:(NSString * _Nullable)adId;
		[Static]
		[Export("setAdvertisingIdentifier:")]
		void SetAdvertisingIdentifier([NullAllowed] string adId);

		// +(void)setPushIdentifier:(NSData * _Nullable)deviceToken;
		[Static]
		[Export("setPushIdentifier:")]
		void SetPushIdentifier([NullAllowed] NSData deviceToken);

		// +(void)trackAction:(NSString * _Nullable)action data:(NSDictionary<NSString *,NSString *> * _Nullable)data;
		[Static]
		[Export("trackAction:data:")]
		void TrackAction([NullAllowed] string action, [NullAllowed] NSDictionary<NSString, NSString> data);

		// +(void)trackState:(NSString * _Nullable)state data:(NSDictionary<NSString *,NSString *> * _Nullable)data;
		[Static]
		[Export("trackState:data:")]
		void TrackState([NullAllowed] string state, [NullAllowed] NSDictionary<NSString, NSString> data);

		// +(BOOL)dispatchEvent:(ACPExtensionEvent * _Nonnull)event error:(NSError * _Nullable * _Nullable)error;
		[Static]
		[Export("dispatchEvent:error:")]
		bool DispatchEvent(ACPExtensionEvent @event, [NullAllowed] out NSError error);

		// +(BOOL)dispatchEventWithResponseCallback:(ACPExtensionEvent * _Nonnull)requestEvent responseCallback:(void (^ _Nonnull)(ACPExtensionEvent * _Nonnull))responseCallback error:(NSError * _Nullable * _Nullable)error;
		[Static]
		[Export("dispatchEventWithResponseCallback:responseCallback:error:")]
		bool DispatchEventWithResponseCallback(ACPExtensionEvent requestEvent, Action<ACPExtensionEvent> responseCallback, [NullAllowed] out NSError error);

		// +(BOOL)dispatchResponseEvent:(ACPExtensionEvent * _Nonnull)responseEvent requestEvent:(ACPExtensionEvent * _Nonnull)requestEvent error:(NSError * _Nullable * _Nullable)error;
		[Static]
		[Export("dispatchResponseEvent:requestEvent:error:")]
		bool DispatchResponseEvent(ACPExtensionEvent responseEvent, ACPExtensionEvent requestEvent, [NullAllowed] out NSError error);

		// +(void)collectLaunchInfo:(NSDictionary * _Nonnull)userInfo;
		[Static]
		[Export("collectLaunchInfo:")]
		void CollectLaunchInfo(NSDictionary userInfo);

		// +(void)collectMessageInfo:(NSDictionary * _Nonnull)messageInfo;
		[Static]
		[Export("collectMessageInfo:")]
		void CollectMessageInfo(NSDictionary messageInfo);

		// +(void)downloadRules;
		[Static]
		[Export("downloadRules")]
		void DownloadRules();

		// +(ACPMobileLogLevel)logLevel;
		// +(void)setLogLevel:(ACPMobileLogLevel)logLevel;
		[Static]
		[Export("logLevel")]
		ACPMobileLogLevel LogLevel { get; set; }

		// +(void)log:(ACPMobileLogLevel)logLevel tag:(NSString * _Nonnull)tag message:(NSString * _Nonnull)message;
		[Static]
		[Export("log:tag:message:")]
		void Log(ACPMobileLogLevel logLevel, string tag, string message);

		// +(void)setWrapperType:(ACPMobileWrapperType)wrapperType;
		[Static]
		[Export("setWrapperType:")]
		void SetWrapperType(ACPMobileWrapperType wrapperType);
	}

	// @interface ACPMobileVisitorId : NSObject
	[BaseType(typeof(NSObject))]
	interface ACPMobileVisitorId
	{
		// @property (nonatomic, strong) NSString * _Nullable idOrigin;
		[NullAllowed, Export("idOrigin", ArgumentSemantic.Strong)]
		string IdOrigin { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable idType;
		[NullAllowed, Export("idType", ArgumentSemantic.Strong)]
		string IdType { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable identifier;
		[NullAllowed, Export("identifier", ArgumentSemantic.Strong)]
		string Identifier { get; set; }

		// @property (readwrite, nonatomic) ACPMobileVisitorAuthenticationState authenticationState;
		[Export("authenticationState", ArgumentSemantic.Assign)]
		ACPMobileVisitorAuthenticationState AuthenticationState { get; set; }
	}

	// @interface ACPExtensionEvent : NSObject
	[BaseType(typeof(NSObject))]
	interface ACPExtensionEvent
	{
		// +(instancetype _Nullable)extensionEventWithName:(NSString * _Nonnull)name type:(NSString * _Nonnull)type source:(NSString * _Nonnull)source data:(NSDictionary * _Nullable)data error:(NSError * _Nullable * _Nullable)error;
		[Static]
		[Export("extensionEventWithName:type:source:data:error:")]
		[return: NullAllowed]
		ACPExtensionEvent ExtensionEventWithName(string name, string type, string source, [NullAllowed] NSDictionary data, [NullAllowed] out NSError error);

		// @property (readonly, nonatomic) NSDictionary * _Nullable eventData;
		[NullAllowed, Export("eventData")]
		NSDictionary EventData { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull eventName;
		[Export("eventName")]
		string EventName { get; }

		// @property (readonly, nonatomic) int eventNumber;
		[Export("eventNumber")]
		int EventNumber { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull eventSource;
		[Export("eventSource")]
		string EventSource { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull eventType;
		[Export("eventType")]
		string EventType { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull eventUniqueIdentifier;
		[Export("eventUniqueIdentifier")]
		string EventUniqueIdentifier { get; }

		// @property (readonly, nonatomic) long eventTimestamp;
		[Export("eventTimestamp")]
		nint EventTimestamp { get; }
	}
}
