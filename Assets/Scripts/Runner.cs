using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Runner : MonoBehaviour
{
    public Animator animator;

    [Range(0, 100)]
    public float Xspeed;
    [Range(0, 100)]
    [SerializeField] float Yspeed;
    [Range(0, 100)]
    public float Zspeed;

    CharacterController controller;

    Player player;

    bool isStarted = false;
    bool b = false;

    private void Awake() {
        controller = GetComponent<CharacterController>();
        animator.SetTrigger("idle");
    }

    public void startGame()
    {
        isStarted = true;
    }

    void Update()
    {
        float mouseX = GetMouseX();

        if (isStarted && !Player.isFinish)
        {
            controller.Move(GetMoveVector(0, 0, Yspeed, Zspeed));
            if (!b)
            {
                b = true;
                animator.SetTrigger("move");
            }
            
        }
        
        if (isStarted && Input.GetMouseButton(0) && !Player.isFinish)
        {
            controller.Move(GetMoveVector(mouseX,Xspeed,0,0));
            if (!b)
            {
                b = true;
                animator.SetTrigger("move");
            }
        }
        else if(!Player.isStruck)
        {
            //animator.SetBool("isMoving", false);
            //controller.Move(GetMoveVector(0,0,Yspeed,0));
        } 
    }

    private Vector3 GetMoveVector(float mouseX, float Xspeed ,float Yspeed , float Zspeed)
    {
        return new Vector3(mouseX * Xspeed * Time.deltaTime, -Yspeed * Time.deltaTime, Zspeed * Time.deltaTime);
    }

    private static float GetMouseX()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");

        if (mouseX > 1)
        {
            mouseX = 1;
        }

        if (mouseX < -1)
        {
            mouseX = -1;
        }

        return mouseX;
    }
}
