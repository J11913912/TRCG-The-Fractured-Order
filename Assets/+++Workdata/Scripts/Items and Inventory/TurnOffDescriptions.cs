using UnityEngine;
using TMPro;

public class TurnOffDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemHeaderText;
    [SerializeField] private GameObject options;

    public void TurnOff()
    {
        options.SetActive(false);
        itemDescriptionText.SetText("");
        itemHeaderText.SetText("");
    }
}