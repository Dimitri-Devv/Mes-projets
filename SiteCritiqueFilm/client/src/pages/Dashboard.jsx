import { useEffect, useState } from "react";
import AdminHeader from "../components/admin/AdminHeader";
import AdminTabs from "../components/admin/AdminTabs";
import FilmManager from "../components/admin/FilmManager";
import GenreManager from "../components/admin/GenreManager";
import UserManager from "../components/admin/UserManager";
import ActorManager from "../components/admin/ActorManager";
import {getFilms} from "../api/films.jsx";
import {getUsers} from "../api/user.jsx";
import {getGenres} from "../api/genre.jsx";
import {getActors} from "../api/actor.jsx";
import { Film, Users, Tags, UserCircle, AlertTriangle } from "lucide-react";
import { getAllReports } from "../api/report.jsx";
export default function Dashboard() {
  const [activeTab, setActiveTab] = useState(null);

  const [stats, setStats] = useState({
    films: 0,
    users: 0,
    genres: 0,
    actors: 0,
  });

  const [reportStats, setReportStats] = useState({
    total: 0,
    pending: 0,
    processed: 0,
  });

  const [latest, setLatest] = useState({
    film: null,
    user: null,
    actor: null,
  });

  useEffect(() => {
    const fetchStats = async () => {
      try {
        const [films, users, genres, actors, reports] = await Promise.all([
          getFilms(),
          getUsers(),
          getGenres(),
          getActors(),
          getAllReports(),
        ]);

        const filmList = Array.isArray(films) ? films : [];
        const userList = Array.isArray(users?.users) ? users.users : Array.isArray(users) ? users : [];
        const genreList = Array.isArray(genres) ? genres : [];
        const actorList = Array.isArray(actors) ? actors : [];

        setStats({
          films: filmList.length,
          users: userList.length,
          genres: genreList.length,
          actors: actorList.length,
        });

        setLatest({
          film: filmList.at(-1) || null,
          user: userList.at(-1) || null,
          actor: actorList.at(-1) || null,
        });

        const reportList = Array.isArray(reports) ? reports : [];

        setReportStats({
          total: reportList.length,
          pending: reportList.filter(r => !r.processed).length,
          processed: reportList.filter(r => r.processed).length,
        });
      } catch (e) {
        console.error("Erreur chargement statistiques dashboard", e);
      }
    };

    fetchStats();
  }, []);

  const openTab = (tab) => {
    setActiveTab(tab);
  };

  return (
    <div className="min-h-screen bg-[#1E1E1E] text-white p-6">
      <AdminHeader />

      <section className="mt-10 max-w-6xl mx-auto">
        <h2 className="text-2xl font-bold mb-6 text-center">Tableau de bord</h2>

        <div className="grid grid-cols-2 md:grid-cols-4 gap-6">
          <div className="bg-[#2A2A2A] rounded-2xl p-6 flex flex-col items-center gap-2 shadow">
            <Film className="w-8 h-8 text-accentuation" />
            <span className="text-xl font-bold">Films</span>
            <span className="text-2xl text-accentuation font-semibold">{stats.films}</span>
          </div>

          <div className="bg-[#2A2A2A] rounded-2xl p-6 flex flex-col items-center gap-2 shadow">
            <Users className="w-8 h-8 text-accentuation" />
            <span className="text-xl font-bold">Utilisateurs</span>
            <span className="text-2xl text-accentuation font-semibold">{stats.users}</span>
          </div>

          <div className="bg-[#2A2A2A] rounded-2xl p-6 flex flex-col items-center gap-2 shadow">
            <Tags className="w-8 h-8 text-accentuation" />
            <span className="text-xl font-bold">Genres</span>
            <span className="text-2xl text-accentuation font-semibold">{stats.genres}</span>
          </div>

          <div className="bg-[#2A2A2A] rounded-2xl p-6 flex flex-col items-center gap-2 shadow">
            <UserCircle className="w-8 h-8 text-accentuation" />
            <span className="text-xl font-bold">Acteurs</span>
            <span className="text-2xl text-accentuation font-semibold">{stats.actors}</span>
          </div>
        </div>

        <div className="mt-12 grid grid-cols-1 md:grid-cols-3 gap-6">
          <div className="bg-[#2A2A2A] rounded-2xl p-6 shadow">
            <h3 className="font-bold mb-2 text-accentuation">Dernier film ajouté</h3>
            {latest.film ? (
              <p className="text-sm">{latest.film.title || latest.film.name}</p>
            ) : (
              <p className="text-sm text-gray-400">Aucun film</p>
            )}
          </div>

          <div className="bg-[#2A2A2A] rounded-2xl p-6 shadow">
            <h3 className="font-bold mb-2 text-accentuation">Dernier utilisateur ajouté</h3>
            {latest.user ? (
              <p className="text-sm">{latest.user.username || latest.user.email}</p>
            ) : (
              <p className="text-sm text-gray-400">Aucun utilisateur</p>
            )}
          </div>

          <div className="bg-[#2A2A2A] rounded-2xl p-6 shadow">
            <h3 className="font-bold mb-2 text-accentuation">Dernier acteur ajouté</h3>
            {latest.actor ? (
              <p className="text-sm">
                {latest.actor.prenom} {latest.actor.nom}
              </p>
            ) : (
              <p className="text-sm text-gray-400">Aucun acteur</p>
            )}
          </div>
        </div>

        <div className="mt-12">
          <h3 className="text-xl font-bold mb-6 text-center">Signalements</h3>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
            <div className="bg-[#2A2A2A] rounded-2xl p-6 shadow flex flex-col items-center gap-2">
              <AlertTriangle className="w-7 h-7 text-yellow-400" />
              <span className="font-semibold">Total</span>
              <span className="text-2xl font-bold text-yellow-400">{reportStats.total}</span>
            </div>

            <div className="bg-[#2A2A2A] rounded-2xl p-6 shadow flex flex-col items-center gap-2">
              <AlertTriangle className="w-7 h-7 text-red-400" />
              <span className="font-semibold">En attente</span>
              <span className="text-2xl font-bold text-red-400">{reportStats.pending}</span>
            </div>

            <div className="bg-[#2A2A2A] rounded-2xl p-6 shadow flex flex-col items-center gap-2">
              <AlertTriangle className="w-7 h-7 text-green-400" />
              <span className="font-semibold">Traités</span>
              <span className="text-2xl font-bold text-green-400">{reportStats.processed}</span>
            </div>
          </div>
        </div>

        <div className="mt-14">
          <h3 className="text-xl font-bold mb-6 text-center">Actions rapides</h3>
          <AdminTabs onSelect={openTab} />
        </div>
      </section>

      {activeTab === "film" && <FilmManager />}
      {activeTab === "user" && <UserManager />}
      {activeTab === "genre" && <GenreManager />}
      {activeTab === "actor" && <ActorManager />}

    </div>
  );
}