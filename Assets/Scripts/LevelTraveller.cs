using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Easings;
public class LevelTraveller : MonoBehaviour
{
    public Vector2Variable lvl_coord;
    public LevelSelection lvl_selection;
    public float moveTime;
    public Boing boing;
    [HideInInspector]
    public PointLevel currentPoint;
    bool canMove;
    IEnumerator currentMove;
    bool locked;
    public SoundPlayer sp;
    public SpriteSwitcher switcher;
    private void Awake()
    {
        canMove = true;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A))
        {
            Move(-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Move(1);
        }
    }

    void Move(int dirX)
    {
        if (!canMove)
        {
            return;
        }

        StopAllCoroutines();
        if (dirX > 0)
        {
            if (currentPoint.right != null)
            {
                if (currentPoint.right.unlocked)
                {
                    if (currentMove != null)
                    {
                        StopCoroutine(currentMove);
                    }
                    currentMove = Moving(currentPoint.right);
                    StartCoroutine(currentMove);
                    lvl_selection.LevelCoordIncrement(1);
                }               
            }
        }
        if (dirX < 0)
        {
            if (currentPoint.left != null)
            {
                if (currentPoint.left.unlocked)
                {
                    if (currentMove != null)
                    {
                        StopCoroutine(currentMove);
                    }
                    currentMove = Moving(currentPoint.left);
                    StartCoroutine(currentMove);
                    lvl_selection.LevelCoordIncrement(-1);
                }
            }
        }
    }

    IEnumerator Moving(PointLevel targetPoint)
    {
        sp.PlaySound(0);
        currentPoint.exitResponse.Invoke();
        boing.t = 0;
        currentPoint.Switch(false);
        targetPoint.Switch(true);
        canMove = false;
        Vector3 startPos = currentPoint.transform.position;
        float t = 0;
        while (t < moveTime)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, targetPoint.transform.position, Ease.SmoothStep(t / moveTime));
            yield return null;
        }
        switcher.Switch();
        currentPoint = targetPoint;
        currentPoint.enterResponse.Invoke();
        if (currentPoint.lvlInfo.success)
        {
            currentPoint.enterUnlockedResponse.Invoke();
        }
        canMove = (locked) ? false : true;
    }

    public void TeleportTo(PointLevel targetPoint)
    {
        if (currentPoint != null)
        {
            currentPoint.Switch(false);
        }
        targetPoint.Switch(true);
        transform.position = targetPoint.transform.position;
        currentPoint = targetPoint;
        lvl_selection.ReadLevel();
    }

    public void ResetTraveller()
    {
        if (currentMove != null)
        {
            StopCoroutine(currentMove);
        }
        canMove = true;
        locked = false;
    }

    public void LockMovement(bool state)
    {
        locked = state;
        if (currentMove != null)
        {
            StopCoroutine(currentMove);
        }
        canMove = !state;
    }
}
