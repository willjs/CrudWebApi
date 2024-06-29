Create database DDCrudAngular

go

use DDCrudAngular

create table Empleado(
IdEmpleado int primary key identity,
NombreDescripcion Varchar(250),
Direccion Varchar(250),
Identificacion varchar(50),
Moneda varchar(50),
FechaCreacion datetime default getdate()
)
go
insert into Empleado(NombreDescripcion,Direccion,Identificacion,Moneda,FechaCreacion) values 
('Lorem Ipsum','cra96#85-134','123456789','oro',getdate())


Select * from Empleado

create proc sp_listaEmpleados
as 
begin
  select 
   IdEmpleado,
   NombreDescripcion,
   Direccion,
   Identificacion ,
   Moneda ,
    CONVERT (CHAR (10), FechaCreacion,103)[FechaCreacion]
	From Empleado
end


create proc sp_obtenerEmpleados(
@IdEmpleado int
)
as 
begin
  select 
   IdEmpleado,
   NombreDescripcion,
   Direccion,
   Identificacion ,
   Moneda ,
    CONVERT (CHAR (10), FechaCreacion,103)[FechaCreacion]
	From Empleado where IdEmpleado = @IdEmpleado
end


create proc sp_crearEmpleado(
@NombreDescripcion Varchar(250),
@Direccion Varchar(250),
@Identificacion varchar(50),
@Moneda varchar(50),
@FechaCreacion varchar (10)
)
as 
begin 

set dateformat dmy
insert into Empleado
(NombreDescripcion,
Direccion,
Identificacion,
Moneda,
FechaCreacion)
values 
(@NombreDescripcion,
@Direccion,
@Identificacion,
@Moneda,
convert(date,@FechaCreacion)
)
end



create proc sp_editarEmpleado(
@IdEmpleado int,
@NombreDescripcion Varchar(250),
@Direccion Varchar(250),
@Identificacion varchar(50),
@Moneda varchar(50),
@FechaCreacion varchar (10)
)
as 
begin 

set dateformat dmy
update Empleado
set
NombreDescripcion=@NombreDescripcion,
Direccion=@Direccion,
Identificacion=@Identificacion,
Moneda=@Moneda,
FechaCreacion=convert(date,@FechaCreacion)
where IdEmpleado = @IdEmpleado
end


create proc sp_eliminarEmpleado(
@IdEmpleado int
)
as 
begin 

delete from Empleado where IdEmpleado = @IdEmpleado

end