import {
  BrowserRouter,
  Routes,
  Route,
  useLocation,
  matchPath,
} from "react-router";

import { Home } from "./pages/Home";
import { Login } from "./pages/Login";
import { ForgotPassword } from "./pages/ForgotPassword";
import { Profil } from "./pages/Profil";
import { DetailFilm } from "./pages/DetailFilm";
import  Dashboard from "./pages/Dashboard";
import { NotFound } from "./pages/NotFound";

import { Navbar } from "./components/partials/Navbar";
import { Footer } from "./components/partials/Footer";

import AdminRoute from "./routes/AdminRoute";

import "./App.css";
import ScrollToTop from "./components/ScollToTop.jsx";
import {MentionsLegales} from "./pages/MentionsLegales.jsx";

function Layout() {
  // Cacher Navbar/Footer sur certaines pages
  const location = useLocation();
  const hiddenRoutes = ["/login", "/dashboard"];
  const shouldHideLayout =
    hiddenRoutes.includes(location.pathname) ||
    Boolean(matchPath("/forgot-password/*", location.pathname));

  return (
    <main className="min-h-screen moving-gradient-bg">
      {!shouldHideLayout && <Navbar />}

      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/forgot-password/:token" element={<ForgotPassword />} />
        <Route path="/detailFilm/:id" element={<DetailFilm />} />
        <Route path="/profil/:id" element={<Profil />} />
        <Route path="/mentionslegales" element={<MentionsLegales />} />

        <Route element={<AdminRoute />}>
          <Route path="/dashboard" element={<Dashboard />} />
        </Route>

        <Route path="*" element={<NotFound />} />
      </Routes>

      {!shouldHideLayout && <Footer />}
    </main>
  );
}

function App() {
  return (
    <BrowserRouter>
      <ScrollToTop />
      <Layout />
    </BrowserRouter>
  );
}

export default App;
