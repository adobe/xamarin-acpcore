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
	cd src/ACPCore.XamarinAndroidBinding/ && rm -rf obj
	cd src/ACPCore.XamarinAndroidBinding/bin && rm -rf Debug
	cd src/ACPIdentity.XamarinAndroidBinding/ && rm -rf obj
	cd src/ACPIdentity.XamarinAndroidBinding/bin && rm -rf Debug
	cd src/ACPLifecycle.XamarinAndroidBinding/ && rm -rf obj
	cd src/ACPLifecycle.XamarinAndroidBinding/bin && rm -rf Debug
	cd src/ACPSignal.XamarinAndroidBinding/ && rm -rf obj
	cd src/ACPSignal.XamarinAndroidBinding/bin && rm -rf Debug
	cd src/ACPSignal.XamarinAndroidBinding/ && rm -rf obj
	cd src/Adobe.ACPCore.iOS/bin/ && rm -rf Debug
	cd src/Adobe.ACPCore.iOS/ && rm -rf obj
	rm -rf bin

clean: msbuild-clean clean-folders setup

# Makes ACPCore android bindings and NuGet packages. The android bindings (.dll) will be available in BindingDirectory/bin/Debug
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
