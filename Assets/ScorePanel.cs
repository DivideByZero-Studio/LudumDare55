using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _addingScoreText;
    [SerializeField] private AnimationClip _addingScoreAnimation;
    [SerializeField] private float _animationDuration;

    [Inject] private ScoreCounterService _scoreCounterService;

    private int _score = 0;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void StartChangingAnim(int newScore)
    {
        StopAllCoroutines();
        _animator.Play("DefaultState");
        StartCoroutine(AddScoreRoutine(newScore));
    }

    private IEnumerator AddScoreRoutine(int newScore)
    {
        _addingScoreText.text = "+" + (newScore - _score).ToString();
        _animator.Play("AddingScoreAnimation");
        yield return new WaitForSeconds(_animationDuration);
        ChangeScore(newScore);
    }

    private void ChangeScore(int newScore)
    {
        _score = newScore;
        _scoreText.text = newScore.ToString();
    }

    private void OnEnable()
    {
        _scoreCounterService.ScoreChanged += StartChangingAnim;
    }

    private void OnDisable()
    {
        _scoreCounterService.ScoreChanged -= StartChangingAnim;
    }
}
