using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float speed;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        transform.position -= new Vector3(speed * Time.deltaTime, 0);

        if (transform.position.x < player.transform.position.x - 5)
        {
            Destroy(this.gameObject);
        }
    }
}
