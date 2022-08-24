using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelElement
    //defines each item in a level by mapping a single character
    //to the prefab, such as #.@$
{
    public string m_Character;
    public GameObject m_prefab;
}

public class levelsbuilder : MonoBehaviour
{
    public int m_CurrentLevel;
    public List<LevelElement> m_LevelElements;
    private level m_level;

    GameObject GetPrefab(char c)
    {
        LevelElement levelElement = m_LevelElements.Find(le => le.m_Character == c.ToString());
        if (levelElement != null)
        {
            return levelElement.m_prefab;
        }
        else return null;
    }

    public void NextLevel()
    {
        m_CurrentLevel++;
        if(m_CurrentLevel >= GetComponent<levels>().m_levels.Count)
        {
            m_CurrentLevel = 0;
        }
    }

    public void Build()
    {
        m_level = GetComponent<levels>().m_levels[m_CurrentLevel];
        //offset coordinates so that center of level is roughly at 0,0
        int startx = -m_level.width/2;
        int x = startx;
        int y = -m_level.Height/2;
        foreach(var row in m_level.m_Rows)
        {
            foreach (var ch in row)
            {
                Debug.Log(ch);
                GameObject prefab = GetPrefab(ch);
                if(prefab)
                {
                    Debug.Log(prefab.name);
                    Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
                }
                x++;
            }
            y++;
            x = startx;
        }
    }
}