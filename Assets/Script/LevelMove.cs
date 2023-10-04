using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{

    public int sceneBuildIndex;
    //level move zoned enter if collider is a player

    //move into another scene
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered");

        //can be other.GetComponent<Player>()
        if (other.tag == "Player")
        {
            print("Switching Scene to" + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}