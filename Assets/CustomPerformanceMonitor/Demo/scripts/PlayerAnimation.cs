using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    const string IS_WALKING = "IsWalking";
    const string IS_RUN = "isRun";

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnim(float h, float v, bool isRunning)
    {
        // Evaluar si el personaje tiene input de movimiento
        bool isMoving = h != 0f || v != 0f;

        // Si se mueve y presiona Fire1, camina o corre según corresponda
        anim.SetBool(IS_WALKING, isMoving && !isRunning);
        anim.SetBool(IS_RUN, isRunning);
    }
}