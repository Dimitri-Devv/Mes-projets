package fr.filmcritique.backend.dtos.report;

import lombok.Data;

@Data
public class ReportDto {

    private Long id;
    private Long reporterId;
    private Long reportedUserId;
    private String message;
    private String createdAt;
    private String reporterUsername;
    private String reportedUsername;

    private boolean processed;
    private Long adminId;
}