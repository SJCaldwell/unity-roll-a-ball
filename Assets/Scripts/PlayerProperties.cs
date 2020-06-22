using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerProperties : MonoBehaviour
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
            gameObject.GetComponent<PlayerController>().enabled = false;
            winText.text = "rip lmao";
            StartCoroutine(WaitAndInvoke(5, levelReload));
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
        Debug.Log("do i even get seen");
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        int nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneToLoad <= totalScenes - 1)
        {
            SceneManager.LoadScene(nextSceneToLoad);
        }

    }

    void levelReload(){
        Debug.Log("Trying to reload");
        int sceneToLoad = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneToLoad);
    }

    delegate void InvokedFunction();

     IEnumerator WaitAndInvoke(float secondsToWait, InvokedFunction func) {
     yield return new WaitForSeconds(secondsToWait);
     func();
}


}