using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Draw : MonoBehaviour
{

    bool LGBTMode = false;

    private Color[] colours = { Color.red, Color.blue, Color.green, Color.yellow, Color.magenta, new Color(255/225, 140/225, 0) };
    private PlayerInput playerInput;

    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    public Camera camera;
    public GameObject brush;
    public GameObject Block;
    public float dissipationTime = 0.75f;

    public PhysicsMaterial2D bouncemat;

    LineRenderer currentLine;

    Vector2 lastPos;

    private void Awake()
    {
        Application.targetFrameRate = 240;
        playerInput = GetComponent<PlayerInput>();
        touchPositionAction = playerInput.actions["TouchPosition"];
        touchPressAction = playerInput.actions["TouchPress"];
        touchPressAction.started += DoDraw;
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey("ExtraBouncy"))
        {
            if(PlayerPrefs.GetInt("ExtraBouncy") == 1)
            {
                Debug.Log("Extra Bouncy");
                bouncemat.bounciness = 1.1f;
                GameObject.Find("MovingBall").GetComponent<Rigidbody2D>().sharedMaterial = bouncemat;//Physic material only updates when reassigning
            }
            else
            {
                bouncemat.bounciness = 1.0f;
                GameObject.Find("MovingBall").GetComponent<Rigidbody2D>().sharedMaterial = bouncemat;//Physic material only updates when reassigning
            }
        }
        if(PlayerPrefs.HasKey("LowGravity"))
        {
            if (PlayerPrefs.GetInt("LowGravity") == 1)
            {
                Debug.Log("Low Gravity");
                Physics2D.gravity = new Vector3(0, -2.0f, 0);
            }
        }
        if (PlayerPrefs.HasKey("QuickDraw"))
        {
            if (PlayerPrefs.GetInt("QuickDraw") == 1)
            {
                Debug.Log("Quick Draw");
                dissipationTime = 0.45f;
            }
        }
        if (PlayerPrefs.HasKey("LGBTMode"))
        {
            if (PlayerPrefs.GetInt("Block") == 1)
            {
                Debug.Log("LGBT Mode");
                LGBTMode = true;
            }
        }
        //etc when I know what other modifiers I want

    }


    void DoDraw(InputAction.CallbackContext context)
    {
        GameObject brushInstance = Instantiate(brush);
        currentLine = brushInstance.GetComponent<LineRenderer>();
        if(LGBTMode)
            currentLine.material.color = colours[Random.Range(0, colours.Length)];
        Vector2 mousePos = camera.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());

        currentLine.SetPosition(0, mousePos);
        currentLine.SetPosition(1, mousePos);
        Invoke("DestroyBrush", dissipationTime);
    }

    private void FixedUpdate()
    {
        if (touchPressAction.ReadValue<float>() == 1)
        {
            continueDrawing();
        }
    }

    void continueDrawing() {
            Vector2 mousePos = camera.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());
            Debug.Log(mousePos);
            if (mousePos != lastPos)
            {
                AddAPoint(mousePos);
                lastPos = mousePos;
            }
    }

    void DestroyBrush()
    {
        Destroy(GameObject.FindGameObjectsWithTag("Brush")[0]);
    }

    
    void AddAPoint(Vector3 pointPos)
    {
        try
        {
            currentLine.positionCount++;
            int positionIndex = currentLine.positionCount - 1;
            currentLine.SetPosition(positionIndex, pointPos);
        }
        catch
        {
            ;
            //Just in case the offchance that a line is deleted between the if statement and the function
        }
        }
}
