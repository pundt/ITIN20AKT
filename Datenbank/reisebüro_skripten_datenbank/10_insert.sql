USE reisebuero;
GO

INSERT INTO Land(bezeichnung) VALUES('Österreich');
INSERT INTO Land(bezeichnung) VALUES('Deutschland');
INSERT INTO Land(bezeichnung) VALUES('Italien');
INSERT INTO Land(bezeichnung) VALUES('Schweiz');
INSERT INTO Land(bezeichnung) VALUES('Frankreich');

INSERT INTO Ort(bezeichnung) VALUES('Wien');
INSERT INTO Ort(bezeichnung) VALUES('Hamburg');
INSERT INTO Ort(bezeichnung) VALUES('Rom');
INSERT INTO Ort(bezeichnung) VALUES('Zürich');
INSERT INTO Ort(bezeichnung) VALUES('Paris');

INSERT INTO Adresse(land_id, ort_id, plz, strasse, nummer)
VALUES(1, 1, 1010, 'Stephansplatz', '1');
INSERT INTO Adresse(land_id, ort_id, plz, strasse, nummer)
VALUES(2, 2, 22222, 'Riemerstraße', '2/2');
INSERT INTO Adresse(land_id, ort_id, plz, strasse, nummer)
VALUES(3, 3, 31113, 'Via Accia', '33');
INSERT INTO Adresse(land_id, ort_id, plz, strasse, nummer)
VALUES(4, 4, 40215, 'Bankenplatz', '4/Top 1');
INSERT INTO Adresse(land_id, ort_id, plz, strasse, nummer)
VALUES(5, 5, 51243, 'Plaza de Chateau', '5');

INSERT INTO Benutzer(email, passwort, vorname, nachname, geschlecht, adresse_id, telefon)
VALUES('muster@itfox.at', CONVERT(VARBINARY(1000), '123user!'), 'Max', 'Muster', 0, 1, 0043676123456);
INSERT INTO Benutzer(email, passwort, vorname, nachname, geschlecht, adresse_id, telefon)
VALUES('marco@itfox.at', CONVERT(VARBINARY(1000), '123user!'), 'Marco', 'Wurz', 0, 2, 0049743212121);
INSERT INTO Benutzer(email, passwort, vorname, nachname, geschlecht, adresse_id, telefon)
VALUES('claudia@itfox.at', CONVERT(VARBINARY(1000), '123user!'), 'Claudia', 'Stiegl', 1, 3, 003256124565);
INSERT INTO Benutzer(email, passwort, vorname, nachname, geschlecht, adresse_id, telefon)
VALUES('daniel@itfox.at', CONVERT(VARBINARY(1000), '123user!'), 'Daniel', 'Zalli', 0, 4, 00396123456);
INSERT INTO Benutzer(email, passwort, vorname, nachname, geschlecht, adresse_id, telefon)
VALUES('stefan@itfox.at', CONVERT(VARBINARY(1000), '123user!'), 'Stefan', 'Groig', 0, 5, 0055236458);

INSERT INTO Kunde(benutzer_id, geburtsdatum, titel, land_id)
VALUES(1, '1990/1/1', 'Mag.', 1);
INSERT INTO Kunde(benutzer_id, geburtsdatum, titel, land_id)
VALUES(2, '1989/3/8', '', 1);
INSERT INTO Kunde(benutzer_id, geburtsdatum, titel, land_id)
VALUES(4, '1991/12/12', 'Dr.', 3);

INSERT INTO Mitarbeiter(benutzer_id, svnr) VALUES(3, 1234);
INSERT INTO Mitarbeiter(benutzer_id, svnr) VALUES(5, 1470);

INSERT INTO Verpflegung(bezeichnung) VALUES('Ohne Verpflegung');
INSERT INTO Verpflegung(bezeichnung) VALUES('Frühstück');
INSERT INTO Verpflegung(bezeichnung) VALUES('Halbpension');
INSERT INTO Verpflegung(bezeichnung) VALUES('Vollpension');
INSERT INTO Verpflegung(bezeichnung) VALUES('All Inclusive');

INSERT INTO Unterkunft(bezeichnung, beschreibung, kategorie, verpflegung_id)
VALUES('Hotel Arosa', 'Dieses Hotel bietet außergewöhnlichen Komfort', 4, 4);
INSERT INTO Unterkunft(bezeichnung, beschreibung, kategorie, verpflegung_id)
VALUES('Hotel de Croissant', 'Dieses Hotel schmeckt nach Croissants', 3, 2);
INSERT INTO Unterkunft(bezeichnung, beschreibung, kategorie, verpflegung_id)
VALUES('Pension Pomp', 'Luxus in seiner schönsten Form', 5, 5);
INSERT INTO Unterkunft(bezeichnung, beschreibung, kategorie, verpflegung_id)
VALUES('Almhütte zum Sepp', 'Gute urige Küche und Jausen', 2, 1);
INSERT INTO Unterkunft(bezeichnung, beschreibung, kategorie, verpflegung_id)
VALUES('Hotel Allin', 'Dieses Hotel bietet alles außer Vollpension', 4, 3);

INSERT INTO Reise(titel, beschreibung, unterkunft_id, preis_erwachsener, preis_kind)
VALUES('Wandern in den Bergen', 'Erleben Sie die schöne Bergwelt in Österreich', 4, 65.99, 32.99);
INSERT INTO Reise(titel, beschreibung, unterkunft_id, preis_erwachsener, preis_kind)
VALUES('Wellnes pur', 'Genießen Sie italienische Luft und Wellnes vom Feinsten', 1, 150.00, 75.00);
INSERT INTO Reise(titel, beschreibung, unterkunft_id, preis_erwachsener, preis_kind)
VALUES('Fußball pur', 'Fahren Sie zum EM-Finale nach Paris!', 2, 199.99, 99.99);
INSERT INTO Reise(titel, beschreibung, unterkunft_id, preis_erwachsener, preis_kind)
VALUES('Urlaub der Creme de la Creme', 'Verbleiben Sie in einem unserer 4000 Zimmer', 3, 259.90, 129.90);
INSERT INTO Reise(titel, beschreibung, unterkunft_id, preis_erwachsener, preis_kind)
VALUES('Pokern wie die Pros', 'Fahren Sie zur World Pokers Tour nach Las Vegas!', 5, 1200.00, 600.00);

INSERT INTO Reisedetail(reise_id, startdatum, enddatum, anmeldefrist)
VALUES(1, '2016-31-07', '2016-06-08', '2016-30-06');
INSERT INTO Reisedetail(reise_id, startdatum, enddatum, anmeldefrist)
VALUES(2, '2016-01-12', '2016-06-12', '2016-10-11');
INSERT INTO Reisedetail(reise_id, startdatum, enddatum, anmeldefrist)
VALUES(3, '2016-25-07', '2016-30-07', '2016-15-06');
INSERT INTO Reisedetail(reise_id, startdatum, enddatum, anmeldefrist)
VALUES(4, '2017-07-01', '2017-21-01', '2016-23-12');
INSERT INTO Reisedetail(reise_id, startdatum, enddatum, anmeldefrist)
VALUES(5, '2017-01-03', '2017-15-03', '2017-02-02');

INSERT INTO Bild(bild) VALUES(0x0);
INSERT INTO Bild(bild) VALUES(0x0);
INSERT INTO Bild(bild) VALUES(0x0);
INSERT INTO Bild(bild) VALUES(0x0);
INSERT INTO Bild(bild) VALUES(0x0);
INSERT INTO Bild(bild) VALUES(0x0);
INSERT INTO Bild(bild) VALUES(0x0);
INSERT INTO Bild(bild) VALUES(0x0);
INSERT INTO Bild(bild) VALUES(0x0);
INSERT INTO Bild(bild) VALUES(0x0);
GO

UPDATE Bild SET bild =
(SELECT *
FROM Openrowset( 
Bulk 'C:\Users\aktuser\Documents\GitHub\ITIN20AKT\Datenbank\reisebüro_skripten_datenbank\testimages\1_hotel_test.jpg', Single_Blob) 
AS import) WHERE id = 11;
GO
INSERT INTO Bild (bild) 
SELECT * 
FROM Openrowset( 
Bulk 'C:\Users\aktuser\Documents\GitHub\ITIN20AKT\Datenbank\reisebüro_skripten_datenbank\testimages\2_hotel_test.jpg', Single_Blob) 
AS import;
GO
INSERT INTO Bild (bild) 
SELECT * 
FROM Openrowset( 
Bulk 'C:\Users\aktuser\Documents\GitHub\ITIN20AKT\Datenbank\reisebüro_skripten_datenbank\testimages\3_hotel_test.jpg', Single_Blob) 
AS import;
GO
INSERT INTO Bild (bild) 
SELECT * 
FROM Openrowset( 
Bulk 'C:\Users\aktuser\Documents\GitHub\ITIN20AKT\Datenbank\reisebüro_skripten_datenbank\testimages\4_hotel_test.jpg', Single_Blob) 
AS import;
GO
INSERT INTO Bild (bild) 
SELECT * 
FROM Openrowset( 
Bulk 'C:\Users\aktuser\Documents\GitHub\ITIN20AKT\Datenbank\reisebüro_skripten_datenbank\testimages\5_hotel_test.jpg', Single_Blob) 
AS import;
GO
INSERT INTO Bild (bild) 
SELECT * 
FROM Openrowset( 
Bulk 'C:\Users\aktuser\Documents\GitHub\ITIN20AKT\Datenbank\reisebüro_skripten_datenbank\testimages\1_reise_test.jpg', Single_Blob) 
AS import;
GO
INSERT INTO Bild (bild) 
SELECT * 
FROM Openrowset( 
Bulk 'C:\Users\aktuser\Documents\GitHub\ITIN20AKT\Datenbank\reisebüro_skripten_datenbank\testimages\2_reise_test.jpg', Single_Blob) 
AS import;
GO
INSERT INTO Bild (bild) 
SELECT * 
FROM Openrowset( 
Bulk 'C:\Users\aktuser\Documents\GitHub\ITIN20AKT\Datenbank\reisebüro_skripten_datenbank\testimages\3_reise_test.jpg', Single_Blob) 
AS import;
GO
INSERT INTO Bild (bild) 
SELECT * 
FROM Openrowset( 
Bulk 'C:\Users\aktuser\Documents\GitHub\ITIN20AKT\Datenbank\reisebüro_skripten_datenbank\testimages\4_reise_test.jpg', Single_Blob) 
AS import;
GO
INSERT INTO Bild (bild) 
SELECT * 
FROM Openrowset( 
Bulk 'C:\Users\aktuser\Documents\GitHub\ITIN20AKT\Datenbank\reisebüro_skripten_datenbank\testimages\5_reise_test.jpg', Single_Blob) 
AS import;
GO

INSERT INTO Buchung(kunde_id, reisedetail_id) VALUES(1, 5);
INSERT INTO Buchung(kunde_id, reisedetail_id) VALUES(2, 4);
INSERT INTO Buchung(kunde_id, reisedetail_id) VALUES(3, 3);
GO

INSERT INTO Bewertung(buchung_id, bewertung) VALUES(3, 5);
INSERT INTO Bewertung(buchung_id, bewertung) VALUES(4, 4);
INSERT INTO Bewertung(buchung_id, bewertung) VALUES(5, 3);
GO

INSERT INTO Bild_Reise(bild_id, reise_id) VALUES(1,1);
INSERT INTO Bild_Reise(bild_id, reise_id) VALUES(2,2);
INSERT INTO Bild_Reise(bild_id, reise_id) VALUES(3,3);
INSERT INTO Bild_Reise(bild_id, reise_id) VALUES(4,4);
INSERT INTO Bild_Reise(bild_id, reise_id) VALUES(5,5);
GO

INSERT INTO Bild_Unterkunft(bild_id, unterkunft_id) VALUES(6, 1);
INSERT INTO Bild_Unterkunft(bild_id, unterkunft_id) VALUES(7, 2);
INSERT INTO Bild_Unterkunft(bild_id, unterkunft_id) VALUES(8, 3);
INSERT INTO Bild_Unterkunft(bild_id, unterkunft_id) VALUES(9, 4);
INSERT INTO Bild_Unterkunft(bild_id, unterkunft_id) VALUES(10, 5);
GO