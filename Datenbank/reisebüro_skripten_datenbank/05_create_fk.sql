ALTER TABLE ma
ADD
CONSTRAINT fk_ma_user
FOREIGN KEY( [user_id] )
REFERENCES [user](id);

ALTER TABLE phone
ADD
CONSTRAINT fk_phone_vw
FOREIGN KEY( vw_id )
REFERENCES vw(id);

ALTER TABLE phone
ADD
CONSTRAINT fk_phone_lvw
FOREIGN KEY( lvw_id )
REFERENCES lvw(id);

ALTER TABLE user_phone
ADD
CONSTRAINT fk_user_phone_user
FOREIGN KEY( [user_id] )
REFERENCES [user](id);

ALTER TABLE user_phone
ADD
CONSTRAINT fk_user_phone_phone
FOREIGN KEY( phone_id )
REFERENCES phone(id);

ALTER TABLE adresse
ADD
CONSTRAINT fk_adresse_strasse
FOREIGN KEY( str_id )
REFERENCES strasse(id);

ALTER TABLE strasse
ADD
CONSTRAINT fk_strasse_plz
FOREIGN KEY( plz_id )
REFERENCES plz(id);

ALTER TABLE plz
ADD
CONSTRAINT fk_plz_stadt
FOREIGN KEY( stadt_id )
REFERENCES stadt(id);

ALTER TABLE stadt
ADD
CONSTRAINT fk_stadt_land
FOREIGN KEY( land_id )
REFERENCES land(id);

ALTER TABLE user_adresse
ADD
CONSTRAINT fk_user_adresse_user
FOREIGN KEY( [user_id] )
REFERENCES [user](id);

ALTER TABLE user_adresse
ADD
CONSTRAINT fk_user_adresse_adresse
FOREIGN KEY( adresse_id )
REFERENCES adresse(id);

ALTER TABLE kunde
ADD
CONSTRAINT fk_kunde_user
FOREIGN KEY( [user_id] )
REFERENCES [user](id);

ALTER TABLE buchung
ADD
CONSTRAINT fk_buchung_kunde
FOREIGN KEY( kunde_id )
REFERENCES kunde(id);

ALTER TABLE buchungsdetail
ADD
CONSTRAINT fk_buchungsdetail_buchung
FOREIGN KEY( buchung_id )
REFERENCES buchung(id);

ALTER TABLE buchungsdetail
ADD
CONSTRAINT fk_buchungsdetail_reise
FOREIGN KEY( reise_id )
REFERENCES reise(id);

ALTER TABLE buchungsdetail
ADD
CONSTRAINT fk_buchungsdetail_zahlung
FOREIGN KEY( zahlung_id )
REFERENCES zahlungsmittel(id);

ALTER TABLE buchungsdetail
ADD
CONSTRAINT fk_buchungsdetail_preiskategorie
FOREIGN KEY( preiskategorie_id )
REFERENCES preiskategorie(id);

ALTER TABLE buchungsdetail_bewertung
ADD
CONSTRAINT fk_buchungsdetail_bewertung_buchungsdetail
FOREIGN KEY( buchungsdetail_id )
REFERENCES buchungsdetail(id);

ALTER TABLE buchungsdetail_bewertung
ADD
CONSTRAINT fk_buchungsdetail_bewertung_bewertung
FOREIGN KEY( bewertung_id )
REFERENCES bewertung(id);

ALTER TABLE reise
ADD
CONSTRAINT fk_reise_dauer
FOREIGN KEY( dauer_id )
REFERENCES dauer(id);

ALTER TABLE reise
ADD
CONSTRAINT fk_reise_beginndatum
FOREIGN KEY( beginndatum_id )
REFERENCES beginndatum(id);

ALTER TABLE reise
ADD
CONSTRAINT fk_reise_verpflegung
FOREIGN KEY( verpflegung_id )
REFERENCES verpflegung(id)

ALTER TABLE reise_reiseart
ADD
CONSTRAINT fk_reise_reiseart_reise
FOREIGN KEY( reise_id )
REFERENCES reise(id);

ALTER TABLE reise_reiseart
ADD
CONSTRAINT fk_reise_reiseart_reiseart
FOREIGN KEY( reiseart_id )
REFERENCES reiseart(id);

ALTER TABLE hotel
ADD
CONSTRAINT fk_hotel_hotelkat
FOREIGN KEY( hotelkat_id )
REFERENCES hotelkategorie(id);

ALTER TABLE reise_hotel
ADD
CONSTRAINT fk_reise_hotel_reise
FOREIGN KEY( reise_id )
REFERENCES reise(id);

ALTER TABLE reise_hotel
ADD
CONSTRAINT fk_reise_hotel_hotel
FOREIGN KEY( hotel_id )
REFERENCES hotel(id);

GO