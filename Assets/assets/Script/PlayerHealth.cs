using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public FadeImage tombolLanjut;
    public FadeImage tombolRestart;
    public FadeImage tombolRestartHolder;
    public RectTransform posisiButton;

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
        tr.TombolBalik.SetActive(true);
        posisiButton.anchoredPosition = new Vector2(625f, 100f);

        Time.timeScale = 0f;
        tombolRestartHolder.FadeIn();
        tombolRestart.FadeIn();


        if(SceneManager.GetActiveScene().name == "LevelTutorial")
        {
            GameManager game = FindObjectOfType<GameManager>();
            game.TutorialBox.SetActive(true);
            game.TutorialText.text = "Nyawamu habis. Ayo kita ulang perjalanan dari awal!";
        }
    }
}
