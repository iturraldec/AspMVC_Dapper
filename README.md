# Aplicación web ASP.NET Core (Modelo-Vista-Controlador), con C#

## Descripción

Aplicación web basada en el framework de Microsoft Asp.Net Core 7.0 y el micro-ORM Dapper.
Muestra por pantalla las categorias y los productos de la base de datos de prueba SQL Server 'Northwind', pudiendo agregar productos a la misma.

## Instalación

Asegurate de:
- Tener instalado los Runtimes de AspNetCore 7 (ó 6)
- Tener instalado el Manejador de base de datos SQL Server
- Tener instalada la base de datos 'Northwind', puedes descargarla [aqui](https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/)
- Cambiar la cadena de conexión a la base de datos en el archivo 'Controllers/HomeController.cs' con las credenciales correspondientes a tu manejador

## Uso
Una vez descargada la aplicación podras ver y seleccionar las categorias de productos, los productos de una categoria determinada y agregar una producto determinado.
    ![Categorias](/assets/images/img_2023-08-01_17-03-39.png)
    ![Productos de la categoria](/assets/images/img_2023-08-01_17-04-01.png)
    ![Producto agregado](/assets/images/img_2023-08-01_17-05-00.png)
    
## Licencia

GNU GPL
