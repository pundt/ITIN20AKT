USE reisebuero;
GO

ALTER TABLE Adresse
ADD
CONSTRAINT PK_Adresse
PRIMARY KEY (id);
GO

ALTER TABLE Land
ADD
CONSTRAINT PK_Land
PRIMARY KEY (id);
GO

ALTER TABLE ReiseBild
ADD
CONSTRAINT PK_ReiseBild
PRIMARY KEY (id);
GO

ALTER TABLE UnterkunftBild
ADD
CONSTRAINT PK_UnterkunftBild
PRIMARY KEY(id);
GO

ALTER TABLE Reisedatum
ADD
CONSTRAINT PK_Reisedatum
PRIMARY KEY(id);
GO

ALTER TABLE Buchung
ADD
CONSTRAINT PK_Buchung
PRIMARY KEY (reisedurchfuehrung_id);
GO

ALTER TABLE BuchungStorniert
ADD
CONSTRAINT PK_BuchungStorniert
PRIMARY KEY (reisedurchführung_id);
GO

ALTER TABLE Reisedurchfuehrung
ADD
CONSTRAINT PK_Reisedurchfuehrung
PRIMARY KEY (id);
GO

ALTER TABLE Reisedatum
ADD
CONSTRAINT PK_Reisedatum
PRIMARY KEY (id);
GO

ALTER TABLE Reise
ADD
CONSTRAINT PK_Reise
PRIMARY KEY (id);
GO

ALTER TABLE Bewertung
ADD
CONSTRAINT PK_Bewertung
PRIMARY KEY (id);
GO


ALTER TABLE Unterkunft
ADD
CONSTRAINT PK_Unterkunft
PRIMARY KEY(id);
GO


ALTER TABLE Verpflegung
ADD
CONSTRAINT PK_Verpflegung
PRIMARY KEY(id);
GO

ALTER TABLE Zahlung
ADD
CONSTRAINT PK_Zahlung
PRIMARY KEY(id);
GO

ALTER TABLE Zahlungsart
ADD
CONSTRAINT PK_Zahlungsart
PRIMARY KEY(id);
GO