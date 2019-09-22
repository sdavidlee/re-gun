using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Weapon class should also contain info about its physical and magic damage and unique skills 
    [SerializeField] private Transform _handTransform;
    public Transform HandTransform { get => _handTransform; }
}
