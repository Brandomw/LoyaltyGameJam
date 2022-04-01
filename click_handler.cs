﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click_handler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 screen_pos = new Vector2(mouse_position.x, mouse_position.y);

            RaycastHit2D hit = Physics2D.Raycast(screen_pos, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                
            }
        }
    }
}
