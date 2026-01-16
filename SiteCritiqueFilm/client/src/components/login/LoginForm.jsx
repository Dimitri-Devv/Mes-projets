import { useState } from "react";
import { Link } from "react-router";
import { Mail, Eye, EyeOff } from "lucide-react";

export const LoginForm = ({
  handleSubmit,
  onSubmit,
  register,
  errors,
  serverError,
  isLoading,
  toggleHaveAccount,
}) => {
  const [showPassword, setShowPassword] = useState(false);

  const togglePasswordVisibility = () => {
    setShowPassword((prev) => !prev);
  };

  return (
    <form
      onSubmit={handleSubmit(onSubmit)}
      className="flex flex-col items-center gap-5 lg:gap-8 p-5 lg:p-8 text-center transition-all duration-500 ease-in-out w-full text-white dark:text-white"
    >
      <h1 className="text-2xl font-bold mb-2">Login</h1>

      <div className="w-[80%]">
        <label htmlFor="email" className="sr-only">
          Email
        </label>
        <div className="flex justify-between p-1 bg-white dark:bg-white text-black dark:text-black">
          <input
            type="email"
            id="email"
            className="w-full"
            placeholder="Email"
            autoComplete="email"
            aria-invalid={!!errors.email}
            aria-describedby={errors.email ? "email-error" : undefined}
            {...register("email", {
              required: "L'email est requis",
              pattern: {
                value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                message: "L'email n'est pas valide",
              },
            })}
          />
          <span aria-hidden="true">
            <Mail />
          </span>
        </div>
        {errors.email && (
          <p id="email-error" className="text-red-500">
            {errors.email.message}
          </p>
        )}
      </div>

      <div className="w-[80%]">
        <label htmlFor="password" className="sr-only">
          Mot de passe
        </label>
        <div className="flex justify-between p-1 bg-white dark:bg-white text-black dark:text-black">
          <input
            type={showPassword ? "text" : "password"}
            id="password"
            placeholder="Mot de passe"
            className="w-full"
            aria-invalid={!!errors.password}
            aria-describedby={errors.password ? "password-error" : undefined}
            {...register("password", {
              required: "Le mot de passe est requis",
              minLength: {
                value: 6,
                message: "Le mot de passe doit avoir au moins 6 caractères",
              },
            })}
          />
          <button
            type="button"
            className="btn btn-outline-secondary control cursor-pointer"
            onClick={togglePasswordVisibility}
            tabIndex={-1}
            title={
              showPassword
                ? "Cacher le mot de passe"
                : "Afficher le mot de passe"
            }
            aria-label={
              showPassword
                ? "Cacher le mot de passe"
                : "Afficher le mot de passe"
            }
            aria-pressed={showPassword}
          >
            {showPassword ? <Eye /> : <EyeOff />}
          </button>
        </div>
        <div className="mt-1 flex justify-end">
          <Link
            className="text-sm hover:underline text-accentuation dark:text-accentuation"
            to="/forgot-password/forgot"
          >
            Mot de passe oublie ?
          </Link>
        </div>
        {errors.password && (
          <p id="password-error" className="text-red-500">
            {errors.password.message}
          </p>
        )}
      </div>

      {serverError && (
        <p className="w-[80%] text-red-500" role="alert">
          {serverError}
        </p>
      )}

      <button
        type="submit"
        disabled={isLoading}
        className={`w-[80%] p-1 rounded-3xl cursor-pointer bg-accentuation dark:bg-accentuation font-bold text-black dark:text-black hover:bg-accentuation/75 dark:hover:bg-accentuation/75 disabled:bg-accentuation/50 disabled:dark:bg-accentuation/50 ${
          isLoading ? "cursor-progress" : ""
        }`}
      >
        {isLoading ? "Connexion..." : "Se connecter"}
      </button>

      <button type="button" onClick={toggleHaveAccount}>
        <p>
          Pas de compte ?{" "}
          <span className="text-accentuation dark:text-accentuation cursor-pointer">
            S'inscrire
          </span>
        </p>
      </button>
    </form>
  );
};
