IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'AxoTourax')
BEGIN

CREATE DATABASE AxoTourax

END


drop table if exists Calcul
drop table if exists Bobine
drop table if exists MatiereBobine
drop table if exists TechniqueBobine

CREATE TABLE Bobine
(
  IdBobine               int      NOT NULL IDENTITY(1,1),
  Reference              nvarchar(50) NOT NULL,
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
  Photo                  nvarchar(50),
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
  TypeCable             nvarchar(50) NOT NULL,
  DateCalcul            date     NOT NULL,
  CONSTRAINT PK_Calcul PRIMARY KEY (IdCalcul)
)
GO

CREATE TABLE MatiereBobine
(
  IdMatiere int      NOT NULL,
  Libelle   nvarchar(50) NOT NULL,
  CONSTRAINT PK_MatiereBobine PRIMARY KEY (IdMatiere)
)
GO

CREATE TABLE TechniqueBobine
(
  IdTechnique int      NOT NULL,
  Libelle     nvarchar(50) NOT NULL,
  CONSTRAINT PK_TechniqueBobine PRIMARY KEY (IdTechnique)
)
GO

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

INSERT INTO MatiereBobine (IdMatiere, Libelle)
values (1, 'bois')

INSERT INTO TechniqueBobine (IdTechnique, Libelle)
values (1, 'enroulement')

INSERT INTO Bobine (Reference, IdMatiere, IdTechnique, Poids, Stock, Prix, DiametreExterieur,
DiametreInterieur, DiametreTrouAxeCentral,LargeurInterieur, LargeurExterieur)
values ('BOB88', 1,1,20,44,99.44,2,1.5,1,14,16)