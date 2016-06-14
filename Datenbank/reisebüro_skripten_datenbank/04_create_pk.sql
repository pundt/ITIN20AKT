ALTER TABLE [user]
ADD
CONSTRAINT pk_user
PRIMARY KEY (id);

ALTER TABLE ma
ADD
CONSTRAINT pk_ma
PRIMARY KEY (id);

ALTER TABLE user_phone
ADD
CONSTRAINT pk_user_phone
PRIMARY KEY (id);

ALTER TABLE phone
ADD
CONSTRAINT pk_phone
PRIMARY KEY (id);

ALTER TABLE vw
ADD
CONSTRAINT pk_vw
PRIMARY KEY (id);

ALTER TABLE lvw
ADD
CONSTRAINT pk_lvw
PRIMARY KEY (id);

ALTER TABLE adresse
ADD
CONSTRAINT pk_adresse
PRIMARY KEY (id);

ALTER TABLE user_adresse
ADD
CONSTRAINT pk_user_adresse
PRIMARY KEY (id);

ALTER TABLE strasse
ADD
CONSTRAINT pk_strasse
PRIMARY KEY (id);

ALTER TABLE plz
ADD
CONSTRAINT pk_plz
PRIMARY KEY (id);

ALTER TABLE stadt
ADD
CONSTRAINT pk_stadt
PRIMARY KEY (id);

ALTER TABLE land
ADD
CONSTRAINT pk_land
PRIMARY KEY (id);

ALTER TABLE kunde
ADD
CONSTRAINT pk_kunde
PRIMARY KEY (id);

ALTER TABLE buchung
ADD
CONSTRAINT pk_buchung
PRIMARY KEY (id);

ALTER TABLE buchungsdetail
ADD
CONSTRAINT pk_buchungsdetail
PRIMARY KEY (id);

ALTER TABLE zahlungsmittel
ADD
CONSTRAINT pk_zahlungsmittel
PRIMARY KEY (id);

ALTER TABLE preiskategorie
ADD
CONSTRAINT pk_preiskategorie
PRIMARY KEY (id);

ALTER TABLE bewertung
ADD
CONSTRAINT pk_bewertung
PRIMARY KEY (id);

ALTER TABLE buchungsdetail_bewertung
ADD
CONSTRAINT pk_buchungsdetail_bewertung
PRIMARY KEY (id);

ALTER TABLE reise
ADD
CONSTRAINT pk_reise
PRIMARY KEY (id);

ALTER TABLE dauer
ADD
CONSTRAINT pk_dauer
PRIMARY KEY (id);

ALTER TABLE beginndatum
ADD
CONSTRAINT pk_beginndatum
PRIMARY KEY (id);

ALTER TABLE hotel
ADD
CONSTRAINT pk_hotel
PRIMARY KEY (id);

ALTER TABLE hotelkategorie
ADD
CONSTRAINT pk_hotelkategorie
PRIMARY KEY (id);

ALTER TABLE reise_hotel
ADD
CONSTRAINT pk_reise_hotel
PRIMARY KEY (id);

ALTER TABLE reiseart
ADD
CONSTRAINT pk_reiseart
PRIMARY KEY (id);

ALTER TABLE verpflegung
ADD
CONSTRAINT pk_verpflegung
PRIMARY KEY (id);

GO