# TablicaTrojkatna
program zrobiony na potrzeby sesji letniej 2020
NIE BYŁ TESTOWANY PRZEZ KAŻDY MOŻLWIY PRZYPADEK TABLICY STANÓW ZATEM OSTROŻNIE Z ZAUFANIEM
Używanie:
1. Do pliku TablicaStanowDoMinimalizacji.xlsx w arkuszu TablicaStanow wprowadź:
- w kolumnie STANY:  nazwy stanów, różne od "0"
- w kolumnie Y: wartość wyjściowa dla danego stanu
- w kolumny następujące po podanych możesz wprowadzić swoje pomocnicze nazwy argumentów np "00", "01"
2. Zapisz zmiany.
3. Zamknij plik
4. Uruchom program
5. Odnajdź w folderze programu ...\TablicaTrojkatna\bin\Release\netcoreapp3.1 plik o nazwie ZminimalizowanaTablicaStanow.xlsx
6. ZminimalizowanaTablicaStanow.xlsx będzie kopią pliku TablicaStanowDoMinimalizacji.xlsx zawierającą dodatkowo dwa arkusze:
- "Trojkatna" - arkusz zawierający pierwotną tablicę trójkątną
- "FullTrojkatna" - arkusz zawierajacy tablice trójkątną z określonymi wykluczonymi parami stanów - "X" - oraz ze zgodnymi parami stanów - "V"
