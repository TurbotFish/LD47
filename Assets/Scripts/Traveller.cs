using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Easings;
public class Traveller : MonoBehaviour
{
    public float moveTime;
    public Boing boing;
    [HideInInspector]
    public Point currentPoint;
    Point target;
    bool canMove;
    IEnumerator currentMove;
    public bool register;
    public Clock clock;
    bool locked;
    public SoundPlayer sp;
    public SpriteSwitcher switcher;

    private void Awake()
    {
        
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A))
        {
            Move(new Vector2(-1, 0));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Move(new Vector2(1, 0));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Move(new Vector2(0, -1));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
        {
            Move(new Vector2(0, 1));
        }
    }

    void Move(Vector2 dir)
    {
        if (!canMove)
        {
            return;
        }

        StopAllCoroutines();
        if (dir.x>0)
        {
            if (currentPoint.right!=null)
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
                        currentMove = null;
                        currentPoint = target;
                        transform.position = target.transform.position;
                    }
                    currentMove = Moving(currentPoint.right);
                    StartCoroutine(currentMove);
                    if (register)
                    {
                        clock.RegisterPlayerInput(new Vector2Int(1, 0));
                    }
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
                        currentMove = null;
                        currentPoint = target;
                        transform.position = target.transform.position;
                    }
                    currentMove = Moving(currentPoint.left);
                    StartCoroutine(currentMove);
                    if (register)
                    {
                        clock.RegisterPlayerInput(new Vector2Int(-1, 0));
                    }
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
                        currentMove = null;
                        currentPoint = target;
                        transform.position = target.transform.position;
                    }
                    currentMove = Moving(currentPoint.up);
                    StartCoroutine(currentMove);
                    if (register)
                    {
                        clock.RegisterPlayerInput(new Vector2Int(0, 1));
                    }
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
                    if (currentMove!=null)
                    {
                        StopCoroutine(currentMove);
                        currentMove = null;
                        currentPoint = target;
                        transform.position = target.transform.position;
                    }
                    currentMove = Moving(currentPoint.down);
                    StartCoroutine(currentMove);
                    if (register)
                    {
                        clock.RegisterPlayerInput(new Vector2Int(0, -1));
                    }
                }
            }
        }
    }

    IEnumerator Moving(Point targetPoint)
    {
        sp.PlaySound(0);
        target = targetPoint;
        boing.t = 0;
        currentPoint.Switch(false);
        if (currentPoint.nbOnTop == 0)
        {
            currentPoint.exitResponse.Invoke();
        }

        targetPoint.Switch(true);
        canMove = false;
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
        canMove = (locked)?false: true;
    }

    public void TeleportTo(Point targetPoint)
    {
        if (currentMove != null)
        {
            StopCoroutine(currentMove);
            currentMove = null;
            currentPoint = target;
            transform.position = target.transform.position;
        }
        if (currentPoint!=null)
        {
            currentPoint.Switch(false);
        }
        targetPoint.Switch(true);
        transform.position = targetPoint.transform.position;
        currentPoint = targetPoint;
    }

    public void ResetTraveller()
    {

        canMove = true;
        locked = false;
    }

    public void LockMovement(bool state)
    {
        locked = state;
        if (currentMove!=null)
        {
            StopCoroutine(currentMove);
            currentMove = null;
            currentPoint = target;
            transform.position = target.transform.position;
        }
        canMove = !state;
    }
}
