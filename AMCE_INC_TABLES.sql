CREATE TABLE TBL_CUSTOMER(
CUST_USERNAME VARCHAR(255) NOT NULL,
CUST_PASSWORD VARCHAR(255) NOT NULL,
PRIMARY KEY(CUST_USERNAME)
);

CREATE TABLE TBL_PRODUCTS(
PRODUCT_ID INT IDENTITY(1,100) NOT NULL,
PRODUCT_NAME VARCHAR(255) NOT NULL,
PRODUCT_TYPE VARCHAR(255) NOT NULL,
PRODUCT_DESCRIPTION VARCHAR(255) NOT NULL,
PRODUCT_PRICE NUMERIC(5,2) NOT NULL,
PRODUCT_IMAGE VARBINARY(MAX) NOT NULL,
PRIMARY KEY(PRODUCT_ID)
);


CREATE TABLE TBL_EMPLOYEE(
EMP_USERNAME VARCHAR(255) NOT NULL,
EMP_PASSWORD VARCHAR(255) NOT NULL,
PRIMARY KEY(EMP_USERNAME)
);

CREATE TABLE TBL_PURCHASES(
CUST_USERNAME VARCHAR(255) NOT NULL,
PRODUCT_ID INT NOT NULL,
PURCHASE_DATE DATE NOT NULL,
DELIVERY_DATE DATE NOT NULL,
FOREIGN KEY(CUST_USERNAME) REFERENCES TBL_CUSTOMER(CUST_USERNAME),
FOREIGN KEY(PRODUCT_ID) REFERENCES TBL_PRODUCTS(PRODUCT_ID)
);

CREATE TABLE TBL_CUST_ADDRESSES(
CUST_USERNAME VARCHAR(255) NOT NULL,
CUST_ADDRESS VARCHAR(255) NOT NULL,
FOREIGN KEY(CUST_USERNAME) REFERENCES TBL_CUSTOMER(CUST_USERNAME),
);

CREATE TABLE TBL_CUSTOMER_PAYMENT_DETAILS(
CUST_USERNAME VARCHAR(255) NOT NULL,
CUST_CARD_NUMBER INT NOT NULL,
CUST_CVV INT NOT NULL,
FOREIGN KEY(CUST_USERNAME) REFERENCES TBL_CUSTOMER(CUST_USERNAME)
);


INSERT INTO TBL_PRODUCTS(PRODUCT_NAME, PRODUCT_TYPE, PRODUCT_DESCRIPTION, PRODUCT_PRICE, PRODUCT_IMAGE) VALUES
('Cycily Heels', 'Heels', 'Green Satin Strappy Heels', 260.00, 
(SELECT * FROM OPENROWSET(BULK N'C:\Users\koket\OneDrive\Documents\PROGRAMMING\Task 2\green_heel.jpg', SINGLE_BLOB) as T1)),
('Red Stunter' , 'Heels', 'Diamonte Red Heels', 440.00, 
(SELECT * FROM OPENROWSET(BULK N'C:\Users\koket\OneDrive\Documents\PROGRAMMING\Task 2\red_heel.jpg', SINGLE_BLOB) as T1)),
('Midnight Strutters', 'Heels', 'Black Satin Heels', 380.00, 
(SELECT * FROM OPENROWSET(BULK N'C:\Users\koket\OneDrive\Documents\PROGRAMMING\Task 2\black_heel.jpg', SINGLE_BLOB) as T1)),
('Active Sneakers', 'Sneakers', 'Patterned Sneakers', 230.00, 
(SELECT * FROM OPENROWSET(BULK N'C:\Users\koket\OneDrive\Documents\PROGRAMMING\Task 2\noname_sneakers.jpg', SINGLE_BLOB) as T1)),
('Vans', 'Sneakers', 'Multicolored Vans', 730.00,
(SELECT * FROM OPENROWSET(BULK N'C:\Users\koket\OneDrive\Documents\PROGRAMMING\Task 2\vans_sneakers.jpg', SINGLE_BLOB) as T1)),
('Snuggly Sweater', 'Sweater', 'Purple Cozy Sweater', 240.00, 
(SELECT * FROM OPENROWSET(BULK N'C:\Users\koket\OneDrive\Documents\PROGRAMMING\Task 2\purple_hoodie.jpg', SINGLE_BLOB) as T1)),
('Long Coat', 'Coat', 'Nude Winter Coat', 685.00,
(SELECT * FROM OPENROWSET(BULK N'C:\Users\koket\OneDrive\Documents\PROGRAMMING\Task 2\nude_coat.jpg', SINGLE_BLOB) as T1)),
('Lacy Crop Top', 'Tops','White Summery Lace Top', 130.00,
(SELECT * FROM OPENROWSET(BULK N'C:\Users\koket\OneDrive\Documents\PROGRAMMING\Task 2\white_top.jpg', SINGLE_BLOB) as T1)),
('ShowStopper', 'Dress', 'Silky Black Dress', 230.00,
(SELECT * FROM OPENROWSET(BULK N'C:\Users\koket\OneDrive\Documents\PROGRAMMING\Task 2\black_silk_dress.jpg', SINGLE_BLOB) as T1)),
('Casual Dress', 'Dress', 'Deep Cut Black Dress', 467.00,
(SELECT * FROM OPENROWSET(BULK N'C:\Users\koket\OneDrive\Documents\PROGRAMMING\Task 2\black_dress.jpg', SINGLE_BLOB) as T1)),
('SkyBlue Blazer', 'Coat', 'Sky Blue Suit Blazer', 827.00,
(SELECT * FROM OPENROWSET(BULK N'C:\Users\koket\OneDrive\Documents\PROGRAMMING\Task 2\blue_blazer.jpg', SINGLE_BLOB) as T1));

SELECT * FROM TBL_PRODUCTS;