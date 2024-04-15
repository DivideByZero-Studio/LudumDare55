using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PeopleMoodVisuals : MonoBehaviour
{
    [SerializeField] private List<Image> _moods;
    [SerializeField, Range(0, 1)] private float _firstMoodThreashold;
    [SerializeField, Range(0, 1)] private float _secondMoodThreashold;
    [SerializeField, Range(0, 1)] private float _thirdMoodThreashold;

    [SerializeField] private List<Image> _arrows;

    [Inject] private PeopleLikeService _peopleLikeService;

    private float _currentMoodValue => _peopleLikeService.CurrentLikes * 1f / _peopleLikeService.MaxLikes;
    private float _lateMoodValue = 1f;

    private void Start()
    {
        SetMoodImage();
        SetArrowImage();
    }

    private void SetMoodImage()
    {
        if (_currentMoodValue <= _thirdMoodThreashold)
        {
            _moods[0].enabled = false;
            _moods[1].enabled = false;
            _moods[2].enabled = true;
            return;
        }
        if (_currentMoodValue <= _secondMoodThreashold)
        {
            _moods[0].enabled = false;
            _moods[1].enabled = true;
            _moods[2].enabled = false;
            return;
        }
        _moods[0].enabled = true;
        _moods[1].enabled = false;
        _moods[2].enabled = false;
        return;
    }

    private void SetArrowImage()
    {
        if(_currentMoodValue < _lateMoodValue)
        {
            _arrows[0].enabled = false;
            _arrows[1].enabled = true;
        }
        else
        {
            _arrows[1].enabled = false;
            _arrows[0].enabled = true;
        }
        _lateMoodValue = _currentMoodValue;
    }

    private void OnEnable()
    {
        _peopleLikeService.OnLikesChanged += SetMoodImage;
        _peopleLikeService.OnLikesChanged += SetArrowImage;
    }

    private void OnDisable()
    {
        _peopleLikeService.OnLikesChanged -= SetMoodImage;
        _peopleLikeService.OnLikesChanged -= SetArrowImage;
    }

}
