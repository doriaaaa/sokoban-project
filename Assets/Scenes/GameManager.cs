using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public levelsbuilder m_LevelBuilder;
    private bool m_ReadyForInput;
    private player m_Player;
    private player prevplay;
    public GameObject LevelCompleted;
    bool pause = false;
    int step1, step2, step3, step4 = 0;
    public Button AutoPlay;
    public Button StopPlay;
    public Button FastForward;
    public Button Backward;

    private void Start()
    {
        LevelCompleted.SetActive(false);
        ResetScene();
    }

    private void FixedUpdate()
    {
        if (IsLevelComplete())
        {
            LevelCompleted.SetActive(true);
        }
        else
        {
            LevelCompleted.SetActive(false);
        }

        if (step2 == 8)
        {
            AutoPlay.interactable = false;
            StopPlay.interactable = false;
            FastForward.interactable = false;
            Backward.interactable = false;
        }
        if (step3 == 15)
        {
            AutoPlay.interactable = false;
            StopPlay.interactable = false;
            FastForward.interactable = false;
            Backward.interactable = false;
        }
        if (step4 == 27)
        {
            AutoPlay.interactable = false;
            StopPlay.interactable = false;
            FastForward.interactable = false;
            Backward.interactable = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();
        if (moveInput.sqrMagnitude > 0.5)
        {
            if (m_ReadyForInput)
            {
                m_ReadyForInput = false;
                m_Player.Move(moveInput);
            }
        }
        else
        {
            m_ReadyForInput = true;
        }
    }

    public void AutomateFirst()
    {
        StartCoroutine(Automate1());
        AutoPlay.interactable = false;
        FastForward.interactable = false;
        Backward.interactable = false;
    }

    public void pauseFirst()
    {
        pause = true;
        AutoPlay.interactable = true;
        FastForward.interactable = false;
        Backward.interactable = false;
    }

    public void FastForward1()
    {
        Vector2 vec = new Vector2(1,0);
        m_Player.Move(vec);
        step1++;
    }

    public void backward1()
    {
        Vector2[] vecArray =
        {
            new Vector2(1,0)
        };

        int walked = 0;
        if (step1 > 0)
        {
            Vector2 prevxy = Vector2.zero;

            foreach (var vec in vecArray)
            {
                if (walked == step1 - 1)
                {
                    prevxy.x -= vec.x;
                    prevxy.y -= vec.y;
                    prevplay = FindObjectOfType<player>();

                    Box[] boxes = FindObjectsOfType<Box>();
                    foreach (var box in boxes)
                    {
                        if (box.transform.position.x == (prevplay.transform.position.x + vec.x))
                        {
                            if (box.transform.position.y == (prevplay.transform.position.y + vec.y))
                            {
                                box.transform.Translate(prevxy);
                                GameObject[] goals = GameObject.FindGameObjectsWithTag("goal");
                                foreach (var goal in goals)
                                {
                                    if (box.transform.position.x == goal.transform.position.x && box.transform.position.y == goal.transform.position.y)
                                    {
                                        box.GetComponent<SpriteRenderer>().color = Color.red;
                                    }
                                    else
                                    {
                                        box.GetComponent<SpriteRenderer>().color = Color.white;
                                    }
                                }
                            }
                        }
                    }
                    m_Player.Move(prevxy);
                    step1--;
                    break;
                }
                walked++;
            }
        }
    }

    public void AutomateSecond()
    {
        StartCoroutine(Automate2());
        AutoPlay.interactable = false;
        FastForward.interactable = false;
        Backward.interactable = false;
    }

    public void pauseSecond()
    {
        pause = true;
        AutoPlay.interactable = true;
        FastForward.interactable = false;
        Backward.interactable = false;
    }

    public void FastForward2()
    {
        Vector2[] vecArray =
        {
            new Vector2(1,0),
            new Vector2(-1,0),
            new Vector2(-1,0),
            new Vector2(1,0),
            new Vector2(0,-1),
            new Vector2(0,1),
            new Vector2(0,1)
        };
        Vector2 move = vecArray[step2];
        m_Player.Move(move);
        step2++;
    }

    public void backward2()
    {
        int[] collide = new int[8] { 1, 0, 1, 0, 1, 0, 1, 0 };
        Vector2[] vecArray =
        {
            new Vector2(1,0),
            new Vector2(-1,0),
            new Vector2(-1,0),
            new Vector2(1,0),
            new Vector2(0,-1),
            new Vector2(0,1),
            new Vector2(0,1)
        };
        int walked = 0;
        if (step2 > 0)
        {
            Vector2 prevxy = Vector2.zero;

            foreach (var vec in vecArray)
            {
                if (walked == step2 - 1)
                {
                    prevxy.x -= vec.x;
                    prevxy.y -= vec.y;
                    prevplay = FindObjectOfType<player>();

                    Box[] boxes = FindObjectsOfType<Box>();
                    foreach (var box in boxes)
                    {
                        if (box.transform.position.x == (prevplay.transform.position.x + vec.x)&& collide[step2 - 1] == 1)
                        {
                            if (box.transform.position.y == (prevplay.transform.position.y + vec.y))
                            {
                                box.transform.Translate(prevxy);
                                GameObject[] goals = GameObject.FindGameObjectsWithTag("goal");
                                foreach (var goal in goals)
                                {
                                    if ((box.transform.position.x == goal.transform.position.x)&&(box.transform.position.y == goal.transform.position.y))
                                    {
                                        box.GetComponent<SpriteRenderer>().color = Color.red;
                                    }
                                    else
                                    {
                                        box.GetComponent<SpriteRenderer>().color = Color.white;
                                    }
                                }
                            }
                        }
                    }
                    m_Player.Move(prevxy);
                    step2--;
                    break;
                }
                walked++;
            }
        }
    }

    public void AutomateThird()
    {
        StartCoroutine(Automate3());
        AutoPlay.interactable = false;
        FastForward.interactable = false;
        Backward.interactable = false;
    }

    public void pauseThird()
    {
        pause = true;
        AutoPlay.interactable = true;
        FastForward.interactable = false;
        Backward.interactable = false;
    }

    public void FastForward3()
    {
        Vector2[] vecArray =
        {
            new Vector2(-1,0),
            new Vector2(1,0),
            new Vector2(0,-1),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(-1,0),
            new Vector2(0,-1),
            new Vector2(-1,0),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(-1,0)
        };
        Vector2 move = vecArray[step3];
        m_Player.Move(move);
        step3++;
    }

    public void backward3()
    {
        int[] collide = new int[15] { 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1 };
        Vector2[] vecArray =
        {
            new Vector2(-1,0),
            new Vector2(1,0),
            new Vector2(0,-1),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(-1,0),
            new Vector2(0,-1),
            new Vector2(-1,0),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(-1,0)
        };

        int walked = 0;
        if (step3 > 0)
        {
            Vector2 prevxy = Vector2.zero;

            foreach (var vec in vecArray)
            {
                if (walked == step3 - 1)
                {
                    prevxy.x -= vec.x;
                    prevxy.y -= vec.y;
                    prevplay = FindObjectOfType<player>();

                    Box[] boxes = FindObjectsOfType<Box>();
                    foreach (var box in boxes)
                    {
                        if (box.transform.position.x == (prevplay.transform.position.x + vec.x) && collide[step3 - 1] == 1)
                        {
                            if (box.transform.position.y == (prevplay.transform.position.y + vec.y))
                            {
                                box.transform.Translate(prevxy);
                                GameObject[] goals = GameObject.FindGameObjectsWithTag("goal");
                                foreach (var goal in goals)
                                {
                                    if ((box.transform.position.x == goal.transform.position.x) && (box.transform.position.y == goal.transform.position.y))
                                    {
                                        box.GetComponent<SpriteRenderer>().color = Color.red;
                                    }
                                    else
                                    {
                                        box.GetComponent<SpriteRenderer>().color = Color.white;
                                    }
                                }
                            }
                        }
                    }
                    m_Player.Move(prevxy);
                    step3--;
                    break;
                }
                walked++;
            }
        }
    }

    public void AutomateForth()
    {
        StartCoroutine(Automate4());
        AutoPlay.interactable = false;
        FastForward.interactable = false;
        Backward.interactable = false;
    }

    public void pauseForth()
    {
        pause = true;
        AutoPlay.interactable = true;
        FastForward.interactable = false;
        Backward.interactable = false;
    }

    public void FastForward4()
    {
        Vector2[] vecArray =
        {
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(1,0),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(0,-1),
            new Vector2(0,1),
            new Vector2(0,1),
            new Vector2(0,1),
            new Vector2(0,1),
            new Vector2(-1,0),
            new Vector2(0,-1),
            new Vector2(-1,0),
            new Vector2(0,-1),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(0,-1),
            new Vector2(0,-1),
            new Vector2(-1,0),
            new Vector2(0,-1),
            new Vector2(-1,0),
            new Vector2(-1,0),
            new Vector2(0,1),
            new Vector2(0,1)
        };
        Vector2 move = vecArray[step4];
        m_Player.Move(move);
        step4++;
    }

    public void backward4()
    {
        int[] collide = new int[27] { 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1 };
        Vector2[] vecArray =
        {
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(1,0),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(0,-1),
            new Vector2(0,1),
            new Vector2(0,1),
            new Vector2(0,1),
            new Vector2(0,1),
            new Vector2(-1,0),
            new Vector2(0,-1),
            new Vector2(-1,0),
            new Vector2(0,-1),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(0,-1),
            new Vector2(0,-1),
            new Vector2(-1,0),
            new Vector2(0,-1),
            new Vector2(-1,0),
            new Vector2(-1,0),
            new Vector2(0,1),
            new Vector2(0,1)
        };

        int walked = 0;
        if (step4 > 0)
        {
            Vector2 prevxy = Vector2.zero;
            foreach (var vec in vecArray)
            {
                if (walked == step4 - 1)
                {
                    prevxy.x -= vec.x;
                    prevxy.y -= vec.y;
                    prevplay = FindObjectOfType<player>();

                    Box[] boxes = FindObjectsOfType<Box>();
                    foreach (var box in boxes)
                    {
                        if (box.transform.position.x == (prevplay.transform.position.x + vec.x) && collide[step4 - 1] == 1)
                        {
                            if (box.transform.position.y == (prevplay.transform.position.y + vec.y))
                            {
                                box.transform.Translate(prevxy);
                                GameObject[] goals = GameObject.FindGameObjectsWithTag("goal");
                                foreach (var goal in goals)
                                {
                                    if (box.transform.position.x == (prevplay.transform.position.x + vec.x))
                                    {
                                        if (box.transform.position.y == goal.transform.position.y)
                                        {
                                            box.GetComponent<SpriteRenderer>().color = Color.red;
                                        }
                                        else
                                        {
                                            box.GetComponent<SpriteRenderer>().color = Color.white;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Box2[] boxes2 = FindObjectsOfType<Box2>();
                    foreach (var box2 in boxes2)
                    {
                        if (box2.transform.position.x == (prevplay.transform.position.x + vec.x) && collide[step4 - 1] == 1)
                        {
                            if (box2.transform.position.y == (prevplay.transform.position.y + vec.y))
                            {

                                box2.transform.Translate(prevxy);
                                GameObject[] goal2 = GameObject.FindGameObjectsWithTag("goal2");
                                foreach (var goal in goal2)
                                {
                                    if (box2.transform.position.x == goal.transform.position.x + vec.x && box2.transform.position.y == goal.transform.position.y)
                                    {
                                        box2.GetComponent<SpriteRenderer>().color = Color.yellow;
                                    }
                                    else
                                    {
                                        box2.GetComponent<SpriteRenderer>().color = Color.white;
                                    }
                                        
                                }
                            }
                        }
                    }
                    m_Player.Move(prevxy);
                    step4--;
                    break;
                }
                walked++;
            }
        }
    }

    private IEnumerator Automate1()
    {
        Vector2 vec = new Vector2(1, 0);
        m_Player.Move(vec);
        yield return new WaitForSeconds(1);
        pause = false;
    }

    private IEnumerator Automate2()
    {
        Vector2[] vecArray =
        {
            new Vector2(1,0),
            new Vector2(-1,0),
            new Vector2(-1,0),
            new Vector2(1,0),
            new Vector2(0,-1),
            new Vector2(0,1),
            new Vector2(0,1)
        };
        int walk = 0;
        foreach (var vec in vecArray)
        {
            walk++;
            if (walk > step2)
            {
                if (pause == true) { break; }
                m_Player.Move(vec);
                Debug.Log(vec);
                yield return new WaitForSeconds(1);
            }
        }
        pause = false;
    }

    private IEnumerator Automate3()
    {
        Vector2[] vecArray =
        {
            new Vector2(-1,0),
            new Vector2(1,0),
            new Vector2(0,-1),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(-1,0),
            new Vector2(0,-1),
            new Vector2(-1,0),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(-1,0)
        };
        int walk = 0;
        foreach (var vec in vecArray)
        {
            walk++;
            if (walk > step3)
            {
                if (pause == true) { break; }
                m_Player.Move(vec);
                Debug.Log(vec);
                yield return new WaitForSeconds(1);
            }
        }
        pause = false;
    }

    private IEnumerator Automate4()
    {
        Vector2[] vecArray =
        {
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(1,0),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(0,-1),
            new Vector2(0,1),
            new Vector2(0,1),
            new Vector2(0,1),
            new Vector2(0,1),
            new Vector2(-1,0),
            new Vector2(0,-1),
            new Vector2(-1,0),
            new Vector2(0,-1),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(0,-1),
            new Vector2(0,-1),
            new Vector2(-1,0),
            new Vector2(0,-1),
            new Vector2(-1,0),
            new Vector2(-1,0),
            new Vector2(0,1),
            new Vector2(0,1)
        };
        int walk = 0;
        foreach (var vec in vecArray)
        {
            walk++;
            if (walk > step4)
            {
                if (pause == true) { break; }
                m_Player.Move(vec);
                Debug.Log(vec);
                yield return new WaitForSeconds(1);
            }
        }
        pause = false;
    }

    public void NextLevel()
    {
        LevelCompleted.SetActive(false);
        m_LevelBuilder.NextLevel();
        StartCoroutine(ResetSceneAsync());
    }

    public void ResetScene()
    {
        AutoPlay.interactable = true;
        FastForward.interactable = true;
        Backward.interactable = true;
        StopPlay.interactable = true;
        step1 = 0;
        step2 = 0;
        step3 = 0;
        step4 = 0;
        LevelCompleted.SetActive(false);
        StartCoroutine(ResetSceneAsync());
    }

    public bool IsLevelComplete()
    {
        Box[] boxes = FindObjectsOfType<Box>();
        foreach (var box in boxes)
        {
            if (!box.m_OnGoal)
            {
                return false;
            }
        }
        Box2[] boxes2 = FindObjectsOfType<Box2>();
        foreach (var box in boxes2)
        {
            if (!box.m_OnGoal2)
            {
                return false;
            }
        }
        return true;
    }

    public void OnExitPressed()
    {
        SceneManager.LoadScene("Main");
    }

    IEnumerator ResetSceneAsync()
    {
        if(SceneManager.sceneCount > 1)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("LevelScene");
            while (!asyncUnload.isDone)
            {
                yield return null;
                Debug.Log("Unloading...");
            }
            Debug.Log("Unload Done!");
            Resources.UnloadUnusedAssets();
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelScene", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
            Debug.Log("Loading...");
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));
        m_LevelBuilder.Build();
        m_Player = FindObjectOfType<player>();
        Debug.Log("level loaded");
    }
}