/*
 Copyright 2020 Adobe. All rights reserved.
 This file is licensed to you under the Apache License, Version 2.0 (the "License");
 you may not use this file except in compliance with the License. You may obtain a copy
 of the License at http://www.apache.org/licenses/LICENSE-2.0
 Unless required by applicable law or agreed to in writing, software distributed under
 the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR REPRESENTATIONS
 OF ANY KIND, either express or implied. See the License for the specific language
 governing permissions and limitations under the License.
*/

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