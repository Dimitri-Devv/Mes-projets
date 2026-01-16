export function AddComment({text, setText, handleSubmit}) {
    return (

    <div className="relative bg-white text-black p-3 sm:p-4 rounded-xl shadow-md w-full max-w-lg text-xs sm:text-sm">

        {/* Zone de texte */}
        <textarea
            value={text}
            onChange={(e) => setText(e.target.value)}
            placeholder="Écrire un commentaire..."
            className="w-full bg-transparent outline-none resize-none text-sm"
            rows="2"
        ></textarea>

        <button
            type="button"
            onClick={(e) => handleSubmit(e)}
            className="flex items-center gap-1 sm:gap-2 mt-2 px-2 py-1 sm:px-3 sm:py-2
             bg-accentuation rounded-lg font-semibold text-black text-xs sm:text-sm"
        >
            Envoyer
        </button>
    </div>
    )
}