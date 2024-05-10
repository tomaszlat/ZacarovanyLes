Začarovaný les

Instalace - spustitelné release verze
https://github.com/tomaszlat/ZacarovanyLes/releases/download/v.1.0.0/Zacarovany.les.rar

Instalace - zdrojový kód
Pro kompilaci doporučuji Visual Studio 2022 s rozšířením MonoGame a po zkompilování je nutné do adresáře se hrou přidat složku Maps (https://github.com/tomaszlat/ZacarovanyLes/tree/master/Zacarovany_les/Maps) obsahující mapy, nebo si vytvořit vlastní dle níže uvedených informací.

Příručka ke hře Začarovaný les
https://github.com/tomaszlat/ZacarovanyLes/blob/master/P%C5%99%C3%ADru%C4%8Dka%20ke%20h%C5%99e%20Za%C4%8Darovan%C3%BD%20les.pdf

Výpočty hodnot schopností v excelu


Ovládání
F5 - Uložení hry (funkční pouze při pohybu na mapě)
F9 - Načtení hry (funkční v každé fázi hry)
Kurzorové šipky - Pohyb po mapě
Myš - Klepnutí LTM nebo PTM na ovládací prvky

Mapy
Pro hru se dají vytvářet vlastní mapy, stačí vložit soubor do složky Maps v adresáři hry ve tvaru mapa(x).csv (bez závorek, kde x je číslo mapy)
Při spuštění nové hry si hra automaticky projde mapy od mapa1.csv po mapax.csv. Při ukládání hry se data mapy ukládají takže i když mapy ve složce změníme hra bude vycházet z načtených dat.

Herní mapy mají velikost 12x12 políček.
jednotlivá políčka oddělíme středníkem a nebo novým řádkem pokud už jsme u 12. políčka řádku
Každá mapa by měla obsahovat počáteční pozici hráče. Hra začíná na první mapě (mapa1.csv)

0 = nic (tráva)
1 = kámen (pevný objekt)
2 = strom (pevný objekt)
3 = hráč (počáteční pozice hráče na dané mapě)
4 = dveře do další mapy (mapa(x+1).csv)
5 = dveře do předchozí mapy (mapa(x-1).csv)
6 = jednoduchý soupeř
7 = středně těžký soupeř
8 = těžký soupeř
9 = lahvička zdraví
10 = lahvička many
11 = dveře ukončující hru

Licence grafiky, zvuků a hudby
https://github.com/tomaszlat/ZacarovanyLes/blob/master/References.txt

Původní projekt v programovacím prostředí Raptor
https://github.com/tomaszlat/ZacarovanyLesRaptor
