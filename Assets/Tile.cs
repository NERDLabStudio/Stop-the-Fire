using System.Collections;
using System.Collections.Generic;
using UnityEngine;


struct TouchingTile
{
    public GameObject obj;
    public int state; //0 unused, 1 safe, 2 burned

}
public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    public int state; //0 unused, 1 safe, 2 burned
    public bool burnt = false;
    public bool safe = false;
    public bool isStartingTile = false;
    public bool helicopter = false;
    public Tile[] pathways;
    public Game game;
    public GameObject helicopterObj;
   

    void Start()
    {
        game = FindObjectOfType<Game>();
    }

    private void OnMouseOver()
    {
         {

            if (Input.GetMouseButtonDown(0))
            {
                if (game.phase == 1)
                {
                    //placing helicopters
                    game.PlaceHelicopter(this);
                }

                if (game.phase == 0)
                {
                    //placing starting fire
                    Debug.Log("Starting Fire!");
                    game.PlaceFire(this, true);
                    game.phase = 1;
                }
            }
        }
    }

    public void SetHelicopter(GameObject h)
    {
        helicopter = h;
    }
    public void RemoveHelicopter()
    {
        Debug.Log("Destroying Helicopter " + helicopterObj);
        Destroy(helicopterObj);
    }
    public void CheckFireContainment()
    {
        for (int i = 0; i < pathways.Length; i++) {
            if (pathways[i].helicopter)
            {
                //has helicopter
                Debug.Log("Has a Helicopter, Safe!");
                pathways[i].safe = true;
                pathways[i].RemoveHelicopter();
            }
            else if (pathways[i].burnt)
            {
                //pathway is already burned
                Debug.Log("This path is already burned");
            }
            else if (pathways[i].safe)
            {
                Debug.Log("This tile (" + gameObject.name + ") is already safe!");
            }
            else
            {
                //not safe
                Debug.Log("This tile isn't safe, burning");
                game.PlaceFire(pathways[i]);
                pathways[i].burnt = true;
                pathways[i].safe = false;
            }
        }

    }

    public bool isSafe(Tile t)
    {
        return t.safe;
    }
    
    public bool isAccessible(Tile t)
    {
        return !t.burnt;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
