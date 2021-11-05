using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

   
    //Timer variable
    private float timer;

    //Variable to change text in scene
    public Text text;

    // Update is called once per frame
    void Update()
    {
        //Timer increments each frame
        timer += Time.deltaTime;

        if (timer >= 5.0f)
        {
            text.text = "However, on your journey you will be pitted against hostiles ships and the dangers of space as you try to reach your goal";


        }

        if (timer >= 10.0f)
        {
            text.text = "Your objective: Destroy anthing or anyone who gets in your way";
        }

        //If the timer is greater than 10.0, change the text
        if (timer >= 15.0f)
        {
            text.text = "Press 'W', 'A', 'S', 'D' to move left, right, up and down";
          
        }
        
        if (timer >= 20.0f)
        {
              text.text = "Press the 'Spacebar' to fire your laser cannon";

        }

        if (timer >= 25.0f)
        {
            text.text = "You're now ready to embark on your journery. Good luck out there!";

        }

        if (timer >= 30.0f)
        {
            SceneManager.LoadScene("Game");
        }

    }


}
