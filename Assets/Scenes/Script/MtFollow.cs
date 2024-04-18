using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MtFollow : MonoBehaviour
{

    public GameObject player_01; 
    public float moveSpeed; 
    private Vector3 player_01Position; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ����� �ִ��� üũ
        if (player_01.gameObject != null)
        {
            // this�� ī�޶� �ǹ� (z���� ī�޶��� �״�� ����)
            player_01Position.Set(player_01.transform.position.x, player_01.transform.position.y, this.transform.position.z);

            // vectorA -> B���� T�� �ӵ��� �̵�
            this.transform.position = Vector3.Lerp(this.transform.position, player_01Position, moveSpeed * Time.deltaTime);
        }
    }
}
