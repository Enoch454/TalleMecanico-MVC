CREATE database Taller_Mecanico2;
GO
use Taller_Mecanico2;
GO
create table Roles(
	Id int  primary key,
	Rol varchar (30)
);
go
create table Estado(
	Id int  primary key,
    Estado varchar(100),
);
GO
create table Usuarios(
	Id int identity primary key,
    Usuario varchar(10),
    Pwd varchar(15),
    IdRol int,
	IdEstado int,
	constraint fk_rol foreign key(IdRol) references Roles(Id),
	constraint fk_estado foreign key(IdEstado) references Estado(Id) 
);
GO
create table Carros
(
	Id int identity primary key,
    Marca varchar(15),
    Modelo varchar(15),
    Color varchar(15),
	codigo varchar(300),
    DescripcionDetalles varchar(100),
	IdEstado int,
    Id_Cliente int,
	Id_Empleado int,
    constraint fk_carros_clientes foreign key (Id_Cliente) references Usuarios(Id),
	constraint fk_carros_empleados foreign key (Id_Empleado) references Usuarios(Id),
	constraint fk_carros_estado foreign key(IdEstado) references Estado(Id) 
);
GO

create table Evidencias
(
	Id int identity primary key,
	Fecha date,
    Comentarios varchar(500),
	foto image,
    Id_Carro int,
    constraint fk_evidencias_carros foreign key (Id_Carro) references Carros(Id)
);
select * from Evidencias

GO
Insert into Roles values(1,'Administrador');
Insert into Roles values(2,'Trabajador');
Insert into Roles values(3,'Cliente');
GO
Insert into Estado values(1,'Disponible');
Insert into Estado values(2,'Ocupado');
Insert into Estado values(3,'En reparacion');
Insert into Estado values(4,'Liberado');

GO
insert Usuarios values('karen','123',1,1);
insert Usuarios values('enoch','123',2,1);
insert Usuarios values('moi','123',3,1);

insert Usuarios values('trab1','111',2,1);
insert Usuarios values('trab2','222',2,1);
insert Usuarios values('trab3','333',2,1);
insert Usuarios values('trab4','444',2,1);

select* from Usuarios
INSERT INTO Evidencias(Foto) SELECT * FROM OPENROWSET(BULK N'C:\Users\Karen\Pictures/chevy.jpg', SINGLE_BLOB) as foto