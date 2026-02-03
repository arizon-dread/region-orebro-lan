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
    OrderDate (DateTime)
    DeliveryAddress (string)
    DeliveryCity (string)
    DeliveryPostalCode (string)
    Customer (string)    
```
```
OrderRow
    Id (Guid)
    OrderId (Guid)
    ItemId (Guid)
    Ammount (int)
```
