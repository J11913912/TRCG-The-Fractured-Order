using UnityEngine;
using UnityEngine.Events;

public class LockInBattle : MonoBehaviour
{
    public bool firstEnemy = false;
    public bool secondEnemy = false;
    public bool thirdEnemy = false;
    public bool fourthEnemy = false;

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
        else
        {
            if (firstEnemy && secondEnemy && thirdEnemy && fourthEnemy)
            {
                Debug.Log("unlocked room");
                BattleWon?.Invoke();
                return;
            }
        }
    }

    public void SetFirstEnemy()
    {
        firstEnemy = true;
        
        if (firstEnemy && secondEnemy && thirdEnemy && fourthEnemy)
        {
            Debug.Log("unlocked room");
            BattleWon?.Invoke();
            return;
        }
    }
    
    public void SetSecondEnemy()
    {
        secondEnemy = true;
        
        if (firstEnemy && secondEnemy && thirdEnemy && fourthEnemy)
        {
            Debug.Log("unlocked room");
            BattleWon?.Invoke();
            return;
        }
    }

    public void SetThirdEnemy()
    {
        thirdEnemy = true;
        
        if (firstEnemy && secondEnemy && thirdEnemy && fourthEnemy)
        {
            Debug.Log("unlocked room");
            BattleWon?.Invoke();
            return;
        }
    }

    public void SetFourthEnemy()
    {
        fourthEnemy = true;
        
        if (firstEnemy && secondEnemy && thirdEnemy && fourthEnemy)
        {
            Debug.Log("unlocked room");
            BattleWon?.Invoke();
            return;
        }
    }
}
