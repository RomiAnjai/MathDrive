using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] hearts;
    public PlayerHealth playerHealth;

    public void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < playerHealth.CurrentHealth;
        }
    }
}
