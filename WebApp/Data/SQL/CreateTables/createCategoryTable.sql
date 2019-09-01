CREATE TABLE dbo.Category
(  
    CategoryID VARCHAR(40) NOT NULL  
    ,Name VARCHAR(60) NOT NULL  
    ,Description VARCHAR(200) NULL  
    ,ParentCategoryID VARCHAR(40) NULL 
    ,SubCategoryListID VARCHAR(40) NULL  
);