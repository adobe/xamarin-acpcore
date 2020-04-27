setup:
	cd src/ACPCore.XamarinAndroidBinding/ && msbuild -t:restore
	cd src/ACPIdentity.XamarinAndroidBinding/ && msbuild -t:restore
	cd src/ACPLifecycle.XamarinAndroidBinding/ && msbuild -t:restore
	cd src/ACPSignal.XamarinAndroidBinding/ && msbuild -t:restore

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

clean: msbuild-clean clean-folders setup

bindings:
	cd src && msbuild -t:build

nuget:
	cd src && msbuild -t:pack
