import { useNavigate } from "react-router";
import { CircleArrowLeft } from "lucide-react";

import { useAuthLogic } from "../hooks/useAuthLogic";
import { useRandomVideo } from "../hooks/useRandomVideo";

import { LoginForm } from "../components/login/LoginForm";
import { RegisterForm } from "../components/login/RegisterForm";
import { LoginOtp } from "../components/login/LoginOtp";

export const Login = () => {
  const navigate = useNavigate();
  const randomVideo = useRandomVideo();
  const {
    haveAccount,
    showVerification,
    toggleHaveAccount,
    handleSubmitLogin,
    handleSubmitRegister,
    handleSubmitVerification,
    errors,
    emailEntered,
    serverError,
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
  } = useAuthLogic();

  return (
    <section className="h-screen overflow-hidden bg-anthracite dark:bg-anthracite">
      <title>
        {haveAccount
          ? "Connexion à votre compte - [nom du site]"
          : "Inscription nouveau compte - [nom du site]"}
      </title>
      <meta
        name="description"
        content="Connectez-vous à votre compte ou inscrivez-vous pour accéder aux fonctionnalités de notre application."
      />
      <link rel="canonical" href={`${window.location.origin}/login`} />

      <div className="flex h-full w-full">
        {/* Triangle gauche */}
        <div
          className={`hidden md:flex absolute z-1 top-0 left-0 h-full w-[30%] transform transition-transform duration-500 ease-in-out ${
            haveAccount ? "origin-left scale-x-0" : "origin-left scale-x-100"
          }`}
        >
          <div className="h-full w-[66.66%] bg-anthracite dark:bg-anthracite"></div>
          <div
            className="h-full w-[33.34%] bg-anthracite dark:bg-anthracite"
            style={{
              clipPath: "polygon(0% 0%, 100% 50%, 0% 100%)",
            }}
          ></div>
        </div>

        {/* Triangle droit */}
        <div
          className={`hidden md:flex md:flex-row-reverse absolute z-1 top-0 right-0 h-full w-[30%] transform transition-transform duration-500 ease-in-out ${
            haveAccount ? "origin-right scale-x-100" : "origin-right scale-x-0"
          }`}
        >
          <div className="h-full w-[66.66%] bg-anthracite dark:bg-anthracite"></div>
          <div
            className="h-full w-[33.34%] bg-anthracite dark:bg-anthracite"
            style={{
              clipPath: "polygon(0% 50%, 100% 0%, 100% 100%)",
            }}
          ></div>
        </div>

        <div className="flex items-center justify-center absolute z-2 top-2 left-2 md:top-4 w-auto">
          <button
            type="button"
            onClick={() => navigate(-1)}
            className="px-4 py-2 rounded-3xl cursor-pointer border border-accentuation dark:border-accentuation font-bold text-accentuation dark:text-accentuation bg-black/80 dark:bg-black/80 hover:text-accentuation/75 dark:hover:text-accentuation/75 "
            aria-label="Revenir à la page précédente"
          >
            <CircleArrowLeft />
          </button>
        </div>

        {/* Vidéo + animation de déplacement */}
        <div
          className={`relative flex w-full md:w-[80%] justify-center md:justify-start items-center px-6 transition-all duration-500 ease-in-out ${
            haveAccount ? "left-0" : "md:left-[20%]"
          }`}
        >
          <video
            className={`absolute z-0 top-0 bottom-0 w-full h-full object-cover transition-all duration-700 ease-in-out ${
              haveAccount
                ? "md:left-0 md:right-auto"
                : "md:left-auto md:right-0"
            }`}
            autoPlay
            loop
            muted
            playsInline
            preload="auto"
            aria-hidden="true"
            role="presentation"
            tabIndex={-1}
            inert
          >
            <source src={randomVideo} type="video/mp4" />
            Votre navigateur ne supporte pas la vidéo.
          </video>

          <div className="relative w-full flex justify-center items-center">
            <div
              className={`md:absolute w-[320px] lg:w-[380px] flex z-20 transition-[left] duration-500 ease-in-out rounded-xl bg-black/85 ${
                haveAccount ? "md:left-[calc(100%-320px)]" : "md:left-0"
              }`}
            >
              {showVerification ? (
                <LoginOtp
                  handleSubmit={handleSubmit}
                  onSubmit={handleSubmitVerification}
                  otp={otp}
                  otpRefs={otpRefs}
                  handleOtpChange={handleOtpChange}
                  handleOtpKeyDown={handleOtpKeyDown}
                  handleOtpPaste={handleOtpPaste}
                  errors={errors}
                  isLoading={isLoading}
                  emailEntered={emailEntered}
                  register={register}
                  setShowVerification={setShowVerification}
                  setHaveAccount={setHaveAccount}
                  setOtp={setOtp}
                />
              ) : haveAccount ? (
                <LoginForm
                  handleSubmit={handleSubmit}
                  onSubmit={handleSubmitLogin}
                  register={register}
                  errors={errors}
                  serverError={serverError}
                  isLoading={isLoading}
                  toggleHaveAccount={toggleHaveAccount}
                />
              ) : (
                <RegisterForm
                  handleSubmit={handleSubmit}
                  onSubmit={handleSubmitRegister}
                  register={register}
                  errors={errors}
                  serverError={serverError}
                  isLoading={isLoading}
                  toggleHaveAccount={toggleHaveAccount}
                  watch={watch}
                />
              )}
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};
