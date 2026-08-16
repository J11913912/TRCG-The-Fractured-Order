using System.Linq;
using UnityEngine;

public class DisplaySpellInfos : MonoBehaviour
{
    public GameObject[] descriptions = new GameObject[4];

    private Corner _corner;
    
    private TMPro.TextMeshProUGUI[] texts = new TMPro.TextMeshProUGUI[2];

    public void SetDescription(Corner corner, string name, string description)
    {
        foreach (GameObject desc in descriptions)
        {
            desc.SetActive(false);
        }
        
        _corner = corner;

        switch (_corner)
        {
            case Corner.upLeft:
                descriptions[0].SetActive(true);
                texts = descriptions[0].GetComponentsInChildren<TMPro.TextMeshProUGUI>();
                texts[0].text = name;
                texts[1].text = description;
                break;
            
            case Corner.upRright:
                descriptions[1].SetActive(true);
                texts = descriptions[1].GetComponentsInChildren<TMPro.TextMeshProUGUI>();
                texts[0].text = name;
                texts[1].text = description;
                break;
            
            case Corner.downRight:
                descriptions[2].SetActive(true);
                texts = descriptions[2].GetComponentsInChildren<TMPro.TextMeshProUGUI>();
                texts[0].text = name;
                texts[1].text = description;
                break;
            
            case Corner.downLeft:
                descriptions[3].SetActive(true);
                texts = descriptions[3].GetComponentsInChildren<TMPro.TextMeshProUGUI>();
                texts[0].text = name;
                texts[1].text = description;
                break;
        }
    }

    public void SetEmpty(Corner corner)
    {
        foreach (GameObject desc in descriptions)
        {
            desc.SetActive(false);
        }
        
        _corner = corner;

        switch (_corner)
        {
            case Corner.upLeft:
                descriptions[0].SetActive(true);
                texts = descriptions[0].GetComponentsInChildren<TMPro.TextMeshProUGUI>();
                texts[0].text = "";
                texts[1].text = "Spell not unlocked, not even in the game yet!";
                break;
            
            case Corner.upRright:
                descriptions[1].SetActive(true);
                texts = descriptions[1].GetComponentsInChildren<TMPro.TextMeshProUGUI>();
                texts[0].text = "";
                texts[1].text = "Spell not unlocked, not even in the game yet!";
                break;
            
            case Corner.downRight:
                descriptions[2].SetActive(true);
                texts = descriptions[2].GetComponentsInChildren<TMPro.TextMeshProUGUI>();
                texts[0].text = "";
                texts[1].text = "Spell not unlocked, not even in the game yet!";
                break;
            
            case Corner.downLeft:
                descriptions[3].SetActive(true);
                texts = descriptions[3].GetComponentsInChildren<TMPro.TextMeshProUGUI>();
                texts[0].text = "";
                texts[1].text = "Spell not unlocked, not even in the game yet!";
                break;
        }
    }
}
