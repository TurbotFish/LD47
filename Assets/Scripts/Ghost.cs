using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Easings;
public class Ghost : MonoBehaviour
{
    public float moveTime;
    public Boing boing;
    [HideInInspector]
    public Point currentPoint;
    IEnumerator currentMove;
    public SpriteSwitcher switcher;

    public void Move(Vector2 dir)
    {
        StopAllCoroutines();
        if (dir.x > 0)
        {
            if (currentPoint.right != null)
            {
                bool d = false;
                for (int i = 0; i < currentPoint.doors.Count; i++)
                {
                    if (!currentPoint.doors[i].open)
                    {
                        if (currentPoint.doors[i].pointA == currentPoint.right || currentPoint.doors[i].pointB == currentPoint.right)
                        {
                            d = true;
                            break;
                        }
                    }
                }
                if (!d)
                {
                    if (currentMove != null)
                    {
                        StopCoroutine(currentMove);
                    }
                    currentMove = Moving(currentPoint.right);
                    StartCoroutine(currentMove);
                }
            }
        }
        if (dir.x < 0)
        {
            if (currentPoint.left != null)
            {
                bool d = false;
                for (int i = 0; i < currentPoint.doors.Count; i++)
                {
                    if (!currentPoint.doors[i].open)
                    {
                        if (currentPoint.doors[i].pointA == currentPoint.left || currentPoint.doors[i].pointB == currentPoint.left)
                        {
                            d = true;
                            break;
                        }
                    }
                }
                if (!d)
                {
                    if (currentMove != null)
                    {
                        StopCoroutine(currentMove);
                    }
                    currentMove = Moving(currentPoint.left);
                    StartCoroutine(currentMove);
                }
            }
        }
        if (dir.y > 0)
        {
            if (currentPoint.up != null)
            {
                bool d = false;
                for (int i = 0; i < currentPoint.doors.Count; i++)
                {
                    if (!currentPoint.doors[i].open)
                    {
                        if (currentPoint.doors[i].pointA == currentPoint.up || currentPoint.doors[i].pointB == currentPoint.up)
                        {
                            d = true;
                            break;
                        }
                    }
                }
                if (!d)
                {
                    if (currentMove != null)
                    {
                        StopCoroutine(currentMove);
                    }
                    currentMove = Moving(currentPoint.up);
                    StartCoroutine(currentMove);
                }
            }
        }
        if (dir.y < 0)
        {
            if (currentPoint.down != null)
            {
                bool d = false;
                for (int i = 0; i < currentPoint.doors.Count; i++)
                {
                    if (!currentPoint.doors[i].open)
                    {
                        if (currentPoint.doors[i].pointA == currentPoint.down || currentPoint.doors[i].pointB == currentPoint.down)
                        {
                            d = true;
                            break;
                        }
                    }
                }
                if (!d)
                {
                    if (currentMove != null)
                    {
                        StopCoroutine(currentMove);
                    }
                    currentMove = Moving(currentPoint.down);
                    StartCoroutine(currentMove);
                }
            }
        }
    }

    IEnumerator Moving(Point targetPoint)
    {
        boing.t = 0;
        currentPoint.Switch(false);
        if (currentPoint.nbOnTop == 0)
        {
            currentPoint.exitResponse.Invoke();
        }
        targetPoint.Switch(true);
        Vector3 startPos = currentPoint.transform.position;
        Vector3 goalPos = targetPoint.transform.position;
        float t = 0;
        while (t < moveTime)
        {
            t += Time.deltaTime;
            float l = Mathf.Clamp(Ease.SmoothStep(t / moveTime), 0, 1);
            transform.position = Vector3.Lerp(startPos, goalPos, l);
            yield return null;
        }
        switcher.Switch();
        currentPoint = targetPoint;
        if (targetPoint.nbOnTop == 1)
        {
            targetPoint.enterResponse.Invoke();
        }
    }

    public void TeleportTo(Point targetPoint)
    {
        if (currentPoint != null)
        {
            currentPoint.Switch(false);
        }
        targetPoint.Switch(true);
        transform.position = targetPoint.transform.position;
        currentPoint = targetPoint;
    }

    public void ResetGhost()
    {
        StopAllCoroutines();
        StopCoroutine(currentMove);
    }
}
