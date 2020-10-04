using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGOffsetter : MonoBehaviour
{
    float offset;
    public float offsetSpeed;
    public Material mat;

    private void Start()
    {
        offset = mat.GetTextureOffset("_MainTex").y ;
    }


    // Update is called once per frame
    void Update()
    {
        offset += offsetSpeed * Time.deltaTime;
        if (offset>1)
        {
            offset = 0;
        }
        mat.SetTextureOffset("_MainTex", new Vector2(0, offset));

    }
}
