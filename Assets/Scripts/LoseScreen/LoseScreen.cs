using TMPro;
using UnityEngine;
using Zenject;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI highscoreLabel;
    [SerializeField] private GameObject screenElements;
    [SerializeField] private AudioClip loseSound;

    [Inject] private PeopleLikeService _peopleLikeService;
    [Inject] private SceneLoaderService _sceneLoaderService;
    [Inject] private AudioService _audioService;
    [Inject] private ScoreCounterService _scoreCounterService;

    private void Start()
    {
        _peopleLikeService.OnZeroLikes += Show;
    }

    private void Show()
    {
        var score = _scoreCounterService.Score;
        var highscore = PlayerPrefs.GetInt("Highscore", score);

        screenElements.SetActive(true);
        scoreLabel.text = $"SCORE: {score}";
        highscoreLabel.text = $"HIGHSCORE: {highscore}";

        _audioService.StopMusic();
        _audioService.StopAmbient();
        _audioService.PlaySFX(loseSound);
    }

    public void OnRestartButtonPressed()
    {
        _sceneLoaderService.ReloadScene();
    }

    public void OnMenuButtonPressed()
    {
        _sceneLoaderService.LoadScene("MainMenu");
    }
}
