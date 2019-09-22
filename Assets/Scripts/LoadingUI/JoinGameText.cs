using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JoinGameText : MonoBehaviour
{
    private TextMeshProUGUI tmPro;

    public int objectNumber;

    private void Awake()
    {
        tmPro = GetComponent<TextMeshProUGUI>();
    }   

    public IEnumerator PlayerHasJoined(Player player)
    {
        tmPro.text = $"Player {player.Controller.Index + 1} Has Joined the Game!";

        yield return new WaitForSeconds(3f);

        tmPro.text = string.Empty;
    }
}