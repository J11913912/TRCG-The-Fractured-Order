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

    private void Awake()
    {
        crownProjectileBehaviour = GetComponent<CrownProjectileBehaviour>();
    }

    public void ConjureCrown()
    {
        if (phaseOne)
        {
            crownProjectiles[0].GetComponent<CrownProjectileBehaviour>().onActiveChange(true);
            crownProjectiles[2].GetComponent<CrownProjectileBehaviour>().onActiveChange(true);
            crownProjectiles[4].GetComponent<CrownProjectileBehaviour>().onActiveChange(true);
        }
        else
        {
            foreach (GameObject crownProjectile in crownProjectiles)
            {
                crownProjectile.GetComponent<CrownProjectileBehaviour>().onActiveChange(true);
            }
        }
    }

    public void StartReleasingProjectiles()
    {
        if (phaseOne)
        {
            StartCoroutine(WaitToRelease(3, 0));
            StartCoroutine(WaitToRelease(6, 2));
            StartCoroutine(WaitToRelease(9, 4));
        }
        else
        {
            StartCoroutine(WaitToRelease(3, 0));
            StartCoroutine(WaitToRelease(6, 2));
            StartCoroutine(WaitToRelease(9, 4));
            StartCoroutine(WaitToRelease(12, 1));
            StartCoroutine(WaitToRelease(15, 3));
        }
            
    }

    private IEnumerator WaitToRelease(int duration, int prefabIndex)
    {
        yield return new WaitForSeconds(duration);
        crownProjectiles[prefabIndex].GetComponent<CrownProjectileBehaviour>().Shoot();
    }
}
