using UnityEngine;
using System.Collections;

public class POCLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Level2Click()
    {
        Application.LoadLevel("PocLevel2");
    }

    public void Level4Click()
    {
        Application.LoadLevel("PocLevel4");
    }

	public void Level5Click()
	{
		Application.LoadLevel("PocLevel5");
	}

	public void Level6Click()
	{
		Application.LoadLevel("PocLevel6");
	}

	public void Level7Click()
	{
		Application.LoadLevel("PocLevel7");
	}
}
