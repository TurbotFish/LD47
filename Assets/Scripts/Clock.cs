using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    public LevelInfo lvlInfo;
    [SerializeField]
    float radius = 8;
    [SerializeField]
    Color tickedColor = Color.cyan;
    [SerializeField]
    List<Tick> ticks = new List<Tick>();
    public float tickTime;
    float t = 0;
    int currentTick = 0;
    bool pause = false;
    public UnityEngine.UI.Image clockDots;
    float loopTime = 0;

    List<Vector3> ghostInputs = new List<Vector3>();
    List<Vector3> playerInputs = new List<Vector3>();
    public Ghost ghost;
    int currentGhostInput;
    public bool ghostMirror;

    float timeSinceStart = 0;
    string baseTimer;
    public TextMeshPro text_timer;

    private void Awake()
    {
        baseTimer = text_timer.text;
        InitializeClock();
    }

    void InitializeClock()
    {
        int c = ticks.Count;
        float angle = 360 / c;
        for(int i = 0; i<c;i++)
        {
            ticks[i].tickedColor = tickedColor;
            ticks[i].transform.position = transform.position +
                new Vector3(-Mathf.Cos(Mathf.Deg2Rad * ((i * angle)+90)) * radius,
                Mathf.Sin(Mathf.Deg2Rad * ((i * angle) + 90)) * radius, 0);
        }
        loopTime = 0;
        playerInputs.Clear();
        ghostInputs.Clear();
        currentGhostInput = 0;
        ghost.gameObject.SetActive(false);
        timeSinceStart = 0;
        text_timer.text = baseTimer;
    }

    void Update()
    {
        if (!pause)
        {
            t += Time.deltaTime;
            if (t > tickTime)
            {
                currentTick++;
                if (currentTick>=ticks.Count)
                {
                    currentTick = 0;
                }
                ticks[currentTick].Trigger();
                t -= tickTime;
            }

            loopTime += Time.deltaTime;
            clockDots.fillAmount = loopTime / (tickTime * ticks.Count);
            if (currentGhostInput<ghostInputs.Count)
            {
                if (loopTime >= ghostInputs[currentGhostInput].z)
                {
                    if (ghostMirror)
                    {
                        ghost.Move(new Vector2(-ghostInputs[currentGhostInput].x, -ghostInputs[currentGhostInput].y));
                    }
                    else
                    {
                        ghost.Move(new Vector2(ghostInputs[currentGhostInput].x, ghostInputs[currentGhostInput].y));
                    }
                    currentGhostInput++;
                }
            }
            timeSinceStart += Time.deltaTime;
            text_timer.text = lvlInfo.ConvertTimeToTimer(timeSinceStart);
        }


    }

    public void ResetClock()
    {
        currentTick = 0;
        foreach(Tick t in ticks)
        {
            t.Reset();
        }
        loopTime = 0;
        t = 0;
        ghostInputs.Clear();
        for(int i = 0;i<playerInputs.Count;i++)
        {
            ghostInputs.Add(playerInputs[i]);
        }
        playerInputs.Clear();
        ghost.gameObject.SetActive(true);
        currentGhostInput = 0;
    }

    public void Pause()
    {
        pause = true;
    }

    public void Unpause()
    {
        pause = false;
    }

    public void RegisterPlayerInput(Vector2Int dir)
    {
        playerInputs.Add(new Vector3(dir.x, dir.y, loopTime));
    }

    public void RegisterTime()
    {
        lvlInfo.time = timeSinceStart;
    }
}
