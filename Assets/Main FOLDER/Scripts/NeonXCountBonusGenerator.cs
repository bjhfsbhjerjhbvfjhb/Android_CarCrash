using TMPro;
using UnityEngine;

public class NeonXCountBonusGenerator : MonoBehaviour
{
    public TextMeshPro textMesh;
    public GameObject mainObject;
    public GameObject shaderObject;
    public AudioClip coinSound;

    private short randChance;
    private int rand;
    private void OnEnable()
    {
        randChance = (short)UnityEngine.Random.Range(0, 4);

        if (randChance == 1)
        {
            mainObject.SetActive(true);
            shaderObject.SetActive(true);
            textMesh.gameObject.SetActive(true);

            rand = GenerateReward();

            if (rand < 0)
            {
                textMesh.color = Color.red;
                shaderObject.GetComponent<Renderer>().material.SetColor("_MainColor",new Color(1f, 0.1f, 0.1f, 1));
                textMesh.text = rand.ToString();
            }
            else if (rand > 0)
            {
                textMesh.color = Color.green;
                shaderObject.GetComponent<Renderer>().material.SetColor("_MainColor",new Color(0f, 0.4f, 0.8f, 1));
                textMesh.text = "+" + rand;
            }
        }
        else
        {
            mainObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && randChance == 1)
        {
            SoundManager.Instance.PlayOneShot(coinSound);
            shaderObject.SetActive(false);
            textMesh.gameObject.SetActive(false);
            UIManager.Instance.SetScore(rand);
        }
    }

    private int GenerateReward()
    {
        int reward;
        reward = Random.Range(-300, 600);

        if (reward <= -50 || reward >= 100)
        {
            return reward;
        }

        return 100;
    }
}
