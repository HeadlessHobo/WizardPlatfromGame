using UnityEngine;
using System.Collections;

public class Handle : MonoBehaviour
{
    public int id;
    public Material toggleOff;
    public Material toggleOn;
    public bool hasTogggled { get; set; }
    private int toggle = -1;
   

    // Use this for initialization
    void Start()
    {
        hasTogggled = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (toggle > 0)
        {

            //GameObject.Find("Elevator").GetComponent<Elevator>().shouldOperate = true;
            renderer.material = toggleOn;
            if (!hasTogggled)
            {
                hasTogggled = true;
                toggleId(id);
                
            }

        }
        else if (toggle < 0)
        {
            // GameObject.Find("Elevator").GetComponent<Elevator>().shouldOperate = false;
            renderer.material = toggleOff;

        }
    }

    void OnTriggerStay(Collider collidingObject)
    {
        if (collidingObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            toggle *= -1;
        }
    }

    public void toggleId(int id)
    {

        foreach (GameObject gameObjToLookThrough in GameObject.FindGameObjectsWithTag("Switch"))
        {
            Toggle tToggle = gameObjToLookThrough.GetComponent<Toggle>();
            if (tToggle.id == id)
            {
                Debug.Log("TOGGLING");
                tToggle.toggle();
            }
        }


    }
}
