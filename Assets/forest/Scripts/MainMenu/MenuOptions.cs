#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour {

	public bool IsFullScreen;
	public Dropdown DropdownResolution, DropdownQuality, DropdownShadows, DropdownScreen;

	void Start()
	{
		// Cargar valores guardados de los dropdowns
		DropdownResolution.value = PlayerPrefs.GetInt("Resolution", 0);		
		DropdownQuality.value = PlayerPrefs.GetInt("Quality", 0);
		DropdownShadows.value = PlayerPrefs.GetInt("Shadows", 0);
		DropdownScreen.value = PlayerPrefs.GetInt("Screen", 1);
		
		// PlayerPrefs no guarda bool, lo manejamos como int (1 = true, 0 = false)
		int screenIndex = PlayerPrefs.GetInt("IsFullScreen", 1);
		IsFullScreen = (screenIndex == 1);
		
		// Sincronizamos también el DropdownScreen con el valor guardado
		DropdownScreen.value = screenIndex;
		
		// Aplicar la configuración de pantalla completa y resolución inicial
		Screen.fullScreen = IsFullScreen;
		SetResolution(DropdownResolution.value);
	}

	void OnDisable(){
		PlayerPrefs.SetInt("Resolution", DropdownResolution.value);
		PlayerPrefs.SetInt("Quality", DropdownQuality.value);
		PlayerPrefs.SetInt("Shadows", DropdownShadows.value);
		PlayerPrefs.SetInt("Screen", DropdownScreen.value);
		
		// Guardar el estado del bool como entero
		PlayerPrefs.SetInt("IsFullScreen", IsFullScreen ? 1 : 0);
    }

	public void SetResolution (int Level){
		if(Level==0){
			Screen.SetResolution(848, 480, IsFullScreen);
		}
		else if(Level==1){
			Screen.SetResolution(1024, 600, IsFullScreen);
		}
		else if(Level==2){
			Screen.SetResolution(1280, 720, IsFullScreen);
		}
		else if(Level==3){
			Screen.SetResolution(1920, 1080, IsFullScreen);
		}		 
	}	

	public void Get_Quality (int Level){
		if(Level==0){
			QualitySettings.SetQualityLevel(0, false);
		}else if(Level==1){
			QualitySettings.SetQualityLevel(1, true);
		}else if(Level==2){
			QualitySettings.SetQualityLevel(2, true);
		}else if(Level==3){
			QualitySettings.SetQualityLevel(3, true);
		}
	}

	public void ShadowsLevel(int Level)
	{
		if (Level == 0)
		{
			QualitySettings.shadows = ShadowQuality.Disable;
			Debug.Log(QualitySettings.shadows);
		}
		else if (Level == 1)
		{
			QualitySettings.shadows = ShadowQuality.HardOnly;
			Debug.Log(QualitySettings.shadows);
		}
		else if (Level == 2)
		{
			QualitySettings.shadows = ShadowQuality.All;
			Debug.Log(QualitySettings.shadows);
		}
	}
	
	public void fullScreen(int Level){
		if(Level == 0){
			Screen.fullScreen = false;
			IsFullScreen = false;
		}else if(Level == 1){
			Screen.fullScreen = true;
			IsFullScreen = true;
		}
		// Aseguramos que el DropdownScreen coincida si se modifica por código o eventos externos
		DropdownScreen.value = Level;
	}
}