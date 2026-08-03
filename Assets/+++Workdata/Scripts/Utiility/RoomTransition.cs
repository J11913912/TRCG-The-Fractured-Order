using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class RoomTransition : MonoBehaviour
{
    public GameObject player;
    public GameObject targetPos;

    public Image fade;
    public CanvasGroup fade2;
    
    public CinemachineCamera playerCam;
    
    public float fadeDuration;
    
    private Vector3 _oldPos;
    
    public LoadingScreenSetter _loadingScreenSetter;
    
  //  private SetLevelConfiner _setLevelConfiner;
    public Collider2D confiner;

    private void Awake()
    {
      //  _setLevelConfiner = GetComponent<SetLevelConfiner>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entered");
                
            player.GetComponent<PlayerInput>().DisableInput();
                
            StartCoroutine(Teleport(other.gameObject));
        }
    }

    IEnumerator Teleport(GameObject _player)
    {
        yield return StartCoroutine(Fade(0f, 1f));
        
        _oldPos = _player.transform.position;
                
        _player.transform.position = targetPos.transform.position;
                
        Vector3 delta = _player.transform.position - _oldPos;
        
        playerCam.OnTargetObjectWarped(player.transform, delta);

        playerCam.PreviousStateIsValid = false;
                
        Debug.Log("Teleport");

        yield return StartCoroutine(Fade(1f, 0f));
    }
    
    IEnumerator Fade(float _startAlpha, float _endAlpha)
    { 
        _startAlpha = fade.color.a;
       
        Color color = fade.color;
        
       // fadeDuration = _loadingScreenSetter.StartThingy();
       
        float time = 0;

        while (time < fadeDuration)
        { 
            time += Time.deltaTime; 
            color.a = Mathf.Lerp(_startAlpha, _endAlpha, time / fadeDuration);
            
            fade.color = color;
            yield return null;

        } 
        
        color.a = _endAlpha;
        fade.color = color;
       
        player.GetComponent<PlayerInput>().EnableInput();
       // _setLevelConfiner.SetNewConfiner(confiner);
    }

    public void EndTransition()
    {
        player.GetComponent<PlayerInput>().EnableInput();
    }
}