# Makefile requires Visual Studio for Mac Community version to be installed
# Tested with 8.5.3 (build 16)
setup:
	cd src/Adobe.ACPCore.Android/ && msbuild -t:restore
	cd src/Adobe.ACPCoreBridge.Android/ && msbuild -t:restore
	cd src/Adobe.ACPIdentity.Android/ && msbuild -t:restore
	cd src/Adobe.ACPLifecycle.Android/ && msbuild -t:restore
	cd src/Adobe.ACPSignal.Android/ && msbuild -t:restore
	cd src/Adobe.ACPCore.iOS/ && msbuild -t:restore
	cd src/Adobe.ACPCore.tvOS/ && msbuild -t:restore	

msbuild-clean:
	cd src && msbuild -t:clean

clean-folders:
	rm -rf src/Adobe.ACPCore.Android/obj
	rm -rf src/Adobe.ACPCore.Android/bin/Debug
	rm -rf src/Adobe.ACPCoreBridge.Android/obj
	rm -rf src/Adobe.ACPCoreBridge.Android/bin/Debug
	rm -rf src/Adobe.ACPIdentity.Android/obj
	rm -rf src/Adobe.ACPIdentity.Android/bin/Debug
	rm -rf src/Adobe.ACPLifecycle.Android/obj
	rm -rf src/Adobe.ACPLifecycle.Android/bin/Debug
	rm -rf src/Adobe.ACPSignal.Android/obj
	rm -rf src/Adobe.ACPSignal.Android/bin/Debug
	rm -rf src/Adobe.ACPSignal.Android/obj
	rm -rf src/Adobe.ACPCore.iOS/bin/Debug
	rm -rf src/Adobe.ACPCore.iOS/obj
	rm -rf src/Adobe.ACPCore.tvOS/bin/Debug
	rm -rf src/Adobe.ACPCore.tvOS/obj
	rm -rf bin

clean: msbuild-clean clean-folders setup

