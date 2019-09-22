using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    #region Singleton
    private static PlayersManager _instance;
    public static PlayersManager Instance { get { return _instance; } }
    #endregion

    public List<Player> Players { get; private set; }

    private void Awake()
    {
        #region Singlton Check
        if (_instance != null && _instance != this)
            Destroy(_instance);
        else
            _instance = this;
        #endregion

        Players = FindObjectsOfType<Player>().ToList();

        DontDestroyOnLoad(this);
    }

    public void AddPlayer(Controller controller)
    {
        Players
            .FirstOrDefault(p => !p.IsAdded)
            .InitializePlayer(controller);
    }
}