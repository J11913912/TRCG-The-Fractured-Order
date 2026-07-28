using UnityEngine;

public class BossInformation : MonoBehaviour
{
   public int health;
   public int maxHealth;
   public int halfHealth;
   
   public bool isDead = false;
   public bool isSecondPhase = false;

   private void Awake()
   {
       health = maxHealth / 2;
   }
   
   public void SetDamage(int damage)
   {
       health -= damage;

       if (health <= halfHealth && !isDead)
       {
           health = halfHealth;
           isSecondPhase = true;
           GetComponent<BossSpinAbility>().SetSecondPhase();
           GetComponent<BossCrushAbility>().SetSecondPhase();
           GetComponent<CrownAbility>().SetSecondPhase();
       }

       if (health <= 0 && isSecondPhase)
       {
           health = 0;
           Dead();
       }
   }

   private void Dead()
   {
       isDead = true;
   }
}
