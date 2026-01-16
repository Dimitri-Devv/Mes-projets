package fr.filmcritique.backend.securities;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpMethod;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.config.annotation.authentication.configuration.AuthenticationConfiguration;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configurers.AbstractHttpConfigurer;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;

import java.util.List;

@Configuration
public class WebSecurityConfig {

    @Autowired
    private AuthEntryPointJwt unauthorizedHandler;

    @Autowired
    private AuthTokenFilter authTokenFilter;

    @Autowired
    private RateLimitFilter rateLimitFilter;

    @Bean
    public AuthenticationManager authenticationManagerBean(
            AuthenticationConfiguration authenticationConfiguration
    ) throws Exception {
        return authenticationConfiguration.getAuthenticationManager();
    }

    @Bean
    public PasswordEncoder passwordEncoder() {
        return new BCryptPasswordEncoder();
    }

    @Bean
    public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {

        http
                // ----------------------------------------------------
                // CORS (à adapter en production)
                // ----------------------------------------------------
                .cors(cors -> cors.configurationSource(request -> {
                    var config = new org.springframework.web.cors.CorsConfiguration();
                    config.setAllowedOrigins(List.of("http://localhost:5173"));
                    config.setAllowedMethods(List.of("GET", "POST", "PUT", "DELETE", "OPTIONS"));
                    config.setAllowedHeaders(List.of("Authorization", "Content-Type", "Accept"));
                    config.setAllowCredentials(true);
                    return config;
                }))

                // ----------------------------------------------------
                // API stateless → pas de sessions server side
                // ----------------------------------------------------
                .csrf(AbstractHttpConfigurer::disable)
                .exceptionHandling(e -> e.authenticationEntryPoint(unauthorizedHandler))
                .sessionManagement(s -> s.sessionCreationPolicy(
                        org.springframework.security.config.http.SessionCreationPolicy.STATELESS))

                // ----------------------------------------------------
                // Autorisations sur les routes
                // ----------------------------------------------------
                .authorizeHttpRequests(auth -> auth

                        // --- ADMIN ONLY ---
                        .requestMatchers("/api/admin/**").hasRole("ADMIN")

                        .requestMatchers(HttpMethod.POST, "/api/films/**").hasRole("ADMIN")
                        .requestMatchers(HttpMethod.PUT, "/api/films/**").hasRole("ADMIN")
                        .requestMatchers(HttpMethod.DELETE, "/api/films/**").hasRole("ADMIN")

                        .requestMatchers(HttpMethod.POST, "/api/genres/**").hasRole("ADMIN")
                        .requestMatchers(HttpMethod.PUT, "/api/genres/**").hasRole("ADMIN")
                        .requestMatchers(HttpMethod.DELETE, "/api/genres/**").hasRole("ADMIN")

                        .requestMatchers(HttpMethod.POST, "/api/actors/**").hasRole("ADMIN")
                        .requestMatchers(HttpMethod.PUT, "/api/actors/**").hasRole("ADMIN")
                        .requestMatchers(HttpMethod.DELETE, "/api/actors/**").hasRole("ADMIN")
                        .requestMatchers(HttpMethod.GET, "/api/reports/**").hasRole("ADMIN")

                        // --- PUBLIC ---
                        .requestMatchers("/v3/api-docs/**", "/swagger-ui/**", "/swagger-ui.html").permitAll()
                        .requestMatchers("/api/auth/**").permitAll()
                        .requestMatchers(HttpMethod.GET, "/api/users/*").permitAll()
                        .requestMatchers(HttpMethod.GET, "/api/users/*/favorites").permitAll()
                        .requestMatchers(HttpMethod.GET, "/api/genres/**").permitAll()
                        .requestMatchers(HttpMethod.GET, "/api/films/**").permitAll()
                        .requestMatchers(HttpMethod.GET, "/api/actors/**").permitAll()
                        .requestMatchers(HttpMethod.GET, "/api/reviews/**").permitAll()
                        .requestMatchers(HttpMethod.GET, "/api/comments/**").permitAll()
                        .requestMatchers(HttpMethod.GET, "/api/reviews/top").permitAll()
                        .requestMatchers(HttpMethod.POST, "/api/reports/create").permitAll()


                        // REVIEWS
                        .requestMatchers(HttpMethod.POST, "/api/reviews/**").authenticated()
                        .requestMatchers(HttpMethod.PUT, "/api/reviews/**").authenticated()
                        .requestMatchers(HttpMethod.DELETE, "/api/reviews/**").authenticated()

                        // COMMENTS
                        .requestMatchers(HttpMethod.POST, "/api/comments/**").authenticated()
                        .requestMatchers(HttpMethod.PUT, "/api/comments/**").authenticated()
                        .requestMatchers(HttpMethod.DELETE, "/api/comments/**").authenticated()

                        // USER ZONE
                        .requestMatchers("/api/users/me/**").authenticated()

                        // autre route
                        .anyRequest().authenticated()
                );

        // ----------------------------------------------------
        // Injection des filtres (ordre important)
        // ----------------------------------------------------

        // Limite brute-force du /login
        http.addFilterBefore(rateLimitFilter, UsernamePasswordAuthenticationFilter.class);

        // Vérification du JWT avant l’authentification Spring
        http.addFilterBefore(authTokenFilter, UsernamePasswordAuthenticationFilter.class);

        return http.build();
    }
}