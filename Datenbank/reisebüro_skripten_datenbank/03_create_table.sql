USE reisebuero;
GO

CREATE TABLE Benutzer (
	id INT IDENTITY NOT NULL,
	email NVARCHAR(50) NOT NULL,
	passwort VARBINARY NOT NULL,
	vorname NVARCHAR(50) NOT NULL,
	nachname NVARCHAR(50) NOT NULL,
	geschlecht BIT NOT NULL,
	adresse_id INT NOT NULL,
	telefon INT NOT NULL
);

CREATE TABLE Mitarbeiter (
	id INT IDENTITY NOT NULL,
	benutzer_id INT NOT NULL,
	svnr INT NOT NULL
);

CREATE TABLE Adresse (
	id INT IDENTITY NOT NULL,
	land_id INT NOT NULL,
	ort_id INT NOT NULL,
	plz INT NOT NULL,
	strasse NVARCHAR(50) NOT NULL,
	nummer NVARCHAR(25) NOT NULL
);

CREATE TABLE Kunde (
	id INT IDENTITY NOT NULL,
	benutzer_id INT NOT NULL,
	geburtsdatum DATETIME NOT NULL,
	titel NVARCHAR(25),
	land_id INT NOT NULL
);

CREATE TABLE buchung (
	id INT IDENTITY NOT NULL,
	kunde_id INT NOT NULL,
	createdate DATETIME DEFAULT GETDATE()
);

CREATE TABLE buchungsdetail (
	id INT IDENTITY NOT NULL,
	buchung_id INT NOT NULL,
	reise_id INT NOT NULL,
	zahlung_id INT NOT NULL,
	bewertung_id INT NOT NULL,
	preiskategorie_id INT NOT NULL
);

CREATE TABLE zahlungsmittel (
	id INT IDENTITY NOT NULL,
	bez NVARCHAR(50) NOT NULL
);

CREATE TABLE preiskategorie (
	id INT IDENTITY NOT NULL,
	bez NVARCHAR(50) NOT NULL,
	von NVARCHAR(30) NOT NULL,
	bis NVARCHAR(30) NOT NULL,
	val NVARCHAR(50) NOT NULL
);

CREATE TABLE bewertung (
	id INT IDENTITY NOT NULL,
	sterne INT NOT NULL
);

CREATE TABLE buchungsdetail_bewertung (
	id INT IDENTITY NOT NULL,
	buchungsdetail_id INT NOT NULL,
	bewertung_id INT NOT NULL
);

CREATE TABLE reise (
	id INT IDENTITY NOT NULL,
	titel NVARCHAR(50) NOT NULL,
	beschreibung NVARCHAR(MAX) NOT NULL,
	dauer_id INT NOT NULL,
	beginndatum_id INT NOT NULL,
	verpflegung_id INT NOT NULL,
	bez NVARCHAR(50) NOT NULL
);

CREATE TABLE dauer (
	id INT IDENTITY NOT NULL,
	tage INT NOT NULL
);

CREATE TABLE beginndatum (
	id INT IDENTITY NOT NULL,
	datum DATETIME NOT NULL
);

CREATE TABLE verpflegung (
	id INT IDENTITY NOT NULL,
	bez NVARCHAR(50) NOT NULL
);

CREATE TABLE reiseart (
	id INT IDENTITY NOT NULL,
	bez NVARCHAR(50) NOT NULL
);

CREATE TABLE reise_reiseart (
	id INT IDENTITY NOT NULL,
	reise_id INT NOT NULL,
	reiseart_id INT NOT NULL
);

CREATE TABLE hotel (
	id INT IDENTITY NOT NULL,
	hotelkat_id INT NOT NULL,
	bez NVARCHAR(50) NOT NULL,
	beschr NVARCHAR(MAX) NOT NULL
);

CREATE TABLE hotelkategorie (
	id INT IDENTITY NOT NULL,
	sterne INT NOT NULL
);

CREATE TABLE reise_hotel (
	id INT IDENTITY NOT NULL,
	reise_id INT NOT NULL,
	hotel_id INT NOT NULL
);

GO