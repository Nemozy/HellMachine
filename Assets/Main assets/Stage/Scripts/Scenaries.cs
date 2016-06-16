using UnityEngine;
using System.Collections;

public class Scenaries : MonoBehaviour
{
    private bool KillFirstAlien = false;
    private bool ShowBossMessage = false;
    private bool CompleteFirstQuest = false;
    private int Aliens = 0;
    private float ComingBossTimer = 295;

    void Update()
    {
        if (KillFirstAlien && Aliens < 4)
        {
            this.transform.Find("TrackingQuest").GetComponent<UnityEngine.UI.Text>().text = "Aliens: " + Aliens.ToString() + "/4";
            this.transform.Find("TrackingQuest").gameObject.SetActive(true);
        }
        else if (!CompleteFirstQuest && Aliens > 3 && !ShowBossMessage)
        {
            CompleteFirstQuest = true;
            GameObject.Find("Terrain").GetComponent<StageEnvironment>().Pause();
            this.transform.Find("QuestMessage").Find("Message").GetComponent<UnityEngine.UI.Text>().text =
                "Поздравляю! Ты справился. Дрон к тебе уже прибыл на борт. Теперь он будет в 2 раза быстрее восстанавливать обшивку корабля.";
            this.transform.Find("QuestMessage").gameObject.SetActive(true);
            GameObject.Find("Player").transform.Find("Ship").GetComponent<HeroController>().Regeneration *= 2;
        }
        else if (!ShowBossMessage && GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime() >= ComingBossTimer)
        {
            this.transform.Find("TrackingQuest").gameObject.SetActive(false);
            GetFirstBoss();
        }
        else if (!ShowBossMessage && !GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetPauseState())
        {
            this.transform.Find("TrackingQuest").GetComponent<UnityEngine.UI.Text>().text = "Boss " + 
                    Formatter.timeFormatMMSS(ComingBossTimer - GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetTime());
        }
    }

    //Убийство инопланетян (квест 2)
    public void KillAlien()
    {
        Aliens += 1;
        if (!KillFirstAlien)
            GetFirstBomb();
    }

    //Предупреждение о приходе босса (квест 3)
    public void GetFirstBoss()
    {
        ShowBossMessage = true;
        GameObject.Find("Terrain").GetComponent<StageEnvironment>().Pause();
        this.transform.Find("QuestMessage").Find("Message").GetComponent<UnityEngine.UI.Text>().text =
            "Привет. Ну, как там дрон поживает? " +
            "Если еще жив, то скоро он очень поможет тебе. Если нет, то готовься к серьезной проверке! " +
            "Мы пеленгуем приближение к тебе очень большого объекта.";
        this.transform.Find("QuestMessage").gameObject.SetActive(true);
    }

    //Убийство первого инопланетянина (квест 1)
    public void GetFirstBomb()
    {
        KillFirstAlien = true;
        GameObject.Find("Terrain").GetComponent<StageEnvironment>().Pause();
        this.transform.Find("QuestMessage").Find("Message").GetComponent<UnityEngine.UI.Text>().text =
            "Привет, командир. Сейчас ты убил своего первого врага. " +
            "Мы неожидали, что в этой системе есть инопланетяне. " +
            "Убей еще 3х и мы сможем отправить дрона, который будет защищать тебя. В твоем вооружении готовится бомба. Она тебе поможет.";
        this.transform.Find("QuestMessage").gameObject.SetActive(true);
    }
}
