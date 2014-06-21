using UnityEngine;
using System.Collections;
using core;
using System.Collections.Generic;

public class Bubble : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case Tags.Ground:
                Debug.Log("Collide Ground");
                _groundColliders.Add(collider);
                break;
        }
    }

	// Use this for initialization
	void Start () 
    {
	}

    public void Initialize(Vector3 dir)
    {
        CreateStateMachine();
        _shootDirection = dir;
        TransitionTo(Shoot);
    }
	
	// Update is called once per frame
	void Update() 
    {
        AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
        _currentAnimationTime = info.normalizedTime;

        if (_currentState != null)
        {
            _currentState.Run(Time.deltaTime);
        }

        _groundColliders.Clear();
        _popDetector.Clear();
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
    }

    private void RunShoot(float deltaTime)
    {
        Move(_shootDirection, SHOOT_SPEED, deltaTime);
        if (IsAnimationCompleted("Shoot") || _groundColliders.Count > 0)
        {
            TransitionTo(Rise);
        }
    }

    private void BeginRise()
    {
        _riseTimer = 0.0f;
    }
    
    private void RunRise(float deltaTime)
    {
        Move(Vector3.up, RISE_SPEED, deltaTime);
        ClampPositionY(MAX_RISE_Y);
        _riseTimer += deltaTime;
        if (DetectPop())
        {
            TransitionTo(Pop);
        }
        else if (_riseTimer > RISE_TIME)
        {
            TransitionTo(Shake);
        }
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
        _shakeTimer = 0.0f;
    }

    private void RunShake(float deltaTime)
    {
        ApplyShake();
        _shakeTimer += deltaTime;
        if (DetectPop())
        {
            TransitionTo(Pop);
        }
        else if (_shakeTimer > RISE_TIME)
        {
            TransitionTo(AboutTopPop);
        }
    }

    private void BeginAboutToPopState()
    {
        _renderer.color = ABOUT_TO_POP_COLOR;
    }

    private void RunAboutToPop(float deltaTime)
    {
        ApplyShake();
        _aboutToPopTimer += deltaTime;
        if (DetectPop() || _aboutToPopTimer > RISE_TIME)
        {
            TransitionTo(Pop);
        }
    }

    private void BeginPop()
    {
        _animator.SetBool("Pop", true);
    }

    private void RunPop(float deltaTime)
    {
        if (IsAnimationCompleted("Pop"))
        {
            TransitionTo(Die);
        }
    }

    private void BeginDie()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void CreateStateMachine()
    {
        Shoot = new State(BeginShoot, RunShoot, null);
        Rise = new State(BeginRise, RunRise, null);
        Fall = new State(BeginFall, RunFall, null);
        Shake = new State(BeginShakeState, RunShake, null);
        AboutTopPop = new State(BeginAboutToPopState, RunAboutToPop, null);
        Pop = new State(BeginPop, RunPop, null);
        Die = new State(BeginDie, null, null);
    }

    private State Shoot;
    private State Rise;
    private State Fall;
    private State Shake;
    private State AboutTopPop;
    private State Pop;
    private State Die;

    #endregion Statemachine

    #region AnimationAPI
    private bool IsAnimationCompleted(string name)
    {
        AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
        return info.IsName(name) && info.normalizedTime >= 1.0f;
    }
    #endregion

    private void ApplyShake()
    {
        Vector3 position = _sprite.localPosition;
        position.y = _shaker.Value;
        _sprite.localPosition = position;
    }

    private void Move(Vector3 direction, float speed, float deltaTime)
    {
        Vector3 position = transform.position;
        position += direction * (deltaTime * speed);
        transform.position = position;
    }

    private void ClampPositionY(float max)
    {
        Vector3 position = transform.position;
        position.y = Mathf.Min(max, position.y);
        transform.position = position;
    }

    private bool DetectPop()
    {
        return _popDetector.ShouldPop;
    }

    [SerializeField]
    private Shaker _shaker;
    [SerializeField]
    private Transform _sprite;
    [SerializeField]
    private SpriteRenderer _renderer;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private BubblePopDetector _popDetector;

    [SerializeField]
    private float _currentAnimationTime;

    private bool _water; // Is it a water bubble.
    private Vector3 _shootDirection = new Vector3(1.0f, 0.0f, 0.0f);

    private State _currentState;
    private float _riseTimer;
    private float _shakeTimer;
    private float _aboutToPopTimer;
    private List<Collider2D> _playerColliders = new List<Collider2D>();
    private List<Collider2D> _groundColliders = new List<Collider2D>();

    // Constants
    [SerializeField]
    private Color GREEN_COLOR = Color.green;
    [SerializeField]
    private Color BLUE_COLOR = Color.blue;
    [SerializeField]
    private Color NEUTRAL_COLOR = Color.white;
    [SerializeField]
    private Color ABOUT_TO_POP_COLOR = Color.red;
    [SerializeField]
    private float SHOOT_SPEED;
    [SerializeField]
    private float RISE_SPEED;
    [SerializeField]
    private float MAX_RISE_Y = 1.8f;
    [SerializeField]
    private float RISE_TIME = 10.0f;
}
