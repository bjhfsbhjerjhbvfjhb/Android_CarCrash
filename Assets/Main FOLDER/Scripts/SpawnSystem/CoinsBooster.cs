using UnityEngine;

public class CoinsBooster : MonoBehaviour
{
    public GameObject mainObject;
    
    private short randChance;
    private short rand;
    
    private void OnEnable()
    {
        randChance = (short)Random.Range(0, 14);

        if (randChance == 1)
        {
            mainObject.SetActive(true);
        }
        else
        {
            mainObject.SetActive(false);
        }  
    }
}
