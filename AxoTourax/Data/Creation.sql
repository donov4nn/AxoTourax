CREATE DATABASE AxoTourax

CREATE TABLE Bobine
(
  IdBobine               int      NOT NULL IDENTITY(1,1),
  Reference              nvarchar NOT NULL,
  IdMatiere              int     ,
  IdTechnique            int     ,
  Poids                  float   ,
  PoidsMaximum           float   ,
  Stock                  int     ,
  Prix                   money   ,
  DiametreExterieur      float   ,
  DiametreInterieur      float   ,
  DiametreTrouAxeCentral float   ,
  LargeurInterieur       float   ,
  LargeurExterieur       float   ,
  Consigne               bit     ,
  Photo                  nvarchar,
  CONSTRAINT PK_Bobine PRIMARY KEY (IdBobine)
)
GO

CREATE TABLE Calcul
(
  IdCalcul              int      NOT NULL IDENTITY(1,1),
  IdBobine              int      NOT NULL,
  DiametreCable         float    NOT NULL,
  PoidsCable            float    NOT NULL,
  CoefficiantCorrection float    NOT NULL,
  Longueur              float    NOT NULL,
  TypeCable             nvarchar NOT NULL,
  DateCalcul            date     NOT NULL,
  CONSTRAINT PK_Calcul PRIMARY KEY (IdCalcul)
)
GO

CREATE TABLE MatiereBobine
(
  IdMatiere int      NOT NULL,
  Libelle   nvarchar NOT NULL,
  CONSTRAINT PK_MatiereBobine PRIMARY KEY (IdMatiere)
)
GO

--CREATE TABLE Role
--(
--  IdRole  int      NOT NULL,
--  Libelle nvarchar NOT NULL,
--  CONSTRAINT PK_Role PRIMARY KEY (IdRole)
--)
--GO

CREATE TABLE TechniqueBobine
(
  IdTechnique int      NOT NULL,
  Libelle     nvarchar NOT NULL,
  CONSTRAINT PK_TechniqueBobine PRIMARY KEY (IdTechnique)
)
GO

--CREATE TABLE Utilisateur
--(
--  IdUtilisateur int      NOT NULL IDENTITY(1,1),
--  Mail          nvarchar NOT NULL,
--  Password      nvarchar NOT NULL,
--  IdRole        int      NOT NULL,
--  CONSTRAINT PK_Utilisateur PRIMARY KEY (IdUtilisateur)
--)
--GO

ALTER TABLE Bobine
  ADD CONSTRAINT FK_MatiereBobine_TO_Bobine
    FOREIGN KEY (IdMatiere)
    REFERENCES MatiereBobine (IdMatiere)
GO

ALTER TABLE Bobine
  ADD CONSTRAINT FK_TechniqueBobine_TO_Bobine
    FOREIGN KEY (IdTechnique)
    REFERENCES TechniqueBobine (IdTechnique)
GO

ALTER TABLE Calcul
  ADD CONSTRAINT FK_Bobine_TO_Calcul
    FOREIGN KEY (IdBobine)
    REFERENCES Bobine (IdBobine)
GO

--ALTER TABLE Utilisateur
--  ADD CONSTRAINT FK_Role_TO_Utilisateur
--    FOREIGN KEY (IdRole)
--    REFERENCES Role (IdRole)
--GO