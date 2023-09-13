CREATE TABLE products (
    ProductBarcode BIGINT NOT NULL PRIMARY KEY,
    ProductName VARCHAR(255) NOT NULL,
    ManufacturingDate DATE NOT NULL,
    BestBefore INT NOT NULL,
    ExpiryDate DATE GENERATED ALWAYS AS (DATE_ADD(ManufacturingDate, INTERVAL BestBefore MONTH)),
    PurchasePrice DECIMAL(10, 2) NOT NULL,
    TaxSlab DECIMAL(10, 2) NOT NULL,
    Tax DECIMAL(10, 2) GENERATED ALWAYS AS (PurchasePrice * TaxSlab),
    SellingPrice DECIMAL(10, 2) GENERATED ALWAYS AS (PurchasePrice + (PurchasePrice * TaxSlab))
);
