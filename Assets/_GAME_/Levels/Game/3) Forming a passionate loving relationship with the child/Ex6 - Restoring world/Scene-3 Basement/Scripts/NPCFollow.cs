using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : MonoBehaviour
{
    public float speed = 3f;

    public Transform target;

    public bool beginFollow;
    public Animator _animator;
    private Vector2 _moveDir;

    private Player_Controller player_Controller;

    // Start is called before the first frame update
    void Start()
    {
        beginFollow = false;
        _animator = GetComponent<Animator>();
        _moveDir = new Vector2(0, 0);
        player_Controller = GetComponent<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (beginFollow && Vector2.Distance(transform.position, target.position) > 1)
        {
            _moveDir = target.position - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime); ;
            player_Controller.MovementUpdate(_moveDir);
        }
    }
}
