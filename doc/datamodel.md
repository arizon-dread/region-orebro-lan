# Datamodel
## Beställbar grej
```
Item
    Id (Guid)
    Manufacturer (string)
    Name (string)
    Price (double)
```
## Inventarier
```
ItemInventory
    Id (Guid)
    ItemId(Guid)
    Inventory(int)
```
## Info text
```
Info
    Id (Guid)
    Title (string)
    Text (string)
    PublishDate (DateTime)
    Unpublished (DataTime?)
```
## Beställningar  
```
Order
    Id (Guid)
    ItemId (Guid)
    Ammount (int)
    Customer (string)
```
