using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    }

    public void AddTime()
    {
        timeRemaining += addTime;
    }
}
