import { Facebook, Instagram, Youtube, Twitter } from "lucide-react";
import { Link } from "react-router";

export const Footer = () => {
  return (
    <footer className="bg-anthracite dark:bg-anthracite text-white py-10 mt-10 lg:mt-20 flex flex-col items-center gap-6 w-full">
      <div className="w-14 h-14 rounded-full flex items-center justify-center">
        <img src="/logo.webp" alt="Logo" className="w-12 h-12" />
      </div>

      <div className="flex items-center gap-4">
        <span className="h-[1px] w-24 bg-accentuation dark:bg-accentuation"></span>
        <p className="uppercase tracking-wide text-sm font-semibold text-accentuation">
          Rejoignez-nous
        </p>
        <span className="h-[1px] w-24 bg-accentuation dark:bg-accentuation"></span>
      </div>

      <div className="flex gap-4 text-accentuation  dark:text-accentuation text-xl">
        <div className="bg-white rounded-full w-7 h-7 flex items-center justify-center">
          <Link to="#">
            <Facebook className="w-5 h-5" />
          </Link>
        </div>
        <div className="bg-white rounded-full w-7 h-7 flex items-center justify-center">
          <Link to="#">
            <Instagram className="w-5 h-5" />
          </Link>
        </div>
        <div className="bg-white rounded-full w-7 h-7 flex items-center justify-center">
          <Link to="#">
            <Youtube className="w-5 h-5" />
          </Link>
        </div>
        <div className="bg-white rounded-full w-7 h-7 flex items-center justify-center">
          <Link to="#">
            <Twitter className="w-5 h-5" />
          </Link>
        </div>
      </div>

      <div>
        <ul className="text-sm text-white dark:text-white flex flex-col sm:flex-row items-center justify-center  space-y-2 sm:space-y-0 sm:space-x-[40px]">
          <li>
            <Link to="/">ACCUEIL</Link>
          </li>
          <li>
            <Link to="/mentionslegales">MENTIONS LEGALES</Link>
          </li>
          <li>
            <Link to="/login"> CONNECTION/INSCRIPTION</Link>
          </li>
        </ul>
      </div>

      <div className="border-t border-accentuation mt-8 pt-4 text-center text-sm text-white/60 dark:text-white/60">
        © {new Date().getFullYear()} Copyright — Tous droits réservés.
      </div>
    </footer>
  );
};
