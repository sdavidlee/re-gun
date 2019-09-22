using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static UnityEngine.SceneManagement.SceneManager;

public class Start : MonoBehaviour
{
    private List<Controller> controllers;
    private bool activated;

    private void Awake()
    {
        activated = false;
    }

    void Update()
    {
        if (!activated)
            return;

        foreach (var controller in controllers)
        {
            if (Input.GetButtonDown(controller.StartKey))
                StartGame();
        }
    }

    public void StartGame()
    {
        var markers = FindObjectsOfType<SelectionMarker>();
        int lockedCount = (from m in markers
                           where m.IsLocked
                           select m)
                          .Count();
        if (markers.Count() != lockedCount){
            Debug.Log("Not all players have locked in");
            return;
        }

        StartCoroutine(StartGameCoroutine());

        //local methods
        IEnumerator StartGameCoroutine()
        {
            Camera.main.GetComponent<AudioListener>().enabled = false;

            AsyncOperation operation = LoadSceneAsync("Level 1", LoadSceneMode.Single);
            while (operation.isDone == false)
                yield return null;
        }
    }

    public Start Activate()
    {
        GetComponent<TextMeshProUGUI>().text = "Start Game!";
        controllers =
            FindObjectsOfType<Player>()
                .Where(p => p.IsAdded)
                .Select(p => p.Controller)
                .ToList();
        activated = true;

        return this;
    }
}
