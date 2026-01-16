CREATE TABLE verification_code (
    id SERIAL PRIMARY KEY,
    email VARCHAR(255) NOT NULL,
    code VARCHAR(10) NOT NULL,
    expiration TIMESTAMP NOT NULL
);
