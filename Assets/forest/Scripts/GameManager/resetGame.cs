using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetGame : MonoBehaviour{
    [SerializeField] GameManager gamemanager;

    void Start(){
       gamemanager = UnityEngine.Object.FindAnyObjectByType<GameManager>();
    }
    public void LoadScene(string Scene){
        SceneManager.LoadScene (Scene);
        gamemanager.StartGame();
    }
    
}
