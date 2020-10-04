using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolver : MonoBehaviour
{
    public float minDissolve, maxDissolve;
    public Material mat;
    public bool disableWhenDissolved;
    public List<GameObject> objects = new List<GameObject>();
    Material m;
    private void Awake()
    {
        m = new Material(mat);
        foreach(GameObject g in objects)
        {
            g.GetComponent<Renderer>().material = m;
        }
    }
    public void Interpolate(float t)
    {
        m.SetFloat("_DissolveProgress", Mathf.Lerp(minDissolve, maxDissolve, t)); 
        if (disableWhenDissolved)
        {
            if (t>=1)
            {
                foreach(GameObject g in objects)
                {
                    g.SetActive(false);
                }
            }
            else if (t<=0)
            {
                foreach (GameObject g in objects)
                {
                    g.SetActive(true);
                }
            }
        }
    }
}
