using UnityEngine;
using System.Collections;

public class POCLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Level1Click()
    {
        Application.LoadLevel("POCLevel1");
    }

    public void Level2Click()
    {
		Application.LoadLevel("POCLevel2");
    }

	public void Level3Click()
	{
		Application.LoadLevel("POCLevel3");
	}

	public void Level4Click()
	{
		Application.LoadLevel("POCLevel4");
	}

	public void Level5Click()
	{
		Application.LoadLevel("POCLevel5");
	}
}
