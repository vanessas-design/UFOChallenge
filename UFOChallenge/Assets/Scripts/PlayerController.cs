using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public Text livesText;
    public Text restartText;

    private Rigidbody2D rb2d;
    private int count;
    private int lives;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        winText.text = "";
        restartText.text = "";
        SetCountText();
        SetLivesText();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
        }
        if (count == 12)
        {
            transform.position = new Vector2(82.7f, 7.2f);
        }

    }
    void SetCountText()
    {
        countText.text = "Count:" + count.ToString();
        if (count >= 20)
        {
            winText.text = "You win! Game created by Vanessa Seymour!";
        }
    }
    void SetLivesText()
    {
        livesText.text = "Lives:" + lives.ToString();
        if (lives == 0)
        {
            winText.text = "Game Over!";
            DestroyScriptInstance();
            restartText.text = "Press 'R' to try again";
        }
    }
    void DestroyScriptInstance()
    {
        Destroy(this.gameObject);
    }
    
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(0);
        }
    }

}

