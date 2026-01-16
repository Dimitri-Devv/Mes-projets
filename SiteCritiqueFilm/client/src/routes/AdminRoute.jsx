import { Navigate, Outlet, useLocation } from "react-router";

import { useAuth } from "../context/AuthContext.jsx";

export default function AdminRoute() {
  const { token, isAdmin } = useAuth();
  const location = useLocation();

  if (!token) {
    return <Navigate to="/login" replace state={{ from: location }} />;
  }

  if (!isAdmin) {
    return <Navigate to="/" replace />;
  }

  return <Outlet />;
}
