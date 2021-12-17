using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;

public class DLLPlugin : MonoBehaviour
{

    [DllImport("Tutorial_9_DLL")]
    static extern Color RandomColor();

    private Vector3 pos;
    private Color cr;

    private MeshRenderer mr;


    // Start is called before the first frame update
    void Start()
    {

        mr = GetComponent<MeshRenderer>();

        //if statement to check if the file exists
        if (System.IO.File.Exists(Application.dataPath + "/color.txt"))
        {
            //Read text inside the file
            string saveString = File.ReadAllText(Application.dataPath + "/color.txt");
            Debug.Log(saveString);

            for (int i = 1; i < saveString.Length; i++)
            {
                string[] array = saveString.Split(',');

                //Convert string data to a float for unity to understand
                //Store data in the array
                cr = new Color(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));

                Debug.Log(cr);
            }

            mr.material.color = new Color(cr.r, cr.g, cr.b);
        }

    }

    // Update is called once per frame
    void Update()
    {

 
    }

    //Function to set the color of a material to be random
    public void RandColor()
    {
        Color rc = RandomColor();
        mr.material.color = new Color(rc.r, rc.g, rc.b);
    }
}
