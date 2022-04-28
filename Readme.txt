Zaèarovanı les

Ovládání
F5 - Uloení hry (funkèní pouze pøi pohybu na mapì)
F9 - Naètení hry (funkèní v kadé fázi hry)
Kurzorové šipky - Pohyb po mapì
Myš - LTM na tlaèítka

Mapy
Pro hru se dají vytváøet vlastní mapy, staèí vloit soubor do sloky mapy ve tvaru mapa(x).csv (bez závorek, kde x je èíslo mapy)
Pøi spuštìní nové hry si hra automaticky projde mapy od mapa1 po mapax. Pøi ukládání hry se data mapy ukládají take i kdy mapy ve sloce zmìníme hra bude vycházet z naètenıch dat.

Herní mapy mají velikost 12x12 políèek.
jednotlivá políèka oddìlíme støedníkem a nebo novım øádkem pokud u jsme u 12. políèka øádku
Kadá mapa by mìla obsahovat poèáteèní pozici hráèe. Hra zaèíná na první mapì (mapa1.csv)

0 = nic (tráva)
1 = kámen (pevnı objekt)
2 = strom (pevnı objekt)
3 = hráè (poèáteèní pozice hráèe na dané mapì)
4 = dveøe do další mapy (mapa(x+1).csv)
5 = dveøe do pøedchozí mapy (mapa(x-1).csv)
6 = jednoduchı soupeø
7 = støednì tìkı soupeø
8 = tìkı soupeø
9 = lahvièka zdraví
10 = lahvièka mana
11 = dveøe konec hry (poslední dveøe, které ukonèí hru)

