using UnityEngine;
using System.Collections;

public class HeroMovingController : MonoBehaviour
{
    public bool LEFT = false;
    public bool RIGHT = false;
    public bool GAS = false;
    private float MAX_SPEED = 0.4f;
    private float Speed = 0f;
    const float DeltaSpeed = 0.15f;
    const float DeltaSpeedStop = 0.35f;
    const float RotationSpeed = 115f;
    private Animator anim;
    public Vector3 MovePos;
    private bool StartMove = false;

	void Start () 
    {
        anim = this.GetComponent<Animator>();
	}

	void FixedUpdate ()
    {
        if (!GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetPauseState())
        {
            #region Управление клавиатурой
            if ((Input.GetKey(KeyCode.W) || GAS) && this.gameObject.GetComponent<HeroController>().GetGas() > 0)
            {
                if (Speed < MAX_SPEED)
                {
                    Speed = Speed + DeltaSpeed * Time.deltaTime > MAX_SPEED ? MAX_SPEED : Speed + DeltaSpeed * Time.deltaTime;
                }
            }

            if (Input.GetKey(KeyCode.A) || LEFT)
            {
                transform.Rotate(Vector3.forward * RotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) || RIGHT)
            {
                transform.Rotate(Vector3.forward * -RotationSpeed * Time.deltaTime);
            }

            if ((Speed > 0 && !Input.GetKey(KeyCode.W) && !StartMove && !GAS) || this.gameObject.GetComponent<HeroController>().GetGas() <= 0)
            {
                Speed = Speed - DeltaSpeedStop * Time.deltaTime < 0 ? 0 : Speed - DeltaSpeedStop * Time.deltaTime;
            }
            #endregion Управление

            #region Управление мышью
            if (Input.GetMouseButtonDown(0) && !GameObject.Find("Player").GetComponent<PlayerController>().MouseOverGUI)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                MovePos = hit.point;
                MovePos.y = this.transform.position.y;
                StartMove = true;
            }
            if (StartMove)
            {
                if (Vector3.Distance(MovePos, this.transform.position) > 5 && this.gameObject.GetComponent<HeroController>().GetGas() > 0 && Speed < MAX_SPEED)
                {
                    Speed = Speed + DeltaSpeed * Time.deltaTime > MAX_SPEED ? MAX_SPEED : Speed + DeltaSpeed;
                }
                if (this.gameObject.GetComponent<HeroController>().GetGas() > 0)
                {
                    this.transform.LookAt(MovePos);
                    this.transform.Rotate(90, 0, 0);
                    if (this.transform.position.x - MovePos.x > -1 && this.transform.position.x - MovePos.x < 1 &&
                        this.transform.position.z - MovePos.z > -1 && this.transform.position.z - MovePos.z < 1)
                    {
                        StartMove = false;
                    }
                }
                else
                {
                    StartMove = false;
                }
            }
            #endregion Управление мышью

            anim.SetFloat("Speed", Speed);
            this.gameObject.GetComponent<HeroController>().RemoveGas(Speed);
            this.transform.position += new Vector3(this.transform.up.x, this.transform.up.y, this.transform.up.z) * Speed;
            if (Speed == 0)
                this.gameObject.GetComponent<HeroController>().RegenerationGas(Time.deltaTime);
            
            #region Вылеты за экран
            Map map = GameObject.Find("Terrain").GetComponent<StageEnvironment>().GetMap();

            if (this.transform.position.x < map.Get_MIN_WIDTH() + 50)
            {
                this.transform.position = new Vector3(map.Get_MIN_WIDTH() + 50, this.transform.position.y, this.transform.position.z);
            }
            if (this.transform.position.x > map.Get_MAX_WIDTH() - 50)
            {
                this.transform.position = new Vector3(map.Get_MAX_WIDTH() - 50, this.transform.position.y, this.transform.position.z);
            }
            if (this.transform.position.z < map.Get_MIN_HEIGHT() + 20)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, map.Get_MIN_HEIGHT() + 20);
            }
            if (this.transform.position.z > map.Get_MAX_HEIGHT() - 20)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, map.Get_MAX_HEIGHT() - 20);
            }
            #endregion Вылеты за экран
        }
    }

    public bool InverseParameter(string param)
    {
        switch (param)
        {
            case "Gas":
                return GAS = !GAS;
            case "Left":
                return LEFT = !LEFT;
            case "Right":
                return RIGHT = !RIGHT;
            default:
                return false;
        }
    }

    public void GoLeft(bool param)
    {
        LEFT = param;
    }
    
    public void GoRight(bool param)
    {
        RIGHT = param;
    }
    
    public void GoGas(bool param)
    {
        GAS = param;
    }
}
