USE reisebuero;
GO

CREATE TABLE Benutzer (
	id INT IDENTITY NOT NULL,
	email NVARCHAR(50) NOT NULL,
	passwort VARBINARY NOT NULL,
	vorname NVARCHAR(50) NOT NULL,
	nachname NVARCHAR(50) NOT NULL,
	geschlecht BIT NOT NULL
);

CREATE TABLE ma (
	id INT IDENTITY NOT NULL,
	[user_id] INT NOT NULL,
	svnr INT NOT NULL
);

CREATE TABLE phone (
	id INT IDENTITY NOT NULL,
	vw_id INT NOT NULL,
	lvw_id INT NOT NULL,
	nummer NVARCHAR(20) NOT NULL
);

CREATE TABLE user_phone (
	id INT IDENTITY NOT NULL,
	phone_id INT NOT NULL,
	[user_id] INT NOT NULL
);

CREATE TABLE vw (
	id INT IDENTITY NOT NULL,
	nummer NVARCHAR(10)
);

CREATE TABLE lvw (
	id INT IDENTITY NOT NULL,
	nummer NVARCHAR(10)
);

CREATE TABLE adresse (
	id INT IDENTITY NOT NULL,
	nr NVARCHAR(10) NOT NULL,
	str_id INT NOT NULL
);

CREATE TABLE strasse (
	id INT IDENTITY NOT NULL,
	bez NVARCHAR(50),
	plz_id INT NOT NULL
);

CREATE TABLE plz (
	id INT IDENTITY NOT NULL,
	nr NVARCHAR(10),
	stadt_id INT NOT NULL
);

CREATE TABLE stadt (
	id INT IDENTITY NOT NULL,
	bez NVARCHAR(50),
	land_id INT NOT NULL
);

CREATE TABLE land (
	id INT IDENTITY NOT NULL,
	bez NVARCHAR(50)
);

CREATE TABLE user_adresse (
	id INT IDENTITY NOT NULL,
	adresse_id INT NOT NULL,
	[user_id] INT NOT NULL
);

CREATE TABLE kunde (
	id INT IDENTITY NOT NULL,
	[user_id] INT NOT NULL,
	gebDatum DATETIME NOT NULL,
	titel NVARCHAR(50),
	staatsbuergerschaft NVARCHAR(50)
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