using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform spawner;
    [SerializeField] private GameObject[] catcti;
    [SerializeField] private GameObject bird;

    [SerializeField] private float speed = 5;
    [SerializeField] private float spawningSpeed = 5;
    [SerializeField] private bool alive;

    private void Start()
    {
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
}
