# Makefile requires Visual Studio for Mac Community version to be installed
# Tested with 8.5.3 (build 16)
setup:
	cd src/Adobe.ACPCore.Android/ && msbuild -t:restore
	cd src/Adobe.ACPCoreBridge.Android/ && msbuild -t:restore
	cd src/Adobe.ACPIdentity.Android/ && msbuild -t:restore
	cd src/Adobe.ACPLifecycle.Android/ && msbuild -t:restore
	cd src/Adobe.ACPSignal.Android/ && msbuild -t:restore
	cd src/Adobe.ACPCore.iOS/ && msbuild -t:restore

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
	mkdir bin
	cp src/Adobe.ACPCore.Android/bin/Debug/*.nupkg ./bin
	cp src/Adobe.ACPCoreBridge.Android/bin/Debug/*.nupkg ./bin
	cp src/Adobe.ACPIdentity.Android/bin/Debug/*.nupkg ./bin
	cp src/Adobe.ACPLifecycle.Android/bin/Debug/*.nupkg ./bin
	cp src/Adobe.ACPSignal.Android/bin/Debug/*.nupkg ./bin
	cp src/Adobe.ACPCore.iOS/bin/Debug/*.nupkg ./bin
