# MDiSUBD1. Тема проекта

# 1. Тема: База данных для управления страйкбольным магазином.(PostreSQL)

# 2. Функциональные требования проекта
* Авторизация и аутентификация пользователей: система для регистрации, входа, и выхода.
* Управление пользователями: возможность добавления, редактирования, удаления пользователей (CRUD).
* Система ролей: разные уровни доступа для пользователей (администратор, менеджер, покупатель).
* Журналирование действий: логирование всех действий пользователей (изменение данных, покупка товаров и т.д.).
* Управление товарами: создание, редактирование и удаление информации о страйкбольных товарах.
* Управление заказами: пользователи могут оформлять заказы, видеть историю покупок.
* Управление категориями товаров: категории (например, оружие, амуниция, аксессуары).
* Склад: управление запасами товаров, отображение количества на складе.
* Отчёты: отчеты по продажам и активности пользователей.
* Отзывы и рейтинги: покупатели могут оставлять отзывы и ставить оценки товарам.

# 3. Таблицы сущностей в БД и их связь
![Image alt](https://github.com/PolskaProger/MDiSUBD/blob/main/Data.png)
## 1. User (Пользователь):
### Поля:
* Id — первичный ключ (PK).
* Login — логин пользователя.
* Email — почта пользователя.
* PasswordHash — хэш пароля.
* Role — внешняя связь с сущностью Role.
* RegDate — дата регистрации.
### Связи:
* One-to-Many с Role — один пользователь может принадлежать одной роли, но одна роль может быть присвоена нескольким пользователям (Foreign Key — Role).
* One-to-One с Cart — один пользователь может иметь только одну корзину.
* One-to-Many с Order — один пользователь может сделать несколько заказов, но каждый заказ принадлежит только одному пользователю.
* One-to-Many с ProductRating — один пользователь может оставить несколько рейтингов.
* One-to-Many с ProductReview — один пользователь может оставить несколько отзывов.
## 2. Role (Роль):
### Поля:
* Id — первичный ключ (PK).
* RoleName — название роли.
### Связи:
* One-to-Many с User — одна роль может быть присвоена нескольким пользователям.
## 3. Cart (Корзина):
### Поля:
* Id — первичный ключ (PK).
* UserId — внешний ключ, ссылающийся на пользователя (FK).
* ListOfProducts — список товаров.
* TotalPrice — общая стоимость.
### Связи:
* One-to-One с User — одна корзина принадлежит одному пользователю (Foreign Key — UserId).
* One-to-One с Order — одна корзина может быть использована в одном заказе.
## 4. Order (Заказ):
### Поля:
* Id — первичный ключ (PK).
* UserId — внешний ключ, ссылающийся на пользователя (FK).
* CartOfUser — ссылка на корзину.
* Status — статус заказа.
* DateOfOrder — дата заказа.
### Связи:
* One-to-Many с User — один пользователь может сделать несколько заказов (Foreign Key — UserId).
* One-to-One с Cart — один заказ соответствует одной корзине.
## 5. Product (Товар):
### Поля:
* Id — первичный ключ (PK).
* NameOfProduct — наименование товара.
* Category — внешний ключ, ссылающийся на категорию товара (FK).
* Description — описание товара.
* Price — цена товара.
* Rating — рейтинг товара.
* InStorage — наличие на складе.
### Связи:
* One-to-Many с Category — один товар относится к одной категории, но одна категория может содержать несколько товаров (Foreign Key — Category).
* One-to-Many с ProductRating — один товар может иметь несколько оценок от пользователей.
* One-to-Many с ProductReview — один товар может иметь несколько отзывов от пользователей.
* One-to-One с Storage — один товар может быть на складе.
## 6. Category (Категория):
### Поля:
* Id — первичный ключ (PK).
* NameOfCategory — название категории.
### Связи:
* One-to-Many с Product — одна категория может включать несколько товаров.
## 7. Storage (Склад):
### Поля:
* ProductId — внешний ключ, ссылающийся на товар (FK).
* Count — количество товаров на складе.
### Связи:
* One-to-One с Product — на складе один товар может быть представлен только одной записью.
## 8. ProductRating (Рейтинг товара):
### Поля:
* Id — первичный ключ (PK).
* UserId — внешний ключ, ссылающийся на пользователя (FK).
* ProductId — внешний ключ, ссылающийся на товар (FK).
* Mark — оценка товара.
### Связи:
* Many-to-One с User — один пользователь может поставить несколько оценок разным товарам.
* Many-to-One с Product — один товар может получить несколько оценок.
## 9. ProductReview (Отзыв о товаре):
### Поля:
* Id — первичный ключ (PK).
* UserId — внешний ключ, ссылающийся на пользователя (FK).
* ProductId — внешний ключ, ссылающийся на товар (FK).
* Description — текст отзыва.
* DateOfReview — дата отзыва.
### Связи:
* Many-to-One с User — один пользователь может оставить несколько отзывов.
* Many-to-One с Product — один товар может иметь несколько отзывов.
## 10. CartItem (Товар в карзине):
### Поля:
* Id — первичный ключ (PK).
* ProductId — внешний ключ, ссылающийся на товар (FK).
* CartId — ссылка на корзину.
* Count — количество товаров на складе.
### Связи:
Many-to-One с Product — один товар может быть добавлен в несколько элементов корзины.
Many-to-One с Cart — одна корзина может содержать несколько товаров (много CartItem привязаны к одной Cart).
## One-to-One (Один к одному):
* User — Cart.
* Cart — Order.
* Product — Storage.
## One-to-Many (Один ко многим):
* Role — User.
* User — Order.
* User — ProductRating.
* User — ProductReview.
* Category — Product.
* Product — ProductRating.
* Product — ProductReview.
* Cart — CartItem (одна корзина может содержать несколько товаров).
* Product — CartItem (один товар может находиться в нескольких корзинах). 

## SQL запросы для создания:

```sql
CREATE TABLE "User" (
    Id SERIAL PRIMARY KEY,
    Login VARCHAR(50) UNIQUE NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    RoleId INT REFERENCES "Role"(Id),
    RegDate DATE DEFAULT CURRENT_DATE
);

CREATE TABLE "Role" (
    Id SERIAL PRIMARY KEY,
    RoleName VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE "Category" (
    Id SERIAL PRIMARY KEY,
    NameOfCategory VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE "Product" (
    Id SERIAL PRIMARY KEY,
    NameOfProduct VARCHAR(100) NOT NULL,
    CategoryId INT REFERENCES "Category"(Id),
    Description TEXT,
    Price NUMERIC(10, 2) NOT NULL CHECK (Price >= 0),
    Rating NUMERIC(2, 1) DEFAULT 0 CHECK (Rating BETWEEN 0 AND 5),
    InStorage BOOLEAN DEFAULT TRUE
);

CREATE TABLE "ProductReview" (
    Id SERIAL PRIMARY KEY,
    UserId INT REFERENCES "User"(Id) ON DELETE CASCADE,
    ProductId INT REFERENCES "Product"(Id) ON DELETE CASCADE,
    Description TEXT NOT NULL,
    DateOfReview DATE DEFAULT CURRENT_DATE
);

CREATE TABLE "ProductRating" (
    Id SERIAL PRIMARY KEY,
    UserId INT REFERENCES "User"(Id) ON DELETE CASCADE,
    ProductId INT REFERENCES "Product"(Id) ON DELETE CASCADE,
    Mark INT CHECK (Mark BETWEEN 1 AND 5)
);


CREATE TABLE "Cart" (
    Id SERIAL PRIMARY KEY,
    UserId INT REFERENCES "User"(Id) ON DELETE CASCADE,
    TotalPrice NUMERIC(10, 2) DEFAULT 0 CHECK (TotalPrice >= 0)
);

CREATE TABLE "Order" (
    Id SERIAL PRIMARY KEY,
    UserId INT REFERENCES "User"(Id),
    CartId INT REFERENCES "Cart"(Id),
    Status VARCHAR(20) NOT NULL,
    DateOfOrder DATE DEFAULT CURRENT_DATE
);

CREATE TABLE "Storage" (
    ProductId INT PRIMARY KEY REFERENCES "Product"(Id),
    Count INT NOT NULL CHECK (Count >= 0)
);


CREATE TABLE "CartItem" (
    Id SERIAL PRIMARY KEY,
    ProductId INT REFERENCES "Product"(Id),
    CartId INT REFERENCES "Cart"(Id),
    Count INT
);

```

# Запросы для получения данных:

## 1. User
### Create
```sql
INSERT INTO "User" (Login, Email, PasswordHash, RoleId, RegDate)
VALUES ('Admin_User', 'admin@example.com', 'PasswordHash', 1, CURRENT_DATE);
```
### Read
```sql
SELECT * FROM "User";
SELECT * FROM "User" WHERE Id = 1;
SELECT * FROM "User" WHERE Login = 'Admin_User';
```
### Update
```sql
UPDATE "User"
SET Email = 'newemail@example.com', PasswordHash = 'newHashPassword'
WHERE Id = 1;
```
### Delete
```sql
DELETE FROM "User" WHERE Id = 1;
```
### Filtration and sort
```sql
SELECT * FROM "User" ORDER BY RegDate DESC;
SELECT * FROM "User" WHERE RoleId = 2;
SELECT * FROM "User" ORDER BY Email ASC;
SELECT * FROM "User" ORDER BY Email ASC;
```

## 2. Role
### Create
```sql
INSERT INTO "Role" (RoleName)
VALUES ('Admin');
```
### Read
```sql
SELECT * FROM "Role";
SELECT * FROM "Role" WHERE Id = 1;
SELECT * FROM "Role" WHERE RoleName = 'Admin';
```
### Update
```sql
UPDATE "Role"
SET RoleName = 'Manager'
WHERE Id = 1;
```
### Delete
```sql
DELETE FROM "Role" WHERE Id = 1;
```
### Filtration and sort
```sql
SELECT * FROM "Role" ORDER BY RoleName ASC;
```

## 3. Category
### Create
```sql
INSERT INTO "Category" (NameOfCategory)
VALUES ('AK-style');
```
### Read
```sql
SELECT * FROM "Category";
SELECT * FROM "Category" WHERE Id = 1;
SELECT * FROM "Category" WHERE NameOfCategory = 'AK-style';
```
### Update
```sql
UPDATE "Category"
SET NameOfCategory = 'AR-style'
WHERE Id = 1;
```
### Delete
```sql
DELETE FROM "Category" WHERE Id = 1;
```
### Filtration and sort
```sql
SELECT * FROM "Category" ORDER BY NameOfCategory ASC;
```

## 4. Product
### Create
```sql
INSERT INTO "Product" (NameOfProduct, CategoryId, Description, Price, Rating, InStorage)
VALUES ('M4A1', 1, 'Классный привод', 799.99, 4.8, true);
```
### Read
```sql
SELECT * FROM "Product";
SELECT * FROM "Product" WHERE Id = 1;
SELECT * FROM "Product" WHERE NameOfProduct = 'M4A1';
```
### Update
```sql
UPDATE "Product"
SET Description = 'Привод АГОНЬ', Price = 899.99, Rating = 4.9
WHERE Id = 1;
```
### Delete
```sql
DELETE FROM "Product" WHERE Id = 1;
```
### Filtration and sort
```sql
SELECT * FROM "Product" ORDER BY NameOfProduct ASC;
SELECT * FROM "Product" WHERE CategoryId = 1 ORDER BY Price DESC;
SELECT * FROM "Product" WHERE InStorage = true ORDER BY Rating DESC;
```

## 5. Product
### Create
```sql
INSERT INTO "ProductReview" (UserId, ProductId, Description, DateOfReview)
VALUES (1, 1, 'Great product, highly recommended!', CURRENT_DATE);
```
### Read
```sql
SELECT * FROM "ProductReview";
SELECT * FROM "ProductReview" WHERE Id = 1;
SELECT * FROM "ProductReview" WHERE UserId = 1 AND ProductId = 1;
```
### Update
```sql
UPDATE "ProductReview"
SET Description = 'Excellent product, I'm very satisfied.'
WHERE Id = 1;
```
### Delete
```sql
DELETE FROM "ProductReview" WHERE Id = 1;
```
### Filtration and sort
```sql
SELECT * FROM "ProductReview" ORDER BY DateOfReview DESC;
SELECT * FROM "ProductReview" WHERE ProductId = 1 ORDER BY DateOfReview DESC;
SELECT * FROM "ProductReview" WHERE UserId = 1 ORDER BY DateOfReview ASC;
```

## 6. ProductRating
### Create
```sql
INSERT INTO "ProductRating" (UserId, ProductId, Mark)
VALUES (1, 1, 5);
```
### Read
```sql
SELECT * FROM "ProductRating";
SELECT * FROM "ProductRating" WHERE Id = 1;
SELECT * FROM "ProductRating" WHERE UserId = 1 AND ProductId = 1;
```
### Update
```sql
UPDATE "ProductRating"
SET Mark = 4
WHERE Id = 1;
```
### Delete
```sql
DELETE FROM "ProductRating" WHERE Id = 1;
```
### Filtration and sort
```sql
SELECT * FROM "ProductRating" ORDER BY Mark DESC;
SELECT * FROM "ProductRating" WHERE ProductId = 1 ORDER BY Mark DESC;
SELECT * FROM "ProductRating" WHERE UserId = 1 ORDER BY Mark ASC;
```

## 7. Cart
### Create
```sql
INSERT INTO "Cart" (UserId, TotalPrice)
VALUES (1, 0);
```
### Read
```sql
SELECT * FROM "Cart";
SELECT * FROM "Cart" WHERE Id = 1;
SELECT * FROM "Cart" WHERE UserId = 1;
```
### Update
```sql
UPDATE "Cart"
SET TotalPrice = 5000
WHERE Id = 1;
```
### Delete
```sql
DELETE FROM "Cart" WHERE Id = 1;
```
### Filtration and sort
```sql
SELECT * FROM "Cart" ORDER BY TotalPrice DESC;
SELECT * FROM "Cart" WHERE UserId = 1 ORDER BY TotalPrice ASC;
```

## 8. CartItem
### Create
```sql
INSERT INTO "CartItem" (ProductId, CartId, Count)
VALUES (1, 1, 2);
```
### Read
```sql
SELECT * FROM "CartItem";
SELECT * FROM "CartItem" WHERE Id = 1;
SELECT * FROM "CartItem" WHERE CartId = 1;
```
### Update
```sql
UPDATE "CartItem"
SET Count = 3
WHERE Id = 1;
```
### Delete
```sql
DELETE FROM "CartItem" WHERE Id = 1;
```
### Filtration and sort
```sql
SELECT * FROM "CartItem" ORDER BY Count DESC;
SELECT * FROM "CartItem" WHERE CartId = 1 ORDER BY ProductId ASC;
```

## 9. Order
### Create
```sql
INSERT INTO "Order" (UserId, CartId, Status)
VALUES (1, 1, 'Pending');
```
### Read
```sql
SELECT * FROM "Order";
SELECT * FROM "Order" WHERE Id = 1;
SELECT * FROM "Order" WHERE UserId = 1;
```
### Update
```sql
UPDATE "Order"
SET Status = 'Completed'
WHERE Id = 1;
```
### Delete
```sql
DELETE FROM "Order" WHERE Id = 1;
```
### Filtration and sort
```sql
SELECT * FROM "Order" ORDER BY DateOfOrder DESC;
SELECT * FROM "Order" WHERE UserId = 1 ORDER BY DateOfOrder ASC;
SELECT * FROM "Order" WHERE Status = 'Pending' ORDER BY DateOfOrder DESC;
```

## 10. Storage
### Create
```sql
INSERT INTO "Storage" (ProductId, Count)
VALUES (1, 50);
```
### Read
```sql
SELECT * FROM "Storage";
SELECT * FROM "Storage" WHERE ProductId = 1;
```
### Update
```sql
UPDATE "Storage"
SET Count = 30
WHERE ProductId = 1;
```
### Delete
```sql
DELETE FROM "Storage" WHERE ProductId = 1;
```
### Filtration and sort
```sql
SELECT * FROM "Storage" ORDER BY Count DESC;
```

# Лабораторная работа №4
### 1. Пул запросов для сложной выборки
#### Запрос с несколькими условиями
##### a. Для таблицы `User`

```sql
SELECT *
FROM "User"
WHERE (roleid = 1)
  AND RegDate >= '2024-01-01'
  AND Email LIKE '%@example.com';
```

##### b. Для таблицы `Product`

```sql
SELECT *
FROM "Product"
WHERE CategoryId = 1
  AND Price BETWEEN 100 AND 1000
  AND InStorage = true;
```

##### c. Для таблицы `Order`

```sql
SELECT *
FROM "Order"
WHERE UserId IN (
    SELECT Id
    FROM "User"
    WHERE roleid=1
)
AND DateOfOrder >= '2024-01-01';
```

#### Запрос с вложенной конструкцией

##### a. Для таблицы `Product`

```sql
SELECT *
FROM "Product"
WHERE Price < (
    SELECT AVG(Price)
    FROM "Product"
    WHERE categoryid = 1
);
```

##### b. Для таблицы `Cart`

```sql
SELECT *
FROM "Cart"
WHERE UserId = (
    SELECT Id
    FROM "User"
    WHERE Login = 'john_doe'
);
```

#### Прочие сложные выборки

##### a. Получение пользователей с количеством заказов

```sql
SELECT u.Login, COUNT(o.Id) AS OrderCount
FROM "User" u
LEFT JOIN "Order" o ON u.Id = o.UserId
GROUP BY u.Login
HAVING COUNT(o.Id) >= 1;
```

##### b. Получение товаров в корзине с общей стоимостью

```sql
SELECT p.NameOfProduct, SUM(ci.Count * p.Price) AS TotalPrice
FROM "CartItem" ci
JOIN "Product" p ON ci.ProductId = p.Id
WHERE ci.CartId = (
    SELECT Id
    FROM "Cart"
    WHERE UserId = 1
)
GROUP BY p.NameOfProduct;
```

### 2. Пул запросов для получения представлений

#### INNER JOIN

##### a. Получение заказов с информацией о пользователе

```sql
SELECT o.Id, u.Login, o.DateOfOrder, o.cartid
FROM "Order" o
INNER JOIN "User" u ON o.UserId = u.Id;
```

#### LEFT JOIN

##### b. Получение всех пользователей и их корзин (если есть)

```sql
SELECT u.Login, c.TotalPrice, c.id
FROM "User" u
LEFT JOIN "Cart" c ON u.Id = c.UserId;
```

#### RIGHT JOIN

##### c. Получение всех корзин и их пользователей (если есть)

```sql
SELECT c.Id AS CartId, u.Login
FROM "Cart" c
RIGHT JOIN "User" u ON c.UserId = u.Id;
```

#### FULL OUTER JOIN

##### d. Получение всех пользователей и всех заказов, даже если у них нет соответствий

```sql
SELECT u.Login, o.Id, o.DateOfOrder
FROM "User" u
FULL OUTER JOIN "Order" o ON u.Id = o.UserId;
```

#### CROSS JOIN

##### e. Получение всех возможных комбинаций пользователей и продуктов

```sql
SELECT u.Login, p.NameOfProduct
FROM "User" u
CROSS JOIN "Product" p;
```

### 3. Пул запросов для получения сгруппированных данных

#### GROUP BY + агрегирующие функции

##### a. Подсчет количества пользователей по ролям

```sql
SELECT RoleId, COUNT(*) AS UserCount
FROM "User"
GROUP BY RoleId;
```

##### b. Подсчет количества заказов по пользователям

```sql
SELECT UserId, COUNT(*) AS OrderCount
FROM "Order"
GROUP BY UserId;
```

##### c. Сумма цен товаров в каждой категории

```sql
SELECT CategoryId, SUM(Price) AS TotalValue
FROM "Product"
GROUP BY CategoryId;
```

#### HAVING

##### d. Получение категорий с средней ценой выше 100

```sql
SELECT CategoryId, AVG(Price) AS AveragePrice
FROM "Product"
GROUP BY CategoryId
HAVING AVG(Price) > 100;
```

##### e. Получение пользователей с более чем 3 заказов

```sql
SELECT UserId, COUNT(*) AS OrderCount
FROM "Order"
GROUP BY UserId
HAVING COUNT(*) > 3;
```

#### PARTITION и оконные функции

##### f. Нумерация пользователей по дате регистрации в каждой роли

```sql
SELECT Id, Login, RoleId,
       ROW_NUMBER() OVER (PARTITION BY Role ORDER BY RegDate) AS RowNum
FROM "User";
```

##### g. Получение средней оценки для каждого продукта с указанием ранга

```sql
SELECT ProductId, Description,
       RANK() OVER (PARTITION BY ProductId ORDER BY Rating DESC) AS RatingRank
FROM "ProductReview";
```

#### UNION

##### h. Объединение пользователей и продуктов в одну выборку

```sql
SELECT Id, Login AS Name FROM "User"
UNION
SELECT Id, NameOfProduct AS Name FROM "Product";
```

### 4. Пул запросов для сложных операций с данными

#### EXISTS

##### a. Получение пользователей, у которых есть заказы

```sql
SELECT *
FROM "User" u
WHERE EXISTS (
    SELECT 1
    FROM "Order" o
    WHERE o.UserId = u.Id
);
```

##### b. Получение продуктов, которые имеют отзывы ниже 3

```sql
SELECT *
FROM "Product" p
WHERE EXISTS (
    SELECT 1
    FROM "ProductRating" r
    WHERE r.ProductId = p.Id AND r.mark < 3
);
```

#### INSERT INTO SELECT

##### c. Архивирование пользователей, зарегистрированных до 2022 года

```sql
INSERT INTO "ArchivedUser" (Id, Login, Email)
SELECT Id, Login, Email
FROM "User"
WHERE RegDate < '2022-01-01';
```

##### d. Копирование продуктов в категорию "Старые товары"

```sql
INSERT INTO "OldProduct" (Id, NameOfProduct, Price)
SELECT Id, NameOfProduct, Price
FROM "Product"
WHERE InStorage = 0;
```

#### CASE

##### e. Получение статуса заказа в зависимости от его суммы

```sql
SELECT o.OrderId, c.TotalPrice,
       CASE
           WHEN c.TotalPrice > 1000 THEN 'High Value'
           WHEN c.TotalPrice BETWEEN 500 AND 1000 THEN 'Medium Value'
           ELSE 'Low Value'
       END AS OrderStatus
FROM "Order" o
JOIN "Cart" c ON o.CartId = c.Id;
```

##### f. Получение статуса продукта в зависимости от его наличия

```sql
SELECT Id, NameOfProduct,
       CASE
           WHEN InStorage = false THEN 'Out of Stock'
           ELSE 'In Stock'
       END AS StockStatus
FROM "Product";
```

#### EXPLAIN

##### g. Анализ выполнения запроса на получение пользователей с определенной ролью

```sql
EXPLAIN SELECT *
FROM "User"
WHERE RoleId = 1;
```

##### h. Анализ выполнения запроса на получение всех заказов

```sql
EXPLAIN SELECT *
FROM "Order";
```
