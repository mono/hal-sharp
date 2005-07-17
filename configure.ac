AC_INIT(README)
AC_CANONICAL_SYSTEM
AM_INIT_AUTOMAKE(hal-sharp, 0.1)
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
	hal-sharp.pc
])


