go
create database zapateriaDB
go
use zapateriaDB
go
SET DATEFORMAT ydm 
-------------------------------------------------------------
--Creación de Tablas
-------------------------------------------------------------
--go
--Create table 
--(



--);
-----------------------
go
Create table Zapato
(
idZapato int not null,
idCategoria int not null,
color varchar(50),
descripcion varchar(50),
cantMax int,
cantMin int,
cantidadTot int
);

go
Create table Proveedor
(
idProveedor int not null,
nombre varchar(50),
telefono int,
pais varchar(50),
direccion varchar(50),
email varchar(50)
);

go
Create table ProveedorContacto
(
idProveedor int not null,
idContacto int not null
);



go
Create table Contacto
(
idContacto int not null,
nombre varchar(50),
telefono int,
email varchar(50)
);

go
Create table ZapatoProveedor
(
idZapato int not null,
idProveedor int not null
);

go
Create table Categoria
(
idCategoria int not null,
nombre varchar(50),
descripcion varchar (50)
);

go
Create table Usuario
(
idUsuario int not null,
email varchar(50),
idRol int not null,
password varchar(50),
nombre varchar(50),
apellidos varchar(50),
estado bit,
);

go
Create table Rol
(
idRol int not null,
descripcion varchar(50)
);

go
Create table Entradas_Salidas
(
idEntradas_Salidas int not null,
fecha date,
idGestion int,
idZapato int,
cantidadProducto int,
idUsuario int not null,
idUbicacion int,
idProveedor int,
descripcion varchar(50)
);

go
Create table TipoGestion
(
idGestion int not null,
Descripcion varchar(50)
);


go
Create table Ubicacion
(
idUbicacion int not null,
descripcion varchar(50),
cantidad int
);

go
Create table ZapatoUbicacion
(
idUbicacion int not null,
idZapato int not null
);

-------------------------------------------------------------
-- Modificación de tablas - Llaves Primarias (PK)
-------------------------------------------------------------
go
Alter table Zapato add constraint PKidZapato primary key (idZapato)
go
Alter table Proveedor add constraint PKProveedor primary key (idProveedor)
go
Alter table Contacto add constraint PKContacto primary key (idContacto)
go
Alter table ZapatoProveedor add constraint PKZapatoProveedor primary key (idZapato,idProveedor)
go
Alter table ZapatoUbicacion add constraint PKZapatoUbicacion primary key (idUbicacion,idZapato)
go
Alter table ProveedorContacto add constraint PKProveedorContacto primary key (idProveedor,idContacto)
go
Alter table Categoria add constraint PKCategoria primary key (idCategoria)
go
Alter table Usuario add constraint PKUsuario primary key (idUsuario)
go
Alter table Rol add constraint PKRol primary key (idRol)
go
Alter table Entradas_Salidas add constraint PKEntradas_Salidasa primary key (idEntradas_Salidas)
go
Alter table TipoGestion add constraint PKTipoGestion primary key (idGestion)
go
Alter table Ubicacion add constraint PKidUbicacion primary key (idUbicacion)

-------------------------------------------------------------
-- Modificación de tablas - Llaves Foráneas (FK)
-------------------------------------------------------------
go
ALTER TABLE Zapato ADD CONSTRAINT FKZapato_Categoria FOREIGN KEY(idCategoria)REFERENCES Categoria(idCategoria)
go
ALTER TABLE ZapatoProveedor ADD CONSTRAINT FKZapatoProveedor_Zapato FOREIGN KEY(idZapato)REFERENCES Zapato(idZapato)
go
ALTER TABLE ZapatoProveedor ADD CONSTRAINT FKZapatoProveedor_Proveedor FOREIGN KEY(idProveedor)REFERENCES Proveedor(idProveedor)
go
ALTER TABLE ProveedorContacto ADD CONSTRAINT FKProveedorContacto_Proveedor FOREIGN KEY(idProveedor)REFERENCES Proveedor(idProveedor)
go
ALTER TABLE ProveedorContacto ADD CONSTRAINT FKProveedorContacto_Contacto FOREIGN KEY(idContacto)REFERENCES Contacto(idContacto)
go
ALTER TABLE Entradas_Salidas ADD CONSTRAINT FKEntradas_Salidas_Usuario FOREIGN KEY(idUsuario)REFERENCES Usuario(idUsuario)
go
ALTER TABLE Entradas_Salidas ADD CONSTRAINT FKEntradas_Salidas_Gestion FOREIGN KEY(idGestion)REFERENCES TipoGestion(idGestion)
go
ALTER TABLE Entradas_Salidas ADD CONSTRAINT FKEntradas_Salidas_Zapato FOREIGN KEY(idZapato)REFERENCES Zapato(idZapato)
go
ALTER TABLE Usuario ADD CONSTRAINT FKUsuario_Rol FOREIGN KEY(idRol)REFERENCES Rol(idRol)
go
ALTER TABLE ZapatoUbicacion ADD CONSTRAINT FKZapatoUbicacion_Zapato FOREIGN KEY(idZapato)REFERENCES Zapato(idZapato)
go
ALTER TABLE ZapatoUbicacion ADD CONSTRAINT FKZapatoUbicacion_Ubicacion FOREIGN KEY(idUbicacion)REFERENCES Ubicacion(idUbicacion)

