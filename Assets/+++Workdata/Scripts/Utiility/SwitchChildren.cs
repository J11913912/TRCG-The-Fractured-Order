using UnityEngine;

public class SwitchChildren : MonoBehaviour
{
    public GameObject child1;
    public GameObject child2;

    public void SwitchOnChild1()
    {
        child1.SetActive(true);
        child2.SetActive(false);
    }

    public void SwitchOnChild2()
    {
        child1.SetActive(false);
        child2.SetActive(true);
    }
}
