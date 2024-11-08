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
![Image alt](https://github.com/PolskaProger/MDiSUBD/blob/main/LR1DB.drawio.png)
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
## 10. SalesReview (Отчет о продажах):
### Поля:
* Id — первичный ключ (PK).
* DateStart — дата начала отчетного периода.
* DateEnd — дата окончания отчетного периода.
* TotalPriceInPeriod — общая сумма продаж за период.
### Связи:
* Нет явных связей, но отчет может быть связан с продажами товаров за определенный период.
# Вывод по типам связей:
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
    ListOfProducts TEXT,  -- Список продуктов в виде JSON или другой структуры
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
    ProductId INT PRIMARY KEY REFERENCES "Product"(Id),
    CartId INT REFERENCES "Cart"(Id),
    Count INT
);

```

# Запросы для получения данных:
