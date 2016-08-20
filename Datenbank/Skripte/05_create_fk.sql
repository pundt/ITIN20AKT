USE reisebuero;
GO

ALTER TABLE Reisedetail
ADD
CONSTRAINT FK_Reisedetail_Reise
FOREIGN KEY (reise_id)
REFERENCES Reise(id);
GO

ALTER TABLE Buchung
ADD
CONSTRAINT FK_Buchung_Benutzer
FOREIGN KEY (benutzer_id)
REFERENCES Benutzer(id);
GO

ALTER TABLE Buchung
ADD
CONSTRAINT FK_Buchung_Benutzer
FOREIGN KEY (benutzer_id)
REFERENCES Benutzer(id);
GO

ALTER TABLE Buchung
ADD
CONSTRAINT FK_Buchung_Benutzer
FOREIGN KEY (benutzer_id)
REFERENCES Benutzer(id);
GO

ALTER TABLE Buchung
ADD
CONSTRAINT FK_Buchung_Zahlung
FOREIGN KEY (zahlung_id)
REFERENCES Zahlung(id);
GO

ALTER TABLE Buchung
ADD
CONSTRAINT FK_Buchung_Reisedurchführung
FOREIGN KEY (reisedurchführung_id)
REFERENCES Reisedurchführung(id);
GO

ALTER TABLE Buchung
ADD
CONSTRAINT FK_Buchung_BuchungStorniert
FOREIGN KEY (buchungStorniert_id)
REFERENCES BuchungStorniert(id);
GO

ALTER TABLE Adresse
ADD
CONSTRAINT FK_Adresse_Land
FOREIGN KEY (land_id)
REFERENCES Land(id);
GO

ALTER TABLE Benutzer
ADD
CONSTRAINT FK_Benutzer_Land
FOREIGN KEY (land_id)
REFERENCES Land(id);
GO

ALTER TABLE Reise
ADD 
CONSTRAINT FK_Reise_Unterkunft
FOREIGN KEY (unterkunft_id)
REFERENCES Unterkunft(id);
GO

ALTER TABLE Reisedatum
ADD
CONSTRAINT FK_Reisedatum_Reise
FOREIGN KEY (reise_id)
REFERENCES Reise(id)
GO

ALTER TABLE Reisedurchfuehrung
ADD
CONSTRAINT FK_Reisedurchfuehrung_Reisedatum
FOREIGN KEY (reisedatum_id)
REFERENCES Reisedatum(id)
GO

ALTER TABLE Benutzer
ADD
CONSTRAINT FK_Benutzer_Adresse
FOREIGN KEY (adresse_id)
REFERENCES Adresse(id);
GO

ALTER TABLE ReiseBild
ADD
CONSTRAINT FK_ReiseBild_Reise
FOREIGN KEY (reise_id)
REFERENCES Reise(id);
GO

ALTER TABLE Bewertung
ADD
CONSTRAINT FK_Bewertung_Reise
FOREIGN KEY (reise_id)
REFERENCES Reise(id);
GO

ALTER TABLE Bild_Reise
ADD
CONSTRAINT FK_Bild_Reise_Bild
FOREIGN KEY (bild_id)
REFERENCES Bild(id);
GO

ALTER TABLE UnterkunftBild
ADD
CONSTRAINT FK_UnterkunftBild_Unterkunft
FOREIGN KEY (unterkunft_id)
REFERENCES Unterkunft(id);
GO

ALTER TABLE Unterkunft
ADD
CONSTRAINT FK_Unterkunft_Verpflegung
FOREIGN KEY (verpflegung_id)
REFERENCES Verpflegung(id);
GO

ALTER TABLE Zahlung
ADD
CONSTRAINT FK_Zahlung_Zahlungsart
FOREIGN KEY (zahlungsart_id)
REFERENCES Zahlungsart(id);
GO

ALTER TABLE BuchungStorniert
ADD
CONSTRAINT FK_BuchungStorniert_Buchung
FOREIGN KEY (reisedurchfuehrung_id)
REFERENCES Buchung(Reisedurchfuehrung_id)
GO
