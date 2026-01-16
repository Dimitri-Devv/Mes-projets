import { useState } from "react";
import { Link } from "react-router";
import Modal from "react-modal";
import { Swiper, SwiperSlide } from "swiper/react";
import {
  EffectCoverflow,
  Autoplay,
  Navigation,
  A11y,
  Keyboard,
} from "swiper/modules";
import { X, Star, CirclePlay } from "lucide-react";

import "swiper/css";
import "swiper/css/effect-coverflow";
import "swiper/css/autoplay";
import "swiper/css/navigation";

export const Carrousel = ({ films = [] }) => {
  const tendanceFilms = films;

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedFilm, setSelectedFilm] = useState(null);

  const formatGenreName = (name = "") => {
    const norm = String(name).toLowerCase();
    return /science[-\s]?fiction/.test(norm) ? "SF" : name;
  };

  const getGenreNames = (film) => {
    const list = Array.isArray(film?.genres)
      ? film.genres
      : film?.genre
      ? [film.genre]
      : [];

    const names = list
      .map((g) => {
        if (typeof g === "string") return formatGenreName(g);
        if (typeof g === "object") return formatGenreName(g.name);
        return "";
      })
      .filter(Boolean);

    const unique = [...new Set(names)];
    return unique.slice(0, 2);
  };

  // Fonction pour obtenir l'URL d'embed d'une video a partir de l'URL originale - ChatGPT
  function getEmbedUrl(rawUrl) {
    try {
      if (!rawUrl) return "";
      const url = new URL(rawUrl);
      const host = url.hostname.replace(/^www\./, "");

      if (
        host === "youtu.be" ||
        host === "youtube.com" ||
        host === "m.youtube.com"
      ) {
        let videoId = "";
        if (host === "youtu.be") {
          videoId = url.pathname.split("/").filter(Boolean)[0] || "";
        } else if (url.pathname.startsWith("/watch")) {
          videoId = url.searchParams.get("v") || "";
        } else if (url.pathname.startsWith("/shorts/")) {
          videoId = url.pathname.split("/").filter(Boolean)[1] || "";
        } else if (url.pathname.startsWith("/embed/")) {
          videoId = url.pathname.split("/").filter(Boolean)[1] || "";
        }
        if (videoId) {
          const start =
            url.searchParams.get("t") || url.searchParams.get("start");
          const startParam = start
            ? `?start=${parseInt(start, 10) || 0}&rel=0&autoplay=1`
            : "?rel=0&autoplay=1";
          return `https://www.youtube.com/embed/${videoId}${startParam}`;
        }
      }

      if (host.endsWith("vimeo.com")) {
        const parts = url.pathname.split("/").filter(Boolean);
        const id = parts[parts.length - 1];
        if (id) return `https://player.vimeo.com/video/${id}?autoplay=1`;
      }

      return rawUrl;
    } catch {
      return rawUrl || "";
    }
  }

  return (
    <section
      role="region"
      aria-roledescription="carousel"
      aria-label="Films en tendance"
      className="relative m-auto lg:my-20 2xl:w-[85%] overflow-hidden"
    >
      <h2 className="sr-only">Films en tendance</h2>
      {tendanceFilms.length > 0 ? (
        <Swiper
          effect={"coverflow"}
          grabCursor={true}
          centeredSlides={true}
          slidesPerView={"auto"}
          a11y={{ enabled: true }}
          keyboard={{ enabled: true }}
          breakpoints={{
            768: { slidesPerView: 2 },
            1024: { slidesPerView: 3 },
            //   1650: { slidesPerView: 4 },
          }}
          loop={true}
          autoplay={{
            pauseOnMouseEnter: true,
            delay: 3500,
            disableOnInteraction: false,
          }}
          speed={1000}
          navigation={{
            nextEl: ".custom-next",
            prevEl: ".custom-prev",
          }}
          coverflowEffect={{
            rotate: 50,
            stretch: 0,
            depth: 100,
            modifier: 1,
            slideShadows: true,
          }}
          modules={[EffectCoverflow, Autoplay, Navigation, A11y, Keyboard]}
          className="mySwiper"
        >
          {/* Il faut 5 films minimum pour que le carrousel fonctionne  */}
          {tendanceFilms.map((film) => (
            <SwiperSlide key={film.id}>
              <div
                className="h-full w-full bg-cover bg-center bg-no-repeat"
                style={{
                  backgroundImage: `url(${film.posterUrl})`,
                }}
              ></div>

              <div className="absolute bottom-10 left-5 w-[41%] h-[75%] max-w-[135px] p-0">
                <Link to={`/detailFilm/${film.id}`}>
                  <img
                    src={film.afficheUrl}
                    alt={film.title}
                    loading="lazy"
                    className="w-full h-[80%] object-cover"
                  />
                </Link>
                <div className="bg-black/80 dark:bg-black/80 text-white dark:text-white font-bold w-full grid grid-cols-2 grid-rows-2">
                  <div className="px-2 my-2 border-r-2 border-accentuation dark:border-accentuation flex items-center justify-center">
                    <span className="text-xs">
                      {getGenreNames(film)[0] || ""}
                    </span>
                  </div>
                  <div className="px-2 py-1 flex items-center justify-center">
                    <span className="text-xs">
                      {getGenreNames(film)[1] || ""}
                    </span>
                  </div>
                  <div className="px-2 py-1 flex items-center justify-center flex-nowrap">
                    <p className="inline-flex items-center gap-1">
                      <Star
                        className="w-4 h-4 inline-block text-accentuation dark:text-accentuation"
                        fill="currentColor"
                        stroke="none"
                      />
                      {film.ratingAverage != null
                        ? Number(film.ratingAverage).toLocaleString("fr-FR", {
                            minimumFractionDigits: 0,
                            maximumFractionDigits: 1,
                          })
                        : "—"}
                    </p>
                  </div>
                  <div className="px-1 py-1 flex items-center justify-center flex-nowrap text-black dark:text-black">
                    <button
                      aria-label={`Voir la bande-annonce de ${film.title}`}
                      onClick={() => {
                        setSelectedFilm(film);
                        setIsModalOpen(true);
                      }}
                      className="inline-flex text-xs sm:gap-0.5 items-center bg-accentuation dark:bg-accentuation py-0.5 px-1 rounded-full hover:bg-accentuation/80 dark:hover:bg-accentuation/80 font-semibold cursor-pointer"
                    >
                      <CirclePlay className="w-4 h-4 inline-block" />
                      Trailer
                    </button>
                  </div>
                </div>
              </div>
            </SwiperSlide>
          ))}
        </Swiper>
      ) : (
        <div className="flex justify-center items-center dark:text-white text-black">
          Aucun film en tendance pour le moment
        </div>
      )}

      <Modal
        isOpen={isModalOpen}
        onRequestClose={() => setIsModalOpen(false)}
        contentLabel={
          selectedFilm
            ? `Bande-annonce: ${selectedFilm.title}`
            : "Bande-annonce"
        }
        style={{
          overlay: {
            zIndex: 50,
            backgroundColor: "rgba(0,0,0,0.5)",
            backdropFilter: "blur(2px)",
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
          },
          content: {
            inset: "auto",
            background: "transparent",
            border: "none",
            padding: 0,
            overflow: "visible",
          },
        }}
      >
        <div className="relative w-[90vw] max-w-[960px] h-auto aspect-video rounded-xl overflow-hidden shadow-2xl">
          {selectedFilm?.trailerUrl ? (
            <iframe
              src={getEmbedUrl(selectedFilm.trailerUrl)}
              title={selectedFilm.title}
              className="w-full h-full"
              loading="lazy"
              referrerPolicy="strict-origin-when-cross-origin"
              allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
              allowFullScreen
            />
          ) : (
            <div className="w-full h-full flex flex-col justify-center items-center bg-black/90 dark:bg-black/90">
              <p className="text-white dark:text-white text-center">
                Aucune bande-annonce disponible
              </p>
            </div>
          )}
          <button
            type="button"
            aria-label="Fermer la modale"
            onClick={() => setIsModalOpen(false)}
            className="absolute top-0 right-0 md:top-2 md:right-2 p-2 rounded-full bg-black/80 dark:bg-black/80 text-white dark:text-white hover:bg-black"
          >
            <X />
          </button>
        </div>
      </Modal>
    </section>
  );
};
