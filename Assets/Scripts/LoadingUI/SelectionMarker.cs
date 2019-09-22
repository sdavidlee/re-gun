using Assets.Fundamentals;
using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;


public class SelectionMarker : MonoBehaviour
{
    public enum Direction { Left, Right };
    public event Action MarkerAdded;

    public Player CorrespondingPlayer { get; private set; }
    public Controller Controller { get { return CorrespondingPlayer.Controller; } }
    public int Index { get; private set; } = -1;
    public bool IsAdded { get { return Index != -1; } }
    public bool IsLocked { get; private set; }
    private TextMeshProUGUI tmPro;
    private Scroll<MarkerFrame> frames;
    private bool isIdle;

    private void Awake()
    {
        tmPro = GetComponent<TextMeshProUGUI>();
        frames = new Scroll<MarkerFrame>(FindObjectsOfType<MarkerFrame>()
                    .OrderBy(f => f.ObjectNumber)
                    .ToList());
        isIdle = true;

        SubscribeToMarkerAdded();
    }

    private bool canBeLocked = false;
    private void Update()
    {
        if (CorrespondingPlayer != null)
        {
            if (Input.GetAxis(Controller.HorizontalKey) > 0.5f && isIdle)
                StartCoroutine(ScrollTo(Direction.Right));
            else if (Input.GetAxis(Controller.HorizontalKey) < -0.5f && isIdle)
                StartCoroutine(ScrollTo(Direction.Left));
            else if (Input.GetButtonDown(Controller.AttackKey) && canBeLocked)
                Lock();

            canBeLocked = true;
        }
    }

    private IEnumerator ScrollTo(Direction direction)
    {
        switch (direction)
        {
            case Direction.Right:
                MoveCursor(Direction.Right);
                break;
            case Direction.Left:
                MoveCursor(Direction.Left);
                break;
            default:
                break;
        }
        OnMarkerAdded();

        isIdle = false;
        yield return new WaitForSeconds(0.35f);
        isIdle = true;
    }
    private void MoveCursor(Direction direction)
    {
        MarkerFrame frame;
        switch (direction)
        {
            case Direction.Left:
                frame = frames.MoveLeft().GetCurrent();
                transform.SetParent(frame.transform, worldPositionStays: false);
                break;
            case Direction.Right:
                frame = frames.MoveRight().GetCurrent();
                transform.SetParent(frame.transform, worldPositionStays: false);
                break;
            default:
                break;
        }
    }

    public SelectionMarker ActivateMarker(int index, Player player)
    {
        this.Index = index;
        tmPro.text = $"{this.Index + 1}P";

        var frames = FindObjectsOfType<MarkerFrame>().OrderBy(f => f.ObjectNumber);
        this.transform.SetParent(frames.First().transform, worldPositionStays: false);
        OnMarkerAdded();

        this.CorrespondingPlayer = player;        
        
        return this;
    }

    private void SubscribeToMarkerAdded()
    {
        foreach (var frame in frames.Items)
        {
            MarkerAdded += frame.OnMarkerAdded;
        }
    }

    private void OnMarkerAdded()
    {
        MarkerAdded?.Invoke();
    }

    public void Lock()
    {
        CorrespondingPlayer.character = frames.GetCurrent().Character;
        FindObjectOfType<Start>().Activate();
        IsLocked = true;
        enabled = false;
    }
}