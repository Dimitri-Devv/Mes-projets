import { useState, useEffect} from 'react';
import { getUsers, adminUpdateUser, adminDeleteUser } from "../../api/user";
import { Flag } from "lucide-react";
import { getAllReports } from "../../api/report";
import ReportAdminModal from "./ReportAdminModal.jsx";
import ConfirmModal from "../ConfirmModal.jsx";

export default function UserManager() {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(false);
  const [search, setSearch] = useState('');
  const [filter, setFilter] = useState('all');
  const [deleteModalOpen, setDeleteModalOpen] = useState(false);
  const [blockModalOpen, setBlockModalOpen] = useState(false);
  const [currentUser, setCurrentUser] = useState(null);
  const [reportModalOpen, setReportModalOpen] = useState(false);
  const [reports, setReports] = useState([]);
  const hasPendingReports = reports.some((r) => !r.processed);
  const fetchReports = async () => {
    try {
      const data = await getAllReports();
      setReports(data || []);
    } catch (e) {
      console.error("Erreur chargement signalements", e);
    }
  };

  const fetchUsers = async () => {
    setLoading(true);
    try {
      const data = await getUsers();
      setUsers(data || []);
    } catch (error) {
      console.error("Erreur lors du chargement des utilisateurs :", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  useEffect(() => {
    fetchReports();
  }, []);

  function openDeleteModal(user) {
    setCurrentUser(user);
    setDeleteModalOpen(true);
  }

  async function confirmDelete() {
    try {
      await adminDeleteUser(currentUser.id);
      await fetchUsers();
      setDeleteModalOpen(false);
      setCurrentUser(null);
    } catch (error) {
      console.error('Erreur lors de la suppression de l\'utilisateur:', error);
    }
  }

  function openBlockModal(user) {
    setCurrentUser(user);
    setBlockModalOpen(true);
  }

  async function confirmBlockToggle() {
    try {
      const user = currentUser;

      if (user.blocked) {
        // === DÉBLOCAGE ===
        let payload = { blocked: false };

        if (user.avertissement >= 3) {
          payload.avertissement = 2;
        }

        await adminUpdateUser(user.id, payload);

      } else {

        await adminUpdateUser(user.id, { blocked: true });
      }

      await fetchUsers();
      setBlockModalOpen(false);
      setCurrentUser(null);

    } catch (error) {
      console.error("Erreur lors du changement de statut de blocage :", error);
    }
  }

  const filteredUsers = users.filter((u) => {
    if (u.role === 'ADMIN') return false;
    const username = u.username?.toLowerCase() || "";
    const email = u.email?.toLowerCase() || "";
    const query = search.toLowerCase();
    const matchesSearch = username.includes(query) || email.includes(query);

    if (filter === 'blocked') {
      return matchesSearch && u.blocked === true;
    } else if (filter === 'unblocked') {
      return matchesSearch && u.blocked === false;
    } else {
      return matchesSearch;
    }
  });


return (
    <section aria-labelledby="admin-users-title" className="text-white px-4 py-6">
      <div className="flex justify-between items-center mb-8 w-full max-w-6xl mx-auto">
        <h2 id="admin-users-title" className="text-3xl font-bold">
          GESTION DES UTILISATEURS
        </h2>
        <button
          onClick={() => { fetchReports(); setReportModalOpen(true); }}
          className={`p-2 rounded-full text-white transition cursor-pointer ${
            hasPendingReports
              ? 'bg-red-600 hover:bg-red-700'
              : 'bg-gray-600 hover:bg-gray-500'
          }`}
          title="Voir les signalements"
        >
          <Flag className="w-6 h-6" />
        </button>
      </div>
      <div className="w-full max-w-6xl mx-auto">
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-3 mb-6">
          <input
            type="search"
            placeholder="Rechercher par nom d'utilisateur ou email"
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            className="w-full px-3 py-2 rounded-lg bg-gray-800 text-white outline-none"
            aria-label="Recherche utilisateur"
          />
          <select
            value={filter}
            onChange={(e) => setFilter(e.target.value)}
            className="w-full px-3 py-2 rounded-lg bg-gray-800 text-white outline-none"
            aria-label="Filtrer les utilisateurs"
          >
            <option value="all">Tous les utilisateurs</option>
            <option value="blocked">Bloqués</option>
            <option value="unblocked">Non bloqués</option>
          </select>
        </div>
        {loading && (
          <div className="flex flex-col justify-center items-center min-h-[280px]">
            <div className="animate-spin w-10 h-10 border-4 border-t-transparent border-accentuation rounded-full"></div>
            <p className="mt-4 text-sm text-gray-300">Chargement des utilisateurs...</p>
          </div>
        )}
        {!loading && (
          <>
            {filteredUsers.length === 0 ? (
              <p className="text-center text-gray-400 mt-10">
                Aucun utilisateur trouvé.
              </p>
            ) : (
              <div className="w-full max-w-6xl mx-auto grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
                {filteredUsers.map((user) => (
                  <div key={user.id} className="bg-anthracite rounded-xl p-4 border border-gray-700 hover:border-accentuation transition-all shadow-lg flex flex-col items-center text-center">
                    <img
                      src={user.avatarUrl || '/default-avatar.png'}
                      alt={user.username}
                      className="w-24 h-24 rounded-full object-cover mb-3 border border-gray-600"
                    />
                    <h3 className="text-lg font-semibold text-white mb-1">{user.username}</h3>
                    <p className="text-sm text-gray-400 mb-2">{user.email}</p>
                    <span className="text-xs px-2 py-1 rounded bg-gray-700 text-gray-300 mb-2">
                      {user.role}
                    </span>
                    <p className="text-xs text-yellow-400 mb-2">Avertissements : {user.avertissement}</p>
                    <p className="text-xs text-gray-500 mb-4">Créé le {new Date(user.createdAt).toLocaleDateString('fr-FR')}</p>
                    <div className="flex justify-center gap-3 mt-auto">
                      <button
                        onClick={() => openBlockModal(user)}
                        className={`px-3 py-2 rounded cursor-pointer ${user.blocked ? 'bg-green-600 hover:bg-green-500' : 'bg-orange-600 hover:bg-orange-500'} text-white font-semibold transition-colors`}
                      >
                        {user.blocked ? 'Débloquer' : 'Bloquer'}
                      </button>
                      <button
                        onClick={() => openDeleteModal(user)}
                        className="px-3 py-2 rounded bg-red-600 text-white font-semibold hover:bg-red-500 transition-colors cursor-pointer"
                      >
                        Supprimer
                      </button>
                    </div>
                  </div>
                ))}
              </div>
            )}
          </>
        )}
      </div>

      {deleteModalOpen && currentUser && (
        <ConfirmModal
          title="Confirmer la suppression"
          message={`Êtes-vous sûr de vouloir supprimer l'utilisateur ${currentUser.username} ?`}
          confirmLabel="Supprimer"
          onConfirm={confirmDelete}
          onCancel={() => setDeleteModalOpen(false)}
        />
      )}

      {blockModalOpen && currentUser && (
        <ConfirmModal
          title="Confirmer le blocage"
          message={`Êtes-vous sûr de vouloir ${currentUser.blocked ? "débloquer" : "bloquer"} l'utilisateur ${currentUser.username} ?`}
          confirmLabel={currentUser.blocked ? "Débloquer" : "Bloquer"}
          onConfirm={confirmBlockToggle}
          onCancel={() => setBlockModalOpen(false)}
        />
      )}
      {reportModalOpen && (
        <ReportAdminModal
          reports={reports}
          onClose={() => {
            setReportModalOpen(false);
            fetchReports();
          }}
        />
      )}
    </section>
  );
}