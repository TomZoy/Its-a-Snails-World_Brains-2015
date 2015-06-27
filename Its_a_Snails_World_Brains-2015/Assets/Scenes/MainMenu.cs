using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playButton()
    {
        Application.LoadLevel("Level1");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

   // public void optionsButton()
    //{
     //   Application.LoadLevel("Options");
    //}
}

