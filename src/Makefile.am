halsharpdir=$(pkglibdir)
halsharp_SCRIPTS = hal-sharp.dll
EXTRA_DIST = $(halsharp_sources) HalTest.cs
CLEANFILES = hal-sharp.dll AssemblyInfo.cs
DISTCLEANFILES = Makefile.in

halsharp_sources = \
	HalContext.cs \
	HalDevice.cs \
	HalUnmanaged.cs \
	HalDefines.cs \
	HalExceptions.cs \
	AssemblyInfo.cs

halsharp_build_sources = $(addprefix $(srcdir)/, $(halsharp_sources))

hal-sharp.dll: 
	$(MCS) -target:library -out:$@ -r:Mono.Posix $(halsharp_build_sources)

