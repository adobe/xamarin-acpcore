using ObjCRuntime;

namespace Com.Adobe.Marketing.Mobile
{
	[Native]
	public enum ACPMobileLogLevel : ulong
	{
		Error = 0,
		Warning = 1,
		Debug = 2,
		Verbose = 3
	}

	[Native]
	public enum ACPMobilePrivacyStatus : long
	{
		OptIn,
		OptOut,
		Unknown
	}

	[Native]
	public enum ACPMobileVisitorAuthenticationState : ulong
	{
		Unknown = 0,
		Authenticated = 1,
		LoggedOut = 2
	}

	[Native]
	public enum ACPMobileWrapperType : ulong
	{
		None = 0,
		ReactNative = 1,
		Flutter = 2,
		Cordova = 3,
		Unity = 4,
		Xamarin = 5
	}

	[Native]
	public enum ACPError : ulong
	{
		Unexpected = 0,
		CallbackTimeout = 1,
		CallbackNil = 2,
		ExtensionNotInitialized = 11
	}

	[Native]
	public enum ACPExtensionError : ulong
	{
		UnexpectedExtensionError = 0,
		BadExtensionNameExtensionError = 1,
		DuplicateExtensionNameExtensionError = 2,
		EventTypeNotSupportedExtensionError = 3,
		EventSourceNotSupportedExtensionError = 4,
		EventDataNotSupportedExtensionError = 5,
		BadExtensionClassExtensionError = 6,
		EventNullError = 7
	}
}