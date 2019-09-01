CREATE TABLE dbo.Item
(  
    ItemID VARCHAR(40) NOT NULL  
    ,ProductID VARCHAR(40) NOT NULL  
    ,Name VARCHAR(60) NOT NULL  
    ,Cost money NULL  
    ,SerialNumber VARCHAR(200) NULL 
    ,ReceptionDate VARCHAR(20) NULL  
    ,CreationDate datetime NULL  
);