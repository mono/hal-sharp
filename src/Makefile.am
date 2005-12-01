MCS_FLAGS=-debug
halsharpdir=$(pkglibdir)
halsharp_SCRIPTS = hal-sharp.dll
EXTRA_DIST = $(halsharp_sources) HalTest.cs hal-sharp.dll.config.in
CLEANFILES = hal-sharp.dll hal-test.exe 
DISTCLEANFILES = Makefile.in *.mdb AssemblyInfo.cs hal-sharp.dll.config

halsharp_sources = \
	DBusError.cs \
	HalContext.cs \
	HalDevice.cs \
	HalUnmanaged.cs \
	HalDefines.cs \
	HalExceptions.cs \
	HalCallbacks.cs \
	AssemblyInfo.cs

halsharp_build_sources = $(addprefix $(srcdir)/, $(halsharp_sources))

all: hal-sharp.dll hal-test.exe

hal-sharp.dll: $(halsharp_build_sources)
	$(MCS) $(MCS_FLAGS) -target:library -out:$@ -r:Mono.Posix $(halsharp_build_sources)

hal-test.exe: HalTest.cs
	$(MCS) $(MCS_FLAGS) -r:hal-sharp.dll -out:$@ HalTest.cs

run:
	mono hal-test.exe

