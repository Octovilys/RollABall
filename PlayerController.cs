using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI livesText;

    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject player;
    public GameObject livesTextObject;

    public Transform RestartPos;  

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private int lives;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        lives = 3;


        SetCountText();
        winTextObject.SetActive(false);

        SetLivesText();
        loseTextObject.SetActive(false);

    }


    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 20)
        {
            winTextObject.SetActive(true);
        }
        else if (lives == 0)
        {
            loseTextObject.SetActive(true);
        }
    }

    void Update()
    {
         if (lives == 0)
        { 
        transform.position = RestartPos.position;
        }
    }
    
    void FixedUpdate()
    {
        
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

    }


    void OnTriggerEnter(Collider other)
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
            SetCountText();
        }
        SetLivesText();

            if (count == 12)
        {
            transform.position = new Vector3(23.0f, 0.0f, 1.0f);
        }
    }

   public void TakeDamage(int d)
   {
       lives -= d;
   }  
}
