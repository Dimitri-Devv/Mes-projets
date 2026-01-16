// --- Composant "Pellicule" (carrousel de critiques) ---
// Affiche une bande horizontale de cartes de critiques défilant automatiquement, façon pellicule de cinéma
import { Swiper, SwiperSlide } from "swiper/react";
import "swiper/css";
import "swiper/css/pagination";
import "../../App.css";
import { Pagination, Autoplay } from "swiper/modules";

import { CardCritique } from "./CardCritique.jsx";

// Conteneur qui centre le carrousel sur la page et limite sa largeur maximale
export const Pellicule = ({ critiques }) => {
  return (
    <div className="w-[90%] lg:w-[80%] xl:w-[70%] mx-auto mt-8">
      {/* Conteneur principal contenant la bande pellicule + le carrousel Swiper */}
      <div className="relative filmstrip bg-accentuation dark:bg-accentuation rounded-sm overflow-hidden">
        {/* Configuration de Swiper : défilement automatique, boucle infinie, largeur des cartes adaptée automatiquement */}
        <Swiper
          slidesPerView="auto" // Les cartes adaptent leur taille automatiquement au lieu d’un nombre fixe par écran
          spaceBetween={20}
          loop={true}
          allowTouchMove={true}
          autoplay={{
            delay: 0,
            disableOnInteraction: false,
            pauseOnMouseEnter: false,
          }} // delay:0 + speed:6000 = défilement continu façon pellicule
          speed={6000}
          modules={[Pagination, Autoplay]}
          className="pellicule-swiper"
        >
          {/* Parcourt toutes les critiques et génère une diapositive par carte */}
          {critiques.map((critique) => (
            // Diapositive Swiper : hauteur 100%, largeur adaptée au contenu de la carte
            <SwiperSlide
              key={critique.id}
              className="!h-full !w-auto flex py-5 border-r-4 border-black"
            >
              {/* Conteneur autour de chaque carte ajoutant la barre verticale de séparation (bord de pellicule) */}
              <div className="relative flex h-full pr-6 after:content-[''] after:absolute after:top-0 after:bottom-0 after:right-0 after:w-[4px] after:bg-black after:opacity-100 last:after:hidden">
                {/* Composant de carte de critique individuelle */}
                <CardCritique critique={critique} />
              </div>
            </SwiperSlide>
          ))}
        </Swiper>
      </div>
    </div>
  );
};
