using UnityEngine;

public class EasterEggROMI : MonoBehaviour
{
    private KeyCode[] kode = { KeyCode.R, KeyCode.O, KeyCode.M, KeyCode.I };
    private int currentIndex = 0;
    public GameObject freeCamObject; // Camera khusus freecam
    private bool freeCamActive = false;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(kode[currentIndex]))
            {
                currentIndex++;

                if (currentIndex >= kode.Length)
                {
                    ToggleFreeCam();
                    currentIndex = 0;
                }
            }
            else
            {
                currentIndex = 0;
            }
        }
    }

    void ToggleFreeCam()
    {
        freeCamActive = !freeCamActive;

        if (freeCamActive)
        {
            ActivateFreeCam();
        }
        else
        {
            DeactivateFreeCam();
        }
    }

    void ActivateFreeCam()
    {
        Debug.Log("ROMI ACTIVATED 😈");

        Time.timeScale = 0f;

        freeCamObject.SetActive(true);
    }

    void DeactivateFreeCam()
    {
        Debug.Log("ROMI OFF");

        Time.timeScale = 1f;

        freeCamObject.SetActive(false);
    }
}
