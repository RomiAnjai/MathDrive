using UnityEngine;

public class PlayerPoint : MonoBehaviour
{
    public float totalPoint;
    public int targetPoint;
    public int rekamanPoin;
    public float maxPoint  = 3000;
    public bool yesTutorial = true;

    void Update()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if(gm.waveNow >= gm.wave+1 && gm.currentState == GameManager.GameState.Running && gm.sudahMenang == false)
        {
            gm.Kemenangan();
        }

        if(yesTutorial == true && totalPoint == 100)
        {
            yesTutorial = false;
            Time.timeScale = 0f;
            gm.PoinTutorial();
        }
    }
    
    public void AddPoint(int amount)
    {
        totalPoint += amount;
        rekamanPoin += amount;

        if (totalPoint >= targetPoint)
        {
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.EnterDecisionMode();
            }
        }
    }

}