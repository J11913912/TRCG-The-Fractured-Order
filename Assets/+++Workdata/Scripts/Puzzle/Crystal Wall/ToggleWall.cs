using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

public class ToggleWall : MonoBehaviour
{
    public static Action<bool> OnWallUsed;
    
    public bool wallOn;
    public bool wallUsed = false;
    private bool _justToggled = false;
    public bool isSide = false;
    
    public GameObject wall;
    public Animator wallAnim;
    public Collider2D wallCollider;
    public GameObject wallCollider2;

    private void Awake()
    {
        if (isSide)
        {
            wallAnim.SetFloat("XDirection", 1f);
        }
    }

    private void OnEnable()
    {
        OnWallUsed += OtherWallSet;
    }

    private void OnDisable()
    {
        OnWallUsed -= OtherWallSet;
    }

    public void OtherWallSet(bool value)
    {
        if (_justToggled) return;

        if (wall.activeSelf == false) return;
        
        if (wallOn)
        {
            wallAnim.SetTrigger("ActionTrigger");
            wallAnim.SetInteger("ActionID", 100);
        }
    }

    public void KillWall()
    {
        Debug.Log("destroy wall");
        
        wallCollider.enabled = false;
        wallCollider2.SetActive(false);
        wallOn = false;
    }

    public void Toggle()
    {
        wallOn = !wallOn;
        _justToggled = true;
        wallAnim.SetTrigger("ActionTrigger");
        wallAnim.SetInteger("ActionID", 10);
        wallCollider.enabled = true;
        wallCollider2.SetActive(true);
        WallManager.SetWallOn?.Invoke();

        StartCoroutine(NotToggled());
    }

    private IEnumerator NotToggled()
    {
        yield return new WaitForEndOfFrame();
        _justToggled = false;
    }
}
