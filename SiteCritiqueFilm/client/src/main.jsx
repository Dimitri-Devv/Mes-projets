import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import Modal from "react-modal";

import App from "./App.jsx";
import { AuthProvider } from "./context/AuthContext.jsx";

Modal.setAppElement("#root");

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <AuthProvider>
      <App />
    </AuthProvider>
  </StrictMode>
);
