using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Point startPoint;
    public Point ghostStartPoint;
    public Ghost g;
    public Traveller t;
    private void Start()
    {
        PlaceTravellerOnStart();
    }

    public void PlaceTravellerOnStart()
    {
        //Traveller t = FindObjectOfType<Traveller>();
        //Ghost g = FindObjectOfType<Ghost>();

        t.TeleportTo(startPoint);
        g.TeleportTo(ghostStartPoint);
    }
}
