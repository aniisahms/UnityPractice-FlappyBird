using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpForce;
    public GameObject loseScreenUI;
    public int score, highScore;
    public Text scoreUI, highScoreUI;
    string storeHighScore = "high_score";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt(storeHighScore);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();
    }

    void PlayerJump()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.singleton.PlaySound(0); // play jump sfx
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    void PlayerLose()
    {
        AudioManager.singleton.PlaySound(1); // play lose sfx

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(storeHighScore, highScore);
        }
        
        highScoreUI.text = "High Score: " + highScore.ToString();
        
        loseScreenUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void AddScore()
    {
        AudioManager.singleton.PlaySound(2); // play score sfx

        score++;
        scoreUI.text = score.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            PlayerLose();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Score"))
        {
            AddScore();
        }
    }
}
