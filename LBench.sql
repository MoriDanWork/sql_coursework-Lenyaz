drop database coursework;
create database coursework;
use coursework;

drop table ingredients
drop table contents
drop table ingredients_in_contents
drop table contents_in_menu
drop table menu

create table ingredients
(
id_ingredient int identity primary key,
ingredient_name varchar(50) unique not null,
proteins int not null,
fats int not null,
carbohydr int not null,
calories int not null,
price float not null, --price per kilo
)



create table contents --ñîñòàâ
(
id_content int identity PRIMARY KEY,
content_name varchar(50) unique not null,
weight_info int not null
)

create table ingredients_in_contents
(
id_ingredient int not null ,
id_content int not null,
FOREIGN KEY (id_content)  REFERENCES contents (id_content),
FOREIGN KEY (id_ingredient)  REFERENCES ingredients (id_ingredient)
)

create table menu
(
id_menu int identity not null primary key,
menu_name varchar(20),
is_dietic bit,
is_vegan bit,
is_full bit,
)

create table contents_in_menu
(
id_content int not null ,
id_menu int not null,
FOREIGN KEY (id_content)  REFERENCES contents (id_content),
FOREIGN KEY (id_menu)  REFERENCES menu(id_menu)
)


create table ingredients_in_contents
(
id_ingredient int not null ,
id_content int not null,
FOREIGN KEY (id_content)  REFERENCES contents (id_content),
FOREIGN KEY (id_ingredient)  REFERENCES ingredients (id_ingredient)
)
///////////////////////////////////////////////////

insert into ingredients (ingredient_name, proteins, fats, carbohydr, calories, price) values ('Water', 0, 0, 0, 0, 100)
insert into ingredients (ingredient_name, proteins, fats, carbohydr, calories, price) values ('Salt', 0, 0, 0, 0, 20)
insert into ingredients (ingredient_name, proteins, fats, carbohydr, calories, price) values ('Suka', 0, 0, 100, 387, 10)

insert into contents (content_name, weight_info) values ('Papich', 100)

select id_ingredient, ingredient_name from ingredients

select sum(price) from ingredients

select * from ingredients;
select * from contents
select Sum(calories) from ingredients 
join ingredients_in_contents on ingredients.id_ingredient = ingredients_in_contents.id_ingredient
join contents on ingredients_in_contents.id_content = contents.id_content where ingredients_in_contents.id_content = '2'


insert into menu (menu_name,is_dietic,is_vegan,is_full) values (1,1,1,1)
select * from menu
select id_content, content_name from contents

select * from ingredients where lower(ingredient_name) like '%s%'; --"ug" search