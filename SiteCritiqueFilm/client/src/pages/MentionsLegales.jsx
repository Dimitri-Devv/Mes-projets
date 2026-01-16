export const MentionsLegales = () => {

    return (
        <div className="max-w-4xl mx-auto px-4 py-20 text-white space-y-10">
            <h1 className="text-3xl sm:text-4xl font-bold text-center">
                Mentions légales
            </h1>

            {/* Éditeur */}
            <section className="space-y-2">
                <h2 className="text-xl font-semibold text-accentuation">
                    Éditeur du site
                </h2>
                <p>
                    Ce site est édité par <strong>Dimitri Ricquier et Xan Balandrau</strong>, dans le cadre
                    d’un projet personnel / étudiant.
                </p>
                <p>Email de contact : <strong>dimrcq80@hotmail.fr</strong></p>
            </section>

            <section className="space-y-2">
                <h2 className="text-xl font-semibold text-accentuation">
                    Hébergement
                </h2>
                <p>
                    Le site est hébergé par un prestataire tiers qui est Render.
                </p>
            </section>

            <section className="space-y-3">
                <h2 className="text-xl font-semibold text-accentuation">
                    Données personnelles
                </h2>
                <p>
                    MovieLab collecte uniquement les données nécessaires au fonctionnement
                    du site :
                </p>
                <ul className="list-disc list-inside text-gray-300">
                    <li>Pseudo</li>
                    <li>Adresse email</li>
                    <li>Avatar</li>
                    <li>Critiques et interactions publiées par l’utilisateur</li>
                </ul>
                <p>
                    Ces données sont utilisées exclusivement pour :
                </p>
                <ul className="list-disc list-inside text-gray-300">
                    <li>La gestion des comptes utilisateurs</li>
                    <li>La publication de critiques</li>
                    <li>La modération et les signalements</li>
                </ul>
            </section>

            <section className="space-y-3">
                <h2 className="text-xl font-semibold text-accentuation">
                    Authentification & sécurité
                </h2>
                <p>
                    L’authentification est réalisée via un{" "}
                    <strong>token JWT</strong>. Aucun cookie de suivi ou de
                    traçage n’est utilisé.
                </p>
                <p>
                    Le token est stocké côté client et permet uniquement l’accès sécurisé
                    aux fonctionnalités nécessitant une authentification.
                </p>
            </section>

            <section className="space-y-2">
                <h2 className="text-xl font-semibold text-accentuation">
                    Cookies
                </h2>
                <p>
                    Le site MovieLab n’utilise aucun cookie de suivi, de publicité ou
                    d’analyse.
                </p>
                <p>
                    Aucun bandeau de consentement n’est donc requis.
                </p>
            </section>

            <section className="space-y-2">
                <h2 className="text-xl font-semibold text-accentuation">
                    Droits des utilisateurs
                </h2>
                <p>
                    Conformément au Règlement Général sur la Protection des Données (RGPD),
                    chaque utilisateur dispose d’un droit d’accès, de modification et de
                    suppression de ses données personnelles.
                </p>
                <p>
                    Ces actions peuvent être effectuées directement depuis l’espace
                    profil ou en contactant l’éditeur du site.
                </p>
            </section>

            <section className="space-y-2">
                <h2 className="text-xl font-semibold text-accentuation">
                    Responsabilité
                </h2>
                <p>
                    MovieLab est une plateforme de critiques de films. Les contenus
                    publiés par les utilisateurs engagent uniquement leurs auteurs.
                </p>
            </section>
        </div>
    )
}

