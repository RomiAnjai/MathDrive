using UnityEngine;
using UnityEngine.SceneManagement;
public class TombolManager : MonoBehaviour
{
    public GameObject TombolBalik;
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void LanjutLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
        Time.timeScale = 1f;
    }

    public void PressPlay()
    {
        SceneManager.LoadScene("Level1");
    }
}
