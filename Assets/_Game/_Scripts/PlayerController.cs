using UnityEngine;
using UnityEngine.UI;

public sealed class PlayerController : MonoBehaviour
{
    
    [SerializeField] private Texture2D texture2D;

    private void Start()
    {
        Cursor.SetCursor(texture2D, Vector2.zero, CursorMode.Auto);

    }
    
}