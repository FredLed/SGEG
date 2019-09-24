CREATE TABLE dbo.Product
(  
    ProductID VARCHAR(40) NOT NULL  
    ,Name VARCHAR(60) NOT NULL  
    ,MSRP FLOAT NULL  
    ,Description VARCHAR(200) NULL  
    ,CUP VARCHAR(20) NULL  
    ,CategoryID VARCHAR(40) NULL  
    ,CreationDate datetime NULL  
);