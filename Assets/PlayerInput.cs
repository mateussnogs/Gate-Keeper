using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public PlayerMovement playerMovement;
    private Vector2 dir;

    void Update()
    {
        KeyboardInput();
    }
    private void KeyboardInput()
    {
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        playerMovement.Move(dir);
    }
}
