Začarovaný les

Ovládání
F5 - Uložení hry (funkční pouze při pohybu na mapě)
F9 - Načtení hry (funkční v každé fázi hry)
Kurzorové šipky - Pohyb po mapě
Myš - Klepnutí LTM nebo PTM na ovládací prvky

Mapy
Pro hru se dají vytvářet vlastní mapy, stačí vložit soubor do složky Maps ve tvaru mapa(x).csv (bez závorek, kde x je číslo mapy)
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
10 = lahvička mana
11 = dveře konec hry (poslední dveře, které ukončí hru)

Původní projekt v programovacím prostředí Raptor
https://github.com/tomaszlat/ZacarovanyLesRaptor
