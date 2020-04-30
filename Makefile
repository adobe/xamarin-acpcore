# Makefile requires Visual Studio for Mac Community version to be installed
# Tested with 8.5.3 (build 16)
setup:
	cd src/ACPCore.XamarinAndroidBinding/ && msbuild -t:restore
	cd src/ACPIdentity.XamarinAndroidBinding/ && msbuild -t:restore
	cd src/ACPLifecycle.XamarinAndroidBinding/ && msbuild -t:restore
	cd src/ACPSignal.XamarinAndroidBinding/ && msbuild -t:restore
	cd src/Adobe.ACPCore.iOS/ && msbuild -t:restore

msbuild-clean:
	cd src && msbuild -t:clean

clean-folders:
	rm -rf src/ACPCore.XamarinAndroidBinding/obj
	rm -rf src/ACPCore.XamarinAndroidBinding/bin/Debug
	rm -rf src/ACPIdentity.XamarinAndroidBinding/obj
	rm -rf src/ACPIdentity.XamarinAndroidBinding/bin/Debug
	rm -rf src/ACPLifecycle.XamarinAndroidBinding/obj
	rm -rf src/ACPLifecycle.XamarinAndroidBinding/bin/Debug
	rm -rf src/ACPSignal.XamarinAndroidBinding/obj
	rm -rf src/ACPSignal.XamarinAndroidBinding/bin/Debug
	rm -rf src/ACPSignal.XamarinAndroidBinding/obj
	rm -rf src/Adobe.ACPCore.iOS/bin/Debug
	rm -rf src/Adobe.ACPCore.iOS/obj
	rm -rf bin

clean: msbuild-clean clean-folders setup

# Makes ACPCore bindings and NuGet packages. The bindings (.dll) will be available in BindingDirectory/bin/Debug
# The NuGet packages get created in the same directory but are then moved to src/bin.
all:
	cd src/ACPCore.XamarinAndroidBinding/ && msbuild -t:pack
	cd src/ACPIdentity.XamarinAndroidBinding/ && msbuild -t:pack
	cd src/ACPLifecycle.XamarinAndroidBinding/ && msbuild -t:pack
	cd src/ACPSignal.XamarinAndroidBinding/ && msbuild -t:pack
	cd src/Adobe.ACPCore.iOS/ && msbuild -t:build	
	mkdir bin
	cp src/ACPCore.XamarinAndroidBinding/bin/Debug/*.nupkg ./bin
	cp src/ACPIdentity.XamarinAndroidBinding/bin/Debug/*.nupkg ./bin
	cp src/ACPLifecycle.XamarinAndroidBinding/bin/Debug/*.nupkg ./bin
	cp src/ACPSignal.XamarinAndroidBinding/bin/Debug/*.nupkg ./bin
	cp src/Adobe.ACPCore.iOS/bin/Debug/*.nupkg ./bin
