using System.Collections;
using System.Linq;
using UnityEngine;

public class CrownAbility : MonoBehaviour
{
    public GameObject crownProjectilePrefab;
    private CrownProjectileBehaviour crownProjectileBehaviour;
    
    public GameObject[] crownProjectiles = new GameObject[5];
    
    public float chargeCooldown;

    public bool phaseOne;

    public bool start = false;
    
    // TODO Randomeness einfuegen

    private void Awake()
    {
        crownProjectileBehaviour = GetComponent<CrownProjectileBehaviour>();
    }

    private void Update()
    {
        if (start)
        {
            start = false;
            
            ConjureCrown();

            StartCoroutine(WaitToRelase());
        }
    }

    private IEnumerator WaitToRelase()
    {
        yield return new WaitForSeconds(1f);
        StartReleasingProjectiles();
    }

    public void ConjureCrown()
    {
        foreach (GameObject crownProjectile in crownProjectiles)
        {
            crownProjectile.GetComponent<CrownProjectileBehaviour>().ResetPosition();
        }
        
        if (phaseOne)
        {
            Debug.Log("phaseOne conjure");
            
            crownProjectiles[0].GetComponent<CrownProjectileBehaviour>().IsActive(true);
            crownProjectiles[2].GetComponent<CrownProjectileBehaviour>().IsActive(true);
            crownProjectiles[4].GetComponent<CrownProjectileBehaviour>().IsActive(true);
        }
        else
        {
            foreach (GameObject crownProjectile in crownProjectiles)
            {
                crownProjectile.GetComponent<CrownProjectileBehaviour>().IsActive(true);
            }
        }
    }

    public void StartReleasingProjectiles()
    {
        if (phaseOne)
        {
            Debug.Log("phaseone realse");
            
            if (crownProjectiles[0].GetComponent<CrownProjectileBehaviour>().projectileActive == true)
            {
                StartCoroutine(WaitToRelease(3, 0));
            }
            
            if (crownProjectiles[2].GetComponent<CrownProjectileBehaviour>().projectileActive == true)
            {
                StartCoroutine(WaitToRelease(6, 2));
            }
            
            if (crownProjectiles[4].GetComponent<CrownProjectileBehaviour>().projectileActive == true)
            {
                StartCoroutine(WaitToRelease(9, 4));
            }
        }
        else
        {
            if (crownProjectiles[0].GetComponent<CrownProjectileBehaviour>().projectileActive == true)
            {
                StartCoroutine(WaitToRelease(3, 0));
            }
            
            if (crownProjectiles[2].GetComponent<CrownProjectileBehaviour>().projectileActive == true)
            {
                StartCoroutine(WaitToRelease(6, 2));
            }
            
            if (crownProjectiles[4].GetComponent<CrownProjectileBehaviour>().projectileActive == true)
            {
                StartCoroutine(WaitToRelease(9, 4));
            }
            
            if (crownProjectiles[1].GetComponent<CrownProjectileBehaviour>().projectileActive == true)
            {
                StartCoroutine(WaitToRelease(12, 1));
            }
            
            if (crownProjectiles[3].GetComponent<CrownProjectileBehaviour>().projectileActive == true)
            {
                StartCoroutine(WaitToRelease(15, 3));
            }
        }
    }

    private IEnumerator WaitToRelease(int duration, int prefabIndex)
    {
        yield return new WaitForSeconds(duration);
        crownProjectiles[prefabIndex].GetComponent<CrownProjectileBehaviour>().Shoot();

        if ((phaseOne && duration == 9) || (!phaseOne && duration == 15))
        {
            Reset();
        }
    }

    private void Reset()
    {
        StopAllCoroutines();
    }
}
