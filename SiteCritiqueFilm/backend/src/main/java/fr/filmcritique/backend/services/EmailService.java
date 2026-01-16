package fr.filmcritique.backend.services;

import jakarta.mail.MessagingException;
import jakarta.mail.internet.MimeMessage;
import lombok.RequiredArgsConstructor;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.mail.javamail.MimeMessageHelper;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class EmailService {

    private final JavaMailSender mailSender;

    public void sendVerificationEmail(String to, String username, String code) {
        try {
            MimeMessage message = mailSender.createMimeMessage();

            MimeMessageHelper helper =
                    new MimeMessageHelper(
                            message,
                            MimeMessageHelper.MULTIPART_MODE_MIXED_RELATED,
                            "UTF-8"
                    );

            String htmlContent = """
                <html>
                  <body style="font-family: Arial, sans-serif; background-color: #f6f6f6; padding: 20px;">
                    <div style="max-width: 600px; margin: auto; background: white; border-radius: 8px; padding: 30px;">
                      <h2 style="color: #4CAF50;">Bienvenue sur FilmCritique 🎬</h2>
                      <p>Bonjour <b>%s</b>,</p>
                      <p>Merci de vous être inscrit !</p>
                      <p>Voici votre code de vérification :</p>
                      <h1 style="text-align: center; color: #333;">%s</h1>
                      <p style="color: #555;">Ce code expirera dans <b>10 minutes</b>.</p>
                      <hr style="border:none; border-top:1px solid #eee; margin:20px 0;">
                      <p style="font-size: 0.9em; color: #777;">
                        Si vous n'êtes pas à l'origine de cette inscription, ignorez cet e-mail.
                      </p>
                    </div>
                  </body>
                </html>
                """.formatted(username, code);

            helper.setFrom("dimrcq80@hotmail.fr");
            helper.setTo(to);
            helper.setSubject("Vérification de votre compte FilmCritique 🎥");
            helper.setText(htmlContent, true);

            mailSender.send(message);

        } catch (MessagingException e) {
            throw new IllegalStateException("Échec de l'envoi du mail de vérification", e);
        }
    }
}
