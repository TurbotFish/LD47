using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    public SpriteRenderer body, hourglass;
    public List<Sprite> bodies;
    public List<Sprite> hourglasses;
    public List<Vector2> hg_positions;

    int currentIndex;

    private void Awake()
    {
        currentIndex = 0;
    }

    public void Switch()
    {
        currentIndex++;
        if (currentIndex >= bodies.Count)
        {
            currentIndex = 0;
        }

        body.sprite = bodies[currentIndex];
        hourglass.sprite = hourglasses[currentIndex];
        hourglass.transform.localPosition = hg_positions[currentIndex];
    }
}
