using Game.Enemies;

namespace Game
{
    public interface IEnemyInteractable
    {
        void EnemyCollided(Enemy enemy);
    }
}