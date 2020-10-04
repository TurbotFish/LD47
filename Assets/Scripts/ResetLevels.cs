using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevels : MonoBehaviour
{
    public bool reset;
    public List<LevelInfo> levels = new List<LevelInfo>();
    // Start is called before the first frame update
    void Start()
    {
        if (reset)
        {
            foreach(LevelInfo li in levels)
            {
                for(int i = 0;i<li.fragments.Length;i++)
                {
                    li.fragments[i] = false;
                }
                li.success = false;
                li.time = 0;
            }
        }
    }
}
