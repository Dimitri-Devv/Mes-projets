## Documentation technique — ProjetPOEI (API Java/Spring)

Cette documentation présente l’architecture, la sécurité, ainsi que les principaux composants (contrôleurs, services, mappers) de l’API. Elle standardise et formalise les responsabilités et les contrats exposés sans révéler d’implémentation interne.

## Sommaire

- [Documentation technique — ProjetPOEI (API Java/Spring)](#documentation-technique--projetpoei-api-javaspring)
- [Sommaire](#sommaire)
- [Vue d’ensemble](#vue-densemble)
- [Documentation API (Swagger)](#documentation-api-swagger)
- [Architecture applicative](#architecture-applicative)
  - [Flux typique d’une requête](#flux-typique-dune-requête)
  - [Architecture globale (vue synthétique)](#architecture-globale-vue-synthétique)
- [Sécurité et authentification](#sécurité-et-authentification)
  - [Filtres de sécurité](#filtres-de-sécurité)
  - [Gestion centralisée des échecs](#gestion-centralisée-des-échecs)
  - [Configuration globale](#configuration-globale)
- [Conventions et DTO](#conventions-et-dto)
- [CONTROLLERS](#controllers)
  - [Résumé des accès (synthèse)](#résumé-des-accès-synthèse)
  - [AdminUserController](#adminusercontroller)
    - [Endpoints principaux](#endpoints-principaux)
    - [Détails des fonctions](#détails-des-fonctions)
      - [🟦 getAllUsers](#-getallusers)
      - [🟩 updateUser](#-updateuser)
      - [🟥 deleteUser](#-deleteuser)
      - [🟨 updateRole](#-updaterole)
  - [AuthController](#authcontroller)
    - [Endpoints principaux](#endpoints-principaux-1)
    - [Exemples de payloads](#exemples-de-payloads)
    - [Détails des méthodes](#détails-des-méthodes)
      - [🟦 register — Inscription](#-register--inscription)
      - [🟩 login — Authentification + JWT](#-login--authentification--jwt)
      - [🟨 verify — Vérification du compte](#-verify--vérification-du-compte)
  - [FilmController](#filmcontroller)
    - [Endpoints principaux](#endpoints-principaux-2)
    - [Détails des méthodes](#détails-des-méthodes-1)
      - [🟦 getAllFilms](#-getallfilms)
      - [🟩 getFilmById](#-getfilmbyid)
      - [🟦 getShortFilms](#-getshortfilms)
      - [🟨 addFilm](#-addfilm)
      - [🟧 updateFilm](#-updatefilm)
      - [🟥 deleteFilm](#-deletefilm)
  - [GenreController](#genrecontroller)
    - [Sécurité par défaut](#sécurité-par-défaut)
      - [Routes publiques](#routes-publiques)
      - [Routes réservées ADMIN](#routes-réservées-admin)
    - [Détails des méthodes](#détails-des-méthodes-2)
      - [🟦 getAllGenres (public)](#-getallgenres-public)
      - [🟩 getGenreById (public)](#-getgenrebyid-public)
      - [🟨 addGenre (ADMIN)](#-addgenre-admin)
      - [🟧 updateGenre (ADMIN)](#-updategenre-admin)
      - [🟥 deleteGenre (ADMIN)](#-deletegenre-admin)
  - [UserController](#usercontroller)
    - [Endpoints principaux](#endpoints-principaux-3)
    - [Détails des méthodes](#détails-des-méthodes-3)
      - [🟦 getUserProfile (public)](#-getuserprofile-public)
      - [🟩 updateOwnProfile (authentifié)](#-updateownprofile-authentifié)
      - [🟥 deleteOwnAccount (authentifié)](#-deleteownaccount-authentifié)
      - [🟦 getFavorites (public)](#-getfavorites-public)
      - [🟩 addFavorite (authentifié)](#-addfavorite-authentifié)
      - [🟥 removeFavorite (authentifié)](#-removefavorite-authentifié)
      - [🟪 getRecommended (authentifié)](#-getrecommended-authentifié)
  - [ActorController](#actorcontroller)
    - [Endpoints principaux](#endpoints-principaux-4)
    - [Détails des méthodes](#détails-des-méthodes-4)
      - [🟦 getAllActors (public)](#-getallactors-public)
      - [🟩 getActorById (public)](#-getactorbyid-public)
      - [🟨 getFilmsByActor (public)](#-getfilmsbyactor-public)
      - [🟦 getActorsByFilm (public)](#-getactorsbyfilm-public)
      - [🟩 addActor (ADMIN)](#-addactor-admin)
      - [🟧 updateActor (ADMIN)](#-updateactor-admin)
      - [🟥 deleteActor (ADMIN)](#-deleteactor-admin)
      - [🟨 addFilmToActor (ADMIN)](#-addfilmtoactor-admin)
      - [🟥 removeFilmFromActor (ADMIN)](#-removefilmfromactor-admin)
  - [ReviewController](#reviewcontroller)
    - [Endpoints principaux](#endpoints-principaux-5)
    - [Détails des méthodes](#détails-des-méthodes-5)
      - [🟦 getReviewsByFilm (public)](#-getreviewsbyfilm-public)
      - [🟩 createReview (authentifié)](#-createreview-authentifié)
      - [🟧 updateReview (owner/admin)](#-updatereview-owneradmin)
      - [🟥 deleteReview (owner/admin)](#-deletereview-owneradmin)
      - [🟦 getReviewsByUser (public)](#-getreviewsbyuser-public)
      - [🟦 getTopReviews (public)](#-gettopreviews-public)
  - [ReviewLikeController](#reviewlikecontroller)
    - [Endpoints principaux](#endpoints-principaux-6)
    - [🟦 toggleLike (authentifié)](#-togglelike-authentifié)
    - [🟩 getStatus (authentifié)](#-getstatus-authentifié)
  - [CommentController](#commentcontroller)
    - [Endpoints principaux](#endpoints-principaux-7)
    - [Détails des méthodes](#détails-des-méthodes-6)
      - [🟦 getCommentsByReview (public)](#-getcommentsbyreview-public)
      - [🟩 createComment (authentifié)](#-createcomment-authentifié)
      - [🟧 updateComment (owner/admin)](#-updatecomment-owneradmin)
      - [🟥 deleteComment (owner/admin)](#-deletecomment-owneradmin)
- [SERVICES](#services)
  - [AdminUserService](#adminuserservice)
    - [Méthodes principales :](#méthodes-principales-)
  - [AuthService](#authservice)
    - [Méthodes principales :](#méthodes-principales--1)
  - [FilmService](#filmservice)
    - [Méthodes principales :](#méthodes-principales--2)
  - [EmailService](#emailservice)
    - [Méthodes principales :](#méthodes-principales--3)
  - [GenreService](#genreservice)
    - [Méthodes principales :](#méthodes-principales--4)
  - [UserService](#userservice)
    - [Méthodes principales :](#méthodes-principales--5)
  - [ActorService](#actorservice)
    - [Méthodes principales :](#méthodes-principales--6)
  - [ReviewService](#reviewservice)
    - [Méthodes principales :](#méthodes-principales--7)
  - [ReviewLikeService](#reviewlikeservice)
    - [Méthodes principales :](#méthodes-principales--8)
  - [CommentService](#commentservice)
    - [Méthodes principales :](#méthodes-principales--9)
- [MAPPERS](#mappers)
  - [ActorMapper](#actormapper)
    - [Méthodes principales :](#méthodes-principales--10)
  - [FilmMapper](#filmmapper)
    - [Méthodes principales :](#méthodes-principales--11)
  - [GenreMapper](#genremapper)
    - [Méthodes principales :](#méthodes-principales--12)
  - [UserMapper](#usermapper)
    - [Méthodes principales :](#méthodes-principales--13)
  - [ReviewMapper](#reviewmapper)
    - [Méthodes principales :](#méthodes-principales--14)
  - [CommentMapper](#commentmapper)
    - [Méthodes principales :](#méthodes-principales--15)
  - [ReviewLikeMapper](#reviewlikemapper)
    - [Méthodes principales :](#méthodes-principales--16)
- [SECURITY](#security)
  - [🟥 AuthEntryPointJwt](#-authentrypointjwt)
  - [🟦 AuthTokenFilter](#-authtokenfilter)
  - [🟩 JwtUtil](#-jwtutil)
  - [🟨 RateLimitFilter](#-ratelimitfilter)
  - [WebSecurityConfig](#websecurityconfig)
- [🗂️ Annexes et notes](#️-annexes-et-notes)
  - [📘 Codes HTTP usuels](#-codes-http-usuels)
  - [🧭 Bonnes pratiques](#-bonnes-pratiques)

<a id="vue-ensemble"></a>

## Vue d’ensemble

- **API REST stateless** sécurisée par **JWT**, développée avec **Spring Boot**.
- **Architecture en couches** clairement séparées :  
  **Controller → Service → Repository → Database**.
- Les **entités JPA ne sont jamais exposées** directement : toutes les réponses sont renvoyées via des **DTO**, garantissant la sécurité, la stabilité et l’absence de références circulaires.
- Gestion des **rôles utilisateurs** :
  - `USER` — accès standard
  - `ADMIN` — accès aux opérations sensibles (gestion utilisateurs, gestion contenus)
- Application pensée pour être **scalable**, **maintenable** et conforme aux bonnes pratiques Spring (SOLID, découplage des responsabilités, architecture REST propre).

<a id="documentation-api"></a>

## Documentation API (Swagger)

L’API dispose également d’une documentation interactive via **Swagger / OpenAPI 3**.

- Interface Swagger UI disponible à l’adresse :

  **`/swagger-ui/index.html`**

- Documentation OpenAPI (format JSON) :  
  **`/v3/api-docs`**

Swagger fournit :

- la liste complète des endpoints,
- les schémas de DTO,
- les paramètres et réponses attendues,
- les codes HTTP possibles,
- la possibilité de tester les requêtes **directement dans le navigateur**.

Cette documentation est générée via les annotations suivantes :

- `@Operation`
- `@ApiResponses`
- `@ApiResponse`
- `@Parameter`
- `@Schema`

<a id="architecture"></a>

## Architecture applicative

- **Contrôleurs** : exposent les endpoints REST, valident les entrées (JSR-303) et délèguent le traitement aux services.
- **Services** : encapsulent la logique métier, appliquent les règles applicatives, les validations supplémentaires et les contrôles d’accès métier.
- **Mappers** : assurent la conversion **Entités ↔ DTO**, permettant d’isoler totalement la couche API de la couche de persistance.
- **Sécurité** : basée sur un filtre JWT, un point d’entrée personnalisé (401 JSON) et un mécanisme de _rate limiting_ ciblé sur le login pour prévenir le bruteforce.

### Flux typique d’une requête

- **Client** → **Controller** (validation, auth) → **Service** (métier) → **Repository** (BDD) → **Service** → **Mapper** → **Controller** (DTO).

### Architecture globale (vue synthétique)

```text
Client
  │
  ├─→ Controller (validation, auth)
  │      │
  │      ├─→ Service (règles métier)
  │      │      │
  │      │      └─→ Repository → Database
  │      │
  │      └─→ Mapper ↔ DTO
  │
  └─→ Security / JWT (filters, entry point, config)
```

<a id="securite-authentification"></a>

## Sécurité et authentification

- **Authentification JWT** : le serveur génère un token signé (HMAC) contenant au minimum :
  - l’identifiant utilisateur (`id`),
  - l’email (`subject`),
  - le rôle (`role`).
- **Autorisation** : gérée par Spring Security via des règles par endpoint et des contrôles de rôle (`USER`, `ADMIN`).
- **Stateless** : aucune session serveur. Chaque requête authentifiée transporte son propre JWT dans l’en-tête `Authorization: Bearer <token>`.

### Filtres de sécurité

- **AuthTokenFilter**

  - extrait le JWT depuis l’en-tête `Authorization`,
  - valide signature + expiration,
  - charge le `UserDetails`,
  - peuple le `SecurityContext` pour le reste du traitement.

- **RateLimitFilter**
  - protège `POST /api/auth/login` du bruteforce,
  - limite à **5 tentatives / 10 minutes** par IP,
  - renvoie **HTTP 429 Too Many Requests** en cas d’abus.

### Gestion centralisée des échecs

- **AuthEntryPointJwt**  
  Intervient pour tout accès non authentifié ou interdit.  
  Retourne une réponse **HTTP 401 JSON claire**, évitant les redirections HTML par défaut de Spring Security.

### Configuration globale

- **WebSecurityConfig**
  - définit toutes les règles d’accès (public, authentifié, administrateur),
  - configure CORS et désactive CSRF (JWT),
  - insère les filtres (JWT + Rate Limiting) dans la chaîne de Spring Security,
  - expose le `PasswordEncoder` (BCrypt) et l’`AuthenticationManager`.

<a id="conventions-dto"></a>

## Conventions et DTO

- **Format des échanges** : toutes les entrées et sorties sont en **JSON**.
- **DTO de requête** : toujours suffixés en `Request` ou `RequestDto`  
  (ex. `FilmCreateRequestDto`, `UserUpdateRequest`).
- **DTO de réponse** : suffixés en `Response` ou `ResponseDto`  
  (ex. `FilmResponseDto`, `UserProfileResponse`).
- **Identifiants** : représentés par un type numérique (`id`) sauf indication contraire.
- **Gestion des erreurs** :
  - Toutes les erreurs métier sont transformées en **HTTP 4xx** appropriés.
  - Le corps de réponse contient un **message clair et contextualisé** pour faciliter le débogage côté client.

<a id="controllers"></a>

## <h2 style="color:#b57bff;">CONTROLLERS</h2>

### Résumé des accès (synthèse)

- **AdminUserController**  
  → Accès **ADMIN** uniquement  
  → Gestion administrative des comptes : consultation, mise à jour, suppression, modification des rôles.

- **AuthController**  
  → Endpoints **publics** : inscription, login (JWT), vérification OTP.  
  → Point d'entrée pour tout le cycle d’authentification.

- **FilmController**  
  → CRUD complet sur les films.  
  → Toutes les réponses utilisent `FilmResponseDto` pour éviter l’exposition des entités.

- **GenreController**  
  → **GET** : accès public  
  → **POST / PUT / DELETE** : réservés aux administrateurs  
  → Gestion des genres cinématographiques.

- **UserController**  
  → Consultation de profil public  
  → Opérations “self” pour l’utilisateur connecté (mise à jour profil, suppression compte)  
  → Gestion des favoris (add/remove).

- **ActorController**  
  → Consultation publique des acteurs  
  → Création / modification / suppression selon les règles métier définies.

- **ReviewController**
  → GET : accès public (liste des critiques d’un film, récupération d’une critique)
  → POST : utilisateur authentifié (1 seule critique par film et par utilisateur)
  → PUT / DELETE : réservés à l’auteur de la critique ou à ADMIN
  → Gestion complète du cycle de vie d’une critique : création, édition, suppression, exposition côté frontend.

- **ReviewLikeController**
  → POST : utilisateur authentifié
  → Gestion du système LIKE / DISLIKE d’une critique
  → Un utilisateur ne peut avoir qu’une réaction à la fois (like ou dislike), remplacée automatiquement en cas de changement
  → Mise à jour automatique des compteurs likesCount et dislikesCount

- **CommentController**
  → GET : accès public (lecture des commentaires liés à une critique)
  → POST : utilisateur authentifié (doit correspondre au userId du JWT)
  → PUT / DELETE : réservés à l’auteur du commentaire ou ADMIN
  → Permet d’ajouter, modifier et supprimer des commentaires associés aux critiques.

- **ReportController**
- → GET : accès admin (lecture de tous les reports)
  → POST : utilisateur authentifié
---

**Note importante**  
Sauf mention contraire, tous les chemins d'API ci-dessous sont relatifs à :  
**`VITE_API_URL`** (valeur définie dans le frontend).

<a id="ctrl-adminuser"></a>

### <h3 style="color:#d3b6ff;">AdminUserController</h3>

**Rôle :** expose les routes réservées aux administrateurs (`ADMIN`).  
Permet la gestion avancée des comptes utilisateurs (profil, statut, rôle, suppression).

---

#### Endpoints principaux

| Méthode | Chemin             | Description                                     | Rôle  |
| ------- | ------------------ | ----------------------------------------------- | ----- |
| GET     | `/users`           | Liste tous les utilisateurs                     | ADMIN |
| PUT     | `/users/{id}`      | Met à jour les informations d’un utilisateur    | ADMIN |
| DELETE  | `/users/{id}`      | Supprime un utilisateur                         | ADMIN |
| PUT     | `/users/{id}/role` | Met à jour le rôle d’un utilisateur (optionnel) | ADMIN |

> ℹ️ La modification du rôle dispose d’une route dédiée.

---

#### Détails des fonctions

##### <a id="ctrl-adminuser-getallusers"></a>🟦 getAllUsers

- **Description :** retourne la liste complète des utilisateurs présents en base.
- **Réponse :** liste de `UserResponse`.
- **Sécurité :** `ADMIN`.

---

##### <a id="ctrl-adminuser-updateuser"></a>🟩 updateUser

- **Description :** met à jour les informations d’un utilisateur.  
  Champs modifiables : avatar, statut (bloqué ou non), avertissements, vérification email…  
  Le **rôle** n’est _pas_ modifié via cette route.
- **Entrées :**
  - `id` — Path parameter
  - `dto` — Body partiel
- **Réponse :** `UserResponse` mis à jour.
- **Sécurité :** `ADMIN`.

---

##### <a id="ctrl-adminuser-deleteuser"></a>🟥 deleteUser

- **Description :** supprime un utilisateur.  
  La suppression d’un administrateur est bloquée au niveau service.
- **Entrées :**
  - `id` — Path parameter
- **Réponse :** `204 No Content`.
- **Sécurité :** `ADMIN`.

---

##### <a id="ctrl-adminuser-updaterole"></a>🟨 updateRole

- **Description :** met à jour le rôle d’un utilisateur (ex. `USER → ADMIN`).  
  Une protection empêche un administrateur de retirer _son propre_ rôle.
- **Entrées :**
  - `id` — Path parameter
  - `dto` — Nouveau rôle
- **Réponse :** `UserResponse` mis à jour.
- **Sécurité :** `ADMIN`.

<a id="ctrl-auth"></a>

### <h3 style="color:#d3b6ff;">AuthController</h3>

**Rôle :** gère l’intégralité du cycle d’authentification et de vérification des comptes (inscription, login JWT, validation par OTP).

---

#### Endpoints principaux

| Méthode | Chemin           | Description                         | Accès  |
| ------- | ---------------- | ----------------------------------- | ------ |
| POST    | `/auth/register` | Inscription d’un nouvel utilisateur | Public |
| POST    | `/auth/login`    | Authentification + génération JWT   | Public |
| POST    | `/auth/verify`   | Vérification d’email via OTP        | Public |

---

#### Exemples de payloads

```json
// POST /auth/register
{
  "username": "alice",
  "email": "alice@example.com",
  "passwordHash": "MotDePasse"
}
```

```json
// POST /auth/login
{
  "email": "alice@example.com",
  "passwordHash": "MotDePasse"
}
```

```json
// POST /auth/verify
{
  "email": "alice@example.com",
  "code": "123456"
}
```

#### Détails des méthodes

##### <a id="ctrl-auth-register"></a>🟦 register — Inscription

**Description :**  
Crée un nouvel utilisateur après validation des données (`email`, `username`, `password`).  
L’email doit être unique ; le mot de passe est automatiquement hashé par le service.

**Entrée :**

- `RegisterRequest` (body JSON)

**Sortie :**

- **201 Created** avec l’utilisateur créé (mot de passe masqué)
- **409 Conflict** si l’email est déjà enregistré

---

##### <a id="ctrl-auth-login"></a>🟩 login — Authentification + JWT

**Description :**  
Vérifie les identifiants et génère un **JWT signé** contenant :

- `id`
- `email` (subject)
- `role`

Le compte doit être **préalablement vérifié par OTP**.

**Entrée :**

- `LoginRequest` (email + password)

**Sortie :**

- **200 OK** avec `LoginResponse` (token JWT + infos essentielles)
- **401 Unauthorized** si identifiants invalides ou compte non vérifié

---

##### <a id="ctrl-auth-verify"></a>🟨 verify — Vérification du compte

**Description :**  
Valide l’email de l’utilisateur via un code OTP **à 6 chiffres**.  
Active définitivement le compte si le code est correct et non expiré.

**Entrée :**

- `email` (string)
- `code` (string, format `NNNNNN`)

**Sortie :**

- **200 OK** si vérification réussie
- **400 Bad Request** si le code est incorrect, expiré ou ne correspond à aucun utilisateur

<a id="ctrl-film"></a>

### <h3 style="color:#d3b6ff;">FilmController</h3>

**Rôle :** expose les opérations CRUD liées aux films.  
Toutes les réponses utilisent des **DTO** (`FilmResponseDto`) afin d’éviter l’exposition des entités JPA.

---

#### Endpoints principaux

| Méthode | Chemin         | Description                              | Accès     |
| ------- | -------------- | ---------------------------------------- | --------- |
| GET     | `/films`       | Récupère la liste des films              | Public    |
| GET     | `/films/{id}`  | Détail d’un film                         | Public    |
| GET     | `/films/short` | Liste allégée des films (id, titre, URL) | Public    |
| POST    | `/films`       | Création d’un film                       | (Admin\*) |
| PUT     | `/films/{id}`  | Mise à jour d’un film                    | (Admin\*) |
| DELETE  | `/films/{id}`  | Suppression d’un film                    | (Admin\*) |

> \* L’accès précis dépend des règles métier définies dans le backend (souvent réservé aux administrateurs).

---

#### Détails des méthodes

##### <a id="ctrl-film-getallfilms"></a>🟦 getAllFilms

- **Description :** retourne l’ensemble des films disponibles (souvent triés par date de sortie côté service).
- **Entrées :** aucune.
- **Réponse :** liste de `FilmResponseDto`.

---

##### <a id="ctrl-film-getfilmbyid"></a>🟩 getFilmById

- **Description :** retourne les informations complètes d’un film via son identifiant.
- **Entrées :**
  - `id` — identifiant du film (path)
- **Réponse :** `FilmResponseDto` détaillé.

---

##### <a id="ctrl-film-getshortfilms"></a>🟦 getShortFilms

- **Description :** retourne une **liste allégée des films** contenant uniquement :

  - `id`
  - `titre`
  - `afficheUrl`
  - `posterUrl`

  Cette route est principalement utilisée pour les sélecteurs de films côté utilisateur  
  (ex : choisir un fond ou une image de couverture dans le profil).

- **Entrées :** aucune.
- **Réponse :** liste de `FilmUpdateUser` (DTO simplifié).

##### <a id="ctrl-film-addfilm"></a>🟨 addFilm

- **Description :** ajoute un nouveau film en base de données.
- **Entrées :**
  - `FilmRequestDto` — données de création (body)
- **Réponse :** `FilmResponseDto` représentant le film créé.

---

##### <a id="ctrl-film-updatefilm"></a>🟧 updateFilm

- **Description :** met à jour un film existant.  
  Le backend applique la mise à jour partielle ou complète selon le DTO utilisé (`FilmUpdateDto`).
- **Entrées :**
  - `id` — identifiant du film (path)
  - `FilmUpdateDto` — données modifiées (body)
- **Réponse :** `FilmResponseDto` mis à jour.

---

##### <a id="ctrl-film-deletefilm"></a>🟥 deleteFilm

- **Description :** supprime un film de la base de données.
- **Entrées :**
  - `id` — identifiant du film (path)
- **Réponse :** **204 No Content** en cas de succès.

---

<a id="ctrl-genre"></a>

### <h3 style="color:#d3b6ff;">GenreController</h3>

**Rôle :** gestion des genres cinématographiques.  
Les opérations de lecture sont publiques, tandis que la création, modification et suppression sont réservées aux administrateurs (`ADMIN`).

---

#### Sécurité par défaut

##### Routes publiques

- GET `/genres`
- GET `/genres/{id}`

##### Routes réservées ADMIN

- POST `/genres`
- PUT `/genres/{id}`
- DELETE `/genres/{id}`

---

#### Détails des méthodes

##### <a id="ctrl-genre-getallgenres"></a>🟦 getAllGenres (public)

- **Description :** retourne la liste complète des genres disponibles.
- **Réponse :** liste de `GenreResponseDto`.

---

##### <a id="ctrl-genre-getgenrebyid"></a>🟩 getGenreById (public)

- **Description :** retourne un genre spécifique par son identifiant.
- **Entrées :**
  - `id` — identifiant du genre (path)
- **Réponse :** `GenreResponseDto`.

---

##### <a id="ctrl-genre-addgenre"></a>🟨 addGenre (ADMIN)

- **Description :** crée un nouveau genre.  
  Le service effectue :
  - validation du DTO
  - contrôle d’unicité du nom
- **Entrées :**
  - `GenreRequestDto` (body)
- **Réponse :** `GenreResponseDto` représentant le genre créé.

---

##### <a id="ctrl-genre-updategenre"></a>🟧 updateGenre (ADMIN)

- **Description :** met à jour un genre existant.  
  Inclut :
  - vérification de l’existence du genre
  - contrôle d’unicité du nom
- **Entrées :**
  - `id` (path)
  - `GenreRequestDto` (body)
- **Réponse :** `GenreResponseDto` mis à jour.

---

##### <a id="ctrl-genre-deletegenre"></a>🟥 deleteGenre (ADMIN)

- **Description :** supprime un genre donné.  
  Une vérification préalable évite les suppressions silencieuses.
- **Entrées :**
  - `id` — identifiant du genre
- **Réponse :** **204 No Content** en cas de succès.

<a id="ctrl-user"></a>

### <h3 style="color:#d3b6ff;">UserController</h3>

**Rôle :** regroupe les opérations « self-service » accessibles à l’utilisateur connecté, ainsi que la consultation publique d’un profil utilisateur.  
Certaines routes sont publiques, d’autres nécessitent une authentification via JWT.

---

#### Endpoints principaux

| Méthode | Chemin                           | Description                            | Accès       |
| ------- | -------------------------------- | -------------------------------------- | ----------- |
| GET     | `/users/{id}`                    | Profil public d’un utilisateur         | Public      |
| GET     | `/users/{id}/favorites`          | Liste des favoris d’un utilisateur     | Public      |
| GET     | `/users/me/recommended`          | Recommandations basées sur les favoris | Auth requis |
| POST    | `/users/{id}/favorites/{filmId}` | Ajouter un film aux favoris            | Auth requis |
| DELETE  | `/users/{id}/favorites/{filmId}` | Retirer un film des favoris            | Auth requis |
| PUT     | `/users/{id}` _(selon config)_   | Mise à jour du compte connecté         | Auth requis |
| DELETE  | `/users/{id}` _(selon config)_   | Suppression du compte connecté         | Auth requis |

> ℹ️ Les opérations _own profile_ utilisent l’identité extraite du JWT (email ou userId).

---

#### Détails des méthodes

##### <a id="ctrl-user-getuserprofile"></a>🟦 getUserProfile (public)

- **Description :** retourne les informations publiques d’un utilisateur (profil, avatar, etc.).
- **Entrées :**
  - `id` — identifiant de l’utilisateur (path)
- **Réponse :** `UserResponse` (version publique).

---

##### <a id="ctrl-user-updateownprofile"></a>🟩 updateOwnProfile (authentifié)

- **Description :** met à jour certaines informations du compte connecté (champs limités, ex. avatar).  
  Sécurisé : impossible de modifier les informations sensibles (rôle, email vérifié, etc.).
- **Entrées :**
  - `userDetails` — identité issue du JWT
  - `UserUpdateDto` (body)
- **Réponse :** `UserResponse` mis à jour.

---

##### <a id="ctrl-user-deleteownaccount"></a>🟥 deleteOwnAccount (authentifié)

- **Description :** supprime définitivement le compte de l’utilisateur connecté.  
  L’email contenu dans le JWT sert d’identifiant fiable.
- **Entrées :**
  - `userDetails` — identité du JWT
- **Réponse :** **204 No Content**.

---

##### <a id="ctrl-user-getfavorites"></a>🟦 getFavorites (public)

- **Description :** retourne la liste des films favoris d’un utilisateur.  
  Aucune donnée sensible n’est exposée.
- **Entrées :**
  - `userId` — identifiant de l’utilisateur (path)
- **Réponse :** liste de films favoris (DTO).

---

##### <a id="ctrl-user-addfavorite"></a>🟩 addFavorite (authentifié)

- **Description :** ajoute un film aux favoris du compte connecté.  
  L’identification est extraite du JWT, empêchant toute manipulation des favoris d’un autre utilisateur.
- **Entrées :**
  - `userDetails` — identité du JWT
  - `filmId` — identifiant du film (path ou body selon design)
- **Réponse :** `UserResponse` mis à jour.

---

##### <a id="ctrl-user-removefavorite"></a>🟥 removeFavorite (authentifié)

- **Description :** retire un film des favoris de l’utilisateur connecté.
- **Entrées :**
  - `userDetails` — identité du JWT
  - `filmId` — identifiant du film
- **Réponse :** `UserResponse` mis à jour.

---

##### 🟪 getRecommended (authentifié)

- **Description :** retourne une liste de films recommandés basée sur les genres des films que l’utilisateur a mis en favoris.
- **Entrées :** aucune (utilise l'identité du JWT)
- **Réponse :** `List<FilmResponseDto>`
- **Règles de fonctionnement :**
  - extrait la liste des films favoris de l’utilisateur,
  - déduit la liste des genres dominants,
  - récupère les films correspondants via `filmRepo.findByGenres_IdIn(...)`,
  - exclut automatiquement les films déjà présents dans les favoris.

---

<a id="ctrl-actor"></a>

### <h3 style="color:#d3b6ff;">ActorController</h3>

**Rôle :** expose l’ensemble des opérations liées aux acteurs ainsi que leurs associations avec les films.  
Lecture publique ; opérations d’écriture et de gestion des relations réservées aux administrateurs (`ADMIN`).

---

#### Endpoints principaux

| Méthode | Chemin                             | Description                          | Accès  |
| ------- | ---------------------------------- | ------------------------------------ | ------ |
| GET     | `/actors`                          | Liste tous les acteurs               | Public |
| GET     | `/actors/{id}`                     | Détail d’un acteur                   | Public |
| GET     | `/actors/{id}/films`               | Liste les films associés à un acteur | Public |
| GET     | `/actors/film/{filmId}`            | Liste les acteurs associés à un film | Public |
| POST    | `/actors`                          | Création d’un acteur                 | ADMIN  |
| PUT     | `/actors/{id}`                     | Mise à jour d’un acteur              | ADMIN  |
| DELETE  | `/actors/{id}`                     | Suppression d’un acteur              | ADMIN  |
| POST    | `/actors/{actorId}/films/{filmId}` | Associe un film à un acteur          | ADMIN  |
| DELETE  | `/actors/{actorId}/films/{filmId}` | Retire un film associé à un acteur   | ADMIN  |

---

#### Détails des méthodes

##### 🟦 getAllActors (public)

- **Description :** retourne la liste complète des acteurs.
- **Entrées :** aucune.
- **Réponse :** `List<ActorResponseDto>`.

---

##### 🟩 getActorById (public)

- **Description :** retourne les informations publiques d’un acteur donné.
- **Entrées :**
  - `id` — identifiant de l’acteur (path)
- **Réponse :** `ActorResponseDto`.
- **Erreurs possibles :**
  - **404 Not Found** — acteur introuvable.

---

##### 🟨 getFilmsByActor (public)

- **Description :** retourne la liste des films associés à un acteur.
- **Entrées :**
  - `id` — identifiant de l’acteur (path)
- **Réponse :** `List<FilmShortDto>`.
- **Erreurs possibles :**
  - **404 Not Found** — acteur introuvable.

---

##### 🟦 getActorsByFilm (public)

- **Description :** liste tous les acteurs associés à un film donné.
- **Entrées :**
  - `filmId` (path)
- **Réponse :** `List<ActorResponseDto>`.
- **Erreurs possibles :**
  - **404 Not Found** — film introuvable.

---

##### 🟩 addActor (ADMIN)

- **Description :** crée un nouvel acteur.  
  Accessible uniquement aux administrateurs.
- **Entrées :**
  - `ActorCreateDto` (body, validé)
- **Réponse :**
  - **201 Created** + `ActorResponseDto`
- **Erreurs possibles :**
  - **400 Bad Request** — données invalides
  - **401 Unauthorized** — token manquant
  - **403 Forbidden** — accès non autorisé

---

##### 🟧 updateActor (ADMIN)

- **Description :** met à jour les informations d’un acteur existant.
- **Entrées :**
  - `id` — identifiant de l’acteur
  - `ActorUpdateDto` — body validé
- **Réponse :** `ActorResponseDto` mis à jour.
- **Erreurs possibles :**
  - **400 Bad Request**
  - **403 Forbidden**
  - **404 Not Found** — acteur introuvable

---

##### 🟥 deleteActor (ADMIN)

- **Description :** supprime un acteur via son ID.
- **Entrées :**
  - `id` — identifiant de l’acteur
- **Réponse :** **204 No Content**.
- **Erreurs possibles :**
  - **401 Unauthorized**
  - **403 Forbidden**
  - **404 Not Found**

---

##### 🟨 addFilmToActor (ADMIN)

- **Description :** crée une relation ManyToMany Acteur ↔ Film.  
  (Acteur _ajoute_ un film à sa liste associée.)
- **Entrées :**
  - `actorId`
  - `filmId`
- **Réponse :** `ActorResponseDto` mis à jour.
- **Erreurs possibles :**
  - **404 Not Found** — acteur ou film introuvable

---

##### 🟥 removeFilmFromActor (ADMIN)

- **Description :** supprime la relation entre un acteur et un film.
- **Entrées :**
  - `actorId`
  - `filmId`
- **Réponse :** `ActorResponseDto` mis à jour.
- **Erreurs possibles :**
  - **404 Not Found**

<a id="ctrl-review"></a>

### <h3 style="color:#d3b6ff;">ReviewController</h3>

**Rôle :** gère les critiques publiées sur les films.  
Lecture publique ; création limitée à un utilisateur authentifié (une seule critique par film).  
Modification et suppression réservées à l’auteur ou à un administrateur (`ADMIN`).

---

#### Endpoints principaux

#### Endpoints principaux

| Méthode | Chemin                                      | Description                          | Accès              |
|---------|---------------------------------------------|--------------------------------------|--------------------|
| GET     | `/reviews/film/{filmId}`                    | Liste toutes les critiques d’un film | Public             |
| GET     | `/reviews/user/{userId}`                    | Liste les critiques d’un utilisateur | Public             |
| GET     | `/reviews/top`                              | Liste les 10 critiques les plus likées | Public          |
| POST    | `/reviews/film/{filmId}/user/{userId}`      | Création d’une critique              | Auth (auteur)      |
| PUT     | `/reviews/{reviewId}/user/{userId}`         | Mise à jour d’une critique           | Auth (owner/admin) |
| DELETE  | `/reviews/{reviewId}/user/{userId}`         | Suppression d’une critique           | Auth (owner/admin) |

---

#### Détails des méthodes

##### 🟦 getReviewsByFilm (public)

- **Description :** récupère la liste des critiques d’un film.
- **Entrées :** `filmId`
- **Réponse :** `List<ReviewResponseDtoProfil>`
- **Erreurs possibles :** 404 si film introuvable.

---

##### 🟩 createReview (authentifié)

- **Description :** crée une critique pour un film (une seule par utilisateur).
- **Entrées :** `filmId`, `userId`, `ReviewCreateDto`
- **Réponse :** `201 Created`
- **Erreurs possibles :** 400, 401, 403

---

##### 🟧 updateReview (owner/admin)

- **Description :** modifie une critique existante.
- **Entrées :** `reviewId`, `userId`, `ReviewUpdateDto`
- **Réponse :** `ReviewResponseDtoProfil`
- **Erreurs possibles :** 403, 404

---

##### 🟥 deleteReview (owner/admin)

- **Description :** supprime une critique.
- **Entrées :** `reviewId`, `userId`
- **Réponse :** `204 No Content`
-

##### 🟦 getReviewsByUser (public)

- **Description :** retourne toutes les critiques écrites par un utilisateur donné.
- **Entrées :**
  - `userId` — identifiant de l’utilisateur (path)
- **Réponse :** `List<ReviewResponseDtoProfil>`
- **Comportement :**
  - vérifie que l'utilisateur existe,
  - récupère toutes les critiques associées (`reviewRepo.findByUserId(userId)`),
  - convertit chaque entité via `ReviewMapper.toResponse`.
- **Erreurs possibles :**
  - **404 Not Found** — utilisateur introuvable.

##### 🟦 getTopReviews (public)

- **Description :** retourne les **10 critiques les plus likées** de la plateforme.
- **Entrées :** aucune.
- **Réponse :** `List<ReviewResponseDtoProfil>` — maximum 10 éléments.
- **Comportement :**

  - trie toutes les critiques par `likesCount` décroissant,
  - limite le résultat à **10**,
  - renvoie les informations complètes via `ReviewMapper.toResponse`  
    (auteur, film, titre, contenu, note, compteurs like/dislike).

- **Erreurs possibles :** aucune (renvoie une liste vide si aucune critique).

---

<a id="ctrl-reviewlike"></a>

### <h3 style="color:#d3b6ff;">ReviewLikeController</h3>

**Rôle :** gère les likes/dislikes sur les critiques.  
Un utilisateur peut like **ou** dislike une critique (jamais les deux).  
Le système remplace automatiquement l’ancienne réaction.

---

| Méthode | Chemin                                           | Description                      | Accès |
| ------- | ------------------------------------------------ | -------------------------------- | ----- |
| PUT     | `/reviews/likes/toggle/{reviewId}`               | Like / Dislike une critique      | Auth  |
| GET     | `/reviews/likes/status/{reviewId}/user/{userId}` | Statut like/dislike pour un user | Auth  |

---

#### Endpoints principaux

| Méthode | Chemin                                           | Description                  | Accès |
| ------- | ------------------------------------------------ | ---------------------------- | ----- |
| PUT     | `/reviews/likes/toggle/{reviewId}`               | Like / Dislike une critique  | Auth  |
| GET     | `/reviews/likes/status/{reviewId}/user/{userId}` | Vérifie la réaction actuelle | Auth  |

#### 🟦 toggleLike (authentifié)

- **Description :** applique la logique complète LIKE / DISLIKE / UNLIKE.
- **Entrée (body JSON)** :
  ```json
  {
    "userId": 7,
    "liked": true
  }
  ```
- **Sortie (200 OK)** :
  ```json
  {
    "reviewId": 10,
    "userId": 7,
    "liked": true,
    "likesCount": 1,
    "dislikesCount": 0
  }
  ```

#### 🟩 getStatus (authentifié)

- **Description :** récupère le statut like/dislike actuel d’un utilisateur pour une critique.
- **Retour :**
  ```json
  {
    "reviewId": 10,
    "userId": 7,
    "liked": true
  }
  ```

<a id="ctrl-comment"></a>

### <h3 style="color:#d3b6ff;">CommentController</h3>

**Rôle :** gère les commentaires liés à une critique :  
lecture, création, modification, suppression.

---

#### Endpoints principaux

| Méthode | Chemin                                      | Description                  | Accès              |
| ------- | ------------------------------------------- | ---------------------------- | ------------------ |
| GET     | `/comments/review/{reviewId}`               | Liste des commentaires       | Public             |
| POST    | `/comments/review/{reviewId}/user/{userId}` | Ajout d’un commentaire       | Auth (auteur)      |
| PUT     | `/comments/{commentId}/user/{userId}`       | Mise à jour d’un commentaire | Auth (owner/admin) |
| DELETE  | `/comments/{commentId}/user/{userId}`       | Suppression d’un commentaire | Auth (owner/admin) |

---

#### Détails des méthodes

##### 🟦 getCommentsByReview (public)

- **Description :** retourne tous les commentaires d’une critique.
- **Entrées :** `reviewId`
- **Réponse :** `List<CommentResponseDto>`

---

##### 🟩 createComment (authentifié)

- **Description :** crée un commentaire sur une critique.
- **Entrées :** `reviewId`, `userId`, `CommentCreateDto`
- **Réponse :** `201 Created`

---

##### 🟧 updateComment (owner/admin)

- **Description :** modifie un commentaire existant.
- **Entrées :** `commentId`, `userId`, `CommentUpdateDto`
- **Réponse :** `CommentResponseDto`

---

##### 🟥 deleteComment (owner/admin)

- **Description :** supprime un commentaire via son ID.
- **Entrées :** `commentId`, `userId`
- **Réponse :** `204 No Content`

---
### <h3 style="color:#d3b6ff;">ReportController</h3>

**Rôle :** gère les signalements effectués par les utilisateurs, ainsi que leur traitement par l’administrateur.  
Permet la création d’un signalement, la consultation globale et le traitement (avec éventuel avertissement).

---

#### Endpoints principaux

| Méthode | Chemin                          | Description                               | Accès      |
| ------- | -------------------------------- | ------------------------------------------- | ---------- |
| POST    | `/reports/create`                | Créer un signalement                       | User       |
| GET     | `/reports`                       | Obtenir la liste de tous les signalements  | Admin      |
| POST    | `/reports/{id}/process`          | Traiter un signalement + avertir l’utilisateur | Admin |

---

#### Détails des méthodes

##### 🟦 create (user)

- **Description :** permet à un utilisateur de signaler un autre utilisateur.
- **Entrées :**
    - `reporterId` : id de l’utilisateur qui signale
    - `reportedId` : id de l’utilisateur signalé
    - `message` : texte du signalement
- **Réponse :** `ReportDto` (signalement créé)
- **Erreurs possibles :**
    - utilisateur introuvable
    - message vide

---

##### 🟩 getAll (admin)

- **Description :** liste tous les signalements effectués dans l’application.
- **Réponse :** `List<ReportDto>`
- **Accès :** réservé aux administrateurs.

---

##### 🟧 process (admin)

- **Description :** traite un signalement :
    - Ajout d’un avertissement **optionnel** (`?warning=true`)
    - Assignation de l’admin qui a traité la demande
    - Marque le signalement comme *traité*
    - Si 3 avertissements → **l’utilisateur est automatiquement bloqué (trigger SQL)**

- **Entrées :**
    - `id` : identifiant du signalement
    - `adminId` : administrateur qui traite
    - `warning` : `true` ou `false` (défaut : `false`)
- **Réponse :** `ReportDto` mis à jour

---
<a id="services"></a>

## <h2 style="color:#b57bff;">SERVICES</h2>

Les services encapsulent toute la logique métier et constituent le cœur fonctionnel de l’application.  
Ils appliquent les règles, validations, contraintes d’intégrité et orchestrent les interactions entre repositories, DTO, mappers et sécurité.

---

### <h3 style="color:#d3b6ff;">AdminUserService</h3>

**Rôle :** regroupe toutes les opérations d’administration des utilisateurs, accessibles uniquement aux administrateurs (`ADMIN`).

#### Méthodes principales :

- **getAllUsers**  
  Retourne tous les utilisateurs, convertis en `UserResponse`.

- **updateRole**  
  Met à jour le rôle d’un utilisateur, avec garde-fous :

  - impossibilité pour un admin de retirer _son propre_ rôle,
  - validation du rôle cible.

- **updateUser**  
  Met à jour des informations de profil _non sensibles_ (avatar, avertissements, blocage…).  
  Le rôle n’est pas modifié ici.

- **deleteUser**  
  Supprime un utilisateur.  
  La suppression d’un administrateur est bloquée pour éviter une perte totale d'accès.

---

### <h3 style="color:#d3b6ff;">AuthService</h3>

**Rôle :** gère l’inscription, l’authentification et la vérification OTP.  
Il génère également les JWT signés (id + email + rôle).

#### Méthodes principales :

- **register**

  - Le rôle `USER` est imposé.
  - Le mot de passe est hashé via `UserService`.
  - Un OTP est généré et envoyé par email.

- **login**

  - Vérifie les identifiants via `AuthenticationManager`.
  - Vérifie que l’email a été validé.
  - Génère le JWT contenant les informations essentielles.

- **verifyEmail**  
  Vérifie un code OTP à **6 chiffres**, active le compte si la vérification est réussie.

---

### <h3 style="color:#d3b6ff;">FilmService</h3>

**Rôle :** centralise toute la logique métier liée aux films.  
S’appuie sur `FilmMapper` pour les conversions et sur le repository pour les accès BDD.

#### Méthodes principales :

- **getAllFilms**  
  Récupère tous les films (souvent triés par date de sortie descendante) et renvoie `FilmResponseDto`.

- **getFilmById**  
  Renvoie le film correspondant ; génère une exception si introuvable.

- **getShortFilms**  
  Retourne une liste allégée des films (`id`, `titre`, `afficheUrl`, `posterUrl`) — utilisée pour la sélection de fonds dans le profil utilisateur.

- **addFilm**  
  Crée un nouveau film depuis un DTO de création.

- **updateFilm**  
  Met à jour un film existant (mise à jour partielle ou complète).

- **deleteFilm**  
  Supprime un film existant ; exception si le film n’existe pas.

- **updateFilmRating**  
  Recalcule la note moyenne depuis les critiques associées, déclenché lors de l’ajout/modification/suppression d’une review.

---

### <h3 style="color:#d3b6ff;">EmailService</h3>

**Rôle :** responsable de l’envoi des e-mails HTML, notamment pour la vérification de compte (OTP).

#### Méthodes principales :

- **sendVerificationEmail**  
  Envoie un e-mail HTML contenant le code OTP.
  - Génère un contenu HTML personnalisé (nom + code).
  - Utilise `JavaMailSender` + `MimeMessageHelper`.
  - Gère thème, mise en forme et expiration du code.
  - Envoie l’e-mail au destinataire.
  - Lance une exception en cas d’erreur d’envoi.

---

### <h3 style="color:#d3b6ff;">GenreService</h3>

**Rôle :** applique la logique métier spécifique aux genres cinématographiques : validation, contrôle d’unicité, conversion entité ↔ DTO.

#### Méthodes principales :

- **getAllGenres**  
  Renvoie la liste des genres sous forme de `GenreResponseDto`.

- **getGenreById**  
  Renvoie un genre précis ; déclenche une `EntityNotFoundException` si absent.

- **addGenre**  
  Crée un genre après vérification d’unicité ; `IllegalArgumentException` en cas de doublon.

- **updateGenre**  
  Met à jour un genre existant (contrôle d’existence + unicité + mapping).

- **deleteGenre**  
  Supprime un genre ; erreur explicite si non trouvé.

---

### <h3 style="color:#d3b6ff;">UserService</h3>

**Rôle :** gestion principale des utilisateurs, hors opérations d’administration.

#### Méthodes principales :

- **registerUser**

  - Vérifie l’unicité de l’email.
  - Hash le mot de passe.
  - Initialise le rôle par défaut.
  - Génère un OTP et l’envoie par email.

- **verifyEmail**  
  Vérifie un OTP (durée limitée) et active le compte.

- **getUserProfile**  
  Renvoie les informations publiques d’un utilisateur sous forme de `UserResponse`.

- **updateSelf**  
  Met à jour les informations du compte connecté (avatar, etc.) via l’email issu du JWT.

- **deleteByEmail**  
  Supprime définitivement le compte connecté.

- **getFavoriteFilms**  
  Renvoie un DTO regroupant l’id utilisateur et ses favoris.

- **addFavoriteFilm / removeFavoriteFilm**  
  Modifie la liste des favoris du compte connecté.

- **getUserIdFromEmail**  
  Traduction email → identifiant utilisateur (extrait du JWT).

- **loadUserByUsername**  
  Implémentation de `UserDetailsService` (email utilisé comme identifiant unique pour Spring Security).
- **getRecommendedFilms**
  Analyse les genres des films favoris de l’utilisateur et renvoie une
  sélection de films recommandés (excluant ceux déjà dans les favoris).
  Utilise `FilmRepo.findByGenres_IdIn()` et `FilmMapper.toResponse`.

---

### <h3 style="color:#d3b6ff;">ActorService</h3>

**Rôle :** gestion des acteurs, de leurs données et des relations ManyToMany Acteur ↔ Film.

#### Méthodes principales :

- **getAllActors**  
  Renvoie la liste complète des acteurs (`ActorResponseDto`).

- **getActorById**  
  Récupère un acteur par identifiant ; exception si introuvable.

- **addActor**  
  Vérifie l’unicité (nom + prénom), crée un acteur depuis `ActorCreateDto`.

- **updateActor**  
  Met à jour un acteur existant (application des champs non nuls via mapper).

- **deleteActor**  
  Supprime un acteur s’il existe ; exception sinon.

- **addFilmToActor**  
  Associe un film à un acteur (relation ManyToMany) et met à jour les deux entités.

- **removeFilmFromActor**  
  Retire la relation film ↔ acteur et sauvegarde les entités modifiées.

- **getFilmsByActor**  
  Renvoie tous les films d’un acteur sous forme de `FilmShortDto` (id, titre, affiche).

- **getActorsByFilmId**  
  Renvoie tous les acteurs associés à un film sous forme de `ActorResponseDto`.

---

### <h3 style="color:#d3b6ff;">ReviewService</h3>

**Rôle :** gère toute la logique métier liée aux critiques : création, mise à jour, suppression, récupération par film.  
Applique les règles métier : une critique par film et par utilisateur, validation des entités film/user, et mapping DTO via `ReviewMapper`.

#### Méthodes principales :

- **getReviewsByFilm**  
  Récupère toutes les critiques associées à un film (`filmId`).  
  → Renvoie une liste de `ReviewResponseDtoProfil`.

- **createReview**  
  Crée une critique pour un film donné.

  - Vérifie que le film existe.
  - Vérifie que l’utilisateur existe.
  - Empêche la création si l’utilisateur a déjà publié une critique pour ce film.  
    → Retourne `ReviewResponseDtoProfil`.
- **updateReview**
Met à jour une critique existante (titre, contenu, note).
  - Vérifie l’existence de la critique.
  - Applique les modifications via ReviewMapper.updateEntity.
  - Sauvegarde puis recalcule la nouvelle moyenne du film.
    → Retourne la critique modifiée.
- **deleteReview**
  Supprime une critique via son identifiant.
  - Vérifie que la critique existe.
  - Supprime la critique.
  - Recalcule la moyenne du film :
    -	0 critique → moyenne = baseRating
    -	Sinon → (baseRating + somme notes) / (nombre de critiques + 1)
    → Retourne 204 No Content.
- **getTopReviews**
  - Retourne les 10 critiques avec le plus de likes.
  - Utilise findTop10ByOrderByLikesCountDesc() pour optimiser.
    → Renvoie List<ReviewResponseDtoProfil>.
- **getReviewsByUser**
  - Vérifie que l’utilisateur existe.
  - Convertit en DTO.
    → Renvoie List<ReviewResponseDtoProfil>.
- **calculateRating (méthode interne)**
Effectue tout le recalcul de la note moyenne d’un film.
  -	Utilise la note baseRating définie par l’administrateur.
  -	Aucun cas spécial pour update/delete → formule unique :
  (baseRating + somme des notes existantes) / (nombre de critiques + 1)
  -	Si aucune critique :
  → moyenne = baseRating.


### <h3 style="color:#d3b6ff;">ReviewLikeService</h3>

**Rôle :** gère le système de réactions LIKE / DISLIKE sur les critiques.  
Un utilisateur peut soit **liker**, soit **disliker**, soit retirer sa réaction.  
Le service met automatiquement à jour les compteurs `likesCount` et `dislikesCount`.

#### Méthodes principales :

- **toggleLike**

  - Récupère la réaction existante (ReviewLike) via un `Optional` (user + review).
  - Vérifie l’unicité : un utilisateur ne peut avoir qu’une seule réaction par critique.
  - Si aucune réaction n’existe, crée un nouveau like/dislike.
  - Si la réaction existe et est identique à la demande, la retire (toggle off).
  - Si la réaction existe mais diffère (like ↔ dislike), la met à jour (switch).
  - Met à jour les compteurs via `updateCounts`.
  - Retourne la réaction sous forme de DTO via le mapper.

- **updateCounts**  
  Met à jour de façon synchrone les compteurs `likesCount` et `dislikesCount` de la critique associée,  
  en s’assurant qu’aucun compteur ne devient négatif.

---

### <h3 style="color:#d3b6ff;">CommentService</h3>

**Rôle :** gère les commentaires associés aux critiques : lecture, création, modification, suppression.  
Inclut un contrôle d’accès : un commentaire ne peut être modifié ou supprimé que par **son auteur** ou par un **administrateur**.

#### Méthodes principales :

- **getCommentsByReview**  
  Récupère tous les commentaires liés à une critique (`reviewId`).  
  → Renvoie une liste de `CommentResponseDto`.

- **create**  
  Crée un nouveau commentaire.

  - Vérifie que la critique existe.
  - Vérifie que l’utilisateur existe.
  - Transforme le DTO en entité via `CommentMapper.toEntity`.  
    → Retourne `CommentResponseDto`.

- **update**  
  Met à jour un commentaire existant.

  - Vérifie l’existence du commentaire.
  - Vérifie l’autorisation (owner ou admin).
  - Applique les modifications via `CommentMapper.updateEntity`.  
    → Retourne `CommentResponseDto`.

- **delete**  
  Supprime un commentaire.
  - Vérifie l’existence.
  - Vérifie que l’utilisateur est propriétaire OU admin.  
    → Retourne **204 No Content**.

---
### <h3 style="color:#d3b6ff;">ReportService</h3>

**Rôle :** gère tout le système de signalement d’utilisateurs, depuis la création d’un signalement jusqu’à son traitement par un administrateur.
Inclut l’ajout d’avertissements et le blocage automatique d’un utilisateur après 3 avertissements (via trigger SQL).

#### Méthodes principales :
- **createReport**
  Crée un signalement entre deux utilisateurs.
  - Vérifie l’existence du reporter et du reportedUser.
  - Enregistre le message et la date de création.
  - Retourne un ReportDto.
- **getAllReports**
  - Retourne la liste complète des signalements.
  - Utilisé dans le panel d’administration.
  - Retour : List<ReportDto>.
- **processReport**
Traite un signalement par un administrateur.
  - Vérifie l’existence du report et de l’admin.
  - Marque le signalement comme traité.
  - Si warning = true, ajoute un avertissement à l’utilisateur signalé.
  - Si l’utilisateur atteint 3 avertissements, le trigger SQL le passe en blocked = true.
  - Retourne un ReportDto mis à jour.
  <a id="mappers"></a>

## <h2 style="color:#b57bff;">MAPPERS</h2>

Les mappers assurent la conversion entre les **entités JPA** et les **DTO** utilisés par l’API.  
Ils garantissent la séparation stricte entre la couche de persistance et la couche d’exposition.

---

### <h3 style="color:#d3b6ff;">ActorMapper</h3>

**Rôle :** conversion entre l’entité `Actor` et ses DTO (`ActorResponseDto`, DTO de création/mise à jour).

#### Méthodes principales :

- **toDto**  
  Convertit un `Actor` vers `ActorResponseDto` (données publiques : id, nom, prénom, bio, avatar).

- **fromCreateDto**  
  Transforme un `ActorCreateDto` en entité `Actor` prête à être persistée.

- **applyUpdate**  
  Met à jour un acteur existant en appliquant uniquement les champs **non nuls** de `ActorUpdateDto`.

---

### <h3 style="color:#d3b6ff;">FilmMapper</h3>

**Rôle :** conversion entre l’entité `Film` et les différents DTO associés.

#### Méthodes principales :

- **toLightResponse**  
  Convertit un `Film` vers une version légère `FilmResponse`  
  → utilisée pour les listes, aperçus rapides ou affichages optimisés.

- **toResponse**  
  Convertit un `Film` vers un `FilmResponseDto` complet  
  → utilisé pour la page détail d’un film (inclut informations étendues).

- **toEntity**  
  Transforme un DTO de création en entité `Film` prête à être persistée.

- **updateEntity**  
  Applique un `FilmUpdateDto` sur une entité existante.  
  → Seuls les champs **non nuls** du DTO sont mis à jour (PATCH-like behavior).

---

### <h3 style="color:#d3b6ff;">GenreMapper</h3>

**Rôle :** conversion entre l’entité `Genre` et ses représentations DTO.

#### Méthodes principales :

- **toResponse**  
  Convertit un `Genre` vers un `GenreResponseDto`, adapté à l’exposition publique.

- **toEntity**  
  Transforme un `GenreRequestDto` (création) en une entité `Genre`.  
  → L’identifiant n’est pas transmis et sera généré par la base.

- **updateEntity**  
  Met à jour une entité `Genre` existante depuis un `GenreRequestDto`.  
  → Seuls les champs autorisés sont modifiés pour préserver l’intégrité métier.

---

### <h3 style="color:#d3b6ff;">UserMapper</h3>

**Rôle :** conversion entre l’entité `User` et ses différents DTO (`UserResponse`, `UserProfileResponse`), y compris le mapping des favoris et des reviews.

#### Méthodes principales :

- **toResponse**  
  Convertit un `User` vers `UserResponse`.  
  → utilisé pour l’affichage standard (id, username, email, avatar, rôle, statut…).

- **toProfile**  
  Convertit un `User` vers `UserProfileResponse` (version enrichie).  
  → inclut :
  - liste des films favoris (`FilmResponse`),
  - liste des critiques de l’utilisateur (`ReviewResponse`),
  - informations complètes de profil.

---

### <h3 style="color:#d3b6ff;">ReviewMapper</h3>

**Rôle :** conversion entre l’entité `Review` et ses DTO (`ReviewResponseDtoProfil`, `ReviewCreateDto`, `ReviewUpdateDto`).  
Il gère également l’association entre critique, film et utilisateur.

#### Méthodes principales :

- **toResponse**  
  Convertit une entité `Review` vers un `ReviewResponseDtoProfil`.  
  → Inclut :
    - id de la critique,
    - titre,
    - contenu,
    - note attribuée,
    - date de création,
    - informations sur le film (id, titre, affiche),
    - informations sur l’auteur (id, username, avatar),
    - nombre de likes et dislikes.

- **toEntity**  
  Transforme un `ReviewCreateDto` en entité `Review`.  
  → Associe directement la critique au `Film` et à l’utilisateur (`User`).  
  → Initialise le titre, contenu et rating.

- **updateEntity**  
  Met à jour une critique existante depuis un `ReviewUpdateDto`.  
  → Met à jour le titre, le contenu et la note.  
  → Utilisé lors de l’édition d’une critique.
---

### <h3 style="color:#d3b6ff;">CommentMapper</h3>

**Rôle :** conversion entre l’entité `Comment` et ses DTO, avec mappage de l’auteur et du contenu.

#### Méthodes principales :

- **toResponse**  
  Convertit un `Comment` vers `CommentResponseDto`.  
  → Inclut :

  - contenu du commentaire,
  - date de création,
  - informations de l’auteur (id, username, avatar).

- **toEntity**  
  Transforme un `CommentCreateDto` en entité `Comment`.  
  → Associe directement le commentaire à la `Review` et au `User`.

- **updateEntity**  
  Met à jour le contenu d’un commentaire existant.  
  → Utilisé lors de l’édition d’un commentaire.

---

### <h3 style="color:#d3b6ff;">ReviewLikeMapper</h3>

**Rôle :** conversion entre l'entité `ReviewLike` et `ReviewLikeResponseDto`.

#### Méthodes principales :

- **toDto**  
  Convertit un `ReviewLike` et la `Review` associée en `ReviewLikeResponseDto`.
  → Inclut :
  - reviewId
  - userId
  - liked (true = like, false = dislike)
  - likesCount
  - dislikesCount

---
### <h3 style="color:#d3b6ff;">ReportMapper</h3>

**Rôle :** conversion entre l’entité `Report` et le DTO `ReportDto`.  
Il expose les informations nécessaires pour l’administration des signalements : auteur, utilisateur signalé, statut, message et traitement.

#### Méthodes principales :

- **toDto**  
  Convertit un `Report` vers `ReportDto`.  
  → Inclut :

    - `id` du signalement
    - `reporterId` (utilisateur qui signale)
    - `reportedUserId` (utilisateur signalé)
    - `message` rédigé par le plaignant
    - `createdAt` (date du signalement)
    - `processed` (true = traité, false = en attente)
    - `adminId` (id de l’administrateur ayant traité le signalement, ou `null` si non traité)
<a id="security-components"></a>

## <h2 style="color:#b57bff;">SECURITY</h2>

L’architecture de sécurité repose sur **Spring Security**, un filtre **JWT stateless** et des points d’entrée personnalisés.  
Chaque composant a un rôle précis dans la chaîne de protection.

---

### 🟥 AuthEntryPointJwt

**Rôle :** point d’entrée déclenché lorsqu’un utilisateur tente d’accéder à une ressource protégée sans être authentifié.

- Retourne systématiquement une réponse **HTTP 401 (Unauthorized)** au format **JSON**.
- Empêche Spring de générer une page HTML par défaut.
- Utilisé notamment par le filtre JWT lors d’un token invalide ou manquant.

---

### 🟦 AuthTokenFilter

**Rôle :** filtre JWT exécuté à **chaque requête**.

Fonctionnement :

1. Récupère le token depuis l’en-tête `Authorization: Bearer <jwt>`.
2. Vérifie la validité et l’intégrité du token via `JwtUtil`.
3. Extrait le `subject` (email).
4. Charge le `UserDetails` correspondant.
5. Renseigne le **SecurityContext**, permettant à Spring d’identifier l’utilisateur.

→ Ne s’applique **pas** aux routes publiques d’authentification : `/auth/login`, `/auth/register`, `/auth/verify`.

---

### 🟩 JwtUtil

**Rôle :** utilitaire central de gestion des JWT.

- Génère des tokens signés (HMAC).
- Valide les tokens reçus (signature, expiration, structure).
- Expose des méthodes d’extraction (ex. `getUserFromToken()`).

**Claims intégrés dans chaque JWT :**

| Claim  | Description                      |
| ------ | -------------------------------- |
| `sub`  | Email de l’utilisateur (subject) |
| `id`   | Identifiant utilisateur          |
| `role` | `USER` ou `ADMIN`                |

---

### 🟨 RateLimitFilter

**Rôle :** protège l’endpoint de connexion contre les attaques par bruteforce.

- S’applique uniquement sur :  
  `POST /api/auth/login`
- Limite chaque IP à **5 tentatives / 10 minutes**.
- Renvoie **HTTP 429 Too Many Requests** en cas de dépassement.
- Indépendant du système JWT (intervient avant l’authentification).

---

### WebSecurityConfig

**Rôle :** configuration centrale de la sécurité Spring.

Inclut :

- Définition de la `SecurityFilterChain` (règles d’accès par endpoint et par rôle).
- Configuration **CORS** et désactivation de **CSRF** (API stateless).
- Injection des filtres :
  - `AuthTokenFilter`
  - `RateLimitFilter`
- Utilitaires exposés :
  - `PasswordEncoder` (BCrypt)
  - `AuthenticationManager` pour la logique d’authentification.

<a id="annexes-notes"></a>

## 🗂️ Annexes et notes

### 📘 Codes HTTP usuels

- **200 OK** — requête traitée avec succès.
- **201 Created** — ressource créée avec succès.
- **204 No Content** — action réussie sans contenu de réponse (ex : suppression).
- **400 Bad Request** — données invalides, erreur de validation ou format incorrect.
- **401 Unauthorized** — utilisateur non authentifié, token invalide ou expiré.
- **403 Forbidden** — authentifié mais pas autorisé (rôle insuffisant).
- **404 Not Found** — ressource introuvable.
- **409 Conflict** — violation d’unicité (ex : email déjà utilisé).
- **429 Too Many Requests** — limite de tentatives dépassée (rate limit).

---

### 🧭 Bonnes pratiques

- **Ne jamais exposer les entités JPA** directement via l’API :  
  toujours utiliser des **DTO** pour éviter les fuites de données, les cycles JSON et préserver la structure interne.

- **Valider systématiquement les DTO** avec `@Valid` et les annotations JSR-303.

- **Journaliser les actions sensibles** (connexion, échecs répétés, opérations admin, suppression, modification critique).

- **Documenter les règles métier** dans :
  - les **services** (source of truth),
  - les **tests** (preuves de conformité),
  - et la documentation technique (comme ce fichier).
