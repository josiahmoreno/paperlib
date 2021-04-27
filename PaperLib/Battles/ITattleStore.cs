using Enemies;

namespace Battle
{
    public interface ITattleStore
    {
        GameText FetchGameText(Enemy enemy);
    }
}