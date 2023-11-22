using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Transform player;
    public Transform endPortal;
    // Start is called before the first frame update
    public void GoToNextLevel()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        int next = current + 1;
        if (next >= SceneManager.sceneCountInBuildSettings)
        {
            next = 0;
        }
        SceneManager.LoadScene(next);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        //restart button
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentScene();
        }
        //check if player is falling to death
        if (player.position.y < -10f)
        {
            ReloadCurrentScene();
        }
        
        //check if game over
        if (endPortal.position.x < player.position.x && endPortal.position.x + 2 > player.position.x && 
            player.position.y > endPortal.position.y - 1 && player.position.y < endPortal.position.y + 1)
        {
            GoToNextLevel();
        }
    }
}
