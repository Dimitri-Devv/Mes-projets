import Modal from "react-modal";
import { useEffect, useState } from "react";
import { useParams } from "react-router";
import { deleteUser, getUserById, updateUser } from "../api/user";
import { CardFilmProfil } from "../components/profil/CardFilmProfil.jsx";
import ReportModal from "../components/report/ReportModal.jsx";
import { Swiper, SwiperSlide } from "swiper/react";
import { Navigation } from "swiper/modules";
import "swiper/css";
import { getFilmById, getFilmsAffiche } from "../api/films";
import { useAuth } from "../context/AuthContext";
import SectionTitle from "../components/SectionTitle.jsx";
import {
  UserRoundCog,
  X,
  UserCircle,
  Image,
  FileText,
  Trash,
  Flag,
} from "lucide-react";
import UserReviewCard from "../components/profil/ReviewProfil.jsx";

export const Profil = () => {
  const { user } = useAuth();
  const { id } = useParams();
  const [userProfil, setUserProfil] = useState(null);
  const [favorites, setFavorites] = useState([]);
  const [reviews, setReviews] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [afficheFilm, setAfficheFilm] = useState([]);
  const [loadingAffiches, setLoadingAffiches] = useState(false);
  const [loading, setLoading] = useState(true);
  const [selectedSection, setSelectedSection] = useState("menu");
  const [isReportOpen, setIsReportOpen] = useState(false);

  const fetchData = async (id) => {
    try {
      setLoading(true);
      const userData = await getUserById(id);
      let coverFilmUrl = null;
      if (userData.coverFilmId) {
        try {
          const film = await getFilmById(userData.coverFilmId);
          coverFilmUrl = film.posterUrl;
        } catch (err) {
          console.warn("Impossible de charger le film de couverture :", err);
        }
      }

      setUserProfil({ ...userData, coverFilmUrl });
      setFavorites(userData.favoriteFilms || []);
      setReviews(userData.reviews || []);
    } catch (error) {
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData(id);
  }, [id]);

  const openModal = async () => {
    setIsModalOpen(true);
    if (afficheFilm.length === 0) {
      try {
        setLoadingAffiches(true);
        const filmsAffiches = await getFilmsAffiche();
        setAfficheFilm(filmsAffiches || []);
      } catch (err) {
        console.error("Erreur chargement affiches :", err);
      } finally {
        setLoadingAffiches(false);
      }
    }
  };

  const handleBack = () => setSelectedSection("menu");

  const avatarOptions = [
    "/assets/avatars/avatar1.webp",
    "/assets/avatars/avatar2.webp",
    "/assets/avatars/avatar3.webp",
    "/assets/avatars/avatar4.webp",
    "/assets/avatars/avatar5.webp",
    "/assets/avatars/avatar6.webp",
    "/assets/avatars/avatar7.webp",
    "/assets/avatars/avatar8.webp",
    "/assets/avatars/avatar9.webp",
    "/assets/avatars/avatar10.webp",
  ];

  if (loading) return <div className="h-screen">Loading...</div>;

  return (
    <div className="pt-16 sm:pt-20">
      <div className="relative w-full h-[260px] md:h-90 lg:h-120 bg-black overflow-hidden">
        <div
          className="absolute inset-0 bg-cover bg-center mx-auto"
          style={{
            backgroundImage: userProfil.coverFilmUrl
              ? `url(${userProfil.coverFilmUrl})`
              : "linear-gradient(to bottom right, #1e3a8a, #9333ea)",
            backgroundSize: "cover",
            backgroundPosition: "center",
            maxWidth: "1250px",
            left: "50%",
            transform: "translateX(-50%)",
            width: "100%",
          }}
        />

        {user && user.id === userProfil.id && (
          <button
            onClick={openModal}
            className="absolute top-4 right-4 bg-black/80 p-2 rounded-full text-white hover:text-violet-400 transition cursor-pointer"
            title="Changer le fond"
          >
            <UserRoundCog className="lg:w-10 lg:h-10" />
          </button>
        )}
        {user && user.id !== userProfil.id && (
          <button
            className="absolute top-4 right-4 sm:right-16 bg-red-600/80 p-2 rounded-full text-white hover:bg-red-700 transition cursor-pointer"
            title="Signaler cet utilisateur"
            onClick={() => setIsReportOpen(true)}
          >
            <Flag className="lg:w-9 lg:h-9" />
          </button>
        )}
      </div>

      <div className="relative text-white pb-10">
        <div className="relative -mt-16 sm:-mt-14 px-4">
          <div className="max-w-6xl mx-15 flex flex-col sm:flex-row items-center sm:items-start gap-6 sm:gap-8 sm:justify-start sm:pl-12">

            {/* Avatar + username */}
            <div className="flex flex-col items-center sm:items-center shrink-0 w-full sm:w-auto">
              <div className="w-28 h-28 sm:w-32 sm:h-32 lg:w-40 lg:h-40 rounded-full bg-white border-4 border-gray-900 overflow-hidden shadow-2xl mx-auto">
                <img
                  src={
                    userProfil.avatarUrl && userProfil.avatarUrl.trim() !== ""
                      ? userProfil.avatarUrl
                      : "/assets/avatars/user-placeholder.jpg"
                  }
                  alt={userProfil.username}
                  className="w-full h-full object-cover"
                />
              </div>

              <h2 className="mt-4 text-lg sm:text-xl lg:text-2xl font-bold uppercase text-center text-white">
                {userProfil.username}
              </h2>
            </div>

            {/* Bio */}
            {userProfil.bio && (
              <div className="w-full sm:max-w-2xl lg:max-w-3xl mt-6 sm:mt-18 sm:ml-6">
                <p className="text-black italic text-sm sm:text-base lg:text-lg leading-relaxed bg-white p-4 sm:p-5 lg:p-6 rounded-md shadow text-center sm:text-left">
                  « {userProfil.bio} »
                </p>
              </div>
            )}
          </div>
        </div>
      </div>

      <section className="mt-6 sm:mt-8 px-4">
        <SectionTitle
            title="WATCHLIST"
            subtitle="Tous les films que tu as vu !"
        />

        {favorites.length === 0 ? (
          <p className="text-gray-500">Aucun film favori pour le moment.</p>
        ) : (
          <Swiper
            modules={[Navigation]}
            spaceBetween={16}
            centeredSlides={favorites.length < 5}
            wrapperClass="justify-center"
            slidesPerView={8}
            breakpoints={{
              1636: { slidesPerView: 8 },
              1280: { slidesPerView: 6 },
              1024: { slidesPerView: 5 },
              768: { slidesPerView: 4 },
              480: { slidesPerView: 3 },
              0: { slidesPerView: 2 },
            }}
            className="mySwiper"
          >
            {favorites.map((film) => (
              <SwiperSlide key={film.id} className="flex justify-center">
                <CardFilmProfil film={film} />
              </SwiperSlide>
            ))}
          </Swiper>
        )}

        <section className="mt-6 sm:mt-8 px-4 flex justify-center">
          <div className="w-full max-w-5xl">
            <SectionTitle
                title="Mes critiques"
                subtitle="Toutes les critiques de films que tu as publié"
            />

            {reviews.length === 0 ? (
              <p className="text-gray-500">Aucune critique pour le moment.</p>
            ) : (
              <div className="max-h-[70vh] overflow-y-auto pr-2 space-y-4 custom-scrollbar mt-2">
                {reviews.map((review) => (
                  <UserReviewCard key={review.id} review={review} />
                ))}
              </div>
            )}
          </div>
        </section>
      </section>

      <Modal
        isOpen={isModalOpen}
        onRequestClose={() => {
          setIsModalOpen(false);
          setSelectedSection("menu");
        }}
        className="bg-white dark:bg-gray-900 p-6 rounded-xl w-[90%] max-w-xl max-h-[90vh] overflow-y-auto shadow-xl outline-none"
        overlayClassName="fixed inset-0 bg-black/60 flex justify-center items-center z-50"
      >
        <div className="flex justify-end">
          <button
            onClick={() => setIsModalOpen(false)}
            className="px-4 py-2 cursor-pointer rounded-lg  text-black dark:text-white"
          >
            <X className="w-6 h-6" />
          </button>
        </div>

        {selectedSection === "menu" && (
          <>
            <h2 className="text-2xl font-bold mb-6 text-black dark:text-white text-center">
              Personnaliser mon profil
            </h2>

            <div className="flex flex-col gap-4 sm:w-[80%] lg:w-[70%] mx-auto">
              <button
                onClick={() => setSelectedSection("avatar")}
                className="flex items-center justify-center bg-gray-300 dark:bg-gray-700 hover:bg-gray-400 text-black dark:text-white font-semibold py-3 rounded-lg transition cursor-pointer"
              >
                <div className="flex items-center w-3/4 gap-3">
                  <UserCircle className="w-6 h-6" />
                  Changer mon avatar
                </div>
              </button>

              <button
                onClick={() => setSelectedSection("poster")}
                className="flex items-center justify-center bg-gray-300 dark:bg-gray-700 hover:bg-gray-400 text-black dark:text-white font-semibold py-3 rounded-lg transition cursor-pointer"
              >
                <div className="flex items-center w-3/4 gap-3 ">
                  <Image className="w-6 h-6" />
                  Choisir un poster de film
                </div>
              </button>

              <button
                onClick={() => setSelectedSection("bio")}
                className="flex items-center justify-center bg-gray-300 dark:bg-gray-700 hover:bg-gray-400 text-black dark:text-white font-semibold py-3 rounded-lg transition cursor-pointer"
              >
                <div className="flex items-center w-3/4 gap-3 ">
                  <FileText className="w-6 h-6" />
                  Modifier ma bio
                </div>
              </button>

              <button
                onClick={() => setSelectedSection("delete")}
                className="flex items-center justify-center bg-red-600 hover:bg-red-700 text-black dark:text-white font-semibold py-3 rounded-lg transition cursor-pointer"
              >
                <div className="flex items-center w-3/4 gap-3 ">
                  <Trash className="w-6 h-6" />
                  Supprimer mon compte
                </div>
              </button>
            </div>
          </>
        )}

        {selectedSection === "avatar" && (
          <section>
            <button
              onClick={handleBack}
              className="text-accentuation mb-3 cursor-pointer"
            >
              ← Retour
            </button>
            <h3 className="text-lg text-center font-bold mb-3 text-black dark:text-white">
              CHANGER L'AVATAR
            </h3>

            <div className="flex flex-col items-center justify-center gap-4">
              <img
                src={userProfil.avatarUrl}
                alt="avatar actuel"
                className="w-24 h-24 rounded-full border-2 border-gray-400 object-cover bg-white"
              />
              <div className="grid grid-cols-3 md:grid-cols-5 gap-2 mt-4">
                {avatarOptions.map((url) => (
                  <img
                    key={url}
                    src={url}
                    alt="Option avatar"
                    onClick={async () => {
                      await updateUser({ avatarUrl: url });
                      setUserProfil({ ...userProfil, avatarUrl: url });
                      setIsModalOpen(false);
                    }}
                    className={`rounded-full cursor-pointer border-2 w-20 h-20 object-cover transition bg-white ${
                      userProfil.avatarUrl === url
                        ? "border-blue-500 scale-105"
                        : "border-transparent hover:scale-105"
                    }`}
                  />
                ))}
              </div>
            </div>
          </section>
        )}

        {selectedSection === "poster" && (
          <section>
            <button
              onClick={handleBack}
              className="text-accentuation mb-3 cursor-pointer"
            >
              ← Retour
            </button>
            <h3 className="text-lg font-bold mb-4 text-black dark:text-white text-center">
              CHOISIR UN POSTER
            </h3>

            {loadingAffiches ? (
              <p className="text-gray-500 text-center">
                Chargement des affiches...
              </p>
            ) : (
              <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-3 p-1 gap-4 overflow-y-auto">
                {afficheFilm.map((film) => (
                  <div
                    key={film.id}
                    className={`relative overflow-hidden rounded-lg border-2 cursor-pointer group transition-transform duration-300 ${
                      userProfil.coverFilmId === film.id
                        ? "border-blue-500 scale-105 shadow-lg"
                        : "border-transparent hover:scale-105 hover:border-gray-400"
                    }`}
                    onClick={async () => {
                      await updateUser({ coverFilmId: film.id });
                      setUserProfil({
                        ...userProfil,
                        coverFilmId: film.id,
                        coverFilmUrl: film.posterUrl,
                      });
                      setIsModalOpen(false);
                    }}
                  >
                    <img
                      src={film.posterUrl}
                      alt={film.title}
                      className="w-full aspect-3/2 object-cover"
                    />
                  </div>
                ))}
              </div>
            )}
          </section>
        )}

        {selectedSection === "bio" && (
          <section>
            <button
              onClick={handleBack}
              className="text-accentuation mb-3 cursor-pointer"
            >
              ← Retour
            </button>
            <h3 className="text-lg font-bold mb-3 text-black dark:text-white text-center">
              MODIFIER LA BIO
            </h3>

            <textarea
              rows={4}
              value={userProfil.bio || ""}
              onChange={(e) =>
                setUserProfil({ ...userProfil, bio: e.target.value })
              }
              placeholder="Parlez un peu de vous..."
              className="w-full p-3 border rounded-lg dark:border-gray-700 dark:bg-gray-800 text-black dark:text-white"
            />

            <button
              onClick={async () => {
                await updateUser({ bio: userProfil.bio });
                setIsModalOpen(false);
              }}
              className="mt-3 px-4 py-2 bg-accentuation hover:bg-purple-400 text-black font-semibold rounded-lg cursor-pointer"
            >
              Enregistrer la bio
            </button>
          </section>
        )}

        {selectedSection === "delete" && (
          <section>
            <button
              onClick={handleBack}
              className="text-accentuation mb-3 cursor-pointer"
            >
              ← Retour
            </button>
            <h3 className="text-lg font-bold mb-3 text-red-600 text-center">
              Supprimer mon compte
            </h3>
            <p className="text-gray-700 dark:text-gray-300 mb-4">
              Cette action est <strong>irréversible</strong>. Votre compte et
              vos données seront supprimés définitivement.
            </p>
            <button
              onClick={async () => {
                await deleteUser();
                setIsModalOpen(false);
              }}
              className="px-4 py-2 bg-red-600 hover:bg-red-700 text-white rounded-lg"
            >
              Confirmer la suppression
            </button>
          </section>
        )}
      </Modal>
      <ReportModal
        isOpen={isReportOpen}
        onClose={() => setIsReportOpen(false)}
        reporterId={user?.id}
        reportedId={userProfil.id}
      />
    </div>
  );
};
