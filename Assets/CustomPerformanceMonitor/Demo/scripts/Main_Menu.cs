using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Main_Menu : MonoBehaviour{

    private void Start(){
        // El menú principal siempre requiere cursor visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
    }

    public void LoadScene(string sceneName)
    { 
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }


    public void QuitGame()
    {
        #if UNITY_EDITOR 
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}