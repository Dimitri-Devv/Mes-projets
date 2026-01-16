import { useState, useEffect } from "react";
import { useAuth } from "../../context/AuthContext";
import { processReport } from "../../api/report";

export default function ReportAdminModal({ reports, onClose, onRefresh }) {
  const { user } = useAuth();
  const adminId = user?.id;

  const [enhancedReports, setEnhancedReports] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    setEnhancedReports(reports);
    setLoading(false);
  }, [reports]);

  return (
    <div
      className="fixed inset-0 z-50 grid place-items-center bg-black/70 p-4"
      onClick={onClose}
      role="dialog"
      aria-modal="true"
    >
      <div
        className="w-full max-w-2xl bg-anthracite dark:bg-gray-900 rounded-2xl shadow-xl
                   border border-gray-700 overflow-y-auto max-h-[80vh]"
        onClick={(e) => e.stopPropagation()}
      >
        {/* HEADER */}
        <div className="flex items-center justify-between px-5 py-4 border-b border-gray-700">
          <h2 className="text-xl font-semibold text-white">
            Signalements utilisateurs
          </h2>
          <button
            onClick={onClose}
            className="text-gray-400 hover:text-white text-2xl font-bold"
          >
            &times;
          </button>
        </div>

        {/* CONTENT */}
        <div className="p-5 space-y-4 text-white">
          {loading ? (
            <div className="flex justify-center py-6">
              <div className="w-8 h-8 border-4 border-gray-400 border-t-accentuation rounded-full animate-spin"></div>
            </div>
          ) : reports.length === 0 ? (
            <p className="text-center text-gray-400">Aucun signalement.</p>
          ) : (
            enhancedReports
              .filter((r) => !r.processed)
              .map((r) => (
                <div
                  key={r.id}
                  className="border border-gray-700 p-3 rounded-lg space-y-1"
                >
                  <p className="text-sm">
                    Signalé par :{" "}
                    <a
                      href={`/profile/${r.reporterId}`}
                      className="text-accentuation underline hover:opacity-80"
                    >
                      {r.reporterUsername}
                    </a>
                  </p>
                  <p className="text-sm">
                    Utilisateur signalé :{" "}
                    <a
                      href={`/profile/${r.reportedUserId}`}
                      className="text-accentuation underline hover:opacity-80"
                    >
                      {r.reportedUsername}
                    </a>
                  </p>
                  <p className="text-sm italic text-gray-300">
                    « {r.message} »
                  </p>
                  <p className="text-xs text-gray-500">
                    Créé le {new Date(r.createdAt).toLocaleDateString("fr-FR")}
                  </p>

                  <div className="flex justify-end pt-3 gap-3">
                    <button
                      onClick={async () => {
                        try {
                          await processReport(r.id, adminId, false);
                          setEnhancedReports((prev) =>
                            prev.filter((rep) => rep.id !== r.id)
                          );
                          if (onRefresh) onRefresh();
                        } catch (e) {
                          console.error("Erreur traitement signalement", e);
                        }
                      }}
                      className="px-3 py-1 bg-gray-600 text-white rounded hover:bg-gray-500 transition"
                    >
                      Traiter sans avertissement
                    </button>

                    <button
                      onClick={async () => {
                        try {
                          await processReport(r.id, adminId, true);
                          setEnhancedReports((prev) =>
                            prev.filter((rep) => rep.id !== r.id)
                          );
                          if (onRefresh) onRefresh();
                        } catch (e) {
                          console.error("Erreur traitement signalement", e);
                        }
                      }}
                      className="px-3 py-1 bg-red-600 text-white rounded hover:bg-red-500 transition"
                    >
                      Avertir l'utilisateur
                    </button>
                  </div>
                </div>
              ))
          )}
        </div>
      </div>
    </div>
  );
}
