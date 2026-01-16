SET default_storage_engine = InnoDb;

drop database if exists breeder;
create database breeder CHARACTER SET utf8
    COLLATE utf8_general_ci;
use breeder;

create table utilisateur
(
    id     int auto_increment primary key,
    pseudo varchar(30),
    mdp    varchar(64),
    mail   varchar(100)
);
create table role
(
    id          int auto_increment primary key,
    libelleRole varchar(30) not null
);

create table typeAnimal
(
    id          int auto_increment primary key,
    libelleType varchar(30) not null
);

create table race
(
    id          int auto_increment primary key,
    libelleRace varchar(30) not null
);

create table statut
(
    id            int auto_increment primary key,
    libelleStatut varchar(30) not null
);

create table animal
(
    id            int auto_increment primary key,
    nom           varchar(30)   not null            DEFAULT 'Inconnu',
    prenom        varchar(30)   not null            DEFAULT 'Inconnu',
    dateNaissance date          not null            default (curdate()),
    sexe          enum ('Mâle','Femelle','Inconnu') default 'Inconnu',
    poidsActuel   decimal(7, 3) not null            DEFAULT 0,
    idStatut      int           not null            default 1,
    idPere        int           not null            DEFAULT 1,
    idMere        int           not null            DEFAULT 1,
    idRace        int           not null            DEFAULT 1,
    idType        int           not null            DEFAULT 1,
    foreign key (idStatut) references statut (id),
    foreign key (idRace) references race (id),
    foreign key (idPere) references animal (id),
    foreign key (idMere) references animal (id),
    foreign key (idType) references typeAnimal (id)
);


create table portee
(
    id            int auto_increment,
    idAnimal      int,
    datePortee    date        not null,
    libellePortee varchar(30) not null,
    primary key (id),
    foreign key (idAnimal) references animal (id)
);

select *
from portee;

create table famille
(
    id       int auto_increment,
    idAnimal int not null,
    idRole   int not null,
    primary key (id, idAnimal),
    foreign key (idAnimal) references animal (id),
    foreign key (idRole) references role (id)
);

create table niveau
(
    id      int auto_increment primary key,
    libelle varchar(30)
);

create table client
(
    id        int auto_increment primary key,
    nom       varchar(30),
    prenom    varchar(30),
    adresse   varchar(100),
    mail      varchar(100),
    telephone char(10),
    idNiveau  int,
    foreign key (idNiveau) references niveau (id)
);

create table clientanimal
(
    id       int auto_increment primary key,
    idClient int,
    idAnimal int,
    foreign key (idClient) references client (id),
    foreign key (idAnimal) references animal (id)
);


create table periodeobservation
(
    periode int
);

create table courbespoids
(
    idAnimal   int,
    dateSaisie date,
    poids      decimal(7, 3),
    moyenne    decimal(7, 3) default 0,
    primary key (idAnimal, dateSaisie),
    foreign key (idAnimal) references animal(id)
);

create table fournisseur
(
    id      int primary key auto_increment,
    libelle varchar(30),
    adresse varchar(100),
    mail    varchar(100),
    numero  char(10)
);

create table commande
(
    id            int primary key auto_increment,
    libelle       varchar(30),
    dateCommande  datetime,
    total         decimal,
    idFournisseur int,
    foreign key (idFournisseur) references fournisseur (id)
);

create table listeCommande
(
    idCommande int,
    idAnimal   int,
    primary key (idCommande, idAnimal),
    foreign key (idCommande) references commande (id),
    foreign key (idAnimal) references animal (id)
);

create table veterinaire
(
    id      int primary key auto_increment,
    nom     varchar(30),
    mail    varchar(100),
    numero  char(10),
    adresse varchar(100)
);

create table vaccin
(
    id      int primary key auto_increment,
    libelle varchar(30)
);

create table listeanimauxveterinaires
(
    idVeterinaire int,
    idAnimal      int,
    observations  text(500),
    primary key (idVeterinaire, idAnimal),
    foreign key (idVeterinaire) references veterinaire (id),
    foreign key (idAnimal) references animal (id)
);

create table listeanimauxvaccins
(
    idAnimal        int,
    idVaccin        int,
    dateVaccination date,
    primary key (idAnimal, idVaccin),
    foreign key (idAnimal) references animal (id),
    foreign key (idVaccin) references vaccin (id)
);

insert into veterinaire(id, nom, mail, numero, adresse)
VALUES (1, 'Inconnu', 'Inconnu', '1111111111', 'Inconnu'),
       (2, 'Berdal', 'berdal@homail.fr', '0606587898', '45 rue damiens');

insert into vaccin(id, libelle)
VALUES (1, 'Inconnu'),
       (2, 'La rage');

insert into utilisateur(id, pseudo, mdp, mail)
VALUES (1, 'admin', sha2(('admin'), 256), 'admin@hotmail.fr');

