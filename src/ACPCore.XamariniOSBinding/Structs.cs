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
}