using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Archer : MonoBehaviour
{
    [SerializeField] private Quiver _quiver;
    [SerializeField] private TMP_Text _text;

    public event Action ArrowsIncreased;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if(touchPosition == transform.position)
                _text.gameObject.SetActive(true);
        }
    }

    public void TakeArrows(IEnumerable<Arrow> arrows)
    {
        if(arrows == null)
            throw new NullReferenceException(arrows.ToString());

        _quiver.Add(arrows);
        ArrowsIncreased?.Invoke();
        _animator.Play(ArcherAnimatorController.States.TakeArrow);
    }
}
