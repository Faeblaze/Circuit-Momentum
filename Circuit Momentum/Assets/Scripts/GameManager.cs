using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const string LAP_TIMES = "LapTimes";
    public const string RACE_TIMES = "RaceTimes";

    public static GameManager Instance { get; private set; }

    public int maxLaps = 8;
    public float startTime = 20F;
    public float addTime = 15F;

    public LapController player;
    public LapController[] enemies;

    private float timeRemaining;

    [Header("UI Manager")]
    public TextMeshProUGUI lapText;
    public TextMeshProUGUI timeText;

    private string lapFormat;
    private string timeFormat;

    private float lapTime = float.PositiveInfinity;
    private float raceTime = 0F;

    private void Awake()
    {
        Instance = this;

        timeRemaining = startTime;

        lapFormat = lapText.text;
        timeFormat = timeText.text;
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;

        lapText.text = string.Format(lapFormat, player.currentLap, maxLaps);
        timeText.text = string.Format(timeFormat, Mathf.FloorToInt(timeRemaining / 60F), Mathf.FloorToInt(timeRemaining % 60F).ToString().PadLeft(2, '0'));

        lapTime += Time.deltaTime;
        raceTime += Time.deltaTime;

        if (timeRemaining <= 0F)
            SceneManager.LoadScene("Menu");
    }

    public void OnPlayerLap()
    {
        timeRemaining += addTime;

        if (!float.IsInfinity(lapTime))
        {
            List<float> times = GetTimes(LAP_TIMES);

            times.Add(lapTime);
            SetTimes(LAP_TIMES, times);
        }

        lapTime = 0F;

        if (player.currentLap > maxLaps)
            OnRaceEnd();
    }

    public void OnRaceEnd()
    {
        List<float> times = GetTimes(RACE_TIMES);

        times.Add(raceTime);
        SetTimes(RACE_TIMES, times);

        SceneManager.LoadScene("Menu");
    }

    public static List<float> GetTimes(string key)
    {
        if (!PlayerPrefs.HasKey(key))
            return new List<float>();

        int length = PlayerPrefs.GetInt(key);
        List<float> values = new List<float>();

        for (int i = 0; i < length; i++)
            values.Add(PlayerPrefs.GetFloat(key + i));

        return values;
    }

    public static void SetTimes(string key, List<float> times)
    {
        times = times.OrderBy(x => x).ToList();
        if (times.Count > 5)
            times = times.Take(5).ToList();

        PlayerPrefs.SetInt(key, times.Count);

        for (int i = 0; i < times.Count; i++)
            PlayerPrefs.SetFloat(key + i, times[i]);

        PlayerPrefs.Save();
    }

#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod]
    private static void OnInit()
    {
        SceneManager.sceneLoaded += (s, m) => DynamicGI.UpdateEnvironment();
    }
#endif
}
