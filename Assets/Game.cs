using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public GameObject fire;
    public GameObject helicopter;
    public List<Tile> burningTiles;
    public int phase = 0; //0 = place starting fire, 1 = placing helicopters
    public int helicoptersLeft = 2;
    // Start is called before the first frame update
    void Start()
    {
        burningTiles = new List<Tile>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaceFire(Tile selectedTile, bool startingTile = false)
    {
       if(startingTile) {
            selectedTile.isStartingTile = true;
       }
        Instantiate(fire, selectedTile.transform);
        selectedTile.safe = false;
        selectedTile.burnt = true;
        selectedTile.state = 2;
//        burningTiles.Add(selectedTile);
    }

    public void ClearBurningTiles()
    {
        burningTiles.Clear();
    } 

    public void PlaceHelicopter(Tile selectedTile)
    {
        if (helicoptersLeft > 0)
        {
            Debug.Log("Placing Helicopter!");
            helicoptersLeft--;
            selectedTile.helicopter = true;
            selectedTile.safe = true;
            selectedTile.helicopterObj = Instantiate(helicopter, selectedTile.transform);

        }

        if (helicoptersLeft <= 0)
        {
            phase = 1;
            helicoptersLeft = 2;
            //check containment
            Debug.Log("No Helicopters Left, Checking Containment of Fires");
            for (int i = 0; i < burningTiles.Count; i++) {
                burningTiles[i].CheckFireContainment();
            }
        }
    }
}
