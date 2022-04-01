using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class building_status : MonoBehaviour
{

    //
    //resource drain tick
    public ParticleSystem smoke;
    private GameObject org_obj;
    private Vector3 org_pos;
    //loyalty drain tick
    //private float tick = 0f;
    private float building_check_tick = 0f;
    private float consumption_tick = 0f;
    private float food_production_tick = 0f;
    private float wood_production_tick = 0f;
    private float treasure_production_tick = 0f;
    private float loyal_tick = 0f;
    private float c_tick = 0f;
    private float f_tick = 0f;
    //private float food_tick = 0f;

    public GameObject building_plot;

    //private float food_production_delay = 5;

    private float delay = 5f;
    // private float c_delay = 5;
    //private float l_delay = 15;

    private float m_tick;
    //public Sprite built_image;
    private Construction construct;
    public bool constructed;
    private bool bonus_applied = false;
    public bool[] upgrades = { false, false, false, false, false, false };
    //Building Status
    [SerializeField] public int building_ID;
    [SerializeField] public string my_name;
    [SerializeField] public string building_type;
    //How much a building costs and what its worth
    [SerializeField] public int food_value;
    [SerializeField] public int treasure_value;
    [SerializeField] public int wood_value;
    [SerializeField] public int loyalty_value;
    
    //population
    [SerializeField] public int max_population_value;
    [SerializeField] public int population_value;
    [SerializeField] public int house_pop_value;
    [SerializeField] public int upgrade_level = 1;
    [SerializeField] public int max_upgrade_level;
    //HP of the building
    [SerializeField] public int HP;
    [SerializeField] public int MHP;

    //production
    [SerializeField] public string produces;
    [SerializeField] public float production_delay = 5f;
    [SerializeField] public int Production; 

    //Loyalty
    [SerializeField] public float loyalty_change_delay = 5f;
    [SerializeField] public int loyalty_change_value;
    [SerializeField] public int starting_loyalty_change_value;
    //food
    [SerializeField] public int food_consumption_amt;
    [SerializeField] public float food_consumption_rate = 5f;
    //market
    private float market_tick;
    //happiness
    private float market_delay = 5;
    public Image current_building_image;
    public Image next_upgrade;

    public Text upgrade_text;
    public GameObject Information_pane;
    public GameObject Upgrade_pane;
    public float[] construction_times = { 5, 10, 15, 20, 25, 30 };
    public int[] wood_upg_cost =
    {
        0,0,0,0,0,0
    };

    public int[] treasure_upg_cost =
    {0,0,0,0,0,0


    };
    public int[] food_upg_cost =
    {0,0,0,0,0,0


    };
    public int[] loyalty_cost =
    {100,250,500,750,999,0


    };
    //int loyalty_cost = 0;
    public string[] upgrade_descriptions = {"","","","","",""
    //        "Building cannot be upgraded further.",
    
    //"Description: This will level your farm upto level 2, producing more food and generating more loyalty" ,
    //"Description: This will level your farm upto level 3, will increase the frequency of food and loyalty" ,
    ////Houses
    //"Description: This will level your House upto level 2, Raising the population cap without impacting loyalty too much." ,
    //"Description: This will level your House upto level 3, Raising the population cap without impacting loyalty too much."  ,
    ////Lumber Yard
    //"Description: This will level your Lumberyard upto level 2, This will increase the yield of wood gained." ,
    //"Description: This will level your Lumberyard upto level 3, This will increase the frequency in which you gain wood" ,
    ////Castle
    //"Description: This will level your Castle upto level 2, Your loyalty meter will drain slower and you will have more plots available." ,
    //"Description: This will level your Castle upto level 3, You will unlock  more plots and buildings" ,
    //"Description: This will level your Castle upto level 4, Your loyalty meter will drain slower and you will unlock more plots" ,
    //"Description: This will level your Castle upto level 5, ????" ,"","","",""
    };

    
    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.SetActive(false);
        //smoke.Stop();
        smoke.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //Time based checks
        check_buildings();
        Destroy_Building();
        check_Smoke();
        //Constructed 
        if (this.gameObject.GetComponent<Construction>().is_constructed() == true && bonus_applied == false)
        {

            
            if (building_type == "House")
            {
                //Debug.Log("This dick shouldn't be doin this");
                GameObject.Find("Game_controller").GetComponent<game_manager>().population_max += 2;
                GameObject.Find("Game_controller").GetComponent<game_manager>().houses += 1;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 1;
            }
            if (building_type == "Farm")
            {
                //Farm production
                GameObject.Find("Game_controller").GetComponent<game_manager>().farms += 1;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 1;
            }
            if (building_type == "Lumberyard")
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().lumberyards += 1;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty -= 3;
                //Wood Production
            }
            if (building_type == "Tavern")
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().taverns += 1;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty -= 3;

                //treasure production
            }
            if (building_type == "Church")
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().churches += 1;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 4;

                //treasure production
            }
            if (building_type == "Market")
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().markets += 1;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 3;

                //treasure production
            }
            if (building_type == "Park")
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().parks += 1;
                if(GameObject.Find("Game_controller").GetComponent<game_manager>().parks < 1)
                { GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 3; }
                else
                {
                    GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 1;
                }
                

                //treasure production
            }
            if (building_type == "Blacksmith")
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().blacksmiths += 1;
                if (GameObject.Find("Game_controller").GetComponent<game_manager>().blacksmiths < 2)
                { GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 3; }
                else
                {
                    GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 1;
                }


                //treasure production
            }
            if (building_type == "Tower")
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().towers += 1;
                if (GameObject.Find("Game_controller").GetComponent<game_manager>().towers < 4)
                { GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 6; }
                else
                {
                    GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 1;
                }
                //treasure production
            }

            bonus_applied = true;
        }
        //Level 2=================================
        if (upgrades[2] == true)
        {
            if (building_type == "House")
            {
                house_bonus();
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 1;
            }
            if(building_type == "Farm")
            {
                farm_Bonus();
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 1;
            }
            if (building_type == "Tavern")
            {
                food_consumption_amt += 3;
                tavern_bonus();
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 1;
            }
            if (building_type == "Lumberyard")
            {
                lumberyard_bonus();
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty -= 1;
            }
            if(building_type == "Market")
            {
                Production += 3;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 10;
            }
           
            if (building_type == "church")
            {

                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 15;
            }
            if (building_type == "Blacksmith")
            {
                food_consumption_amt -= 2;
                Production += 5;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 1;
            }
            if (building_type == "Park")
            {

                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 50;
            }
            if (building_type == "Castle")
            {

                GameObject.Find("Game_controller").GetComponent<game_manager>().max_loyalty += 150;
            }
            upgrades[2] = false;
        }

        //Level 3 =================================
        if (upgrades[3] == true)
        {
            //for now because some might have delay bonuses just for balance purpose
            if (building_type == "House")
            {
                house_bonus();
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 10;
            }
            if (building_type == "Farm")
            {
                farm_Bonus();
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 10;
            }
            if (building_type == "Tavern")
            {
                food_consumption_amt += 3;
                tavern_bonus();
                
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 15;
            }
            if (building_type == "Lumberyard")
            {
                lumberyard_bonus();
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 5;


            }
            if (building_type == "Market")
            {
                
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 20;
            }
            if (building_type == "church")
            {

                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 30;
            }
            if (building_type == "Blacksmith")
            {
                food_consumption_amt -= 3;
                Production += 5;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 5;
            }
            if (building_type == "Park")
            {

                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 100;
            }
            if (building_type == "Castle")
            {

                GameObject.Find("Game_controller").GetComponent<game_manager>().max_loyalty += 200;
            }
            upgrades[3] = false;
        }

        //level 4 =================================
        if (upgrades[4] == true)
        {
            if (building_type == "Castle")
            {

                GameObject.Find("Game_controller").GetComponent<game_manager>().max_loyalty += 300;
            }
            // apply bonus 2
            upgrades[4] = false;
        }

        //level 5 ==========================
        if (upgrades[5] == true)
        {
            if (building_type == "Castle")
            {

                GameObject.Find("Game_controller").GetComponent<game_manager>().max_loyalty += 250;
            }
            // apply bonus 2
            upgrades[5] = false;
        }

        //constant effect
        if (this.gameObject.GetComponent<Construction>().is_constructed() == true &&
            this.gameObject.GetComponent<Construction>().upgrading == false &&
            bonus_applied == true)
        {

            food_consumption(food_consumption_rate, food_consumption_amt);
            if (building_type == "House")
            {
                //Houses drain loyalty
                //produce(upgrade_level * 1, "loyalty");
                f_tick += Time.deltaTime;
                if (f_tick > 60)
                {
                    GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 3;
                    Debug.Log("farm_loyalty");
                    f_tick = 0f;

                }
            }
            if (building_type == "Farm")
            {
                //food produces loyalty
                produce(Production, "food");

                f_tick += Time.deltaTime;
                if (f_tick > 30)
                {
                    GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += 2;
                    Debug.Log("farm_loyalty");
                    f_tick = 0f;

                }
            }
            if (building_type == "Lumberyard")
            {
                produce(Production, "wood");
                //Wood Production
            }
            if (building_type == "Tavern")
            {
                produce(Production, "treasure");
                //treasure production
            }
            if (building_type == "Castle")
            {
                HP = MHP;
                // Debug.Log("What");
                produce(upgrade_level, "food");
                produce(upgrade_level, "wood");
                produce(upgrade_level, "treasure");
                //treasure production
            }

            if (building_type == "Market")
            {
                if (GameObject.Find("Game_controller").GetComponent<game_manager>().houses > 1)
                {
                    if (GameObject.Find("Game_controller").GetComponent<game_manager>().markets == 1)
                    {
                        produce(upgrade_level * 5, "random");
                        Debug.Log("2 balanced as all things should be");
                    }
                    if (GameObject.Find("Game_controller").GetComponent<game_manager>().markets == 2)
                    {
                        produce(upgrade_level * 3, "random");
                        Debug.Log("2 markets");
                    }
                    if (GameObject.Find("Game_controller").GetComponent<game_manager>().markets == 3)
                    {
                        produce(upgrade_level * 2, "random");
                        Debug.Log("3 markets");
                    }
                    if (GameObject.Find("Game_controller").GetComponent<game_manager>().markets > 3)
                    {
                        produce(upgrade_level * 1 + Production, "random");
                        Debug.Log("over saturated");
                    }
                }
                else
                {
                    produce(upgrade_level * 1, "random");
                    m_tick += Time.deltaTime;
                    if (m_tick > 10)
                    {
                        Debug.Log("No market for you");
                        m_tick = 0f;
                        if (HP < 0)
                        {
                            Destroy_Building();
                        }
                        else
                        {
                            HP -= GameObject.Find("Game_controller").GetComponent<game_manager>().markets;
                        }
                    }

                }


            }
            if (building_type == "Park")
            {
                produce(upgrade_level * 1, "loyalty");
            }
            if (building_type == "Tower")
            {
                produce(2, "loyalty");
            }
            if (building_type == "Church")
            {
                if (GameObject.Find("Game_controller").GetComponent<game_manager>().houses > 1)
                {
                    if (GameObject.Find("Game_controller").GetComponent<game_manager>().churches == 1)
                    {
                        produce(upgrade_level * 1, "loyalty");
                        Debug.Log("churches are balanced as all things should be");
                    }
                    if (GameObject.Find("Game_controller").GetComponent<game_manager>().churches == 2)
                    {
                        produce(upgrade_level * 2, "loyalty");
                        Debug.Log("2 churches");
                    }
                    if (GameObject.Find("Game_controller").GetComponent<game_manager>().churches > 2)
                    {
                        produce(upgrade_level * 1, "loyalty");
                        Debug.Log("3 churches too many");
                        c_tick += Time.deltaTime;
                        if (c_tick > 30)
                        {
                            Debug.Log("No church for you");
                            m_tick = 0f;
                            if (HP < 0)
                            {
                                Destroy_Building();
                            }
                            else
                            {
                                HP -= GameObject.Find("Game_controller").GetComponent<game_manager>().churches * 3;
                            }
                        }
                    }


                }
                //Raise population 
                //loyalty Drain
            }
        }
        




    }

    public void check_Smoke()
    {
        if (HP < (MHP / 2))
        {
            if(!smoke.isPlaying)
            {
                smoke.Play();
            }
            Debug.Log("Playing effect");
            //smoke.Play();
        }
        else
        {
            smoke.Stop();
            //smoke.Stop();
            //Debug.Log("not Playing effect");
        }
    }

    public void check_buildings()
    {

        building_check_tick += Time.deltaTime;
        if (building_check_tick > 1)
        {
            int pop_limit = (GameObject.Find("Game_controller").GetComponent<game_manager>().population_max) + 2;
            Debug.Log(pop_limit);
            if (GameObject.Find("Game_controller").GetComponent<game_manager>().population > pop_limit)
            {
                if (building_type == "Castle" || building_type == "Park" || building_type == "House")
                {

                }
                else
                {
                    
                    HP-= 3;
                }
                building_check_tick = 0;
            }
        }
    }


    //if constructed we begin the loop

    void house_bonus()
    {
        if (building_type == "House")
        {

            GameObject.Find("Game_controller").GetComponent<game_manager>().population_max += 2;
            house_pop_value += 2;
            food_consumption_amt += 7;
            //loyalty_change_value -= 2;
        }

    }

    void farm_Bonus()
    {
        Production += 4;
    }
    void lumberyard_bonus()
    {
        Production += 5;
    }
    void tavern_bonus()
    {
        Production += 2;
    }
    void Market_bonus()
    {
       
    }
    void production_bonus(int amt)
    {
            Production += amt;
    }
    void production_delay_change(float amt)
    {
        production_delay = amt;
    }

    //food consumption
    void food_consumption(float me_delay, int amt)
    {
        consumption_tick += Time.deltaTime;
       // Debug.Log(consumption_tick); 
        if (consumption_tick > me_delay)
        {
            
            if (GameObject.Find("Game_controller").GetComponent<game_manager>().food < amt)
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty -= 2;
                HP--;
            }
            else
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().food -= amt;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_food -= amt;
            }
            consumption_tick = 0f;
        }
    }
    void treasure_consumption(float me_delay, int amt)
    {
        consumption_tick += Time.deltaTime;
        // Debug.Log(consumption_tick); 
        if (consumption_tick > me_delay)
        {

            if (GameObject.Find("Game_controller").GetComponent<game_manager>().treasure < amt)
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty -= 2;
                HP--;
            }
            else
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().treasure -= amt;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_treasure -= amt;
            }
            consumption_tick = 0f;
        }
    }
    void wood_consumption(float me_delay, int amt)
    {
        consumption_tick += Time.deltaTime;
        // Debug.Log(consumption_tick); 
        if (consumption_tick > me_delay)
        {

            if (GameObject.Find("Game_controller").GetComponent<game_manager>().wood < amt)
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty -= 2;
                HP--;
            }
            else
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().wood -= amt;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_wood -= amt;
            }
            consumption_tick = 0f;
        }
    }
    


    void Destroy_Building()
    {
        if (this.gameObject.GetComponent<building_status>().HP < 0)
        {
            
            if (building_type == "House")
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().population_max -= house_pop_value;
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty -= (5 * upgrade_level);
            }
            else
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().population -= population_value;
            }
            org_pos = this.gameObject.transform.localPosition;
            this.gameObject.transform.localPosition.Set(-50,-50,-10);
            
            Instantiate(building_plot, org_pos, Quaternion.identity);
            GameObject.Find("Master_UI_Controller").GetComponent<Build_menu>().menu_off();
            GameObject.Find("Master_UI_Controller").GetComponent<Build_menu>().my_audio.clip = GameObject.Find("Master_UI_Controller").GetComponent<Build_menu>().explosion;
            GameObject.Find("Master_UI_Controller").GetComponent<Build_menu>().my_audio.Play();
            building_count(-1);
            GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty -= 15;
            Destroy(this.gameObject);
        }

    }
    void produce(int amt, string type)
    {
        //Debug.Log(food_production_tick + "/" + delay);
        food_production_tick += Time.deltaTime;
        if (food_production_tick > production_delay)
        {
            if(building_type == "Market")
            {
                
            }

            switch (type)
            {
                case "food":
                    food_production_tick = 0f;
                    GameObject.Find("Game_controller").GetComponent<game_manager>().food += amt;
                    GameObject.Find("Game_controller").GetComponent<game_manager>().qued_food += amt;
                    //Debug.Log("food produced:" + amt);
                    break;

                case "wood":
                    food_production_tick = 0f;
                    GameObject.Find("Game_controller").GetComponent<game_manager>().wood += amt;
                    GameObject.Find("Game_controller").GetComponent<game_manager>().qued_wood += amt;
                    //Debug.Log("wood produced:" + amt);
                    break;
                case "treasure":
                    food_production_tick = 0f;
                    GameObject.Find("Game_controller").GetComponent<game_manager>().treasure += amt;
                    GameObject.Find("Game_controller").GetComponent<game_manager>().qued_treasure += amt;
                    //Debug.Log("treasure produced:" + amt);
                    break;

                case "random":
                    int random_number = Random.Range(1, 4);
                    Debug.Log("random_number chosen :" + random_number + " Amt: "+ amt);
                    switch (random_number)
                    {
                        
                        case 1:

                            GameObject.Find("Game_controller").GetComponent<game_manager>().food += amt;
                            GameObject.Find("Game_controller").GetComponent<game_manager>().qued_food += amt;
                            market_tick = 0f;
                            break;
                        case 2:
                            GameObject.Find("Game_controller").GetComponent<game_manager>().wood += amt;
                            GameObject.Find("Game_controller").GetComponent<game_manager>().qued_wood += amt;
                            market_tick = 0f;
                            break;
                        case 3:
                            GameObject.Find("Game_controller").GetComponent<game_manager>().treasure += amt;
                            GameObject.Find("Game_controller").GetComponent<game_manager>().qued_treasure += amt;
                            market_tick = 0f;
                            break;
                        default:
                            Debug.Log("Random Number is out of range :" + random_number);
                            market_tick = 0f;
                            break;

                    }
                    food_production_tick = 0f;
                    break;

                case "loyalty":
                    food_production_tick = 0f;
                    GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += amt;
                    break;

                default:
                    Debug.Log("Out of range");
                    break;
            }

           
        }


    }

    
    void loyalty_change(int l_amount, float l_delay)
    {
        loyal_tick += Time.deltaTime;
        if (loyal_tick > l_delay)
        {
            loyal_tick = 0f;
            if(l_amount> 0)
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty -= l_amount;
            }
            else
            {
                GameObject.Find("Game_controller").GetComponent<game_manager>().qued_loyalty += l_amount;
            }
            
            //if current loyalty is greater than building status max drain if its not drain from HP instead
            
            
            //Debug.Log(GameObject.Find("Game_controller").GetComponent<game_manager>().current_loyalty);
        }
    }


    public void building_count(int value)
    {
        Debug.Log("wat");
        if (building_type == "House")
        {
            GameObject.Find("Game_controller").GetComponent<game_manager>().houses += value;
            
        }
        if (building_type == "Farm")
        {
            //Farm production
            GameObject.Find("Game_controller").GetComponent<game_manager>().farms += value;
            
        }
        if (building_type == "Lumberyard")
        {
            GameObject.Find("Game_controller").GetComponent<game_manager>().lumberyards += value;
            //Wood Production
        }
        if (building_type == "Tavern")
        {
            GameObject.Find("Game_controller").GetComponent<game_manager>().taverns += value;
            
            //treasure production
        }
        if (building_type == "Church")
        {
            GameObject.Find("Game_controller").GetComponent<game_manager>().churches += value;
            
            //treasure production
        }
        if (building_type == "Market")
        {
            GameObject.Find("Game_controller").GetComponent<game_manager>().markets += value;
            //treasure production
        }
        if (building_type == "Park")
        {
            GameObject.Find("Game_controller").GetComponent<game_manager>().parks += value;
            //treasure production
        }
        if (building_type == "Tower")
        {
            GameObject.Find("Game_controller").GetComponent<game_manager>().towers += value;
            //treasure production
        }
        if (building_type == "Blacksmith")
        {
            GameObject.Find("Game_controller").GetComponent<game_manager>().blacksmiths += value;
            //treasure production
        }
    }

}
