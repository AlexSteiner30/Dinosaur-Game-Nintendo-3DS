using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreTxt;
    [SerializeField] private Text highScoreTxt;
    [SerializeField] private GameObject gameOver;

    [SerializeField] private Transform spawner;
    [SerializeField] private GameObject[] catcti;
    [SerializeField] private GameObject bird;

    [SerializeField] public float speed = 5;
    [SerializeField] private float spawningSpeed = 5;
    [SerializeField] private int score = 0;
    [SerializeField] private bool alive;

    private void Start()
    {
        alive = true;

        int highScore = PlayerPrefs.GetInt("HighScore");

        if (highScore <= 9)
            highScoreTxt.text = "0000" + highScore.ToString();
        else if (highScore >= 10 && highScore <= 99)
            highScoreTxt.text = "000" + highScore.ToString();
        else if (highScore >= 100 && highScore <= 999)
            highScoreTxt.text = "00" + highScore.ToString();
        else if (highScore >= 1000 && highScore <= 9999)
            highScoreTxt.text = "0" + highScore.ToString();
        else if (highScore >= 10000 && highScore <= 99999)
            highScoreTxt.text = highScore.ToString();


        UnityEngine.N3DS.Keyboard.SetType(N3dsKeyboardType.Qwerty);

        StartCoroutine(Spawn());
        StartCoroutine(Score());
    }

    IEnumerator Spawn()
    {
        while (alive)
        {
            GameObject instatiate;

            for (int i = 0; i < Random.Range(5, 10); i++)
            {
                instatiate = Instantiate(catcti[Random.Range(0, catcti.Length)], new Vector3(spawner.position.x, spawner.position.y, 0), Quaternion.identity);
                instatiate.GetComponent<Enemy>().speed = speed;

                yield return new WaitForSeconds(spawningSpeed);
            }

            instatiate = Instantiate(bird, new Vector3(spawner.position.x, Random.Range(0, 5.5f), 0), Quaternion.identity);
            instatiate.GetComponent<Enemy>().speed = speed;

            yield return new WaitForSeconds(spawningSpeed);
        }
    }

    IEnumerator Score()
    {
        while(alive)
        {
            yield return new WaitForSeconds(0.1f);
            score++;

            if(score <= 9)
                scoreTxt.text = "0000" + score.ToString();
            else if(score >= 10 && score <= 99)
                scoreTxt.text = "000" + score.ToString();
            else if (score >= 100 && score <= 999)
                scoreTxt.text = "00" + score.ToString();
            else if (score >= 1000 && score <= 9999)
                scoreTxt.text = "0" + score.ToString();
            else if (score >= 10000 && score <= 99999)
                scoreTxt.text = score.ToString();
        }
    }

    public void GameOver()
    {
        alive = false;
        Time.timeScale = 0f;

        gameOver.SetActive(true);


        try
        {
            int highScore = PlayerPrefs.GetInt("HighScore");

            if(score > highScore)
            {
                PlayerPrefs.SetInt("HighScore", score);

                highScore = PlayerPrefs.GetInt("HighScore");

                if (highScore <= 9)
                    highScoreTxt.text = "0000" + highScore.ToString();
                else if (highScore >= 10 && highScore <= 99)
                    highScoreTxt.text = "000" + highScore.ToString();
                else if (highScore >= 100 && highScore <= 999)
                    highScoreTxt.text = "00" + highScore.ToString();
                else if (highScore >= 1000 && highScore <= 9999)
                    highScoreTxt.text = "0" + highScore.ToString();
                else if (highScore >= 10000 && highScore <= 99999)
                    highScoreTxt.text = highScore.ToString();
            }
        }

        catch
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        print("Game Over!");

        StartCoroutine(NewGame());
    }

    IEnumerator NewGame()
    {
        while (!Input.GetKeyDown(KeyCode.Space) || !UnityEngine.N3DS.GamePad.GetButtonHold(N3dsButton.X))
        {
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 2f;
    }
}
