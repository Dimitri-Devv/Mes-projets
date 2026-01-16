# Application Web « Blogs Films » (MovieLab)

![Frontend](https://img.shields.io/badge/Frontend-React-61DAFB?logo=react&style=for-the-badge)
![Backend](https://img.shields.io/badge/Backend-SpringBoot-6DB33F?logo=springboot&style=for-the-badge)
![Database](https://img.shields.io/badge/Database-PostgreSQL-336791?logo=postgresql&style=for-the-badge)

---

## Sommaire

- [Présentation du projet](#présentation-du-projet)
- [Objectifs](#objectifs)
- [Architecture](#architecture)
- [Technologies utilisées](#technologies-utilisées)
- [Fonctionnalités principales](#fonctionnalités-principales)
- [Installation et lancement](#installation-et-lancement)
  - [Frontend](#frontend)
  - [Backend](#backend)
- [Contribution](#contribution)
- [Licences](#licences)

---

## Présentation du projet

**Blogs Films (MovieLab)** est une application web permettant aux utilisateurs de consulter des films, publier des critiques, interagir avec celles des autres utilisateurs et gérer une liste de films favoris.  
Le projet s’inscrit dans une démarche pédagogique visant à concevoir une application **full-stack** moderne, sécurisée et maintenable.

---

## Objectifs

- Mettre en œuvre une architecture frontend / backend découplée
- Concevoir et consommer une API REST
- Implémenter un système d’authentification sécurisé
- Appliquer les bonnes pratiques de développement web
- Proposer une interface utilisateur claire et responsive

---

## Architecture

Le projet est structuré en deux parties distinctes :

- **Frontend** : Application React assurant l’interface utilisateur
- **Backend** : API REST Spring Boot gérant la logique métier, la sécurité et l’accès aux données

Les échanges se font via des requêtes HTTP entre le frontend et le backend.

---

## Technologies utilisées

### Frontend
- React
- JavaScript
- HTML / CSS

### Backend
- Spring Boot
- Spring Security
- JPA / Hibernate

### Base de données
- PostgreSQL

---

## Fonctionnalités principales

- Consultation d’un catalogue de films
- Publication, modification et suppression de critiques
- Système de likes et dislikes sur les critiques
- Gestion des films favoris
- Recommandations de films basées sur les préférences de l’utilisateur
- Authentification et sécurisation des actions sensibles

---

## Installation et lancement

### Prérequis

- Node.js
- Java 17 ou supérieur
- PostgreSQL

---

### Frontend

```bash
npm install
npm start
```

---

### Backend

```bash
./mvnw spring-boot:run
```

---

## Contribution

Projet réalisé par :

- Dimitri Ricquier
- Xan Balandrau  
  https://github.com/xanbalandrau

---

## Licences

Les avatars utilisés dans le projet proviennent de **Freepik** et sont distribués sous licence  
[Creative Commons Attribution 4.0 International (CC BY 4.0)](https://creativecommons.org/licenses/by/4.0/).
