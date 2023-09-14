using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    public GameObject mainObject;
    public AudioClip coinSound;
    public ParticleSystem coinPS;

    public bool notDespawn;
    
    private short randChance;

    private void OnEnable()
    {
        if (!notDespawn)
        {
            randChance = (short) Random.Range(0, 2);

            if (randChance == 1)
            {
                mainObject.SetActive(true);
            }
            else
            {
                mainObject.SetActive(false);
            }  
        }
        else
        {
            mainObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && (randChance == 1 || notDespawn))
        {
            SoundManager.Instance.PlayOneShot(coinSound);
            coinPS.Play();
            UIManager.Instance.SetScore(Random.Range(100, 200));
            mainObject.SetActive(false);
        }
    }
}
