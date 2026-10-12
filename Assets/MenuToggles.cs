using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class MenuToggles : MonoBehaviour
{
    [Header("Referencias de Toggles")]
    public Toggle toggleFpsCounter;
    public Toggle togglePostProcessing;

    [Header("Elementos a Controlar")]
    public GameObject fpsCounterPanel; // El panel que contiene los textos de los FPS
    //public Behaviour[] postProcessVolumes; // Array para asignar los 2 componentes de Post-process de las cámaras
    public PostProcessVolume[] postProcessVolumes;
    void Start()
    {
        // 1. Cargar estados guardados (por defecto en 1 / true si no existe la clave)
        int fpsState = PlayerPrefs.GetInt("FpsCounterState", 1);
        int ppState = PlayerPrefs.GetInt("PostProcessingState", 1);

        // Asignar valores a los Toggles visuales
        if (toggleFpsCounter != null)
            toggleFpsCounter.isOn = (fpsState == 1);

        if (togglePostProcessing != null)
            togglePostProcessing.isOn = (ppState == 1);

        // Aplicar estado inicial
        ApplyFpsCounter(fpsState == 1);
        ApplyPostProcessing(ppState == 1);
    }

    void OnDisable()
    {
        // Guardar estados al cerrar o cambiar de menú
        if (toggleFpsCounter != null)
            PlayerPrefs.SetInt("FpsCounterState", toggleFpsCounter.isOn ? 1 : 0);

        if (togglePostProcessing != null)
            PlayerPrefs.SetInt("PostProcessingState", togglePostProcessing.isOn ? 1 : 0);
    }

    // Métodolazado al Toggle de FPS Counter en el Inspector (On Value Changed - Dynamic/Static bool)
    public void OnToggleFpsChanged(bool isEnabled)
    {
        ApplyFpsCounter(isEnabled);
    }

    // Método lazado al Toggle de Post-processing en el Inspector (On Value Changed - Dynamic/Static bool)
    public void OnTogglePostProcessingChanged(bool isEnabled)
    {
        ApplyPostProcessing(isEnabled);
    }

    private void ApplyFpsCounter(bool active)
    {
        if (fpsCounterPanel != null)
        {
            fpsCounterPanel.SetActive(active);
        }
    }

    private void ApplyPostProcessing(bool active)
    {
        if (postProcessVolumes != null)
        {
            foreach (var volume in postProcessVolumes)
            {
                if (volume != null)
                {
                    volume.enabled = active;
                }
            }
        }
    }
}