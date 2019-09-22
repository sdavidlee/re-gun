using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    #region Singleton
    private static ControllerManager _instance;
    public static ControllerManager Instance { get { return _instance; } }
    #endregion

    public event Action ControllersAssigned;
    public List<Controller> Controllers { get; set; }

    private void Awake()
    {
        #region Singleton Instantiation
        if (_instance != null && _instance != this)
            Destroy(_instance);
        else
            _instance = this;
        #endregion

        this
            .GetControllers()
            .AssignControllersIndexes()
            .OnControllersAssigned();

        DontDestroyOnLoad(this);
    }

    public void OnControllersAssigned()
    {
        ControllersAssigned?.Invoke();
    }
}
