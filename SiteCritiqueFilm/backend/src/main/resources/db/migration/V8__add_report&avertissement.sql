
CREATE TABLE reports (
                         id SERIAL PRIMARY KEY,

                         reported_user_id INTEGER NOT NULL,   -- celui qui a été signalé
                         reporter_id INTEGER NOT NULL,        -- celui qui a fait le signalement

                         message TEXT NOT NULL,               -- raison du signalement
                         status VARCHAR(20) DEFAULT 'PENDING',-- statut (PENDING / REVIEWED / CLOSED)

                         created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

                         CONSTRAINT fk_reported_user
                             FOREIGN KEY (reported_user_id) REFERENCES users(id) ON DELETE CASCADE,

                         CONSTRAINT fk_reporter_user
                             FOREIGN KEY (reporter_id) REFERENCES users(id) ON DELETE CASCADE
);

-- =========================================
-- Fonction du trigger
-- =========================================
CREATE OR REPLACE FUNCTION block_user_on_third_warning()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW.avertissement >= 3 AND OLD.avertissement < 3 THEN
        NEW.blocked = TRUE;
END IF;

RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- =========================================
-- Trigger
-- =========================================
DROP TRIGGER IF EXISTS trg_block_user_on_third_warning ON users;

CREATE TRIGGER trg_block_user_on_third_warning
    BEFORE UPDATE OF avertissement ON users
FOR EACH ROW
EXECUTE FUNCTION block_user_on_third_warning();