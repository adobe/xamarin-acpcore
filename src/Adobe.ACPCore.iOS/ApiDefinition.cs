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

	// @interface ACPExtensionListener : NSObject
	[BaseType(typeof(NSObject))]
	interface ACPExtensionListener
	{
		// @property (readonly, nonatomic) ACPExtension * _Nonnull extension;
		[Export("extension")]
		ACPExtension Extension { get; }

		// -(void)hear:(ACPExtensionEvent * _Nonnull)event;
		[Export("hear:")]
		void Hear(ACPExtensionEvent @event);
	}

	// @interface ACPExtensionApi : NSObject
	[BaseType(typeof(NSObject))]
	interface ACPExtensionApi
	{
		// -(BOOL)registerListener:(Class _Nonnull)listenerClass eventType:(NSString * _Nonnull)eventType eventSource:(NSString * _Nonnull)eventSource error:(NSError * _Nullable * _Nullable)error;
		[Export("registerListener:eventType:eventSource:error:")]
		bool RegisterListener(Class listenerClass, string eventType, string eventSource, [NullAllowed] out NSError error);

		// -(BOOL)registerWildcardListener:(Class _Nonnull)listenerClass error:(NSError * _Nullable * _Nullable)error;
		[Export("registerWildcardListener:error:")]
		bool RegisterWildcardListener(Class listenerClass, [NullAllowed] out NSError error);

		// -(BOOL)setSharedEventState:(NSDictionary * _Nullable)state event:(ACPExtensionEvent * _Nullable)event error:(NSError * _Nullable * _Nullable)error;
		[Export("setSharedEventState:event:error:")]
		bool SetSharedEventState([NullAllowed] NSDictionary state, [NullAllowed] ACPExtensionEvent @event, [NullAllowed] out NSError error);

		// -(BOOL)clearSharedEventStates:(NSError * _Nullable * _Nullable)error;
		[Export("clearSharedEventStates:")]
		bool ClearSharedEventStates([NullAllowed] out NSError error);

		// -(NSDictionary * _Nullable)getSharedEventState:(NSString * _Nonnull)name event:(ACPExtensionEvent * _Nullable)event error:(NSError * _Nullable * _Nullable)error;
		[Export("getSharedEventState:event:error:")]
		[return: NullAllowed]
		NSDictionary GetSharedEventState(string name, [NullAllowed] ACPExtensionEvent @event, [NullAllowed] out NSError error);

		// -(void)unregisterExtension;
		[Export("unregisterExtension")]
		void UnregisterExtension();
	}

	// @interface ACPExtension : NSObject
	[BaseType(typeof(NSObject))]
	interface ACPExtension
	{
		// -(NSString * _Nullable)name;
		[NullAllowed, Export("name")]
		string Name { get; }

		// -(NSString * _Nullable)version;
		[NullAllowed, Export("version")]
		string Version { get; }

		// -(void)unexpectedError:(NSError * _Nonnull)error;
		[Export("unexpectedError:")]
		void UnexpectedError(NSError error);

		// -(void)onUnregister;
		[Export("onUnregister")]
		void OnUnregister();

		// @property (readonly, nonatomic) ACPExtensionApi * _Nonnull api;
		[Export("api")]
		ACPExtensionApi Api { get; }
	}

	// @interface ACPIdentity : NSObject
	[BaseType(typeof(NSObject))]
	interface ACPIdentity
	{
		// +(void)appendToUrl:(NSURL * _Nullable)baseUrl withCallback:(void (^ _Nullable)(NSURL * _Nullable))callback;
		[Static]
		[Export("appendToUrl:withCallback:")]
		void AppendToUrl([NullAllowed] NSUrl baseUrl, [NullAllowed] Action<NSUrl> callback);

		// +(void)appendToUrl:(NSURL * _Nullable)baseUrl withCompletionHandler:(void (^ _Nullable)(NSURL * _Nullable, NSError * _Nullable))completionHandler;
		[Static]
		[Export("appendToUrl:withCompletionHandler:")]
		void AppendToUrl([NullAllowed] NSUrl baseUrl, [NullAllowed] Action<NSUrl, NSError> completionHandler);

		// +(NSString * _Nonnull)extensionVersion;
		[Static]
		[Export("extensionVersion")]
		string ExtensionVersion { get; }

		// +(void)getIdentifiers:(void (^ _Nonnull)(NSArray<ACPMobileVisitorId *> * _Nullable))callback;
		[Static]
		[Export("getIdentifiers:")]
		void GetIdentifiers(Action<ACPMobileVisitorId[]> callback);

		// +(void)getIdentifiersWithCompletionHandler:(void (^ _Nonnull)(NSArray<ACPMobileVisitorId *> * _Nullable, NSError * _Nullable))completionHandler;
		[Static]
		[Export("getIdentifiersWithCompletionHandler:")]
		void GetIdentifiersWithCompletionHandler(Action<ACPMobileVisitorId[], NSError> completionHandler);

		// +(void)getExperienceCloudId:(void (^ _Nonnull)(NSString * _Nullable))callback;
		[Static]
		[Export("getExperienceCloudId:")]
		void GetExperienceCloudId(Action<NSString> callback);

		// +(void)getExperienceCloudIdWithCompletionHandler:(void (^ _Nonnull)(NSString * _Nullable, NSError * _Nullable))completionHandler;
		[Static]
		[Export("getExperienceCloudIdWithCompletionHandler:")]
		void GetExperienceCloudIdWithCompletionHandler(Action<NSString, NSError> completionHandler);

		// +(void)registerExtension;
		[Static]
		[Export("registerExtension")]
		void RegisterExtension();

		// +(void)syncIdentifier:(NSString * _Nonnull)identifierType identifier:(NSString * _Nonnull)identifier authentication:(ACPMobileVisitorAuthenticationState)authenticationState;
		[Static]
		[Export("syncIdentifier:identifier:authentication:")]
		void SyncIdentifier(string identifierType, string identifier, ACPMobileVisitorAuthenticationState authenticationState);

		// +(void)syncIdentifiers:(NSDictionary * _Nullable)identifiers;
		[Static]
		[Export("syncIdentifiers:")]
		void SyncIdentifiers([NullAllowed] NSDictionary identifiers);

		// +(void)syncIdentifiers:(NSDictionary * _Nullable)identifiers authentication:(ACPMobileVisitorAuthenticationState)authenticationState;
		[Static]
		[Export("syncIdentifiers:authentication:")]
		void SyncIdentifiers([NullAllowed] NSDictionary identifiers, ACPMobileVisitorAuthenticationState authenticationState);

		// +(void)getUrlVariables:(void (^ _Nonnull)(NSString * _Nullable))callback;
		[Static]
		[Export("getUrlVariables:")]
		void GetUrlVariables(Action<NSString> callback);

		// +(void)getUrlVariablesWithCompletionHandler:(void (^ _Nonnull)(NSString * _Nullable, NSError * _Nullable))completionHandler;
		[Static]
		[Export("getUrlVariablesWithCompletionHandler:")]
		void GetUrlVariablesWithCompletionHandler(Action<NSString, NSError> completionHandler);
	}

	// @interface ACPLifecycle : NSObject
	[BaseType(typeof(NSObject))]
	interface ACPLifecycle
	{
		// +(NSString * _Nonnull)extensionVersion;
		[Static]
		[Export("extensionVersion")]
		string ExtensionVersion { get; }

		// +(void)registerExtension;
		[Static]
		[Export("registerExtension")]
		void RegisterExtension();
	}

	//// @interface  (ACPMobileVisitorId)
	//[BaseType(typeof(ACPMobileVisitorId))]
	//interface ACPMobileVisitorId_
	//{
	//	// -(instancetype)initWithOrigin:(NSString *)idOrigin type:(NSString *)idType id:(NSString *)identifier authentication:(ACPMobileVisitorAuthenticationState)authenticationState;
	//	[Export("initWithOrigin:type:id:authentication:")]
	//	IntPtr Constructor(string idOrigin, string idType, string identifier, ACPMobileVisitorAuthenticationState authenticationState);
	//}

	// @interface ACPHttpConnection : NSObject
	[BaseType(typeof(NSObject))]
	interface ACPHttpConnection
	{
		// -(instancetype _Nullable)initWithResponse:(NSHTTPURLResponse * _Nullable)response data:(NSData * _Nullable)data;
		[Export("initWithResponse:data:")]
		IntPtr Constructor([NullAllowed] NSHttpUrlResponse response, [NullAllowed] NSData data);

		// @property (readonly, nonatomic) NSHTTPURLResponse * _Nullable response;
		[NullAllowed, Export("response")]
		NSHttpUrlResponse Response { get; }

		// @property (readonly, nonatomic) NSData * _Nullable data;
		[NullAllowed, Export("data")]
		NSData Data { get; }
	}

	// @protocol ACPHttpConnectionPerformer <NSObject>
	/*
	  Check whether adding [Model] to this declaration is appropriate.
	  [Model] is used to generate a C# class that implements this protocol,
	  and might be useful for protocols that consumers are supposed to implement,
	  since consumers can subclass the generated class instead of implementing
	  the generated interface. If consumers are not supposed to implement this
	  protocol, then [Model] is redundant and will generate code that will never
	  be used.
	*/
	[Protocol]
	[BaseType(typeof(NSObject))]
	interface ACPHttpConnectionPerformer
	{
		// @required -(BOOL)shouldOverride:(NSURL * _Nonnull)url method:(NSString * _Nonnull)method;
		[Abstract]
		[Export("shouldOverride:method:")]
		bool ShouldOverride(NSUrl url, string method);

		// @required -(void)requestUrl:(NSURL * _Nonnull)url httpCommand:(NSString * _Nonnull)command connectPayload:(NSString * _Nonnull)payload requestPropertyDict:(NSDictionary<NSString *,NSString *> * _Nonnull)requestProperty connectTimeout:(NSTimeInterval)connectTimeout readTimeout:(NSTimeInterval)readTimeout completion:(void (^ _Nonnull)(ACPHttpConnection * _Nullable))completion;
		[Abstract]
		[Export("requestUrl:httpCommand:connectPayload:requestPropertyDict:connectTimeout:readTimeout:completion:")]
		void RequestUrl(NSUrl url, string command, string payload, NSDictionary<NSString, NSString> requestProperty, double connectTimeout, double readTimeout, Action<ACPHttpConnection> completion);
	}

	// @interface ACPNetworkServiceOverrider : NSObject
	[BaseType(typeof(NSObject))]
	interface ACPNetworkServiceOverrider
	{
		// +(void)setHttpConnectionPerformer:(id<ACPHttpConnectionPerformer> _Nullable)performer;
		[Static]
		[Export("setHttpConnectionPerformer:")]
		void SetHttpConnectionPerformer([NullAllowed] ACPHttpConnectionPerformer performer);
	}

	// @interface ACPSignal : NSObject
	[BaseType(typeof(NSObject))]
	interface ACPSignal
	{
		// +(NSString * _Nonnull)extensionVersion;
		[Static]
		[Export("extensionVersion")]
		string ExtensionVersion { get; }

		// +(void)registerExtension;
		[Static]
		[Export("registerExtension")]
		void RegisterExtension();
	}   
}