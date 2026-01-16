export const LoginOtp = ({
  handleSubmit,
  onSubmit,
  register,
  otp,
  otpRefs,
  handleOtpChange,
  handleOtpKeyDown,
  handleOtpPaste,
  errors,
  isLoading,
  emailEntered,
  setShowVerification,
  setHaveAccount,
  clearErrors,
  setOtp,
}) => {
  return (
    <form
      onSubmit={handleSubmit(onSubmit)}
      className="flex flex-col items-center gap-5 p-5 text-center transition-all duration-500 ease-in-out w-full text-white dark:text-white"
    >
      <h2
        id="otp-instructions"
        className="w-full font-bold text-lg text-center text-accentuation dark:text-accentuation"
      >
        Entrez le code envoyé par email à{" "}
        <span className="bold">{emailEntered || "votre adresse email"}</span>
      </h2>

      <div
        className="w-[80%] flex justify-center gap-2"
        role="group"
        aria-labelledby="otp-instructions"
      >
        {otp.map((digit, idx) => (
          <input
            key={idx}
            ref={(el) => (otpRefs.current[idx] = el)}
            type="text"
            inputMode="numeric"
            pattern="[0-9]*"
            maxLength={1}
            className="w-10 h-12 text-center rounded bg-white dark:bg-white text-black dark:text-black border border-gray-300 focus:outline-none focus:ring-2 focus:ring-accentuation"
            value={digit}
            aria-label={`Chiffre ${idx + 1} du code de vérification`}
            onChange={(e) => handleOtpChange(idx, e)}
            onKeyDown={(e) => handleOtpKeyDown(idx, e)}
            onPaste={handleOtpPaste}
          />
        ))}
      </div>
      <input
        type="hidden"
        {...register("verificationCode", {
          required: "Le code de vérification est requis",
          pattern: {
            value: /^\d{6}$/,
            message: "Le code de vérification doit contenir 6 chiffres",
          },
        })}
        value={otp.join("")}
        readOnly
      />
      {errors.verificationCode && (
        <p className="mt-1 text-red-500">{errors.verificationCode.message}</p>
      )}
      <button
        type="submit"
        className={`w-[80%] p-1 rounded-3xl cursor-pointer bg-accentuation dark:bg-accentuation font-bold text-black dark:text-black hover:bg-accentuation/75 dark:hover:bg-accentuation/75 ${
          isLoading ? "cursor-progress" : ""
        }`}
      >
        {isLoading ? "Validation ..." : "Valider"}
      </button>
      <button
        type="button"
        className="w-[80%] p-1 rounded-3xl cursor-pointer border border-accentuation dark:border-accentuation font-bold text-accentuation dark:text-accentuation hover:text-accentuation/75 dark:hover:text-accentuation/75"
        onClick={(e) => {
          e.preventDefault();
          e.stopPropagation();
          requestAnimationFrame(() => {
            setShowVerification(false);
            setHaveAccount(false);
            clearErrors();
            setOtp(Array(6).fill(""));
          });
        }}
      >
        Annuler
      </button>
    </form>
  );
};
