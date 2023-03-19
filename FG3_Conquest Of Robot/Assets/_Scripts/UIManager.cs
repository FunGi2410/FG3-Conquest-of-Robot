using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject gameWonUI;

    private void Start()
    {
        FindObjectOfType<Player>().OnDeath += OnGameOver;
        FindObjectOfType<Spawner>().OnFireAllEnemy += OnGameWon;
    }

    void OnGameOver()
    {
        this.gameOverUI.SetActive(true);
    }

    void OnGameWon()
    {
        this.gameWonUI.SetActive(true);
    }

    // UI Input
    public void OnPlayAgainBnt()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // UI Level
    public void ToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToPrevLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
