-- V10__update_reports_table.sql

DO $$
BEGIN
    IF EXISTS (
        SELECT 1 FROM information_schema.columns
        WHERE table_name='reports' AND column_name='status'
    ) THEN
ALTER TABLE reports
    RENAME COLUMN status TO processed;
END IF;
END $$;


ALTER TABLE reports
    ALTER COLUMN processed DROP DEFAULT;


UPDATE reports SET processed = FALSE WHERE processed IS NULL;

ALTER TABLE reports
    ALTER COLUMN processed TYPE BOOLEAN
    USING (processed::BOOLEAN);

ALTER TABLE reports
    ALTER COLUMN processed SET DEFAULT FALSE;

ALTER TABLE reports
    ADD COLUMN IF NOT EXISTS admin_id INTEGER REFERENCES users(id);


ALTER TABLE reports
    ADD COLUMN IF NOT EXISTS created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP;