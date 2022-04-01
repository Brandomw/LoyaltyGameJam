using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status_Text : MonoBehaviour
{

    public Image status_image;

    public GameObject Master_UI_Controller;
    public GameObject upgade_pane;
    public GameObject no_more;

    public GameObject org_obj;
    public GameObject Selected_name;
    public GameObject HP_Min_text;
    public GameObject HP_Max_text;

    public GameObject production;
    public GameObject population;
    public GameObject loyalty;
    public int number_of_stars;
    public Image[] stars;
    public Sprite empty_star;
    public Sprite full_star;

    public int ui_upg_level;
    public int upgrade_level;
    public int building_id;

    public Text upgrade_desc;
    public Text upg_wood_cost;
    public Text upg_treasure_cost;
    public Text upg_loyalty_cost;

    public GameObject upg_loyalty_cost_obj;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
       // status_image = org_obj.GetComponent<Image>();
        org_obj = Master_UI_Controller.GetComponent<Build_menu>().org_obj;
        upgrade_level = org_obj.GetComponent<building_status>().upgrade_level;
        ui_upg_level = upgrade_level;

        //normalizes the number
        number_of_stars = org_obj.GetComponent<building_status>().max_upgrade_level;
        building_id = org_obj.GetComponent<building_status>().building_ID;
        //reset the starlength
        for (int x = 0; x < stars.Length; x++)
        {
            stars[x].enabled = true;
        }

            for (int x= 0; x < stars.Length; x++)
        {
            if(x < number_of_stars)
            {
                stars[x].sprite = empty_star;
            }
            else
            {
                stars[x].enabled = false;
            }

            
        }
        for (int y = 0; y < upgrade_level; y++)
        {

            if (org_obj.GetComponent<building_status>().upgrade_level >= org_obj.GetComponent<building_status>().max_upgrade_level)
            {
                Debug.Log("ZX");
                building_id = 0;
                upgade_pane.SetActive(false);
                no_more.SetActive(true);
                //upgrade_desc.text = org_obj.GetComponent<building_status>().upgrade_descriptions[building_id];
                //upg_wood_cost.text = org_obj.GetComponent<building_status>().wood_upg_cost[building_id].ToString();
                //upg_treasure_cost.text = org_obj.GetComponent<building_status>().treasure_upg_cost[building_id].ToString();

                
                stars[y].sprite = full_star;
            }
            else
            {
                
                no_more.SetActive(false);
                upgade_pane.SetActive(true);
                stars[y].sprite = full_star;

                if (org_obj.GetComponent<building_status>().building_type == "Castle")
                {
                    upgrade_desc.text = org_obj.GetComponent<building_status>().upgrade_descriptions[building_id + y];
                    //building ID + Y
                    upg_wood_cost.text = org_obj.GetComponent<building_status>().wood_upg_cost[building_id + y].ToString();
                    upg_treasure_cost.text = org_obj.GetComponent<building_status>().treasure_upg_cost[building_id + y].ToString();

                }
                else
                {
                    
                    upgrade_desc.text = org_obj.GetComponent<building_status>().upgrade_descriptions[building_id + y];
                    //building ID + Y
                    upg_wood_cost.text = org_obj.GetComponent<building_status>().wood_upg_cost[building_id + y].ToString();
                    upg_treasure_cost.text = org_obj.GetComponent<building_status>().treasure_upg_cost[building_id + y].ToString();
                    //y could = the number in list for upgrade
                }
            }
            
        }
        
            if (org_obj == null)
        {

        }
        else
        {
            Selected_name.GetComponent<Text>().text = org_obj.GetComponent<building_status>().my_name.ToString();


            HP_Min_text.GetComponent<Text>().text = org_obj.GetComponent<building_status>().HP.ToString();
            HP_Max_text.GetComponent<Text>().text = org_obj.GetComponent<building_status>().MHP.ToString();
            production.GetComponent<Text>().text = org_obj.GetComponent<building_status>().produces.ToString();
            population.GetComponent<Text>().text = org_obj.GetComponent<building_status>().population_value.ToString();
            if (org_obj.GetComponent<building_status>().building_type == "house")
            {
                //loyalty.GetComponent<Text>().text = org_obj.GetComponent<building_status>().Loyalty_drain.ToString();
            }
            else
            {
                loyalty.GetComponent<Text>().text = "None";
            }
            

        }
    }



}
