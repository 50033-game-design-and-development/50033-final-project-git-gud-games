using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineDollyCartController : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineDollyCart dollyCart;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float startPosition = 0f;
    
    // Start is called before the first frame update
    void Start() {
        dollyCart.m_Speed = 0;
        dollyCart.m_Position = 0;
    }

    public void Move() {
        dollyCart.m_Position = startPosition;
        dollyCart.m_Speed = moveSpeed;

    }

    
}
