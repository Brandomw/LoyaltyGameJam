using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class construct_button : MonoBehaviour
{

    public AudioSource my_sudio_source;
    public AudioClip my_clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void play_clip()
    {
        my_sudio_source.clip = my_clip;
        my_sudio_source.Play();
    }
}
