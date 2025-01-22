using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetUpBoundaries : MonoBehaviour
{
    GameObject LEdge, REdge, TEdge, FinishZone;

    [SerializeField]
    private float edgeOffset = 5;
    private float buttonOffset = 0.04f;

    static GameObject instance;

    // Start is called before the first frame update
    void Start()
    {
        GameObject LEdge = GameObject.Find("LeftEdge");
        GameObject REdge = GameObject.Find("RightEdge");
        GameObject TEdge = GameObject.Find("TopEdge");
        GameObject FinishZone = GameObject.Find("FinishZone");
     
        LEdge.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(edgeOffset, Screen.height / 2, 0));
        REdge.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width-edgeOffset, Screen.height / 2, 0));
        TEdge.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height-edgeOffset, 0));
        FinishZone.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, edgeOffset, 0));

        LEdge.transform.position = new Vector3(LEdge.transform.position.x, LEdge.transform.position.y, 0);
        REdge.transform.position = new Vector3(REdge.transform.position.x, REdge.transform.position.y, 0);
        TEdge.transform.position = new Vector3(TEdge.transform.position.x, TEdge.transform.position.y, 0);
        FinishZone.transform.position = new Vector3(FinishZone.transform.position.x, FinishZone.transform.position.y, 0);

        if(instance == null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            GameObject Button = GameObject.Find("Canvas/Button");
            Button.transform.position = Camera.main.ScreenToWorldPoint(new Vector3((buttonOffset * Screen.width), Screen.height - (buttonOffset * Screen.width), 0));
            Button.transform.position = new Vector3(Button.transform.position.x, Button.transform.position.y, 0);
        }
    }
}
