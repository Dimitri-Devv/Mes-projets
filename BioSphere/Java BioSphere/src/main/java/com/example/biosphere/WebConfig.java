package com.example.biosphere.config;

import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.ResourceHandlerRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;

@Configuration
public class WebConfig implements WebMvcConfigurer {

    @Override
    public void addResourceHandlers(ResourceHandlerRegistry registry) {
        // 🔹 Dossier local où les images sont sauvegardées (adapte selon ton setup)
        String uploadsPath = System.getProperty("user.dir") + "/uploads/";

        // 🔹 Expose les fichiers via http://IP:8081/uploads/nom_fichier.jpg
        registry.addResourceHandler("/uploads/**")
                .addResourceLocations("file:" + uploadsPath);
    }
}
