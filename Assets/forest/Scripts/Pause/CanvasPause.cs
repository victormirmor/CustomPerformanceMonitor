#region Previas
using UnityEngine;
using System.Collections; 
using UnityEngine.SceneManagement;
using MiJuego.InputAdaptador;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion
public class CanvasPause : MonoBehaviour {
	public Canvas canvasPausa;
	public string MainMenu = "mainMenu";
	GameManager gamanager;

	void Awake(){
    gamanager = UnityEngine.Object.FindAnyObjectByType<GameManager>();
	CursorTrue();
    }

	void Start(){
		canvasPausa.enabled = false;
		Time.timeScale = 1;
	}
	void Update(){
		if (CrossPlatformInputManager.GetButton ("Cancel")){
			Pause();
			}	
	}
	public void Pause(){		
		canvasPausa.enabled = !canvasPausa.enabled;
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		CursorTrue();
	}

	
	public void CursorTrue(){
		Cursor.visible = true; 
	}
	public void CursorFalse()
	{
		Cursor.visible = false;
	}

	public void Quit()
	{
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	public void scene(string name)
	{
		SceneManager.LoadScene(name);
	}
	public void Main_Menu_scene(){
		gamanager.ResetGame();
	SceneManager.LoadScene (MainMenu);
	}
}
