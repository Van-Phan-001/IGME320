using UnityEngine;
using UnityEngine.UI;

public class DragIn : MonoBehaviour
{
    [SerializeField] private Text text;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        text.text = collision.name;
    }

}
