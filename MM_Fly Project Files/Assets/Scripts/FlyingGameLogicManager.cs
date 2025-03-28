using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class FlyingGameLogicManager : MonoBehaviour
{
    private bool isGameOver;
    private int playerScore;
    private AudioSource sfxGame;
    public AudioResource sfxScoreInc;
    public AudioResource sfxPlayerDied;
    public Text scoreText;
    public GameObject gameOverScreen;

    void Start()
    {
        sfxGame = GetComponent<AudioSource>();
    }

    [ContextMenu("Increase Score")]
    public void IncrementScore()
    {
        if (!isGameOver)
        {
            playerScore += 1;
            scoreText.text = playerScore.ToString();
            sfxGame.resource = sfxScoreInc;
            sfxGame.Play();
        }
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            gameOverScreen.SetActive(true);
            sfxGame.resource = sfxPlayerDied;
            sfxGame.Play();
        }
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

    public bool GetIsGameOver()
    {
        return isGameOver;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
