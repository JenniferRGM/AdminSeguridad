CREATE DATABASE AdminSeguridad;
GO
USE AdminSeguridad;
GO

CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL,
    Apellido1 NVARCHAR(50) NOT NULL,
    Apellido2 NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Clave NVARCHAR(50) NOT NULL,
    FechaCreacion DATE NOT NULL,
    FechaActualizacion DATE NOT NULL,
    RolID INT NOT NULL,
    FOREIGN KEY (RolID) REFERENCES Roles(RolID)
);

CREATE TABLE Telefonos (
    TelefonoID INT PRIMARY KEY IDENTITY(1,1),
    UsuarioID INT NOT NULL,
    CodigoPais NVARCHAR(5) NOT NULL,
    NumeroTelefono NVARCHAR(15) NOT NULL,
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);

CREATE TABLE Roles (
    RolID INT PRIMARY KEY IDENTITY(1,1),
    NombreRol NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Permisos (
    PermisoID INT PRIMARY KEY IDENTITY(1,1),
    NombrePermiso NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Menus (
    MenuID INT PRIMARY KEY IDENTITY(1,1),
    NombreMenu NVARCHAR(50) NOT NULL UNIQUE,
    URL NVARCHAR(200) -- Columna URL para asociar el formulario web que se muestra según el permiso
);

CREATE TABLE Roles_Permisos (
    RolID INT NOT NULL,
    PermisoID INT NOT NULL,
    MenuID INT NOT NULL,
    PermisoLectura BIT NOT NULL,
    PermisoEscritura BIT NOT NULL,
    PermisoModificacion BIT NOT NULL,
    PermisoEliminacion BIT NOT NULL,
    FechaCreacion DATE NOT NULL,
    FechaActualizacion DATE NOT NULL,
    PRIMARY KEY (RolID, PermisoID, MenuID),
    FOREIGN KEY (RolID) REFERENCES Roles(RolID),
    FOREIGN KEY (PermisoID) REFERENCES Permisos(PermisoID),
    FOREIGN KEY (MenuID) REFERENCES Menus(MenuID)
);

-- Creación de tablas para ubicación

CREATE TABLE Provincias (
    ProvinciaID INT PRIMARY KEY IDENTITY(1,1),
    NombreProvincia NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Cantones (
    CantonID INT PRIMARY KEY IDENTITY(1,1),
    NombreCanton NVARCHAR(50) NOT NULL UNIQUE, 
    ProvinciaID INT NOT NULL,
    FOREIGN KEY (ProvinciaID) REFERENCES Provincias(ProvinciaID)
);

CREATE TABLE Distritos (
    DistritoID INT PRIMARY KEY IDENTITY(1,1),
    NombreDistrito NVARCHAR(50) NOT NULL UNIQUE, 
    CantonID INT NOT NULL,
    FOREIGN KEY (CantonID) REFERENCES Cantones(CantonID)
);

CREATE TABLE Ubicaciones (
    UbicacionID INT PRIMARY KEY IDENTITY(1,1),
    UsuarioID INT NOT NULL,
    ProvinciaID INT NOT NULL,
    CantonID INT NOT NULL,
    DistritoID INT NOT NULL,
    OtrasSenias NVARCHAR(100) NOT NULL,
    UNIQUE (UsuarioID, ProvinciaID, CantonID, DistritoID),
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID),
    FOREIGN KEY (ProvinciaID) REFERENCES Provincias(ProvinciaID),
    FOREIGN KEY (CantonID) REFERENCES Cantones(CantonID),
    FOREIGN KEY (DistritoID) REFERENCES Distritos(DistritoID)
);

-- Catálogo de Permisos

CREATE TABLE CatalogoPermisos (
    PermisoID INT PRIMARY KEY IDENTITY(1,1),
    DescripcionPermiso NVARCHAR(100) NOT NULL UNIQUE
);

-- Tabla de Login 

CREATE TABLE Login (
    UsuarioID INT NOT NULL, -- ID del usuario
    Usuario NCHAR(10) NOT NULL, -- Nombre de usuario
    Clave VARCHAR(50) NOT NULL, -- Clave del usuario
    PRIMARY KEY (UsuarioID),
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
) ON [PRIMARY]; 