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

using System.Reflection;
using System.Runtime.CompilerServices;

using Foundation;

// This attribute allows you to mark your assemblies as “safe to link”.
// When the attribute is present, the linker—if enabled—will process the assembly
// even if you’re using the “Link SDK assemblies only” option, which is the default for device builds.

[assembly: LinkerSafe]

// Information about this assembly is defined by the following attributes.
// Change them to the values specific to your project.

[assembly: AssemblyTitle("ACPCore for Xamarin iOS")]
[assembly: AssemblyDescription("ACPCore for Xamarin iOS")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Adobe Inc.")]
[assembly: AssemblyProduct("ACPCore")]
[assembly: AssemblyCopyright("Copyright 2020 Adobe. All rights reserved. This file is licensed to you under the Apache License, Version 2.0 (the \"License\"); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an \"AS IS\" BASIS, WITHOUT WARRANTIES OR REPRESENTATIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

//      Major Version
//      Minor Version 
//      Patch Version
[assembly: AssemblyVersion("0.0.1")]

// The following attributes are used to specify the signing key for the assembly,
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]