# Makes ACPCore bindings and NuGet packages. The bindings (.dll) will be available in BindingDirectory/bin/Debug
# The NuGet packages get created in the same directory but are then moved to src/bin.
release:	
	cd src/Adobe.ACPCore.Android/ && msbuild -t:pack
	cd src/Adobe.ACPCoreBridge.Android/ && msbuild -t:pack
	cd src/Adobe.ACPIdentity.Android/ && msbuild -t:pack
	cd src/Adobe.ACPLifecycle.Android/ && msbuild -t:pack
	cd src/Adobe.ACPSignal.Android/ && msbuild -t:pack
	cd src/Adobe.ACPCore.iOS/ && msbuild -t:build	
	cd src/Adobe.ACPCore.tvOS/ && msbuild -t:build	
	mkdir bin
	cp src/Adobe.ACPCore.Android/bin/Debug/*.nupkg ./bin
	cp src/Adobe.ACPCoreBridge.Android/bin/Debug/*.nupkg ./bin
	cp src/Adobe.ACPIdentity.Android/bin/Debug/*.nupkg ./bin
	cp src/Adobe.ACPLifecycle.Android/bin/Debug/*.nupkg ./bin
	cp src/Adobe.ACPSignal.Android/bin/Debug/*.nupkg ./bin
	cp src/Adobe.ACPCore.iOS/bin/Debug/*.nupkg ./bin
	cp src/Adobe.ACPCore.tvOS/bin/Debug/*.nupkg ./bin

ACP_SDK_PATH = ./acp-sdk
ACP_SDK_IOS_ACPCORE_PATH = ./acp-sdk/iOS/ACPCore
ACP_SDK_TVOS_ACPCORE_PATH = ./acp-sdk/tvOS/ACPCore
UNIVERSAL_CORE_IOS_PATH = ./acp-sdk/universal-core-ios
UNIVERSAL_CORE_TVOS_PATH = ./acp-sdk/universal-core-tvos
UNIVERSAL_CORE_IOS_ACPCORE_PATH = ./acp-sdk/universal-core-ios/ACPCore
UNIVERSAL_CORE_TVOS_ACPCORE_PATH = ./acp-sdk/universal-core-tvos/ACPCore
SIMULATOR_DIRECTORY_NAME = ios-arm64_i386_x86_64-simulator
SIMULATOR_TVOS_DIRECTORY_NAME = tvos-arm64_x86_64-simulator
DEVICE_DIRECTORY_NAME = ios-arm64_armv7_armv7s
DEVICE_TVOS_DIRECTORY_NAME = tvos-arm64

download-acp-sdk:
	mkdir -p $(ACP_SDK_PATH)
	git clone --depth 1 https://github.com/Adobe-Marketing-Cloud/acp-sdks.git $(ACP_SDK_PATH)

update-core-ios-static-libraries:
	mkdir -p $(UNIVERSAL_CORE_IOS_PATH)
	mv $(ACP_SDK_IOS_ACPCORE_PATH) $(UNIVERSAL_CORE_IOS_PATH)
	lipo -remove arm64 -output $(UNIVERSAL_CORE_IOS_ACPCORE_PATH)/ACPCore.xcframework/$(SIMULATOR_DIRECTORY_NAME)/libACPCore_iOS_clean.a $(UNIVERSAL_CORE_IOS_ACPCORE_PATH)/ACPCore.xcframework/$(SIMULATOR_DIRECTORY_NAME)/libACPCore_iOS.a
	lipo -remove arm64 -output $(UNIVERSAL_CORE_IOS_ACPCORE_PATH)/ACPIdentity.xcframework/$(SIMULATOR_DIRECTORY_NAME)/libACPIdentity_iOS_clean.a $(UNIVERSAL_CORE_IOS_ACPCORE_PATH)/ACPIdentity.xcframework/$(SIMULATOR_DIRECTORY_NAME)/libACPIdentity_iOS.a
	lipo -remove arm64 -output $(UNIVERSAL_CORE_IOS_ACPCORE_PATH)/ACPLifecycle.xcframework/$(SIMULATOR_DIRECTORY_NAME)/libACPLifecycle_iOS_clean.a $(UNIVERSAL_CORE_IOS_ACPCORE_PATH)/ACPLifecycle.xcframework/$(SIMULATOR_DIRECTORY_NAME)/libACPLifecycle_iOS.a
	lipo -remove arm64 -output $(UNIVERSAL_CORE_IOS_ACPCORE_PATH)/ACPSignal.xcframework/$(SIMULATOR_DIRECTORY_NAME)/libACPSignal_iOS_clean.a $(UNIVERSAL_CORE_IOS_ACPCORE_PATH)/ACPSignal.xcframework/$(SIMULATOR_DIRECTORY_NAME)/libACPSignal_iOS.a
	lipo -create $(UNIVERSAL_CORE_IOS_ACPCORE_PATH)/ACPCore.xcframework/$(DEVICE_DIRECTORY_NAME)/libACPCore_iOS.a $(UNIVERSAL_CORE_IOS_ACPCORE_PATH)/ACPCore.xcframework/$(SIMULATOR_DIRECTORY_NAME)/libACPCore_iOS_clean.a  -output $(UNIVERSAL_CORE_IOS_PATH)/libACPCore_iOS.a
	lipo -create $(UNIVERSAL_CORE_IOS_ACPCORE_PATH)/ACPIdentity.xcframework/$(DEVICE_DIRECTORY_NAME)/libACPIdentity_iOS.a $(UNIVERSAL_CORE_IOS_ACPCORE_PATH)/ACPIdentity.xcframework/$(SIMULATOR_DIRECTORY_NAME)/libACPIdentity_iOS_clean.a -output $(UNIVERSAL_CORE_IOS_PATH)/libACPIdentity_iOS.a
	lipo -create $(UNIVERSAL_CORE_IOS_ACPCORE_PATH)/ACPLifecycle.xcframework/$(DEVICE_DIRECTORY_NAME)/libACPLifecycle_iOS.a $(UNIVERSAL_CORE_IOS_ACPCORE_PATH)/ACPLifecycle.xcframework/$(SIMULATOR_DIRECTORY_NAME)/libACPLifecycle_iOS_clean.a  -output $(UNIVERSAL_CORE_IOS_PATH)/libACPLifecycle_iOS.a
	lipo -create $(UNIVERSAL_CORE_IOS_ACPCORE_PATH)/ACPSignal.xcframework/$(DEVICE_DIRECTORY_NAME)/libACPSignal_iOS.a $(UNIVERSAL_CORE_IOS_ACPCORE_PATH)/ACPSignal.xcframework/$(SIMULATOR_DIRECTORY_NAME)/libACPSignal_iOS_clean.a  -output $(UNIVERSAL_CORE_IOS_PATH)/libACPSignal_iOS.a
	mv $(UNIVERSAL_CORE_IOS_PATH)/libACP*_iOS.a ./src/Adobe.ACPCore.iOS

update-core-tvos-static-libraries:
	mkdir -p $(UNIVERSAL_CORE_TVOS_PATH)
	mv $(ACP_SDK_TVOS_ACPCORE_PATH) $(UNIVERSAL_CORE_TVOS_PATH)
	lipo -remove arm64 -output $(UNIVERSAL_CORE_TVOS_ACPCORE_PATH)/ACPCoreTV.xcframework/$(SIMULATOR_TVOS_DIRECTORY_NAME)/libACPCore_tvOS_clean.a $(UNIVERSAL_CORE_TVOS_ACPCORE_PATH)/ACPCoreTV.xcframework/$(SIMULATOR_TVOS_DIRECTORY_NAME)/libACPCore_tvOS.a
	lipo -remove arm64 -output $(UNIVERSAL_CORE_TVOS_ACPCORE_PATH)/ACPIdentityTV.xcframework/$(SIMULATOR_TVOS_DIRECTORY_NAME)/libACPIdentity_tvOS_clean.a $(UNIVERSAL_CORE_TVOS_ACPCORE_PATH)/ACPIdentityTV.xcframework/$(SIMULATOR_TVOS_DIRECTORY_NAME)/libACPIdentity_tvOS.a
	lipo -remove arm64 -output $(UNIVERSAL_CORE_TVOS_ACPCORE_PATH)/ACPLifecycleTV.xcframework/$(SIMULATOR_TVOS_DIRECTORY_NAME)/libACPLifecycle_tvOS_clean.a $(UNIVERSAL_CORE_TVOS_ACPCORE_PATH)/ACPLifecycleTV.xcframework/$(SIMULATOR_TVOS_DIRECTORY_NAME)/libACPLifecycle_tvOS.a
	lipo -remove arm64 -output $(UNIVERSAL_CORE_TVOS_ACPCORE_PATH)/ACPSignalTV.xcframework/$(SIMULATOR_TVOS_DIRECTORY_NAME)/libACPSignal_tvOS_clean.a $(UNIVERSAL_CORE_TVOS_ACPCORE_PATH)/ACPSignalTV.xcframework/$(SIMULATOR_TVOS_DIRECTORY_NAME)/libACPSignal_tvOS.a
	lipo -create $(UNIVERSAL_CORE_TVOS_ACPCORE_PATH)/ACPCoreTV.xcframework/$(DEVICE_TVOS_DIRECTORY_NAME)/libACPCore_tvOS.a $(UNIVERSAL_CORE_TVOS_ACPCORE_PATH)/ACPCoreTV.xcframework/$(SIMULATOR_TVOS_DIRECTORY_NAME)/libACPCore_tvOS_clean.a  -output $(UNIVERSAL_CORE_TVOS_PATH)/libACPCore_tvOS.a
	lipo -create $(UNIVERSAL_CORE_TVOS_ACPCORE_PATH)/ACPIdentityTV.xcframework/$(DEVICE_TVOS_DIRECTORY_NAME)/libACPIdentity_tvOS.a $(UNIVERSAL_CORE_TVOS_ACPCORE_PATH)/ACPIdentityTV.xcframework/$(SIMULATOR_TVOS_DIRECTORY_NAME)/libACPIdentity_tvOS_clean.a -output $(UNIVERSAL_CORE_TVOS_PATH)/libACPIdentity_tvOS.a
	lipo -create $(UNIVERSAL_CORE_TVOS_ACPCORE_PATH)/ACPLifecycleTV.xcframework/$(DEVICE_TVOS_DIRECTORY_NAME)/libACPLifecycle_tvOS.a $(UNIVERSAL_CORE_TVOS_ACPCORE_PATH)/ACPLifecycleTV.xcframework/$(SIMULATOR_TVOS_DIRECTORY_NAME)/libACPLifecycle_tvOS_clean.a  -output $(UNIVERSAL_CORE_TVOS_PATH)/libACPLifecycle_tvOS.a
	lipo -create $(UNIVERSAL_CORE_TVOS_ACPCORE_PATH)/ACPSignalTV.xcframework/$(DEVICE_TVOS_DIRECTORY_NAME)/libACPSignal_tvOS.a $(UNIVERSAL_CORE_TVOS_ACPCORE_PATH)/ACPSignalTV.xcframework/$(SIMULATOR_TVOS_DIRECTORY_NAME)/libACPSignal_tvOS_clean.a  -output $(UNIVERSAL_CORE_TVOS_PATH)/libACPSignal_tvOS.a
	mv $(UNIVERSAL_CORE_TVOS_PATH)/libACP*_tvOS.a ./src/Adobe.ACPCore.tvOS