insert into role(libelleRole)
values ('Inconnu'),
       ('Père'),
       ('Mère'),
       ('Enfant');

insert into typeanimal(libelleType)
values ('Inconnu'),
       ('Chien'),
       ('Chat'),
       ('Souris');

insert into race(libelleRace)
values ('Inconnue'),
       ('Boxer'),
       ('Sphynx'),
       ('Muridae');

insert into statut(libelleStatut)
values ('Inconnu'),
       ('Décédé'),
       ('Vendu'),
       ('Malade'),
       ('Disparu'),
       ('Elevage'),
       ('Réservé');

insert into animal(id)
values (1);

insert into animal(nom, prenom, dateNaissance, sexe, poidsActuel, idStatut, idPere, idMere, idRace, idType)
values ('Test', 'Marquise', '2018-01-14', 'Femelle', 0.100, 6, 1, 1, 3, 3),
       ('Test', 'Rio', '2020-04-08', 'Femelle', 0.129, 6, 1, 2, 3, 3),
       ('Test', 'Robby', '2020-04-08', 'Mâle', 0.097, 6, 1, 2, 3, 3),
       ('Test', 'Rosie', '2020-04-08', 'Femelle', 0.097, 6, 1, 2, 3, 3),
       ('Test', 'Rio moontie', '2020-04-08', 'Mâle', 0.107, 6, 1, 2, 3, 3);

insert into famille(id, idAnimal, idRole)
values (1, 2, 3),
       (1, 1, 2),
       (1, 3, 4),
       (1, 4, 4),
       (1, 5, 4),
       (1, 6, 4);


insert into niveau(libelle)
values ('Nouveau'),
       ('Habitué'),
       ('Partenaire');

insert into client(id, nom, prenom, adresse, mail, telephone, idNiveau)
values (1, 'Serin', 'Enzo', '10 rue Phillipe', 'eserin@hotmail.fr', '0645785112', 1);

insert into fournisseur(id, libelle, adresse, mail, numero)
values (1, 'Inconnu', 'Inconnu', 'Inconnu', '0303030303'),
       (2, 'Ricquier', '10 rue du chateau', 'dricquier@hotmail.fr', '0745895123');

insert into commande(id, libelle, dateCommande, total, idFournisseur)
values (1, 'Croquettes', '2020-01-10', 60, 1);

insert into listeCommande(idCommande, idAnimal)
values (1, 2),
       (1, 3),
       (1, 4);

insert into periodeobservation(periode)
values (60);


