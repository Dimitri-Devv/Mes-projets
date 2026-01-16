package com.example.biosphere.service;
import com.mailjet.client.MailjetRequest;
import com.mailjet.client.MailjetClient;
import com.mailjet.client.resource.Emailv31;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.json.JSONArray;
import org.json.JSONObject;

@Service
public class EmailService {

    private final MailjetClient client;

    @Value("${mailjet.sender}")
    private String senderEmail;

    public EmailService(
            @Value("${mailjet.api.key}") String apiKey,
            @Value("${mailjet.api.secret}") String apiSecret
    ) {
        this.client = new MailjetClient(apiKey, apiSecret);
    }

    public void sendVerificationEmail(String email, String code) throws Exception {
        JSONObject message = new JSONObject()
                .put(Emailv31.Message.FROM, new JSONObject()
                        .put("Email", senderEmail)
                        .put("Name", "BioSphere"))
                .put(Emailv31.Message.TO, new JSONArray()
                        .put(new JSONObject().put("Email", email)))
                .put(Emailv31.Message.SUBJECT, "Vérification de votre compte BioSphere ✅")
                .put(Emailv31.Message.HTMLPART,
                        "<div style='font-family:Arial, sans-serif;text-align:center;padding:20px;border-radius:12px;background:#f4fffb;border:1px solid #2a9d8f30;'>"
                        + "<img src='https://i.imgur.com/2J8E4jL.png' alt='BioSphere' style='width:120px;margin-bottom:10px;'/>"
                        + "<h2 style='color:#2a9d8f;margin-bottom:10px;'>Bienvenue sur BioSphere 🌱</h2>"
                        + "<p style='font-size:15px;color:#444;margin-bottom:20px;'>Merci de créer un compte avec nous !</p>"
                        + "<p style='font-size:16px;font-weight:bold;color:#2a9d8f;'>Voici ton code de vérification :</p>"
                        + "<div style='font-size:32px;font-weight:900;color:#ffffff;background:#2a9d8f;width:150px;margin:12px auto;padding:8px 0;border-radius:8px;'>" + code + "</div>"
                        + "<p style='color:#555;'>Ce code expire dans 15 minutes.</p>"
                        + "<p style='font-size:13px;color:#777;margin-top:20px;'>Si tu n'es pas à l'origine de cette demande, ignore cet email.</p>"
                        + "</div>");

        MailjetRequest request = new MailjetRequest(Emailv31.resource)
                .property(Emailv31.MESSAGES, new JSONArray().put(message));

        client.post(request);
    }
}