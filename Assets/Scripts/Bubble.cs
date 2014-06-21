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
        TransitionTo(AboutTopPop);
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
    private void BeginShot()
    {

    }

    private void ShotState(float deltaTime)
    {

    }

    private void BeginShakeState()
    {
        _shaker.Reset();
    }

    private void ShakeState(float deltaTime)
    {
        ApplyShake();
    }

    private void BeginAboutToPopState()
    {
        _renderer.color = ABOUT_TO_POP_COLOR;
    }

    private void AboutToPopState(float deltaTime)
    {
        ApplyShake();
    }

    private void CreateStateMachine()
    {
        Shot = new State(BeginShot, ShotState, null);
        Shake = new State(BeginShakeState, ShakeState, null);
        AboutTopPop = new State(BeginAboutToPopState, AboutToPopState, null);
    }

    private State Shot;
    private State FallDownFromSky;
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

    private bool _water; // Is it a water bubble.

    private State _currentState;

    // Constants
    private Color GREEN_COLOR = Color.green;
    private Color BLUE_COLOR = Color.blue;
    private Color NEUTRAL_COLOR = Color.white;
    private Color ABOUT_TO_POP_COLOR = Color.red;




}
