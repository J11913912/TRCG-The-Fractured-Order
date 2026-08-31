using UnityEngine;
using UnityEngine.Events;

public class EldritchGuardManager : MonoBehaviour
{
    private int HashActionTrigger = Animator.StringToHash("ActionTrigger");
    private int HashActionID = Animator.StringToHash("ActionID");
    
    public BoxCollider2D talkingTrigger;
    public BoxCollider2D youShallNotPassColl;
    public BoxCollider2D safetyColl;
    public BoxCollider2D cutsceneTrigger;
    
    private Animator animator;

    public UnityEvent ImBack;
    public UnityEvent YouShallPass;

    public bool youShallPass = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetYouShallPass()
    {
        youShallPass = true;
        YouShallPass?.Invoke();
    }

    public void Vanish()
    {
        Debug.Log("Vanish");
        
        talkingTrigger.enabled = false;
        youShallNotPassColl.enabled = false;

        cutsceneTrigger.enabled = true;

        SetAnimation(10);
    }

    public void PopUp()
    {
        ImBack?.Invoke();
        SetAnimation(20);
    }

    public void SetAnimation(int ID)
    {
        animator.SetTrigger(HashActionTrigger);
        animator.SetInteger(HashActionID, ID);
    }
}
