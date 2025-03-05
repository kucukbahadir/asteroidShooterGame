using UnityEngine;
using UnityEngine.Events;

public class Lives : MonoBehaviour
{
    [SerializeField] private int lives;

    public UnityEvent OnDecreaseLives;
    public UnityEvent OnZeroLives;

    public void DecreaseLives(int decreaseAmount)
    {
        lives -= decreaseAmount;

        OnDecreaseLives?.Invoke();

        if(lives <= 0)
        {
            OnZeroLives?.Invoke();
        }
    }
}
