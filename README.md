OLProgram
=========

[![Windows Build status](https://img.shields.io/appveyor/ci/NicolaiSoeborg/02350-Projekt/master.svg?label=Windows%20build)](https://ci.appveyor.com/project/NicolaiSoeborg/02350-projekt/branch/master)
[![GitHub issues](https://img.shields.io/github/issues/NicolaiSoeborg/02350-Projekt.svg)](issues)
[![Beerpay](https://beerpay.io/NicolaiSoeborg/02350-Projekt/badge.svg?style=flat)](https://beerpay.io/NicolaiSoeborg/02350-Projekt)
[![license](https://img.shields.io/badge/License-Beerware-blue.svg)](LICENSE)

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
 - Fix database (sqlite)
 - Fix building on appveyor
 - Update this todo list with rest of todos


## UNIX support
Building on UNIX is a problem as WPF isn't implemented in Mono (and isn't going to be implemented [anytime soon](http://www.mono-project.com/docs/gui/wpf/)).

Travis CI is setup to build OLProgram on Mono, but is expected to fail:

[![Mono build status](https://img.shields.io/travis/NicolaiSoeborg/02350-Projekt/master.svg?label=Mono%20build)](https://travis-ci.org/NicolaiSoeborg/02350-Projekt)
