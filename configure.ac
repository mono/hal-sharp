AC_INIT(README)
AC_CANONICAL_SYSTEM
AM_INIT_AUTOMAKE(hal-sharp, 0.3)
AM_MAINTAINER_MODE

AC_PROG_INSTALL

dnl pkg-config
AC_PATH_PROG(PKG_CONFIG, pkg-config, no)
if test "x$PKG_CONFIG" = "xno"; then
	AC_MSG_ERROR([You need to install pkg-config])
fi

AC_PATH_PROG(INSTALL, install, /usr/bin/install)
AC_SUBST(INSTALL)
AC_SUBST(PKG_CONFIG)

dnl Mono C# Compiler and Runtime
AC_PATH_PROG(MCS, mcs, no)
AC_PATH_PROG(MONO, mono, no)
CS="C#"

if test "x$MCS" = "xno"; then
	AC_MSG_ERROR([You need to install the Mono $CS compiler])
fi

if test "x$MONO" = "xno"; then
	AC_MSG_ERROR([You need to install the Mono runtime])
fi

dnl monodoc
AC_PATH_PROG(MDASSEMBLER, mdassembler, no)
AC_PATH_PROG(MONODOCER, monodocer, no)
if test "x$MONODOCER" = "xno" -o "x$MDASSEMBLER" = "xno"; then
  enable_monodoc=no
  doc_sources_dir=
else
  enable_monodoc=yes
  doc_sources_dir="`pkg-config --variable=sourcesdir monodoc`"
fi
AC_SUBST(MDASSEMBLER)
AC_SUBST(MONODOCER)
AM_CONDITIONAL(ENABLE_MONODOC, test "x$enable_monodoc" = "xyes")


dnl Test for HAL
PKG_CHECK_MODULES(HAL,
	dbus-1 \
	dbus-glib-1 \
	hal >= 0.5.2)

AC_SUBST(MCS)

AC_OUTPUT([
	Makefile
	src/Makefile
	src/AssemblyInfo.cs
	src/hal-sharp.dll.config
	doc/Makefile
	hal-sharp.pc
])

echo "
hal-sharp-$VERSION

    Install Prefix:    ${prefix}
    Mono C# Compiler:  ${MCS}
    Mono Runtime:      ${MONO}

    Documentation:     ${enable_monodoc}
"

