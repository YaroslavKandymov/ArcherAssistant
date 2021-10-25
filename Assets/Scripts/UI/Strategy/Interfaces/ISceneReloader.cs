using System.Collections.Generic;

public interface ISceneReloader
{
    void Restart(IEnumerable<ArcherAssistant> assistants, IEnumerable<Archer> archers, IEnumerable<Quiver> quivers);
}
