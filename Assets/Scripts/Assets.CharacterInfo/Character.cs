﻿using System;
using UnityEngine;
using Assets.Fundamentals.Extensions;
using Assets.GameUI;

namespace Assets.CharacterInfo
{
    [Flags]
    public enum PlayerAnimations
    {
        None = 1,
        TakingHit = 2,
        Hitting = 4,
        Dead = 8,
        UsingSkill = 16,
        InstallingMine = 32,
        Retrieving = 64
    }
    public enum FacingDirection { Front, Back, SideUp, SideDown }

    [RequireComponent(typeof(PlayerActions))]
    [RequireComponent(typeof(PlayerStats))]
    public class Character : MonoBehaviour
    {
        public event Action PlayerDied;

        [SerializeField] private GameObject _minePrefab;
        public GameObject MinePrefab { get => _minePrefab; }
        [SerializeField] private Transform _minePosition;
        public Vector3 MinePosition { get => _minePosition.position; }
        [SerializeField] private Transform _center;
        public Transform Center { get => _center;}
        public PlayerAnimations CurrentAnimation { get; set; } = PlayerAnimations.None;
        public PlayerStats Stats { get; private set; }
        public PlayerActions Actions { get; private set; }
        public Vector3 Position { get => transform.position.FlatOut(); }
        public Animator Animator { get; private set; }
        private Weapon Weapon { get; set; }
         
        public static Character ThePlayer { get; private set; }

        private void Awake()
        {
            ThePlayer = this;

            Actions = GetComponent<PlayerActions>();
            Animator = GetComponent<Animator>();
            Stats = GetComponent<PlayerStats>();
            Weapon = GetComponentInChildren<Weapon>();

            //PlayerDied += FindObjectOfType<UITextManager>().OnPlayerDeath_DisPlayGameOver;

            transform.position = new Vector3(transform.position.x, 0.17f, transform.position.z);
        }

        public void OnPlayerDeath()
        {
            if (Stats.Health <= 0)
                PlayerDied?.Invoke();
        }
    }
}