# Universiteti i Prishtinës 'Hasan Prishtina'
* Fakulteti i Inxhinierisë Elektrike dhe Kompjuterike
* Departamenti: Inxhinieri Kompjuterike dhe Softuerike

# TCP Socket Programming
Projekt nga lënda "Rrjeta Kompjuterike".

# Contributors 
* [Agnesa Rama](https://github.com/agnesarama1)
* [Afrim Ymeri](https://github.com/afrimymeri)
* [Agnesa Mani](https://github.com/Agnesamani)
* [Albin Shala](https://github.com/albinshala)

# Asistenti i lëndës
Dr. Sc. Mergim H. Hoti.

# Project Description
Sistemi lejon klientët të lidhen me serverin, të ndërveprojnë me skedarët në server, të ekzekutojnë komanda dhe të dërgojnë mesazhe. Ndërveprimi bazohet në një grup komandash të paracaktuara.

Serveri: 
* Serveri funksionon si një shërbim qendror që pret klientët përmes një porti të caktuar (8081) dhe menaxhon kërkesat e tyre. Ai përdor TCPListener për të pranuar lidhjet dhe multithreading për të trajtuar shumë klientë njëkohësisht, pa ndërprerë funksionimin e përgjithshëm. Serveri ka një direktori të përbashkët (C:\SharedFolder) ku ruan skedarët për operacione si listim, lexim dhe shkrim. Gjithashtu, serveri përfshin një mekanizëm kontrolli të privilegjeve bazuar në IP-në e klientit, duke kufizuar operacionet kritike si shkrimi në skedarë dhe ekzekutimi i komandave vetëm për klientët e autorizuar. Çdo kërkesë nga klienti trajtohet me kujdes për të shmangur gabimet dhe për të siguruar një përgjigje të shpejtë dhe të saktë. Serveri poashtu përmban funksionin CheckPrivileges që kontrollon nëse një adresë IP e dhënë ka privilegje të veçanta për të kryer veprime të caktuara. Ky funksion përdor një listë të paracaktuar të adresave IP të privilegjuara dhe krahason adresën IP të klientit që jepet si parametër me këtë listë. Nëse IP-ja e klientit gjendet në listë, funksioni kthen true; përndryshe kthen false. Poashtu e përmban edhe mundësine që dy IP address-a të jenë admin.
  
Klienti: 
* Klienti vepron si një ndërfaqe interaktive që i lejon përdoruesit të lidhen me serverin dhe të kryejnë operacione të ndryshme. Ai përdor TcpClient për të krijuar një lidhje me serverin dhe për të komunikuar me të përmes komandave. Pasi të lidhet, klienti ofron një menu të thjeshtë që mbështet operacione si listimi i skedarëve, leximi i përmbajtjes së një skedari, shkrimi i përmbajtjes në një skedar, ekzekutimi i komandave, dhe dërgimi i mesazheve. Për përdoruesin e zakonshëm, klienti është i lehtë për t'u përdorur, ndërsa ruan një nivel të lartë fleksibiliteti për kërkesa më komplekse. Komunikimi me serverin është i sigurt dhe gabimet trajtohen në mënyrë që të mos ndërpritet ndërveprimi. Implementimi është flexible dhe trajton gabimet për të ruajtur stabilitetin e komunikimit.

# Si të Përdoret
Serveri: 
* Hapni projektin Server.cs dhe ekzekutoni atë.
* Serveri do të nisë në portin 8081 dhe do të presë lidhjet nga klientët.
* Sigurohuni që direktoria C:\SharedFolder ekziston në sistemin tuaj dhe keni file me content brenda për të pasur mundësi t'i ekzekutoni komandat.

Klienti: 
* Hapni projektin Client.cs dhe ekzekutoni atë.
* Shkruani adresën IP të serverit dhe portin për lidhje (p.sh., 172.16.110.248:8081).
* Përdorni menynë për të zgjedhur operacionet që dëshironi të kryeni.
  
# Kërkesat
.NET Framework ose .NET Core.

# Përfundim
Ky projekt demonstron një sistem të thjeshtë dhe funksional për komunikimin klient-server në C#, duke ofruar operacione të zakonshme për menaxhimin e skedarëve dhe komandave. Me një arkitekturë të thjeshtë, ai është ideal për qëllime edukative dhe eksperimentale. Përmirësimet e mëtejshme mund të përfshijnë shtimin e enkriptimit për komunikim më të sigurt dhe zgjerimin e funksionaliteteve për raste përdorimi më të avancuara.
