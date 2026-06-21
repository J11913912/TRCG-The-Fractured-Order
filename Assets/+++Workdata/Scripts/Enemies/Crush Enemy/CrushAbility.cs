using System.Collections;
using UnityEngine;

public class CrushAbility : MonoBehaviour
{
    public float shockWaveTime;

    public bool startShock = false;
    private bool shockWaveActive = false;

    public GameObject shockWave;


    private void Update()
    {
        if (startShock && !shockWaveActive)
        {
            ShockWave();
        } 
    }

    private void ShockWave()
    {
        Debug.Log("Shock Wave");
        
        startShock = false;
        
        shockWaveActive = true;
        
        shockWave.SetActive(true);
        
        StartCoroutine(ResetShockWave());
        
    }

    IEnumerator ResetShockWave()
    {
        yield return new WaitForSeconds(shockWaveTime);
        shockWave.SetActive(false);
        shockWaveActive = false;
    }
}
