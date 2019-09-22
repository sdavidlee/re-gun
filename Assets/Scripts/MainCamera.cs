using Assets.Supervisor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.CharacterInfo.Character;

public class MainCamera : MonoBehaviour
{
    private Vector3 Offset { get; set; } = new Vector3(0, 8.3f, -11.3f) * 1.3f;

    private void Awake()
    {
        GameManager.Instance.GlobalLateUpdate += Behavior;
    }

    private void Behavior()
    {
        var targetPos = ThePlayer.Position + Offset;
        var smooth = 8f;
        transform.position = Vector3.Slerp(transform.position, targetPos, smooth * Time.deltaTime);
    }
}
