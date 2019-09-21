using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonNewScene : MonoBehaviour
{
    public string nextLevel;
    public void onClick()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
