using UnityEngine;

public class Box : MonoBehaviour
{
    public bool m_OnGoal;
    bool flag = false;

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

    public bool Move(Vector2 direction)
    {
        if(BoxBlocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            if (!IsLevelComplete() && flag == false)
            {
                transform.Translate(direction);
            }
            if (IsLevelComplete() && flag == false)
            {
                flag = true;
            }
            TestforOnGoal();
            return true;
        }
    }

    bool BoxBlocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
        foreach (var wall in walls)
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
                return true;
            }
        }
        GameObject[] boxes2 = GameObject.FindGameObjectsWithTag("box2");
        foreach (var box in boxes2)
        {
            if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        return false;
    }

    void TestforOnGoal()
    {
        GameObject[] goals = GameObject.FindGameObjectsWithTag("goal");
        foreach(var goal in goals)
        {
            if(transform.position.x == goal.transform.position.x && transform.position.y == goal.transform.position.y)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                m_OnGoal = true;
                return;
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        m_OnGoal = false;
    }
}
