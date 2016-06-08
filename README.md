OLProgram
=========================

[![Build status](https://ci.appveyor.com/api/projects/status/huwcl6w75rt7c4am/branch/master?svg=true)](https://ci.appveyor.com/project/NicolaiSoeborg/02350-projekt/branch/master)
[![GitHub issues](https://img.shields.io/github/issues/NicolaiSoeborg/02350-Projekt.svg)](https://github.com/NicolaiSoeborg/02350-Projekt/issues)
[![Beerpay](https://beerpay.io/NicolaiSoeborg/02350-Projekt/badge.svg?style=flat)](https://beerpay.io/NicolaiSoeborg/02350-Projekt)
[![license](https://img.shields.io/badge/License-Beerware-blue.svg)](https://github.com/NicolaiSoeborg/02350-Projekt/blob/master/LICENSE)


See [rapport.pdf](rapport.pdf) (danish) for info about the project.
Source project is "OLProgram.sln".

The program contains the following default users and products:

```
Barcode	Name
1106	Bjarne
1002	Nicolai
1003	Silas
1004	Greven
```

```
ProductId		ProductName			Price
1				Grøn Tuborg		 0
2				Guld Tuborg			0
3				Somersby			0
5708429004221	Svaneke Grunge IPA	0
```

To login as admin, press "Ctrl + K" and type the password: "OLProgram"

## Version
1.0.0 -- see todo list

## TODO
 - Fix database
 - Fix building on appveyor
 - Update this todo list with rest of todos


## UNIX support
Building on UNIX is a problem as WPF isn't implemented in Mono (and isn't going to be implemented [anytime soon](http://www.mono-project.com/docs/gui/wpf/)).

Travis CI is setup to (try to) build on OLProgram on Mono, but is expected to fail: [![Mono build status](https://travis-ci.org/NicolaiSoeborg/02350-Projekt.svg?branch=master)](https://travis-ci.org/NicolaiSoeborg/02350-Projekt)
