package fr.filmcritique.backend.services;

import fr.filmcritique.backend.entities.Report;
import fr.filmcritique.backend.entities.User;
import fr.filmcritique.backend.mappers.ReportMapper;
import fr.filmcritique.backend.dtos.report.ReportDto;
import fr.filmcritique.backend.repositories.ReportRepo;
import fr.filmcritique.backend.repositories.UserRepo;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class ReportService {

    private final ReportRepo reportRepo;
    private final UserRepo userRepo;

    public ReportDto createReport(Long reporterId, Long reportedId, String message) {

        User reporter = userRepo.findById(reporterId)
                .orElseThrow(() -> new RuntimeException("Reporter inconnu"));

        User reportedUser = userRepo.findById(reportedId)
                .orElseThrow(() -> new RuntimeException("Utilisateur signalé introuvable"));

        Report report = Report.builder()
                .reporter(reporter)
                .reportedUser(reportedUser)
                .message(message)
                .createdAt(java.time.LocalDateTime.now())
                .build();

        reportRepo.save(report);
        return ReportMapper.toDto(report);
    }

    public List<ReportDto> getAllReports() {
        return reportRepo.findAll()
                .stream()
                .map(ReportMapper::toDto)
                .toList();
    }

    public ReportDto processReport(Long reportId, Long adminId, boolean addWarning) {

        Report report = reportRepo.findById(reportId)
                .orElseThrow(() -> new RuntimeException("Signalement introuvable"));

        User admin = userRepo.findById(adminId)
                .orElseThrow(() -> new RuntimeException("Admin introuvable"));

        report.setProcessed(true);
        report.setAdmin(admin);

        // L'admin choisit de mettre un avertissement ?
        if (addWarning) {
            User badUser = report.getReportedUser();
            badUser.setAvertissement(badUser.getAvertissement() + 1);
            userRepo.save(badUser); // ⚠️ le trigger SQL gère le blocage auto
        }

        reportRepo.save(report);
        return ReportMapper.toDto(report);
    }
}