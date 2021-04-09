using Channels;
using Levels;
using Snake;
using UnityEngine;

public class LevelPortal : MonoBehaviour
{
    [SerializeField] private LevelChannel _levelChannel;
    private Collider2D _collider;

    private Level _oldLevel;
    private Level _nextLevel;
    
    private void Awake()
    {
        _levelChannel.levelCompletedEvent += LevelCompleteEvent;
        _collider = GetComponent<Collider2D>();
        gameObject.SetActive(false);
    }

    private void LevelCompleteEvent(Level oldLevel, Level newLevel)
    {
        gameObject.SetActive(true);
        _oldLevel = oldLevel;
        _nextLevel = newLevel;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Component snakeHead;

        if (other.TryGetComponent(typeof(SnakeHead), out snakeHead))
        {
            gameObject.SetActive(false);
            
            _levelChannel.OnLevelChanging(_oldLevel, _nextLevel);
        }
    }
}
