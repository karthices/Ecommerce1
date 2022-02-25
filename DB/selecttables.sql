use EcommerceDev;
select * from Users;
select * from Categories;
select * from Products;
--drop table Users
-- drop table categories

--update Users set isActive=1 where UserId = 1

select * from Categories;

select * from Categories order by CategoriesId desc;



select c1.categoriesId,c1.categoriestitle,c2.categoriestitle as parentcategory,c1.categoriesimage,c1.isactive from Categories c1
left join Categories c2 on c2.categoriesId=c1.parentcategory order by c1.CategoriesId desc;