using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Easings;

public class LevelSelection : MonoBehaviour
{
    public Vector2Variable lvl_coord;
    public LevelTraveller traveller;
    public Transform fragmentParent;
    public List<GameObject> fragments;
    public float fragmentSpacing;
    public Material pickupHiddenMat, fragmentMat;
    public TextMeshPro tmp_lvlName;
    public TextMeshPro tmp_timer;
    public int currentWorld;
    public int currentLevel;
    public List<World> worlds;

    public List<LevelPointGroup> pointLevels;
    public GameEvent loadLevelEvent;

    public PressKeyEvent pressKey;

    public float worldOffset = 19;
    public Transform map;
    public float offsetTime = 2;
    public Vector3 mapStartPos;
    IEnumerator offsetRoutine;

    private void Start()
    {

        map.position = mapStartPos + new Vector3(-lvl_coord.value.x*worldOffset, 0,0);

        PlaceTravellerOnLevel();

        for(int i = 0;i<pointLevels.Count;i++)
        {
            for(int j = 0;j<pointLevels[i].points.Count;j++)
            {
                if (pointLevels[i].points[j].lvlInfo.success)
                {
                    pointLevels[i].points[j].unlocked = true;
                }
                if (pointLevels[i].points[j].left != null)
                {
                    if (pointLevels[i].points[j].left.unlocked && pointLevels[i].points[j].left.lvlInfo.success)
                    {
                        pointLevels[i].points[j].unlocked = true;
                    }
                }
                if (pointLevels[i].points[j].unlocked)
                {
                    pointLevels[i].points[j].inSprite.GetComponent<SpriteRenderer>().color = pointLevels[i].points[j].unlockedColor;
                }
            }
        }
    }

    void PlaceTravellerOnLevel()
    {
        traveller.TeleportTo(pointLevels[Mathf.RoundToInt(lvl_coord.value.x)].points[Mathf.RoundToInt(lvl_coord.value.y)]);
        if (pointLevels[Mathf.RoundToInt(lvl_coord.value.x)].points[Mathf.RoundToInt(lvl_coord.value.y)].lvlInfo.success)
        {
            pointLevels[Mathf.RoundToInt(lvl_coord.value.x)].points[Mathf.RoundToInt(lvl_coord.value.y)].enterUnlockedResponse.Invoke();
        }
        pressKey.enabled = true;
    }

    public void ReadLevel()
    {
        currentWorld = Mathf.RoundToInt(lvl_coord.value.x);
        currentLevel = Mathf.RoundToInt(lvl_coord.value.y);
        tmp_lvlName.text = worlds[currentWorld].levels[currentLevel].lvlName;
        tmp_timer.text = (worlds[currentWorld].levels[currentLevel].time == 0) ? "time: --:--:--" : 
            worlds[currentWorld].levels[currentLevel].ConvertTimeToTimer(worlds[currentWorld].levels[currentLevel].time);

        foreach(GameObject g in fragments)
        {
            g.SetActive(false);
            g.GetComponent<MeshRenderer>().material = pickupHiddenMat;
        }

        for(int i = 0;i< worlds[currentWorld].levels[currentLevel].fragments.Length;i++)
        {
            fragments[i].SetActive(true);
            if (worlds[currentWorld].levels[currentLevel].fragments[i])
            {
                if (i<fragments.Count)
                {
                    fragments[i].GetComponent<MeshRenderer>().material = fragmentMat;
                }
            }
        }
        int c = worlds[currentWorld].levels[currentLevel].fragments.Length;
        float x = -(c - 1) * 0.5f * fragmentSpacing;
        fragmentParent.localPosition = new Vector3(x, fragmentParent.localPosition.y, fragmentParent.localPosition.z);
    }

    public void LevelCoordIncrement(int delta)
    {
        int y = Mathf.RoundToInt(lvl_coord.value.y);
        int x = Mathf.RoundToInt(lvl_coord.value.x);
        y += delta;
        if (y>=worlds[Mathf.RoundToInt(lvl_coord.value.x)].levels.Count)
        {
            if (x<worlds.Count-1)
            {
                if (offsetRoutine != null)
                {
                    StopCoroutine(offsetRoutine);
                    map.position = mapStartPos + new Vector3(-lvl_coord.value.x * worldOffset, 0, 0);
                }

                y = 0;
                x++;
                lvl_coord.value = new Vector2(x, y);

                offsetRoutine = Offsetting();
                StartCoroutine(offsetRoutine);
            }

        }
        else if (y<0)
        {
            if (x>0)
            {
                if (offsetRoutine != null)
                {
                    StopCoroutine(offsetRoutine);
                    map.position = mapStartPos + new Vector3(-lvl_coord.value.x * worldOffset, 0, 0);
                }

                x--;
                y = worlds[x].levels.Count - 1;
                lvl_coord.value = new Vector2(x, y);
                offsetRoutine = Offsetting();
                StartCoroutine(offsetRoutine);
            }
        }
        else
        {
            lvl_coord.value = new Vector2(x, y);
        }
    }

    public void LoadLevel()
    {
        loadLevelEvent.Raise();
        pointLevels[Mathf.RoundToInt(lvl_coord.value.x)].points[Mathf.RoundToInt(lvl_coord.value.y)].LoadLevel();
    }

    IEnumerator Offsetting()
    {
        Vector3 startPos = map.position;
        Vector3 goalPos = mapStartPos + new Vector3(-lvl_coord.value.x * worldOffset, 0, 0);
        float t = 0;
        while(t<offsetTime)
        {
            t += Time.deltaTime;
            float l = Mathf.Clamp(Ease.SmoothStep(t / offsetTime), 0, 1);
            map.position = Vector3.Lerp(startPos, goalPos, l);
            yield return null;
        }
    }
}

[System.Serializable]
public class World
{
    public List<LevelInfo> levels = new List<LevelInfo>();
}

[System.Serializable]
public class LevelPointGroup
{
    public List<PointLevel> points = new List<PointLevel>();
}
