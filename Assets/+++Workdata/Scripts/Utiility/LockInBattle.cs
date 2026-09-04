using UnityEngine;
using UnityEngine.Events;

public class LockInBattle : MonoBehaviour
{
    private bool firstEnemy = false;
    private bool secondEnemy = false;
    private bool thirdEnemy = false;
    private bool fourthEnemy = false;

    public bool isTwoEnemies = false;

    public UnityEvent BattleWon;

    private void Update()
    {
        if (isTwoEnemies)
        {
            if (firstEnemy && secondEnemy)
            {
                BattleWon?.Invoke();
                return;
            }
        }

        if (!isTwoEnemies)
        {
            if (firstEnemy && secondEnemy && thirdEnemy && fourthEnemy)
            {
                BattleWon?.Invoke();
                return;
            }
        }
    }

    public void SetFirstEnemy()
    {
        firstEnemy = true;
    }
    
    public void SetSecondEnemy()
    {
        secondEnemy = true;
    }

    public void SetThirdEnemy()
    {
        thirdEnemy = true;
    }

    public void SetFourthEnemy()
    {
        fourthEnemy = true;
    }
}
