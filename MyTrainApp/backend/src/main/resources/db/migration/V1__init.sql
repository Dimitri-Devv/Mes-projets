create table users (
                       id bigserial primary key,
                       email varchar(255) not null unique,
                       password_hash varchar(255) not null,
                       created_at timestamptz not null default now()
);

create index idx_users_email on users(email);