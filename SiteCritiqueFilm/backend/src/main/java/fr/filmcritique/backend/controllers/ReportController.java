package fr.filmcritique.backend.controllers;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.tags.Tag;

import fr.filmcritique.backend.dtos.report.ReportDto;
import fr.filmcritique.backend.services.ReportService;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequiredArgsConstructor
@RequestMapping("/api/reports")
@Tag(name = "Reports", description = "Gestion des signalements utilisateurs")
public class ReportController {

    private final ReportService reportService;

    // Un utilisateur envoie un signalement
    @Operation(
            summary = "Créer un signalement",
            description = "Permet à un utilisateur de signaler un autre utilisateur en envoyant un message."
    )
    @ApiResponses(value = {
            @ApiResponse(responseCode = "200", description = "Signalement créé avec succès"),
            @ApiResponse(responseCode = "400", description = "Requête invalide"),
            @ApiResponse(responseCode = "404", description = "Utilisateur non trouvé")
    })
    @PostMapping("/create")
    public ReportDto create(
            @RequestParam Long reporterId,
            @RequestParam Long reportedId,
            @RequestParam String message
    ) {
        return reportService.createReport(reporterId, reportedId, message);
    }

    // Admin : voir tous les signalements
    @Operation(
            summary = "Récupérer tous les signalements",
            description = "Permet à un administrateur de voir tous les signalements effectués par les utilisateurs."
    )
    @ApiResponses(value = {
            @ApiResponse(responseCode = "200", description = "Liste des signalements récupérée avec succès")
    })
    @GetMapping
    public List<ReportDto> getAll() {
        return reportService.getAllReports();
    }

    // Admin : traiter un signalement (+ mettre un avertissement si demandé)
    @Operation(
            summary = "Traiter un signalement",
            description = "Permet à un administrateur de traiter un signalement et de mettre un avertissement si demandé."
    )
    @ApiResponses(value = {
            @ApiResponse(responseCode = "200", description = "Signalement traité avec succès"),
            @ApiResponse(responseCode = "404", description = "Signalement ou administrateur non trouvé")
    })
    @PostMapping("/{id}/process")
    public ReportDto process(
            @PathVariable Long id,
            @RequestParam Long adminId,
            @RequestParam(defaultValue = "false") boolean warning
    ) {
        return reportService.processReport(id, adminId, warning);
    }
}