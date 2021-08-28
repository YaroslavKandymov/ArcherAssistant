using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(ArcherAssistant))]
[RequireComponent(typeof(Animator))]
public class PlayerArrowCollector : MonoBehaviour, ICollector
{
    [SerializeField] private float seconds;

    private ArcherAssistant _archerAssistant;
    private Animator _animator;
    private readonly List<Arrow> _arrows = new List<Arrow>();

    private void Start()
    {
        _archerAssistant = GetComponent<ArcherAssistant>();
        _animator = GetComponent<Animator>();
    }

    public void Collect(Arrow arrow)
    {

    }
}
