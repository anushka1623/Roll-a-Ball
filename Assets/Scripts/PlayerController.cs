using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float time ;
    private bool check = true;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timeText;
    public GameObject BackToMenuObject;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        time = 25;
        setCountText();
        setTimeText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        InvokeRepeating("setTimeText", 1, 1);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 16 && time>0 && check)
        {
            winTextObject.SetActive(true);
            timeText.gameObject.SetActive(false);
            BackToMenuObject.SetActive(true);
        }
    }

    void setTimeText()
    {
        time--;
        timeText.text = "Time left : " + time.ToString();
        if (time <= 0)
        {
            timeText.gameObject.SetActive(false);
            BackToMenuObject.SetActive(true);
            if (count<16)
            loseTextObject.SetActive(true);

        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            setCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DontPickUp") && count<16)
        {
            loseTextObject.SetActive(true);
            check = false;
            timeText.gameObject.SetActive(false);
            BackToMenuObject.SetActive(true);
        }
    }


}
