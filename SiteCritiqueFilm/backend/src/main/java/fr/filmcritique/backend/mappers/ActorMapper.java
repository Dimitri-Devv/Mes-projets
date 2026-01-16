package fr.filmcritique.backend.mappers;

import fr.filmcritique.backend.dtos.actor.ActorCreateDto;
import fr.filmcritique.backend.dtos.actor.ActorResponseDto;
import fr.filmcritique.backend.dtos.actor.ActorUpdateDto;
import fr.filmcritique.backend.entities.Actor;

public class ActorMapper {

    public static ActorResponseDto toDto(Actor actor) {
        return new ActorResponseDto(
                actor.getId(),
                actor.getNom(),
                actor.getPrenom(),
                actor.getBio(),
                actor.getAvatarUrl()
        );
    }

    public static Actor fromCreateDto(ActorCreateDto dto) {
        Actor actor = new Actor();
        actor.setNom(dto.getNom());
        actor.setPrenom(dto.getPrenom());
        actor.setBio(dto.getBio());
        actor.setAvatarUrl(dto.getAvatarUrl());
        return actor;
    }

    public static void applyUpdate(Actor actor, ActorUpdateDto dto) {
        if (dto.getNom() != null) actor.setNom(dto.getNom());
        if (dto.getPrenom() != null) actor.setPrenom(dto.getPrenom());
        if (dto.getBio() != null) actor.setBio(dto.getBio());
        if (dto.getAvatarUrl() != null) actor.setAvatarUrl(dto.getAvatarUrl());
    }
}