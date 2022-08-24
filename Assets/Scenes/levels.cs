using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class level //single level
{
    public List<string> m_Rows = new List<string>();

    public int Height { get { return m_Rows.Count; } }
    public int width
    {
        get
        {
            int maxLength = 0;
            foreach (var r in m_Rows)
            {
                if(r.Length > maxLength)
                {
                    maxLength = r.Length;
                }
            }
            return maxLength;
        }
    }
}

public class levels : MonoBehaviour //all levels
{
    public string filename;
    public List<level> m_levels;

    void Awake()
    {
        TextAsset textAsset = (TextAsset)Resources.Load(filename);
        if (!textAsset)
        {
            Debug.Log("Levels: " + filename + ".txt does not exist.");
            return;
        }
        else Debug.Log("levels:" + filename + " imported");
        string completeText = textAsset.text;
        string[] lines = completeText.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        m_levels.Add(new level());
        for (long i = 0; i<lines.LongLength; i++)
        {
            string line = lines[i];
            if (line.StartsWith(";"))
            {
                Debug.Log("new level added");
                m_levels.Add(new level());
                continue;
            }
            m_levels[m_levels.Count - 1].m_Rows.Add(line);
        }
    }
}
