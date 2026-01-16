package fr.filmcritique.backend.services;

import fr.filmcritique.backend.dtos.actor.ActorCreateDto;
import fr.filmcritique.backend.dtos.actor.ActorResponseDto;
import fr.filmcritique.backend.dtos.actor.ActorUpdateDto;
import fr.filmcritique.backend.dtos.film.FilmShortDto;
import fr.filmcritique.backend.entities.Actor;
import fr.filmcritique.backend.entities.Film;
import fr.filmcritique.backend.mappers.ActorMapper;
import fr.filmcritique.backend.repositories.ActorRepo;
import fr.filmcritique.backend.repositories.FilmRepo;
import jakarta.persistence.EntityNotFoundException;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class ActorService {

    private final ActorRepo actorRepo;
    private final FilmRepo filmRepo;

    public List<ActorResponseDto> getAllActors() {
        return actorRepo.findAll()
                .stream()
                .map(ActorMapper::toDto)
                .toList();
    }

    public ActorResponseDto getActorById(Long id) {
        Actor actor = actorRepo.findById(id)
                .orElseThrow(() -> new EntityNotFoundException("Acteur introuvable : " + id));
        return ActorMapper.toDto(actor);
    }

    public ActorResponseDto addActor(ActorCreateDto dto) {

        if (actorRepo.existsByNomIgnoreCaseAndPrenomIgnoreCase(dto.getNom(), dto.getPrenom())) {
            throw new IllegalArgumentException("Cet acteur existe déjà : "
                    + dto.getNom() + " " + dto.getPrenom());
        }

        Actor actor = ActorMapper.fromCreateDto(dto);
        return ActorMapper.toDto(actorRepo.save(actor));
    }

    public ActorResponseDto updateActor(Long id, ActorUpdateDto dto) {

        Actor existing = actorRepo.findById(id)
                .orElseThrow(() -> new EntityNotFoundException("Acteur introuvable : " + id));

        ActorMapper.applyUpdate(existing, dto);

        return ActorMapper.toDto(actorRepo.save(existing));
    }

    public void deleteActor(Long id) {
        if (!actorRepo.existsById(id)) {
            throw new EntityNotFoundException("Acteur inexistant");
        }
        actorRepo.deleteById(id);
    }

    public ActorResponseDto addFilmToActor(Long actorId, Long filmId) {

        Actor actor = actorRepo.findById(actorId)
                .orElseThrow(() -> new EntityNotFoundException("Acteur introuvable"));

        Film film = filmRepo.findById(filmId)
                .orElseThrow(() -> new EntityNotFoundException("Film introuvable"));

        actor.getFilms().add(film);
        film.getActors().add(actor);

        filmRepo.save(film);

        return ActorMapper.toDto(actorRepo.save(actor));
    }

    public ActorResponseDto removeFilmFromActor(Long actorId, Long filmId) {

        Actor actor = actorRepo.findById(actorId)
                .orElseThrow(() -> new EntityNotFoundException("Acteur introuvable"));

        Film film = filmRepo.findById(filmId)
                .orElseThrow(() -> new EntityNotFoundException("Film introuvable"));

        actor.getFilms().remove(film);
        film.getActors().remove(actor);

        filmRepo.save(film);

        return ActorMapper.toDto(actorRepo.save(actor));
    }

    public List<FilmShortDto > getFilmsByActor(Long actorId) {

        Actor actor = actorRepo.findById(actorId)
                .orElseThrow(() -> new EntityNotFoundException("Acteur introuvable"));

        return actor.getFilms()
                .stream()
                .map(f -> new FilmShortDto(f.getId(), f.getTitle(), f.getAfficheUrl()))
                .toList();
    }

    public List<ActorResponseDto> getActorsByFilmId(Long filmId) {

        Film film = filmRepo.findById(filmId)
                .orElseThrow(() -> new EntityNotFoundException("Film introuvable"));

        return film.getActors()
                .stream()
                .map(ActorMapper::toDto)
                .toList();
    }
}