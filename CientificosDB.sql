CREATE DATABASE CientificosDB;
GO

USE CientificosDB;
GO

-- 1. Tabla Cientificos
CREATE TABLE Cientificos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Nacionalidad VARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Email VARCHAR(150) UNIQUE NOT NULL
);

-- 2. Tabla Especialidades
CREATE TABLE Especialidades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255)
);

-- 3. Tabla intermedia CientificoEspecialidad (Muchos a Muchos)
CREATE TABLE CientificoEspecialidad (
    CientificoId INT NOT NULL,
    EspecialidadId INT NOT NULL,
    PRIMARY KEY (CientificoId, EspecialidadId),
    FOREIGN KEY (CientificoId) REFERENCES Cientificos(Id) ON DELETE CASCADE,
    FOREIGN KEY (EspecialidadId) REFERENCES Especialidades(Id) ON DELETE CASCADE
);

-- 4. Tabla Descubrimientos
CREATE TABLE Descubrimientos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Titulo VARCHAR(150) NOT NULL,
    Descripcion VARCHAR(MAX),
    FechaDescubrimiento DATE NOT NULL,
    Lugar VARCHAR(150),
    CientificoId INT NOT NULL,
    FOREIGN KEY (CientificoId) REFERENCES Cientificos(Id) ON DELETE CASCADE
);

-- 5. Tabla Investigaciones
CREATE TABLE Investigaciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Titulo VARCHAR(150) NOT NULL,
    AreaEstudio VARCHAR(100) NOT NULL,
    FechaInicio DATE NOT NULL,
    FechaFin DATE NULL,
    Estado VARCHAR(50) CHECK (Estado IN ('En curso', 'Finalizada', 'Publicada')) NOT NULL,
    CientificoId INT NOT NULL,
    FOREIGN KEY (CientificoId) REFERENCES Cientificos(Id) ON DELETE CASCADE
);

SELECT * FROM Cientificos