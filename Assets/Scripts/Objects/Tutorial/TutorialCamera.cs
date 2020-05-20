using UnityEngine;

public class TutorialCamera : MonoBehaviour
{
    [SerializeField] SpriteRenderer background;

    Vector2 resolution;

    void Start() => Fix();

    private void Update()
    {
        if (Screen.width != resolution.x || Screen.height != resolution.y)
            Fix();
    }
            
    private void Fix()
    {     
        Camera.main.SetWidth(background.size.x);
        transform.position = background.transform.position;
        transform.Translate(0f, (30.8f - Camera.main.GetHeight()) / -2, 0f);

        resolution.x = Screen.width;
        resolution.y = Screen.height;
    }
}
