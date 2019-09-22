using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    #region Variables
    public int Index { get; set; }
    public string AttackKey { get; private set; }
    public string HorizontalKey { get; private set; }
    public string VerticalKey { get; private set; }
    public string SelectKey { get; private set; }
    public string StartKey { get; private set; }
    #endregion

    public void SetIndex(int index)
    {
        this.Index = index;
        AttackKey = "Attack" + this.Index;
        HorizontalKey = "Horizontal" + this.Index;
        VerticalKey = "Vertical" + this.Index;
        StartKey = "Start" + this.Index;
        SelectKey = "Select" + this.Index;
    }

    public void OnControllerAssigned()
    {
        Debug.Log($"Controller {Index} has been assigned ");
        gameObject.name = "Controller" + Index;
    }
}
