using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour {

    // The scene attached to this button
    public string SceneToLoad;

	// Use this for initialization
	void Start () {
        Button btn = GetComponentInParent<Button>();
        btn.onClick.AddListener(LoadLevel);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LoadLevel()
    {
        //print(gameObject.name + " has been pressed");
        if (SceneToLoad != "")
            SceneManager.LoadScene(SceneToLoad);
    }
}
