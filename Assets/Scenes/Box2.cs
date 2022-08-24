using UnityEngine;

public class Box2 : MonoBehaviour
{
    public bool m_OnGoal2;
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
        if (Box2Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            if(!IsLevelComplete() && flag == false)
            {
                transform.Translate(direction);
            }
            if(IsLevelComplete() && flag == false)
            {
                flag = true;
            }
            TestforOnGoal2();
            return true;
        }
    }

    bool Box2Blocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
        foreach (var wall in walls)
        {
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("box");
        foreach (var box in boxes)
        {
            if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
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

    void TestforOnGoal2()
    {
        GameObject[] goals2 = GameObject.FindGameObjectsWithTag("goal2");
        foreach (var goal in goals2)
        {
            if (transform.position.x == goal.transform.position.x && transform.position.y == goal.transform.position.y)
            {
                GetComponent<SpriteRenderer>().color = Color.yellow;
                m_OnGoal2 = true;
                return;
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        m_OnGoal2 = false;
    }
}
