# ProgGame - C# tanulás élvezetesen

## Leírás

A feladat egy olyan webalkalmazás készítése, ahonnan kezdő C# feladatok tölthetőek le. Egy feladat == 1 zippelt solution. A solutionben van egy leírás md fájlban (ez a portálon is olvasható), illetve függvénytörzsek fix paraméterekkel és visszatérési értékkel. A törzsben kell a feladatot megoldani. A megoldás után a feladat visszatölthető zipként és ekkor az alkalmazás kiértékeli és a felhaszáló begyűjti a pontot. Valamilyen gamificationnel a felhasználó kaphasson a pontokért valamit (bedget, képességet, stb.)


### Működés

Két felhasználótípus szükséges (tanuló és oktató). Az oktató tudjon feladatokat kiírni és hozzá feltölteni a váz solutiont unit tesztekkel. A unit tesztek viszont a hallgatók által letöltött változatban ne legyenek benne.  A szerverre visszatöltött solutionbe kerüljenek visszaillesztésre a unit tesztek és fussanak le. A pontozást a csapat találhatja ki. Érdemes lenne azt is belerakni a pontozásba, hogy mennyi idő alatt fug le a függvény átlagosan. Minél rövidebb, annál több pontot kapna a tanuló. 


### Böngésző nézet

Itt érhető el minden funkció.

### Mobil nézet

Szükséges egy telepíthető mobilalkalmazás készítése is. Itt viszont csak a feladatok legyenek böngészhetőek, illetve a pontok és a gamifikáció jelenjen meg. A feladat letöltést és visszatöltést itt nem kell megoldani. Ez elég a 2023/24/2 félévben.


### Biztonsági intézkedések

Szükséges lenne azt is vizsgálni, hogy ezt a megoldást már leadta-e más is. Ha igen, akkor a megoldás egy eléggé lerágott csont és nem ér sok pontot. Ha viszont egy meglepően új megoldást adnak le, aminek jobb a futásideje, mint ami másoknál lenni szokott, ott sok ponttal kellene jutalmazni. Az egyezőség vizsgálathoz célszerű lenne Pintér Ádám oktatót felkeresni (pinter.adam@nik.uni-obuda.hu), aki phd munkájában ezzel foglalkozik.
