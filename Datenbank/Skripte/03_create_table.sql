USE reisebuero;
GO

CREATE TABLE Benutzer (
	id INT IDENTITY NOT NULL,
	email NVARCHAR(50) NOT NULL,
	passwort VARBINARY(1000) NOT NULL,
	vorname NVARCHAR(50) NOT NULL,
	nachname NVARCHAR(50) NOT NULL,
	geschlecht BIT NOT NULL,
	adresse_id INT NOT NULL,
	telefon NVARCHAR(25) NOT NULL,
	titel NVARCHAR(25),
	geburtsdatum DATETIME NOT NULL,
	land_id INT NOT NULL,
	ist_Mitarbeiter BIT NOT NULL,
	erstelldatum DATETIME DEFAULT GETDATE()
);

CREATE TABLE Adresse (
	id INT IDENTITY NOT NULL,
	land_id INT NOT NULL,
	adresse NVARCHAR(255) NOT NULL,
	erstelldatum DATETIME DEFAULT GETDATE()
);

CREATE TABLE Buchung (
	reisedurchfuehrung_id INT NOT NULL,
	benutzer_id INT NOT NULL,
	passnummer NVARCHAR(25) NOT NULL,
	erstelldatum DATETIME DEFAULT GETDATE()
);

CREATE TABLE Buchung_Zahlung(
	reisedurchfuehrung_id INT NOT NULL,
	zahlung_id INT NOT NULL,
	erstelldatum DATETIME DEFAULT GETDATE()
);

CREATE TABLE BuchungStorniert(
	reisedurchfuehrung_id INT NOT NULL
);

CREATE TABLE Bewertung (
	id INT IDENTITY NOT NULL,
	wertung INT NOT NULL,
	reise_id INT NOT NULL,
	erstelldatum DATETIME DEFAULT GETDATE()
);

CREATE TABLE Reise (
	id INT IDENTITY NOT NULL,
	titel NVARCHAR(50) NOT NULL,
	beschreibung NVARCHAR(MAX) NOT NULL,
	unterkunft_id INT NOT NULL,
	preis_erwachsener DECIMAL(6,2) NOT NULL,
	preis_kind DECIMAL(6,2) NOT NULL,
	erstelldatum DATETIME DEFAULT GETDATE()
);

CREATE TABLE Verpflegung (
	id INT IDENTITY NOT NULL,
	bezeichnung NVARCHAR(50) NOT NULL,
	erstelldatum DATETIME DEFAULT GETDATE()
);

CREATE TABLE Unterkunft (
	id INT IDENTITY NOT NULL,
	bezeichnung NVARCHAR(50) NOT NULL,
	beschreibung NVARCHAR(MAX) NOT NULL,
	kategorie INT NOT NULL,
	verpflegung_id INT NOT NULL,
	erstelldatum DATETIME DEFAULT GETDATE()
);

CREATE TABLE Reisedatum (
	id INT IDENTITY NOT NULL,
	reise_id INT NOT NULL,
	startdatum DATETIME NOT NULL,
	enddatum DATETIME NOT NULL,
	anmeldefrist DATETIME NOT NULL,
	erstelldatum DATETIME DEFAULT GETDATE()
);

CREATE TABLE Reisedurchfuehrung(
	id INT IDENTITY NOT NULL,
	reisedatum_id INT NOT NULL,
	erstelldatum DATETIME DEFAULT GETDATE()
);

CREATE TABLE Land (
	id INT IDENTITY NOT NULL,
	bezeichnung NVARCHAR(50),
	erstelldatum DATETIME DEFAULT GETDATE()
);

CREATE TABLE Bild (
	id INT IDENTITY NOT NULL,
	bilddaten VARBINARY(MAX) NOT NULL,
	erstelldatum DATETIME DEFAULT GETDATE()
);

CREATE TABLE Unterkunft_Bild (
	id INT IDENTITY NOT NULL,
	bild_id INT NOT NULL,
	unterkunft_id INT NOT NULL,
	erstelldatum DATETIME DEFAULT GETDATE()
);

CREATE TABLE Reise_Bild(
	id INT IDENTITY NOT NULL,
	bild_id INT NOT NULL,
	reise_id INT NOT NULL,
	erstelldatum DATETIME DEFAULT GETDATE()
);

CREATE TABLE Zahlungsart(
	id INT IDENTITY NOT NULL,
	bezeichnung NVARCHAR(50) NOT NULL,
	erstelldatum DATETIME DEFAULT GETDATE()
);

CREATE TABLE Zahlung(
	id INT IDENTITY NOT NULL,
	vorname NVARCHAR(50) NOT NULL,
	nachname NVARCHAR(50) NOT NULL,
	nummer NVARCHAR(50) NOT NULL,
	zahlungsart_id INT NOT NULL,
	erstelldatum DATETIME DEFAULT GETDATE()
);

GO