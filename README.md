# Praktikos užduotys

## Pirma užduotis

Parametrų palyginimo įrankis, kuris leidžia palyginti dviejų gaminio konfigūracijos failų parametrus.
Kodas rašytas stengiantis laiktytis SOLID objektinio probramavimo principų.

## Antra užduotis

Pirmoje užduotyje sukurtą įrankį integruoti į REST WEB API.

Šios užduoties yra 3 variantai:
### 1 variantas (main branch)

Abu failai yra priimami, laikomi atmintyje ir su jais yra iškart dirbama.
Lyginant su pirma užduotimi, yra perrašytos failo skaitymo ir lyginimo klasės. Failų skaitimas skiriasi tuo, nes failai nėra išsaugomi, o jų informacija yra iškart nuskaitoma į failo modelio objektą. Lyginime yra naudojama kita duomenų struktūra ir kitoks lyginimo būdas.
Rezultatuose yra atskirai išvedama kiekvieno failo informaciją (failo pavadinimas, metaduomenys). Taip pat, išvedami visi lyginimo rezultatai.

### 2 variantas (Part2-v2 branch)

Šiame variante yra pakeistas rezultatų išvedimas. Atsikrai išvedami tik failų pavadinimai. Failo informaciją (kur ID yra string tipo) yra taip pat lyginama, kaip ir kiti duomenys (kur ID yra int arba double).
Filtruoti galima abu rezultatų sąrašus atskirai.

### 3 variantas (Part2-v3 branch)

Šiame variante buvo panaudojamos jau sukurtos failo skaitymo ir lyginimo klasės iš pirmos užduoties. Taip pat, įkelti failai yra laikinai išsaugomi.
Kadangi naudojamos pirmos užduoties klasės, duomenys yra išvedami kaip ir pirmame variante.
