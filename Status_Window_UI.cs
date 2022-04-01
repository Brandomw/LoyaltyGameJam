using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status_Window_UI : MonoBehaviour
{

    public GameObject MC;
    public Text Status_txt;
    public Text add_txt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        Debug.Log(MC.GetComponent<Build_menu>().org_obj.GetComponent<building_status>().my_name);
            //Debug.Log(copy_object.GetComponent<building_status>().building_type);
            //Status_txt.GetComponent<Text>().text = org_obj.GetComponent<building_status>().my_name.ToString();

       
        
            
        
        

    }
}
