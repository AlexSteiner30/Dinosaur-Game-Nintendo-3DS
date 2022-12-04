using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;

    private void FixedUpdate()
    {
        transform.position -= new Vector3(gameManager.GetComponent<GameManager>().speed * Time.deltaTime, 0);
    }
}
