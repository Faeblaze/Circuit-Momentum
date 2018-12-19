using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreLister : MonoBehaviour
{
    public TextMeshProUGUI[] bestLaps;
    public TextMeshProUGUI[] bestRaces;

    private void Awake()
    {
        List<float> lapTimes = GameManager.GetTimes(GameManager.LAP_TIMES);
        List<float> raceTimes = GameManager.GetTimes(GameManager.RACE_TIMES);

        for (int i = 0; i < lapTimes.Count && i < bestLaps.Length; i++)
        {
            bestLaps[i].text = Format(lapTimes[i]);
        }

        for (int i = 0; i < raceTimes.Count && i < bestRaces.Length; i++)
        {
            bestRaces[i].text = Format(raceTimes[i]);
        }
    }

    private string Format(float t)
    {
        return t.ToString("F3") + "s";
        //return string.Format("{0}:{1}", Mathf.FloorToInt(lapTimes[i] / 60F), Mathf.FloorToInt(lapTimes[i] % 60F).ToString().PadLeft(2, '0'));
    }
}
