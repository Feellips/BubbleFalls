using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject _childBubble;
    [SerializeField] private GameObject _childBubble2;

    private void Update()
    {
        var horizontalAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var structure = new MyStruct(gameObject, horizontalAxis);
            StartCoroutine(nameof(PerformMovement), structure);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            if (_childBubble is not null)
            {
                var structure = new MyStruct(_childBubble, horizontalAxis);
                StartCoroutine(nameof(PerformMovement), structure);
            }

            if (_childBubble2 is not null)
            {
                var structure2 = new MyStruct(_childBubble2, horizontalAxis);
                StartCoroutine(nameof(PerformMovement), structure2);
            }

        }

    }

    private void Move(GameObject player, float axis)
    {
        if (axis != 0)
        {
            var move = Vector3.right * axis * 10;

            if (CanMove(player.transform.position, move)) return;

            player.transform.Translate(move);
        }
    }

    private IEnumerator PerformMovement(MyStruct myStruct)
    {
        var possibleMove = Vector3.right * myStruct.Axis * 10;

        if (CanMove(myStruct.Player.transform.position, possibleMove))
            for (int i = 0; i < 10; i++)
            {
                var move = Vector3.right * myStruct.Axis;

                myStruct.Player.transform.Translate(move);

                yield return null;
            }
    }

    private bool CanMove(Vector3 position, Vector3 desired)
    {
        var possibleMove = position.x + desired.x;

        return possibleMove <= 10 && possibleMove >= -10;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward);
    }

    class MyStruct
    {
        public GameObject Player;
        public float Axis;

        public MyStruct(GameObject player, float axis)
        {
            Player = player;
            Axis = axis;
        }
    }
}
