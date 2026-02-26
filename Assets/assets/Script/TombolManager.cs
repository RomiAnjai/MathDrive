using UnityEngine;
using UnityEngine.SceneManagement;
public class TombolManager : MonoBehaviour
{
    public GameObject TombolBalik;
    public GameObject ScreenSettings;
    public GameObject ScreenLearn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ScreenSettings.SetActive(false);
            ScreenLearn.SetActive(false);
        }
    }
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
        SceneManager.LoadScene("LevelTutorial");
    }

    public void LanjutDariTutorial()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1f;
    }

    public void OpenSetting()
    {
        ScreenSettings.SetActive(true);
    }

    public void OpenLearn()
    {
        ScreenLearn.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
