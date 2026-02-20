using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private void Update()
    {
        transform.Translate(new Vector2(-speed * Time.deltaTime, 0)); // двигаем объект влево
    }
}