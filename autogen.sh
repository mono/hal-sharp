#!/bin/sh
aclocal
automake -a
autoconf
./configure --enable-maintainer-mode $*

