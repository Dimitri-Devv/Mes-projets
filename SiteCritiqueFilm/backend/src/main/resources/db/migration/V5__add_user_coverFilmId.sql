ALTER TABLE users ADD COLUMN cover_film_id BIGINT NULL;
ALTER TABLE users
    ADD CONSTRAINT fk_user_cover_film
        FOREIGN KEY (cover_film_id) REFERENCES films(id)
            ON DELETE SET NULL;