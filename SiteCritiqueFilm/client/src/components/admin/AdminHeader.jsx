import { Link } from "react-router";
import { Home } from "lucide-react";

export default function AdminHeader() {
  return (
    <header className="bg-anthracite p-4 rounded-lg flex justify-between items-center mb-6">

      <h1 className="text-2xl font-bold text-white">Dashboard</h1>


      <Link
        to="/"
        aria-label="Retour à l'accueil"
        className="p-2 rounded-full bg-white text-black hover:bg-gray-300 transition flex items-center justify-center"
      >
        <Home className="w-6 h-6" />
      </Link>
    </header>
  );
}