
 <center> <h1>Game Design Document</h1></center>
<center><h2>Jakub Hofman C3c </h2></center>
<center><h3>verze 1.0</h3></center>

# Struktura

- ### Úvod

- ### Gameplay

- ### Objekty

- ### Modeling

- ### Programování

- ### Ovládání


## Úvod
---
Tento dokument specifikuje průběh a nadcházející vývoj herního titulu nesoucím jméno "Dez'nan". Tvorba započala cca v **únoru roku 2024** a byla ukončena v **červnu** tohoto roku. Jedná se o školní projekt na předmět Multimédia a vývoj her (dále jen "Mvh"). Návrhářem a tvůrcem je Jakub Hofman, student školy SPŠE Ječná.  
Tento dokument je určen ke čtení pro *uživatele, programátory, grafické designéry, učitele Mvh* či jiné zástupce z oblasti informačních technologií.


## Gameplay
---
Po zapnutí hry se objeví hlavní menu, kde se nacházejí možnosti pro úpravu zvuku, vypnutí hry a startnutí nové hry.

Hráč se po kliknutí na startnutí nové hry objeví **v Lobby**, ze kterého má možnost dále přejít do **libovolné úrovně**, jež má zpřístupněnou. Stačí, aby prošel jedním ze 3 portálů, které se zde nachází. 

Po výběru se spawne na **mapě**. Zde se nachází **brána**, kterou musí **ubránit před nepřáteli**. Kdykoli nějaký nepřítel branou projde, hráči se o určitý počet sníží životy. Jakmile se hodnota zmíněných životů **dostane na 0**, úroveň končí a hráč je odkázán **zpět do Lobby**. Naopak když se hráči povede porazit všechny nepřátele, vyhraje a **získá tak 1 diamant** a odemkne se mu nová úroveň. Ve hře jsou celkem 3 s tím, že se odemykají postupně na základě hráčových výher.

"Jak ale ubrání hráč všechny útočníky?" Velmi snadno, má k dispozici kuši, se kterou může nepřátelům uštědřit smrtící rány. S kuší se však obecně těžko střílí za pochodu, proto je potřeba, aby hráč vždy když chce střílet zastavil a pečlivé namířil. V druhé řadě má u sebe kanón, kterým může též střílet. Nicméně nepřátelům to nic neudělá jelikož slouží k postavení věží. Funguje to tak, že hráč si z **build menu zvolí věž, co chce postavit**, následně kam vystřelená koule z této zbraně dopadne, tam se tato věž vybuduje. Po tomto procesu se hráči odečte cena věže od jeho coinů, ale jenom, pokud se věž skutečně postaví. Toto napovídá tomu, že může dojít i k situaci, že se nestane po dopadu koule na zem nic. To se stane když dopadne na nepříliš rovný povrch. Budovu na nevhodném místě jenom nelze zkonstruovat.

Co se týče **coinů na nákup věží**, na začátku úrovně jich má hráč určitý obnos, může je využít k **zaplacení ceny** za postavení nějaké **věže/í**. Až bude chtít, může **spustit vlnu nepřátel**. Ti budou vycházet ze začátku cesty a bude jich **různé množství** (záleží na úrovni či vlně). Takovýchto vln se bude nácházet v levelu více a při hraní **bude možné hru pozastavit** otevřením menu pro stavbu věží, které slouží k obraně. Jakmile se nepřátelé k budově přiblíží, začne na ně útočit. Na výběr je ze 4 druhů věží s tím, že každá se chová jinak. Je tedy potřeba pečlivě promyslet strategii boje. Nelze ale blokovat nepřátele tak, že jim hráč postaví věže do cesty.

## Objekty
---
- **`Lobby`**
	- výběr úrovně (portálu)
	- možnost koupě vylepšení pro hráče
		- vyšší rychlost pohybu
		- více života v úrovních
		- celkovou menší cenu věží
	
-  **`Úrovně`**
	- odemykány na základě **výher** (na začátku pouze **1**)
		- za každou výhru nová úroveň
	- celkem **3 různé mapy (úrovně)**
	- liší se **obtížností**
		- množství nepřátel
	- symbolizovány portály, které se nacházejí v Lobby

- **`Nepřátelé`**
	- odlišní **schopnostmi a vzhledem**
	- mohou mít **obranu** = snížení uděleného poškození o nějakou část (%)
	- **3 druhy**
		- Goblin
			- velká rychlost  x  malé životy
		- Orc
			- střední rychlost  x  velké životy, decentní obrana
		- Dark knight
			- malá rychlost  x  obrana, mnoho života
	- po smrti z nich hráč dostane různé množství **coinů** (1, 3, 5)
	- každý za různý počet životů, pokud se dostanou na konec mapy (1, 4, 7)
	
- **` Věže `**
	- **4 druhy**
		- Lučištníci
			- rychlá střelba  x  malé poškození
		- Mortar
			- pomalá střelba  x  zasáhne více cílů najednou
		- Gold mine
			- může těžit zlato pro hráče  x  nenapadá nepřátele
		- Mág
			- velké poškození  x  pomalý útok 
	- možné ji odstranit
	- kupovány za coiny, liší se cenou (20, 35, 15, 30)
	 
-  **`Hráč`**
	- má své rozhraní Player UI (**canvas**)
		- **životy**
		- **spuštění vlny**
		- **pauznutí úrovně pro nákup věže**
	- může **útočit**  do nepřátel formou střílení šípů z kuše
	- v Lobby si může **nakoupit vylepšení** za *diamanty*

- **`Player UI`**
	- skládá se z několika částí:
		- **`Nápověda pro spuštění vlny`**
			- text, který se zobrazí, když na bitevním poli není žádný nepřítel
			- po stisknutí "E" se spustí další vlna nepřátel 
		- **`Shop menu`**
			- slouží k nákupu vylepšení pro hráče
			- měnou jsou **diamanty** --> 1 diamant = 1 vylepšení
			- **`Diamanty`**
				- obdrží hráč vždy 1 za výhru úrovně
		- **`Build menu`**
			- slouží k nákupu věží, které hráč může postavit v průběhu úrovní
			- různé ceny coinů pro odlišné věže
			-  **`Coiny`**
				- přičte se hráči po **smrti nepřítele**
				- množství záleží na typu nepřítele


## Modeling
---
Pro modelování určitých objektů byl použit program **Blender**. Mezi tyto objekty se například řadí **Věže** a **Nepřátelé**. Následně jsou objekty exportovány do příslušného herního enginu. Budou na sobě mít *textury či materiály*. Mapy byly všechny vytvořeny pomocí "komponenty" **Terrain**, která se v Unity nachází.
Nicméně se ve hře také nacházejí mnou nedělané modely, kterými jsou hráčovi zbraně: kuše a kanón.

## Programování
---
Celý proces tvorby a testování hry se odehrává v **Unity Engine**, bude tedy využit programovací jazyk **C#**. Hra bude dělána ve **3D prostoru**, čemuž tedy bude odpovídat i použití metod, proměnných, atd.


## Ovládání
---
![[ControlsDeznan.png]]