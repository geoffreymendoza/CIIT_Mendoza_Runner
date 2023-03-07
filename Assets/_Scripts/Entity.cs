using System;
using UnityEngine;


public abstract class Entity : MonoBehaviour, IHealthHandler
{
    [SerializeField] private EntityType _entityType;
    [SerializeField] private int initialHealth;
    protected int health;
    public int CurrentHealth {
        get => health;
        protected set => health = Mathf.Clamp(value, 0, MaxHealth);
    }
    public int MaxHealth { get; protected set; }
    public bool IsAlive => CurrentHealth > 0;
    protected Animator anim;
    
    public virtual void Apply(ApplyType type) {
        if (!IsAlive) return;
        
        switch (type) {
            case ApplyType.Damage:
                CurrentHealth--;
                break;
            case ApplyType.Regen:
                CurrentHealth++;
                break;
        }

        if (!IsAlive) {
            //TODO death callback
            GameManager.ChangeState(GameState.Lose);
        }
    }

    private void Initialize() {
        MaxHealth = initialHealth;
        CurrentHealth = MaxHealth;
        Debug.Log($"{this.transform.name} Current Health is {CurrentHealth}");
    }

    protected virtual void Awake() {
        anim = GetComponent<Animator>();
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    protected virtual void OnDestroy() {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }
    
    private void OnGameStateChanged(GameState state) {
        switch (state) {
            case GameState.Menu:
                anim.CrossFade(Data.IDLE_ANIM, 0);
                break;
            case GameState.Initialize:
                break;
            case GameState.InGame:
                anim.CrossFade(Data.RUNNING_ANIM, 0);
                break;
            case GameState.Pause:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
        }
    }

    protected virtual void Start() {
        Initialize();
    }
}

public enum EntityType
{
    Unassigned,
    BalloonMan,
}