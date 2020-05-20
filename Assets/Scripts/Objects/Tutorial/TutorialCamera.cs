using UnityEngine;

public class TutorialCamera : MonoBehaviour
{
    [SerializeField] SpriteRenderer background;

    Vector2 resolution;

    void Start()
    {
        resolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        Fix();
    }

    private void Update()
    {
        if (Screen.currentResolution.width != resolution.x || Screen.currentResolution.height != resolution.y)
        {
            Debug.Log("a");
            Fix();
        }
    }
            
    private void Fix()
    {
        Camera.main.orthographicSize = background.size.x * Screen.height / Screen.width / 2;
        transform.position = background.transform.position;
        transform.Translate(0f, Mathf.Abs(30.8f - Camera.main.GetHeight()) / 2, 0f);
    }
}
