using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PermanentUI : MonoBehaviour
{
    //Player State ;
    public int Mushroom = 0;
    public int health = 5;
    
    public TextMeshProUGUI MushroomText;
    public Text healthAmount;

    public static PermanentUI perm;

    private void Start()
    {
        //do not destroy
        DontDestroyOnLoad(gameObject);

        MushroomText.text = Mushroom.ToString();
        healthAmount.text = health.ToString();


        //Singleton
        if (!perm)
        {
            perm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //reset hp vs cherry
    public void Reset()
    {
        Mushroom = 0;
        MushroomText.text = Mushroom.ToString();
        health = 5;
        healthAmount.text = health.ToString();
    }

}
