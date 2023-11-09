using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConstants", menuName = "ScriptableObjects/PlayerConstants", order = 0)]
public class PlayerConstants : ScriptableObject {
    public float mouseSensitivityX = 0.4f;
    public float mouseSensitivityY = 0.25f;

    public float viewMinimumY = -60f;
    public float viewMaximumY = 60f;

    public float moveSpeed = 10f;
    public float gravity = -9.81f;
}