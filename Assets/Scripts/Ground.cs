using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void FixedUpdate()
    {
        transform.position -= new Vector3(gameManager.speed * Time.deltaTime, 0);
    }
}
