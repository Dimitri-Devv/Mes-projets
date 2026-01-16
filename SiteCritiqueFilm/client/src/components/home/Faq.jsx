import { ChevronDown } from "lucide-react";

export default function FAQ() {
    const faqItems = [
        {
            question: "Comment fonctionne la notation des films ?",
            answer:
                "Chaque utilisateur peut attribuer une note sur 10. La moyenne affichée est calculée en temps réel à partir de l’ensemble des critiques publiées."
        },
        {
            question: "Puis-je modifier ou supprimer ma critique ?",
            answer:
                "Oui. Tant que vous êtes connecté, vous pouvez modifier ou supprimer votre critique à tout moment depuis votre profil."
        },
        {
            question: "Comment signaler un contenu inapproprié ?",
            answer:
                "Une fonctionnalité de signalement sera bientôt disponible. En attendant, vous pouvez nous contacter directement via la page de contact."
        },
        {
            question: "Pourquoi une seule critique par film ?",
            answer:
                "Cela permet de garantir une moyenne plus juste et d’éviter les abus. Vous pouvez néanmoins modifier votre critique à tout moment."
        }
    ];

    return (
        <section className="relative mt-28 mb-32 px-4">

            <div className="max-w-4xl mx-auto space-y-4">
                {faqItems.map((faq, index) => (
                    <details
                        key={index}
                        className="group rounded-2xl border border-white/10 bg-white/5 backdrop-blur-md
                       transition-all duration-300 hover:border-accentuation/60"
                    >
                        <summary
                            className="flex cursor-pointer list-none items-center justify-between
                         gap-4 px-6 py-5 text-lg font-semibold text-white"
                        >
                            <span>{faq.question}</span>

                            <ChevronDown
                                className="h-5 w-5 shrink-0 text-accentuation transition-transform
                           duration-300 group-open:rotate-180"
                            />
                        </summary>

                        <div className="px-6 pb-6 text-sm sm:text-base text-gray-300 leading-relaxed">
                            {faq.answer}
                        </div>
                    </details>
                ))}
            </div>
        </section>
    );
}