using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoosterTextView : MonoBehaviour
{
    [SerializeField] private List<BoosterImage> _boosterImages;
    [SerializeField] private Vector3 _targetScale;
    [SerializeField] private float _targetHeight;
    [SerializeField] private float _duration; 
    
    private ArrowBooster[] _boosters;
    private Transform _scaleObject;

    private void Awake()
    {
        _boosters = GetComponentsInChildren<ArrowBooster>();
    }

    private void OnEnable()
    {
        foreach (var booster in _boosters)
        {
            booster.Taken += OnTaken;
        }
    }

    private void OnDisable()
    {
        foreach (var booster in _boosters)
        {
            booster.Taken -= OnTaken;
        }
    }

    private void OnTaken(ArrowBooster booster)
    {
        foreach (var boosterImage in _boosterImages)
        {
            if (boosterImage.BoostNumber == booster.Coefficient)
            {
                _scaleObject = boosterImage.ScaleObject.gameObject.transform;
                _scaleObject.DOScale(_targetScale, _duration).OnComplete(() => _scaleObject.transform.localScale = boosterImage.StartScale);
                _scaleObject.DOLocalMoveY(_targetHeight, _duration).OnComplete(() => _scaleObject.transform.localPosition = boosterImage.StartPosition);
            }
        }
    }

    [Serializable]
    public class BoosterImage
    {
        public int BoostNumber;
        public Vector3 StartScale;
        public Vector3 StartPosition;
        public GameObject ScaleObject;
    }
}
