ASSEMBLIES = \
	hal-sharp.dll

UPDATE_ASSEMBLIES = $(addprefix -assembly:lib/, $(ASSEMBLIES))

if ENABLE_MONODOC
SOURCESDIR=$(prefix)/lib/monodoc/sources
TARGETS=hal-sharp-docs.zip hal-sharp-docs.tree hal-sharp-docs.source
UPDATER = $(MONODOCER) -path:en $(UPDATE_ASSEMBLIES)
else
SOURCESDIR=$(datadir)
TARGETS=
UPDATER = echo "All optional assemblies must be built to use the updater." && exit 1
endif

monodocdir=$(SOURCESDIR)
monodoc_DATA=$(TARGETS)

assemble: hal-sharp-docs.zip hal-sharp-docs.tree

hal-sharp-docs.zip hal-sharp-docs.tree: $(srcdir)/en/*.xml $(srcdir)/en/*.xml
	$(MDASSEMBLER) --ecma $(srcdir)/en -o hal-sharp-docs

get-assemblies:
	mkdir -p lib
	cp $(top_builddir)/src/hal-sharp.dll lib

update: get-assemblies
	$(UPDATER) --delete

CLEANFILES=hal-sharp-docs.zip hal-sharp-docs.tree
EXTRA_DIST=hal-sharp-docs.source
MAINTAINERCLEANFILES=Makefile.in

NAMESPACES=Hal

dist-hook:
	mkdir -p $(distdir)/en
	cp $(srcdir)/en/*.xml $(distdir)/en/
	for i in $(NAMESPACES); do                            \
		mkdir -p $(distdir)/en/$$i;                       \
		cp $(srcdir)/en/$$i/*.xml $(distdir)/en/$$i;      \
	done

clean-local:
	rm -rf lib
