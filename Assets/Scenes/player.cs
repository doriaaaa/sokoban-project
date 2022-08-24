using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    bool flag = false;

    public bool Move (Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < 0.5)
        {
            direction.x = 0;
        }
        else
        {
            direction.y = 0;
        }
        direction.Normalize();
        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            if(!IsLevelComplete() && flag == false)
            {
                transform.Translate(direction);
            }
            if (IsLevelComplete() && flag == false)
            {
                flag = true;
                transform.Translate(direction);
            }
            return true;
        }
    }

    bool IsLevelComplete()
    {
        Box[] boxes = FindObjectsOfType<Box>();
        foreach (var box in boxes)
        {
            if (!box.m_OnGoal)
            {
                return false;
            }
        }
        if (FindObjectsOfType<Box2>() != null)
        {
            Box2[] boxes2 = FindObjectsOfType<Box2>();
            foreach (var box in boxes2)
            {
                if (!box.m_OnGoal2)
                {
                    return false;
                }
            }
        }
        return true;
    }

    bool Blocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
        foreach(var wall in walls)
        {
            if(wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("box");
        foreach (var box in boxes)
        {
            if(box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
            {
                Box bx = box.GetComponent<Box>();
                if (bx && bx.Move(direction))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        GameObject[] boxes2 = GameObject.FindGameObjectsWithTag("box2");
        foreach (var box in boxes2)
        {
            if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
            {
                Box2 bx = box.GetComponent<Box2>();
                if (bx && bx.Move(direction))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }
}
