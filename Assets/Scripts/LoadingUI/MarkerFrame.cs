using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.CharacterInfo;

public class MarkerFrame : MonoBehaviour
{
    public int ObjectNumber { get; }

    [SerializeField] private int objectNumber;
    [SerializeField] private Character _character;
    public Character Character { get { return _character; } }
    private IEnumerable<SelectionMarker> markers;

    public void OnMarkerAdded()
    {
        IList<SelectionMarker> markers = GetComponentsInChildren<SelectionMarker>().OrderBy(m => m.Index).ToList();

        for (int i = 0; i < markers.Count(); i++)
            markers[i].transform.SetSiblingIndex(i);
    }
}
