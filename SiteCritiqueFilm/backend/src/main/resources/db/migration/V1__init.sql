-- === SCHEMA INIT (PostgreSQL) ===

CREATE TABLE users (
                       id SERIAL PRIMARY KEY,
                       avertissement INTEGER DEFAULT 0,
                       blocked BOOLEAN DEFAULT FALSE,
                       is_email_verified BOOLEAN DEFAULT FALSE,
                       created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                       username VARCHAR(100) NOT NULL,
                       email VARCHAR(150) NOT NULL,
                       avatar_url TEXT,
                       password_hash TEXT NOT NULL,
                       role VARCHAR(50) NOT NULL
);

CREATE TABLE genres (
                        id SERIAL PRIMARY KEY,
                        name VARCHAR(100) NOT NULL
);

CREATE TABLE films (
                       id SERIAL PRIMARY KEY,
                       year SMALLINT,
                       created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                       genre_id INTEGER REFERENCES genres(id),
                       director VARCHAR(150),
                       affiche_url TEXT,
                       poster_url TEXT,
                       synopsis TEXT,
                       title VARCHAR(255) NOT NULL,
                       trailer_url TEXT,
                       is_tendance BOOLEAN DEFAULT FALSE,
                       rating_average DOUBLE PRECISION DEFAULT 0,
                       date_sortie DATE
);

CREATE TABLE actors (
                        id SERIAL PRIMARY KEY,
                        avatar_url TEXT,
                        bio TEXT,
                        name VARCHAR(150) NOT NULL
);

CREATE TABLE film_actor (
                            id SERIAL PRIMARY KEY,
                            film_id INTEGER REFERENCES films(id),
                            actor_id INTEGER REFERENCES actors(id)
);

CREATE TABLE reviews (
                         id SERIAL PRIMARY KEY,
                         dislikes_count INTEGER DEFAULT 0,
                         likes_count INTEGER DEFAULT 0,
                         rating INTEGER NOT NULL,
                         created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                         film_id INTEGER REFERENCES films(id),
                         user_id INTEGER REFERENCES users(id),
                         content TEXT NOT NULL,
                         image_url TEXT,
                         title VARCHAR(255) NOT NULL
);

CREATE TABLE comments (
                          id SERIAL PRIMARY KEY,
                          created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                          review_id INTEGER REFERENCES reviews(id),
                          user_id INTEGER REFERENCES users(id),
                          content TEXT NOT NULL
);

CREATE TABLE review_likes (
                              id SERIAL PRIMARY KEY,
                              liked BOOLEAN NOT NULL,
                              review_id INTEGER REFERENCES reviews(id),
                              user_id INTEGER REFERENCES users(id)
);