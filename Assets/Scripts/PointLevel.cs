using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Easings;
using UnityEngine.Events;
public class PointLevel : MonoBehaviour
{
    public bool debug;
    public Transform point_parent;
    public Transform inSprite, outSprite;
    public float transformTime = 0.5f;
    public Vector3 inScaleFree, outScaleFree;
    public Vector3 inScaleOcc, outScaleOcc;

    public PointLevel left,right;

    public LevelInfo lvlInfo;
    public bool unlocked;

    public UnityEvent enterResponse;
    public UnityEvent exitResponse;
    public UnityEvent enterUnlockedResponse;
    Color baseColor;
    public Color unlockedColor;
    public SceneLoader loader;
    public SoundPlayer sp;

    private void Awake()
    {
        baseColor = inSprite.GetComponent<SpriteRenderer>().color;    
    }

    private void Start()
    {


    }
    public void Switch(bool o)
    {
        if(debug)
        {
            Debug.Log("debug " + o);
        }
        StartCoroutine(Switching(o));
    }

    IEnumerator Switching(bool occupied)
    {
        if (occupied)
        {
            sp.PlaySound(0);
        }
        Vector3 startScaleIn = !occupied ? inScaleOcc : inScaleFree;
        Vector3 startScaleOut = !occupied ? outScaleOcc : outScaleFree;
        Vector3 startEuler = occupied ? new Vector3(0, 0, 45) : Vector3.zero;

        Vector3 targetScaleIn = !occupied ? inScaleFree : inScaleOcc;
        Vector3 targetScaleOut = !occupied ? outScaleFree : outScaleOcc;
        Vector3 targetEuler = occupied ? Vector3.zero : new Vector3(0, 0, 45);

        float t = 0;
        while (t < transformTime)
        {
            t += Time.deltaTime;
            float l = Mathf.Clamp(Ease.SmoothStep(t / transformTime), 0, 1);
            inSprite.localScale = Vector3.Lerp(startScaleIn, targetScaleIn, l);
            outSprite.localScale = Vector3.Lerp(startScaleOut, targetScaleOut, l);
            point_parent.localRotation = Quaternion.Euler(Vector3.Lerp(startEuler, targetEuler, l));
            yield return null;
        }
    }

    public void LoadLevel()
    {
        loader.LoadSceneAsync(0);
    }
}
