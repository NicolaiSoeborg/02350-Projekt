OLProgram
=========

[![Windows Build status](https://img.shields.io/appveyor/ci/NicolaiSoeborg/02350-Projekt/master.svg?label=Windows%20build)](https://ci.appveyor.com/project/NicolaiSoeborg/02350-projekt/)
[![GitHub issues](https://img.shields.io/github/issues/NicolaiSoeborg/02350-Projekt.svg)](issues)
[![Beerpay](https://beerpay.io/NicolaiSoeborg/02350-Projekt/badge.svg?style=flat)](https://beerpay.io/NicolaiSoeborg/02350-Projekt)
[![license](https://img.shields.io/badge/License-Beerware-blue.svg)](LICENSE)

See [rapport.pdf](rapport.pdf) (danish) for info about the project.
Source project is "OLProgram.sln".

The program contains the following default users and products:

```
Barcode    Name     Team
1337       Admin    Vektor
```

```
ProductId        ProductName           Price    Stock
5708429004221    Svaneke Grunge IPA    20       0
```

To login as admin, press `Ctrl + K` and type the password: `OLProgram`.

## Version
[1.1](https://github.com/NicolaiSoeborg/02350-Projekt/releases/)

## TODO
 - Add: Extra change on bill ("svind") -- see AdminVM.cs
 - Fix: Delete many users / products at once
 - Add: Import / export users

## A few notes
 - Requires [.NET Framework 4.5.2](https://www.microsoft.com/en-us/download/details.aspx?id=42642).
 - User barcodes are exactly 4 chars.
 - [DB Browser for SQLite](http://sqlitebrowser.org/) is handy for modifying the database (`beer.sqlite3`).

## UNIX support
Building on UNIX is a problem as WPF isn't implemented in Mono (and isn't going to be implemented [anytime soon](http://www.mono-project.com/docs/gui/wpf/)).

Travis CI is setup to build OLProgram (using Mono), but is expected to fail:

  [![Mono build status](https://img.shields.io/travis/NicolaiSoeborg/02350-Projekt/sqlite.svg?label=Mono%20build)](https://travis-ci.org/NicolaiSoeborg/02350-Projekt)
