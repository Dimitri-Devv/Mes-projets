export default function SectionTitle({ title, subtitle }) {
  return (
    <div className="relative pt-14 sm:pt-16 md:pt-20 mb-12 sm:mb-16 md:mb-20 text-center px-4">
      {subtitle && (
        <p className="mb-3 sm:mb-4 text-[10px] sm:text-xs font-semibold uppercase tracking-[0.25em] sm:tracking-[0.35em] text-accentuation/80">
          {subtitle}
        </p>
      )}

      <h2 className="relative inline-block text-2xl sm:text-3xl md:text-4xl lg:text-5xl font-extrabold tracking-tight text-white leading-tight">
        {title}

        {/* underline */}
        <span className="absolute left-1/2 -bottom-3 sm:-bottom-4 h-[2px] sm:h-[3px] w-2/3 -translate-x-1/2 rounded-full bg-gradient-to-r from-transparent via-accentuation to-transparent" />
      </h2>

      {/* glow */}
      <div className="pointer-events-none absolute inset-0 -z-10 flex justify-center">
        <div className="h-16 sm:h-20 md:h-24 w-48 sm:w-64 md:w-72 rounded-full bg-accentuation/20 blur-3xl" />
      </div>
    </div>
  );
}