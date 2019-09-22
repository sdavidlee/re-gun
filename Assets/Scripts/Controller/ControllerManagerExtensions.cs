using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEngine.Object;

public static class ControllerManagerExtensions
{
    public static ControllerManager GetControllers(this ControllerManager manager)
    {
        manager.Controllers = FindObjectsOfType<Controller>().ToList();

        return manager;
    }

    public static ControllerManager AssignControllersIndexes(this ControllerManager manager)
    {
        int index = 0;
        
        foreach (var controller in manager.Controllers)
        {
            controller.SetIndex(index);
            index++;
        }

        return manager.HaveControllersSubscribed();
    }

    private static ControllerManager HaveControllersSubscribed(this ControllerManager manager)
    {
        foreach (var controller in manager.Controllers)
            manager.ControllersAssigned += controller.OnControllerAssigned;
 
        return manager;
    }
}
