package fr.filmcritique.backend.mappers;

import fr.filmcritique.backend.dtos.report.ReportDto;
import fr.filmcritique.backend.entities.Report;

public class ReportMapper {

    public static ReportDto toDto(Report report) {
        ReportDto dto = new ReportDto();
        dto.setId(report.getId());
        dto.setReporterId(report.getReporter().getId());
        dto.setReportedUserId(report.getReportedUser().getId());
        dto.setMessage(report.getMessage());
        if (report.getCreatedAt() != null) {
            dto.setCreatedAt(report.getCreatedAt().toString());
        } else {
            dto.setCreatedAt("");
        }
        dto.setReporterUsername(report.getReporter().getUsername());
        dto.setReportedUsername(report.getReportedUser().getUsername());

        dto.setProcessed(report.isProcessed());
        dto.setAdminId(report.getAdmin() != null ? report.getAdmin().getId() : null);
        return dto;
    }
}