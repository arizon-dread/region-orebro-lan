# region-orebro-lan KLIRR hackathon projekt

## Information

Vi har bestämt oss för att bygga en tjänst som dels tillhandahåller viktig information för kontakt inom organisationen och dels en lösning för att kunna göra beställningar av förbrukningsvaror för vården.

## Arkitektur

Systemet kommer bestå av en klientdel som kommer köras på varje dator och en serverdel som kommer köras som en central del. Tanken med projektet är att klienterna ska kunna fungera oavsett om servern är online eller inte.

### Klient

Klienterna kommer att synka ner data från servern kontinuerligt och kunna lägga beställningar som också synkas när servern är online. För att fungera när servern är offline skrivs alla beställningar lokalt först och går att granska och printa lokalt i klienten. Orderstatus syns på varje beställning för att indikera om den skickats till servern eller inte.  
Klienterna tillhandahåller också information om kontaktvägar i organisationen för att fungera som en informationskälla när övriga tjänster och kommunikationsvägar är nere.  

### Server

Servern består av ett admin-gränssnitt och ett api som tar emot beställningar och kan leverera information om verksamhetsens kommunikationsvägar. Tanken är sedan att man ska kunna koppla på mikrotjänster som läser ut beställningar och integrerar mot leverantörer.
