using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public AudioSource pickupSound;
    private Rigidbody rb;
    private int count;
    private int pickupCount;
    public Text countText;
    public Text winText;

    void Start(){
        rb = GetComponent<Rigidbody>();
        winText.text = "";
        count = 0;
        pickupCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        pickupSound = GetComponent<AudioSource>();
        SetCountText();
    }

    //For Physics updates?
    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            //
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            pickupSound.Play();
            SetWinText();
            SetLevel();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
    }

    void SetCountText(){
        countText.text = "Count : " + count.ToString();
    }

    void SetWinText(){
        if (pickupCount == count){
            winText.text = "You win!";
        }
    }

    void SetLevel(){
        if (pickupCount == count){
            Invoke("nextLevel", 5f);
        }
    }

    void nextLevel(){
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        int nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneToLoad <= totalScenes - 1)
        {
            SceneManager.LoadScene(nextSceneToLoad);
        }

    }
}


