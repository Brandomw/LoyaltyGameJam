using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selected_UI : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    Sprite my_image;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        my_image = spriteRenderer.sprite;
        my_image = GameObject.Find("Master_UI_Controller").GetComponent<Build_menu>().selected_mouse_obj;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
