using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpHeight;

    [SerializeField] private bool isGrounded;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            StartCoroutine(PlayJump());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        GetComponent<AudioSource>().mute = true;

        if (collision.gameObject.tag == "Obstacle")
        {
            gameManager.GameOver();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    IEnumerator PlayJump()
    {
        GetComponent<AudioSource>().mute = false;
        yield return new WaitForSecondsRealtime(0.5f);
        GetComponent<AudioSource>().mute = true;
    }
}
