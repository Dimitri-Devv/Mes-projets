import { useState, useRef, useEffect } from "react";
import { useNavigate } from "react-router";
import { useForm } from "react-hook-form";

import { useAuth } from "../context/AuthContext";

import { loginApi, registerApi, verifyEmailApi } from "../api/auth";

export const useAuthLogic = () => {
  const { login } = useAuth();

  const [haveAccount, setHaveAccount] = useState(true);
  const [showVerification, setShowVerification] = useState(false);
  const [serverError, setServerError] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const [emailEntered, setEmailEntered] = useState("");
  const [otp, setOtp] = useState(Array(6).fill(""));

  const navigate = useNavigate();
  const otpRefs = useRef([]);
  const {
    register,
    handleSubmit,
    reset,
    setValue,
    watch,
    formState: { errors },
  } = useForm({
    mode: "onChange",
  });

  // ----------------------- Début de la partie OTP - ChatGPT ----------------------- //
  // Fonction pour gérer le focus sur le premier chiffre de l'OTP
  useEffect(() => {
    if (showVerification && otpRefs.current[0]) {
      otpRefs.current[0].focus();
    }
  }, [showVerification]);

  // Fonction pour mettre à jour la valeur de l'OTP
  const updateOtpValue = (nextOtp) => {
    setOtp(nextOtp);
    const code = nextOtp.join("");
    setValue("verificationCode", code, {
      shouldValidate: true,
      shouldDirty: true,
    });
  };

  // Fonction pour gérer le changement de chiffre de l'OTP
  const handleOtpChange = (index, e) => {
    const raw = e.target.value.replace(/\D/g, "");
    const nextOtp = [...otp];
    if (raw.length === 0) {
      nextOtp[index] = "";
      updateOtpValue(nextOtp);
      return;
    }
    nextOtp[index] = raw[raw.length - 1];
    updateOtpValue(nextOtp);
    if (raw && index < otpRefs.current.length - 1) {
      otpRefs.current[index + 1]?.focus();
    }
  };

  // Fonction pour gérer la navigation entre les chiffres de l'OTP
  const handleOtpKeyDown = (index, e) => {
    if (e.key === "Backspace" && !otp[index] && index > 0) {
      otpRefs.current[index - 1]?.focus();
    }
    if (e.key === "ArrowLeft" && index > 0) {
      e.preventDefault();
      otpRefs.current[index - 1]?.focus();
    }
    if (e.key === "ArrowRight" && index < 5) {
      e.preventDefault();
      otpRefs.current[index + 1]?.focus();
    }
  };

  // Fonction pour gérer le coller de l'OTP
  const handleOtpPaste = (e) => {
    const pasted = (e.clipboardData.getData("text") || "")
      .replace(/\D/g, "")
      .slice(0, 6);
    if (!pasted) return;
    e.preventDefault();
    const nextOtp = Array(6)
      .fill("")
      .map((_, i) => pasted[i] || "");
    updateOtpValue(nextOtp);
    const nextIndex = Math.min(pasted.length, 5);
    otpRefs.current[nextIndex]?.focus();
  };
  // ----------------------- Fin de la partie OTP ----------------------- //

  const handleSubmitLogin = async (data) => {
    try {
      setServerError("");
      setIsLoading(true);
      const response = await loginApi(data.email, data.password);
      login(response.token);
      setIsLoading(false);
      navigate(`/profil/${response.userId}`, { replace: true });
    } catch (error) {
      console.error(error);
      const message =
        (error?.response?.data &&
          (error?.response?.data?.message ||
            error?.response?.data?.error ||
            (typeof error?.response?.data === "string"
              ? error.response.data
              : null))) ||
        error?.message ||
        "Une erreur est survenue lors de la connexion.";
      setServerError(message);
      setIsLoading(false);
    }
  };

  const handleSubmitRegister = async (data) => {
    try {
      setServerError("");
      setIsLoading(true);
      await registerApi(data.email, data.password, data.username);
      setEmailEntered(data.email);
      setShowVerification(true);
      reset();
      setOtp(Array(6).fill(""));
      setIsLoading(false);
    } catch (error) {
      console.error(error);
      setIsLoading(false);
      const message =
        (error?.response?.data &&
          (error?.response?.data?.message ||
            error?.response?.data?.error ||
            (typeof error?.response?.data === "string"
              ? error.response.data
              : null))) ||
        error?.message ||
        "Une erreur est survenue lors de l'inscription.";
      setServerError(message);
    }
  };

  const handleSubmitVerification = async (data) => {
    try {
      setServerError("");
      setIsLoading(true);
      await verifyEmailApi(emailEntered, data.verificationCode);
      setShowVerification(false);
      setHaveAccount(true);
      reset();
      setIsLoading(false);
    } catch (error) {
      console.error(error);
      setIsLoading(false);
      const message =
        (error?.response?.data &&
          (error?.response?.data?.message ||
            error?.response?.data?.error ||
            (typeof error?.response?.data === "string"
              ? error.response.data
              : null))) ||
        error?.message ||
        "Une erreur est survenue lors de la vérification de l'email.";
      setServerError(message);
    }
  };

  const toggleHaveAccount = () => {
    setHaveAccount((prev) => !prev);
    setShowVerification(false);
    reset();
    setServerError("");
    setOtp(Array(6).fill(""));
  };

  return {
    haveAccount,
    showVerification,
    toggleHaveAccount,
    handleSubmitLogin,
    handleSubmitRegister,
    handleSubmitVerification,
    serverError,
    errors,
    isLoading,
    otp,
    otpRefs,
    handleOtpChange,
    handleOtpKeyDown,
    handleOtpPaste,
    register,
    handleSubmit,
    watch,
    setOtp,
    setShowVerification,
    setHaveAccount,
    emailEntered,
  };
};
