using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelectScript : MonoBehaviour
{
    public void selectScene()
    {
        switch (this.gameObject.name)
        {
            case "Level1":
                SceneManager.LoadScene("GameScene1");
                break;
            case "Level2":
                SceneManager.LoadScene("GameScene2");
                break;
            case "Level3":
                SceneManager.LoadScene("GameScene3");
                break;
            case "Level4":
                SceneManager.LoadScene("GameScene4");
                break;
        }
    }
}
