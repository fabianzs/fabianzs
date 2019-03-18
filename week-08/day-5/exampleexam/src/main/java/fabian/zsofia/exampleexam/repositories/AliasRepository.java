package fabian.zsofia.exampleexam.repositories;

import fabian.zsofia.exampleexam.models.Alias;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

@Repository
public interface AliasRepository extends CrudRepository<Alias, Long> {

    @Query(value = "SELECT * FROM alias WHERE alias.url_alias LIKE :alias", nativeQuery = true)
    Alias findAliasByUrlAlias(@Param("alias") String urlAlias);

}