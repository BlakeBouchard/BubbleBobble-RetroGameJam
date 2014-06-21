using UnityEngine;
using System.Collections;
using core;

public class Bubble : MonoBehaviour 
{
    public static void ShootBubble(Vector3 direction)
    {

    }

	// Use this for initialization
	void Start () 
    {
        CreateStateMachine();
        TransitionTo(Shoot);
	}
	
	// Update is called once per frame
	void Update() 
    {
        if (_currentState != null)
        {
            _currentState.Run(Time.deltaTime);
        }
	}

    #region StatemachineAPI
    private void TransitionTo(State state)
    {
        if (_currentState != null)
        {
            _currentState.End();
        }
        _currentState = state;
        _currentState.Start();
    }
    #endregion

    #region Statemachine
    private void BeginShoot()
    {
        _animator.SetTrigger("Pop");
    }

    private void RunShoot(float deltaTime)
    {
        //AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
    }

    private void BeginRise()
    {
    }
    
    private void RunRise(float deltaTime)
    {
    }

    private void BeginFall()
    {
    }

    private void RunFall(float deltaTime)
    {
    }

    private void BeginShakeState()
    {
        _shaker.Reset();
    }

    private void RunShake(float deltaTime)
    {
        ApplyShake();
    }

    private void BeginAboutToPopState()
    {
        _renderer.color = ABOUT_TO_POP_COLOR;
    }

    private void RunAboutToPop(float deltaTime)
    {
        ApplyShake();
    }

    private void BeginPop()
    {
        _animator.SetBool("Pop", true);
    }

    private void CreateStateMachine()
    {
        Shoot = new State(BeginShoot, RunShoot, null);
        Rise = new State(BeginRise, RunRise, null);
        Fall = new State(BeginFall, RunFall, null);
        Shake = new State(BeginShakeState, RunShake, null);
        AboutTopPop = new State(BeginAboutToPopState, RunAboutToPop, null);
        Pop = new State(BeginPop, null, null);
    }

    private State Shoot;
    private State Rise;
    private State Fall;
    private State Shake;
    private State AboutTopPop;
    private State Pop;


    #endregion Statemachine


    private void ApplyShake()
    {
        Vector3 position = _sprite.transform.position;
        position.y = _shaker.Value;
        _sprite.position = position;
    }

    [SerializeField]
    private Shaker _shaker;
    [SerializeField]
    private Transform _sprite;
    [SerializeField]
    private SpriteRenderer _renderer;
    [SerializeField]
    private Animator _animator;

    private bool _water; // Is it a water bubble.

    private State _currentState;

    // Constants
    private Color GREEN_COLOR = Color.green;
    private Color BLUE_COLOR = Color.blue;
    private Color NEUTRAL_COLOR = Color.white;
    private Color ABOUT_TO_POP_COLOR = Color.red;
}
