#region Namespaces
using System.Collections;
using System.Collections.Generic;
using MiJuego.InputAdaptador;
using UnityEngine;
#endregion
public class C_Controller : MonoBehaviour{
    #region Variables
	[Header ("Gravity")]
	public float gravity = 20.0F;
	private Vector3 gravityDirection = Vector3.zero; 
    [SerializeField] bool CursorIsvisble;

    [Header ("Movement")]
    [SerializeField] float Speed;
	[SerializeField] string Horizontal = "Horizontal", Vertical = "Vertical";

	private CharacterController _controller;
    private float Axis_Horizontal, Axis_Vertical;    
    private Vector3 _move;    
    private Animator _anim;
    #endregion
    #region Unity voids
    private void Start() {
        _anim = gameObject.GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }
    void Update(){
        Cursor.visible = CursorIsvisble;
        Move();
		Gravity();
    } 
    #endregion
    #region Move  
    void Move(){
		Axis_Horizontal = CrossPlatformInputManager.GetAxis(Horizontal);
		Axis_Vertical = CrossPlatformInputManager.GetAxis(Vertical);

		_move = new Vector3(Axis_Vertical, 0, Axis_Horizontal);

		_controller.Move(_move * Time.deltaTime * Speed);       
                          
        // Calculamos la magnitud del movimiento para el Blend Tree 1D ("Speed")
        float currentSpeed = new Vector3(Axis_Horizontal, 0, Axis_Vertical).magnitude;
        _anim.SetFloat("Speed", currentSpeed);
			
		Rotate(_move);
    } 
    #endregion  
	#region create voids
    void Rotate(Vector3 move){
        Vector3 Rotation = new Vector3 (move.x, 0, move.z);        
        if (Axis_Horizontal != 0 || Axis_Vertical != 0){
            transform.rotation = Quaternion.LookRotation(Rotation);
        }
    }

	void Gravity() {      
		gravityDirection.y -= gravity * Time.deltaTime;
		_controller.Move(gravityDirection * Time.deltaTime);
	}
    #endregion
}