import { Star } from "lucide-react";
import { Link } from "react-router";

// --- Composant d'affichage d'une critique de film (carte individuelle) ---
export const CardCritique = ({ critique }) => {
  // Conteneur principal de la carte : fond, ombre, taille fixe et comportement responsive
  return (
    <div className="relative bg-anthracite/60 bg-opacity-30 rounded-md px-4 py-4 flex flex-col overflow-hidden items-center h-full text-white shadow-[0_4px_10px_rgba(0,0,0,0.25)] w-[260px] sm:w-[300px] max-w-full min-h-[180px] sm:min-h-[200px]">
      {/* Grille d'en-tête : avatar (colonne fixe), infos (colonne flexible), note (colonne auto) */}
      <div className="grid grid-cols-[48px_1fr_auto] grid-rows-2 gap-x-2 gap-y-1 w-full items-center overflow-hidden font-bold">
        {/* Avatar de l'utilisateur (ou image par défaut) */}
        <Link to={`/profil/${critique.userId}`} className="row-span-2">
          <img
            src={critique.avatarUrl || "/default-avatar.png"}
            alt={critique.username}
            className="w-12 h-12 rounded-full object-cover border-2 border-accentuation text-xs hover:opacity-80 transition"
          />
        </Link>

        {/* Nom de l'utilisateur qui a publié la critique */}
        <Link
          to={`/profil/${critique.userId}`}
          className="text-sm text-white hover:text-accentuation transition"
        >
          {critique.username}
        </Link>

        {/* Bloc affichant la note du film avec une icône étoile */}
        <div className="flex items-center gap-1 text-xs text-white justify-end min-w-[40px]">
          <Star className="w-4 h-4 fill-current text-accentuation" />
          <span>{critique.rating}</span>
        </div>

        {/* Titre du film (affiché sur maximum 2 lignes) */}
        <span className="text-xs text-accentuation max-w-[180px] sm:max-w-[220px] block leading-tight line-clamp-2">
          {critique.filmTitle}
        </span>
      </div>

      {/* Texte de la critique (limité à 4 lignes pour éviter le débordement) */}
      <div className="col-span-3 mt-2">
        <p className="text-xs leading-snug line-clamp-4">
          "{critique.content}"
        </p>
      </div>

      {/* Bande pellicule décorative en haut et en bas de la carte */}
      <span className="absolute -top-3 left-0 w-full h-3 bg-[url('/filmstrip.svg')] bg-repeat-x"></span>
      <span className="absolute -bottom-3 left-0 w-full h-3 bg-[url('/filmstrip.svg')] bg-repeat-x"></span>
    </div>
  );
};
