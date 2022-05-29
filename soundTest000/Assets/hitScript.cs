using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class hitScript : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource hit;

    private void OnCollisionEnter(Collision collision)
    {
        hit.Play();

        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.tag = "Untagged";
            collision.gameObject.GetComponent<ConstantForce>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,5,0));
            collision.gameObject.transform.GetChild(1).GetComponent<AudioSource>().Play();

            StartCoroutine(DestroyDelay(collision.gameObject));
            // StartCoroutine(Distortion());
        }
    }
    IEnumerator DestroyDelay(GameObject enemy)
    {
        yield return new WaitForSeconds(0.05f);
        gameObject.transform.position = new Vector3(0, -5, 0);
        yield return new WaitForSeconds(1.95f);
        Destroy(enemy);
        Destroy(gameObject,1f);
    }
    
    /*public IEnumerator Distortion()
    {
        mixer.SetFloat("Distortion", 1);
        mixer.SetFloat("Limiter", -6);
        yield return new WaitForSeconds(0.2f);
        mixer.SetFloat("Distortion", 0);
        mixer.SetFloat("Limiter", 0);
    }*/
}
