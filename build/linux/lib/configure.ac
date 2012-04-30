dnl Warning: This is an automatically generated file, do not edit!
dnl Process this file with autoconf to produce a configure script.
AC_PREREQ([2.54])
AC_INIT([HumanRightsTracker], [1.0.2])
AM_INIT_AUTOMAKE([foreign])
AM_MAINTAINER_MODE

dnl pkg-config
AC_PATH_PROG(PKG_CONFIG, pkg-config, no)
if test "x$PKG_CONFIG" = "xno"; then
        AC_MSG_ERROR([You need to install pkg-config])
fi

SHAMROCK_EXPAND_LIBDIR
SHAMROCK_EXPAND_BINDIR
SHAMROCK_EXPAND_DATADIR

AC_PROG_INSTALL

AC_PATH_PROG(GMCS, gmcs, no)
if test "x$GMCS" = "xno"; then
        AC_MSG_ERROR([gmcs Not found])
fi


AC_ARG_ENABLE(debug_x86,
	AC_HELP_STRING([--enable-debug_x86],
		[Use 'DEBUG_X86' Configuration [default=YES]]),
		enable_debug_x86=yes, enable_debug_x86=no)
AM_CONDITIONAL(ENABLE_DEBUG_X86, test x$enable_debug_x86 = xyes)
if test "x$enable_debug_x86" = "xyes" ; then
	CONFIG_REQUESTED="yes"
fi
AC_ARG_ENABLE(release_x86,
	AC_HELP_STRING([--enable-release_x86],
		[Use 'RELEASE_X86' Configuration [default=NO]]),
		enable_release_x86=yes, enable_release_x86=no)
AM_CONDITIONAL(ENABLE_RELEASE_X86, test x$enable_release_x86 = xyes)
if test "x$enable_release_x86" = "xyes" ; then
	CONFIG_REQUESTED="yes"
fi
AC_ARG_ENABLE(windows,
	AC_HELP_STRING([--enable-windows],
		[Use 'WINDOWS' Configuration [default=NO]]),
		enable_windows=yes, enable_windows=no)
AM_CONDITIONAL(ENABLE_WINDOWS, test x$enable_windows = xyes)
if test "x$enable_windows" = "xyes" ; then
	CONFIG_REQUESTED="yes"
fi
AC_ARG_ENABLE(windows_x86,
	AC_HELP_STRING([--enable-windows_x86],
		[Use 'WINDOWS_X86' Configuration [default=NO]]),
		enable_windows_x86=yes, enable_windows_x86=no)
AM_CONDITIONAL(ENABLE_WINDOWS_X86, test x$enable_windows_x86 = xyes)
if test "x$enable_windows_x86" = "xyes" ; then
	CONFIG_REQUESTED="yes"
fi
if test -z "$CONFIG_REQUESTED" ; then
	AM_CONDITIONAL(ENABLE_DEBUG_X86, true)
	enable_debug_x86=yes
fi


dnl package checks, common for all configs
PKG_CHECK_MODULES([GTK_SHARP_20], [gtk-sharp-2.0])
PKG_CHECK_MODULES([GLIB_SHARP_20], [glib-sharp-2.0])
PKG_CHECK_MODULES([GLADE_SHARP_20], [glade-sharp-2.0])

dnl package checks, per config


AC_CONFIG_FILES([
Models/models.pc
Models/Makefile
Translation/Makefile
AODL/aodl.pc
AODL/Makefile
AODL.pdf.exporter/aodl.pdf.exporter.pc
AODL.pdf.exporter/Makefile
MonoSqlite/monosqlite.pc
MonoSqlite/Makefile
Reports/reports.pc
Reports/Makefile
DataBase/database.pc
DataBase/Makefile
Views/views.pc
Views/Makefile
HumanRightsTracker/humanrightstracker
HumanRightsTracker/Makefile
SchemaGenerator/schema_generator
SchemaGenerator/Makefile
Makefile

])

AC_OUTPUT
