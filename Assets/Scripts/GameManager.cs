using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;

    [SerializeField] private Transform spawner;
    [SerializeField] private GameObject[] catcti;
    [SerializeField] private GameObject bird;

    [SerializeField] public float speed = 5;
    [SerializeField] private float spawningSpeed = 5;
    [SerializeField] private bool alive;

    private void Start()
    {
        alive = true;

        StartCoroutine(Spawn());
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

    public void GameOver()
    {
        alive = false;
        Time.timeScale = 0f;

        gameOver.SetActive(true);

        print("Game Over!");

        StartCoroutine(NewGame());
    }

    IEnumerator NewGame()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
