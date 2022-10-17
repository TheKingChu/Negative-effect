using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    static int numberOfNotes = 0;
    public GameObject collected;
    public AudioSource noteSound;

    // Start is called before the first frame update
    void Start()
    {
        numberOfNotes = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        noteSound.Play();
        numberOfNotes++;

        //if you have all the notes you will get a message saying "all notes has been collected"
        if(numberOfNotes == 5)
        {
            collected.SetActive(true);
        }

        Destroy(gameObject);
    }
}
