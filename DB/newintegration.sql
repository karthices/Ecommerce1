-- insert default users

--insert into Users(Username,[Password],DisplayName,ContactNo,IsActive) 
--values('karthices','123','Karthikeyan','7736422669',1);


insert into Categories(CategoriesTitle,ParentCategory,CategoriesImage,CategoriesSlug,IsActive)
values('Phones',0,'dummy.jpg','phones',1),
('HeadPhones',0,'dummy.jpg','headphones',1),
('MemoryCard',0,'dummy.jpg','memorycard',1);