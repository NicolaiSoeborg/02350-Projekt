[![Build status](https://ci.appveyor.com/api/projects/status/huwcl6w75rt7c4am/branch/master?svg=true)](https://ci.appveyor.com/project/NicolaiSoeborg/02350-projekt/branch/master)

# OLProgram
Se [rapport.pdf](rapport.pdf) for info om projektet.
Kildekoden ligger i "src" mappen.

Vi har givet programmet følgende standard brugere og produkter:

```
Barcode	Navn
1106	Bjarne
1002	Nicolai
1003	Silas
1004	Greven
```

```
ProductId		ProductName			Pris
1				Grøn Tuborg			0
2				Guld Tuborg			0
3				Somersby			0
5708429004221	Svaneke Grunge IPA	0
```

For at købe en produkt, benyttes enten musen eller produktets stregkode indtastes efterfulgt af "[Enter]".

For at logge ind som admin trykkes "Ctrl + K".
Koden er: OLProgram
Tryk "Esc" for at gå til forsiden.
Bemærk: Hvis du, midt i et køb, skifter til admin,
	så gemmes kurven, så brugeren kan logge ind (igen) og fortsætte sit køb.


## Version
0.x -- see todo list

## TODO
 - Fix database
 - Update this todo list with rest of todos


# Linux support
Building on UNIX is a problem as WPF isn't implemented in Mono ([and isn't going to implement it anytime soon](http://www.mono-project.com/docs/gui/wpf/)).

So building on 

[![Build Status](https://travis-ci.org/NicolaiSoeborg/02350-Projekt.svg?branch=master)](https://travis-ci.org/NicolaiSoeborg/02350-Projekt)

# License
Beerware
