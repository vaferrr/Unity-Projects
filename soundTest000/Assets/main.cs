using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource fire;
    public GameObject ball;
    public GameObject menu;
    public GameObject deathScr;
    int health = 3;
    int ballsLenght;

    public void ToggleMute(bool toggle)
    {
        if (toggle) mixer.SetFloat("MasterVolume", -80);
        else mixer.SetFloat("MasterVolume", 0);
    }
    public void VolumeChangeSound(float value)
    {
        mixer.SetFloat("SoundVolume", Mathf.Log10(value) * 20);
    }
    public void VolumeChangeMusic(float value)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    void OnMouseDown()
    {
        ballsLenght = GameObject.FindGameObjectsWithTag("ball").Length;

        mixer.SetFloat("Distortion", ballsLenght/10f);
        mixer.SetFloat("Limiter", ballsLenght/-6f);

        fire.Play();
        GameObject spawnedBall = Instantiate(ball, gameObject.transform.position + new Vector3(-2.5f, 0, 0), Quaternion.identity);
        spawnedBall.GetComponentInChildren<Rigidbody>().AddForce(new Vector3(-1500, 0, 0));
        StartCoroutine(DestroyDelay(spawnedBall));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Destroy(collision.gameObject);
            health--;
            if (health == 0)
            {
                deathScr.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    void Update()
    {
        Vector3 move = new Vector3(0, 0, Input.GetAxis("Horizontal"));
        gameObject.transform.position += move * Time.deltaTime * 5;

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (menu.activeSelf == false)
            {
                menu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                menu.SetActive(false);
                gameObject.GetComponent<BoxCollider>().enabled = true;
                Time.timeScale = 1;
            }
        }
    }

    IEnumerator DestroyDelay(GameObject ball)
    {
        yield return new WaitForSeconds(5);
        Destroy(ball);
    }
}
