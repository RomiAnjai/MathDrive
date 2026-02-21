using UnityEngine;
using System.IO;
using System;

public class ScreenshotManager : MonoBehaviour
{
    public static ScreenshotManager Instance;

    [Header("Settings")]
    public string folderName = "Screenshots";
    public KeyCode screenshotKey = KeyCode.F12;
    public bool includeTimestamp = true;
    public int superSize = 1; // 1 = normal resolution, 2 = 2x resolution

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            TakeScreenshot();
        }
    }

    public void TakeScreenshot()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string fileName = "screenshot";

        if (includeTimestamp)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            fileName += "_" + timestamp;
        }

        fileName += ".png";

        string fullPath = Path.Combine(folderPath, fileName);

        ScreenCapture.CaptureScreenshot(fullPath, superSize);

        Debug.Log("Screenshot saved to: " + fullPath);
    }
}