-------------------------------------------------------------
-- Add Values
-------------------------------------------------------------
use zapateriaDB

select * from Categoria

go
Insert into Categoria values('01','Nike Force 1','Hombre');
Insert into Categoria values('02','Nike  Air Max','Mujer');
Insert into Categoria values('03','Nike Jordan','Hombre');
Insert into Categoria values('04','Nike Pegasus','Mujer');
Insert into Categoria values('05','Nike Revolution','Hombre');
Insert into Categoria values('06','Puma Radiografia','Mujer');
Insert into Categoria values('07','Adias Hoops 2','Hombre')
Insert into Categoria values('08','Fila','Hombre');
Insert into Categoria values('09','Nike','Mujer');
Insert into Categoria values('010','New Balance','Hombre');

go
Insert into Ubicacion values('01','Bodega','0');
Insert into Ubicacion values('02','Tienda','0');
Insert into Ubicacion values('03','Almacen','0');

go
Insert into Contacto values('01','Rodolfo Peréz','85751249', 'rodolfo001@gmail.com');
Insert into Contacto values('02','Rodrigo Garcia','88847962', 'rodrigoG_002@gmail.com');
Insert into Contacto values('03','Andrés Carranza','86745214', 'andreca003@hotmail.com');
Insert into Contacto values('04','Vladimir Zamora','89751245', 'Vladi004@gmail.com');
insert into Contacto values('005', 'Ramiro Carrillo','61257896' ,'ramiroCarrillo005@gmail.com');

go 
insert into Proveedor values ('01', 'Nike', '85751249', 'Estados Unidos','Eugene' ,'rodolfo001@gmail.com');
insert into Proveedor values ('02', 'New Balance.', '88847962', 'Costa Rica','San José' ,'rodrigoG_002@gmail.com');
insert into Proveedor values ('03', 'Puma', '86745214', 'Alemania','Herzogenaurach' ,'andreca003@hotmail.com');
insert into Proveedor values ('04', 'Adidas', '89751245', 'Alemania','Herzogenaurach' ,'Vladi004@gmail.com');
insert into Proveedor values ('05', 'Fila', '61257896', 'Corea del Sur','Seoul' ,'ramiroCarrillo005@gmail.com');

go
Insert into Rol values('1','Admin');
Insert into Rol values('2','Encargado');

go
Insert into Usuario values ('1','karla_vanessa_21@hotmail.com','1','123456','Karla','Ballestero Castro','1'); 
Insert into Usuario values ('2','kevinpaniagua@gmail.com','2','123456','Kevin','Paniagua Herrera','2'); 

----------------------------------------------------------------------------------------------------------
go
insert into Zapato values ('001', '01', 'Negro', 'Nike Force 1','5','3','5');
insert into Zapato values ('002', '02', 'Negro', 'Nike Air Max','5','3','5');
insert into Zapato values ('003', '03', 'Negro', 'Nike Jordan','5','3','5');
insert into Zapato values ('004', '04', 'Negro', 'Nike Pegasus','5','3','5');
insert into Zapato values ('005', '05', 'Negro', 'Nike Revolution','5','3','5');
insert into Zapato values ('006', '06', 'Negro', 'Puma Radiografia','5','3','5');
insert into Zapato values ('007', '07', 'Negro', 'Adidas Hoops 2','5','3','5');
insert into Zapato values ('008', '08', 'Negro', 'Fila','5','3','5');
insert into Zapato values ('009', '09', 'Negro', 'Nike','5','3','5');
insert into Zapato values ('010', '010','Negro', 'New Balance','5','3','5');
insert into Zapato values ('011', '010','Negro', 'New Balance','5','3','5');
