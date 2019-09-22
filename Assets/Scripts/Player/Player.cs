using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Assets.Fundamentals;
using Assets.CharacterInfo;

public class Player : MonoBehaviour
{
    public JoinGameText joinGameText;
    public Controller Controller { get; private set; }
    public bool IsAdded { get { return Controller != null; } }
    public SelectionMarker marker { get; private set; }
    public Character character { get; set; }

    public Player InitializePlayer(Controller controller)
    {
        this.Controller = controller;
        gameObject.name = $"Player-{controller.name}";

        joinGameText = FindObjectsOfType<JoinGameText>().FirstOrDefault(t => t.objectNumber == this.Controller.Index);
        StartCoroutine(
            joinGameText.PlayerHasJoined(player: this));

        marker = FindObjectsOfType<SelectionMarker>()
                    .First(m => !m.IsAdded)
                    .ActivateMarker(index: controller.Index, player: this);

        return this;
    }
}