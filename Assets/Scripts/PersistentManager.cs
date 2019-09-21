using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentManager : MonoBehaviour
{
    public string TitleScene;
    public int score, playerHealth=10;
    public static PersistentManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(TitleScene);
    }

}