insert into courbespoids(idAnimal, dateSaisie, poids)
values (3, '2020-04-08', 0.126),
       (4, '2020-04-08', 0.097),
       (5, '2020-04-08', 0.097),
       (6, '2020-04-08', 0.107),
       (3, '2020-04-09', 0.129),
       (4, '2020-04-09', 0.102),
       (5, '2020-04-09', 0.104),
       (6, '2020-04-09', 0.111),
       (3, '2020-04-10', 0.147),
       (4, '2020-04-10', 0.121),
       (5, '2020-04-10', 0.124),
       (6, '2020-04-10', 0.116),
       (3, '2020-04-11', 0.161),
       (4, '2020-04-11', 0.133),
       (5, '2020-04-11', 0.136),
       (6, '2020-04-11', 0.125),
       (3, '2020-04-12', 0.183),
       (4, '2020-04-12', 0.151),
       (5, '2020-04-12', 0.153),
       (6, '2020-04-12', 0.132),
       (3, '2020-04-13', 0.197),
       (4, '2020-04-13', 0.162),
       (5, '2020-04-13', 0.161),
       (6, '2020-04-13', 0.137),
       (3, '2020-04-14', 0.214),
       (4, '2020-04-14', 0.170),
       (5, '2020-04-14', 0.176),
       (6, '2020-04-14', 0.148),
       (3, '2020-04-15', 0.228),
       (4, '2020-04-15', 0.188),
       (5, '2020-04-15', 0.185),
       (6, '2020-04-15', 0.153),
       (3, '2020-04-16', 0.247),
       (4, '2020-04-16', 0.190),
       (5, '2020-04-16', 0.199),
       (6, '2020-04-16', 0.172),
       (3, '2020-04-17', 0.256),
       (4, '2020-04-17', 0.197),
       (5, '2020-04-17', 0.212),
       (6, '2020-04-17', 0.182),
       (3, '2020-04-18', 0.265),
       (4, '2020-04-18', 0.205),
       (5, '2020-04-18', 0.229),
       (6, '2020-04-18', 0.192),
       (3, '2020-04-19', 0.269),
       (4, '2020-04-19', 0.220),
       (5, '2020-04-19', 0.234),
       (6, '2020-04-19', 0.192),
       (3, '2020-04-20', 0.289),
       (4, '2020-04-20', 0.228),
       (5, '2020-04-20', 0.245),
       (6, '2020-04-20', 0.206),
       (3, '2020-04-21', 0.306),
       (4, '2020-04-21', 0.241),
       (5, '2020-04-21', 0.253),
       (6, '2020-04-21', 0.211),
       (3, '2020-04-22', 0.312),
       (4, '2020-04-22', 0.256),
       (5, '2020-04-22', 0.263),
       (6, '2020-04-22', 0.217),
       (3, '2020-04-23', 0.325),
       (4, '2020-04-23', 0.261),
       (5, '2020-04-23', 0.274),
       (6, '2020-04-23', 0.223),
       (3, '2020-04-24', 0.332),
       (4, '2020-04-24', 0.269),
       (5, '2020-04-24', 0.284),
       (6, '2020-04-24', 0.223),
       (3, '2020-04-25', 0.349),
       (4, '2020-04-25', 0.281),
       (5, '2020-04-25', 0.291),
       (6, '2020-04-25', 0.225),
       (3, '2020-04-26', 0.359),
       (4, '2020-04-26', 0.294),
       (5, '2020-04-26', 0.308),
       (6, '2020-04-26', 0.227),
       (3, '2020-04-27', 0.372),
       (4, '2020-04-27', 0.303),
       (5, '2020-04-27', 0.310),
       (6, '2020-04-27', 0.227),
       (3, '2020-04-28', 0.390),
       (4, '2020-04-28', 0.312),
       (5, '2020-04-28', 0.314),
       (6, '2020-04-28', 0.232),
       (3, '2020-04-29', 0.411),
       (4, '2020-04-29', 0.329),
       (5, '2020-04-29', 0.309),
       (6, '2020-04-29', 0.228),
       (3, '2020-04-30', 0.429),
       (4, '2020-04-30', 0.342),
       (5, '2020-04-30', 0.318),
       (6, '2020-04-30', 0.219),
       (3, '2020-05-01', 0.445),
       (4, '2020-05-01', 0.363),
       (5, '2020-05-01', 0.336),
       (3, '2020-05-02', 0.460),
       (4, '2020-05-02', 0.378),
       (5, '2020-05-02', 0.360),
       (3, '2020-05-03', 0.476),
       (4, '2020-05-03', 0.390),
       (5, '2020-05-03', 0.399),
       (3, '2020-05-04', 0.491),
       (4, '2020-05-04', 0.409),
       (5, '2020-05-04', 0.406),
       (3, '2020-05-05', 0.509),
       (4, '2020-05-05', 0.422),
       (5, '2020-05-05', 0.429),
       (3, '2020-05-06', 0.514),
       (4, '2020-05-06', 0.431),
       (5, '2020-05-06', 0.438),
       (3, '2020-05-07', 0.532),
       (4, '2020-05-07', 0.453),
       (5, '2020-05-07', 0.445),
       (3, '2020-05-08', 0.542),
       (5, '2020-05-08', 0.462),
       (6, '2020-05-08', 0.442);



drop trigger if exists AvantSupprimerFournisseur;
drop trigger if exists AvantSupprimerAnimal;
drop trigger if exists AvantSupprimerRole;
drop trigger if exists AvantSupprimerRace;
drop trigger if exists AvantSupprimerTypeAnimal;
drop trigger if exists AvantSupprimerStatut;

create trigger AvantSupprimerFournisseur
    before delete
    on fournisseur
    for each row
    if old.id = 1 then
        SIGNAL SQLSTATE '45000' set message_text = 'Le fournisseur inconnu ne peut pas être supprimé';
    end if;

create trigger AvantSupprimerAnimal
    before delete
    on animal
    for each row
begin
    if old.id = 1 then
        SIGNAL SQLSTATE '45000' set message_text = 'L''animal inconnu ne peut pas être supprimé';
    end if;

    update animal set idMere = 1 where idMere = old.id;
    update animal set idPere = 1 where idPere = old.id;
end;


create trigger AvantSupprimerRole
    before delete
    on role
    for each row
    if old.id = 1 then
        SIGNAL SQLSTATE '45000' set message_text = 'Le role inconnu ne peut pas être supprimé';
    end if;

create trigger AvantSupprimerRace
    before delete
    on race
    for each row
begin
    if old.id = 1 then
        SIGNAL SQLSTATE '45000' set message_text = 'La race inconnue ne peut pas être supprimée';
    end if;

    update animal set idRace = 1 where idRace = old.id;
end;


create trigger AvantSupprimerTypeAnimal
    before delete
    on typeAnimal
    for each row
begin
    if old.id = 1 then
        SIGNAL SQLSTATE '45000' set message_text = 'Le type inconnu ne peut pas être supprimé';
    end if;

    update animal set idType = 1 where idType = old.id;
end;


create trigger AvantSupprimerStatut
    before delete
    on statut
    for each row
begin
    if old.id = 1 then
        SIGNAL SQLSTATE '45000' set message_text = 'Le statut inconnu ne peut pas être supprimé';
    end if;

    update animal set idStatut = 1 where idStatut = old.id;

end;
    