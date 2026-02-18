using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    void Start()
    {
        currentHealth = maxHealth;
        FindObjectOfType<HealthUI>().UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
    currentHealth -= damage;
    FindObjectOfType<HealthUI>().UpdateHearts();

    if (currentHealth <= 0)
    {
        Die();
    }
    }

    void Die()
    {
        Debug.Log("GAME OVER");
        TombolManager tr = FindObjectOfType<TombolManager>();
        Time.timeScale = 0f;
        tr.TombolBalik.SetActive(true);
    }
}
