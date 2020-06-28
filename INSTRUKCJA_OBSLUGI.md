# TablicaTrojkatna
program zrobiony na potrzeby sesji letniej 2020
NIE BYŁ TESTOWANY PRZEZ KAŻDY MOŻLWIY PRZYPADEK TABLICY STANÓW ZATEM OSTROŻNIE Z ZAUFANIEM
Używanie:
1. Do pliku ...\TablicaTrojkatna\bin\Debug\netcoreapp3.1\TablicaStanowDoMinimalizacji.xlsx w arkuszu TablicaStanow wprowadź:
- w kolumnie STANY:  nazwy stanów, różne od "0"
- w kolumnie Y: wartość wyjściowa dla danego stanu ("0","1","-")
- w wiersze następujące po podanych możesz wprowadzić swoje pomocnicze nazwy argumentów np "00", "01", a w kolumny pod nimi wprowadź stany przejściowe (stan niekoreślony = brak wypełnienia komórki, NIE "-")
2. Zapisz zmiany.
3. Zamknij plik
4. Uruchom program
5. Odnajdź w folderze programu ...\TablicaTrojkatna\bin\Debug\netcoreapp3.1 plik o nazwie ZminimalizowanaTablicaStanow.xlsx
6. ZminimalizowanaTablicaStanow.xlsx będzie kopią pliku TablicaStanowDoMinimalizacji.xlsx zawierającą dodatkowo dwa arkusze:
- "Trojkatna" - arkusz zawierający pierwotną tablicę trójkątną
- "FullTrojkatna" - arkusz zawierajacy tablice trójkątną z określonymi wykluczonymi parami stanów - "X" - oraz ze zgodnymi parami stanów - "V"
7. W pliku TablicaStanowDoMinimalizacji.xlsx znajduje się również arkusz "Przerzutniki", w którym automat minimalny oraz bez wyścigów bądź hazardu można od razu zamienić na tablicę Karnaugha danego przerzutnika (WARTOŚCI W TABELCE RÓŻOWEJ WPROWADZAMY ZGODNIE Z ZAPISEM BINARNYM, NIE KODEM GREYA)
