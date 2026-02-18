using UnityEngine;

public class PlayerPoint : MonoBehaviour
{
    public float totalPoint;
    public int targetPoint;
    public int rekamanPoin;
    public float maxPoint  = 3000;

    void Update()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if(gm.waveNow >= gm.wave+1 && gm.currentState == GameManager.GameState.Running && gm.sudahMenang == false)
        {
            gm.Kemenangan();
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