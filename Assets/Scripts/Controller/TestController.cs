using Assets.Fundamentals;
using Assets.Scripts.Extensions;
using UnityEngine;
using Assets.CharacterInfo;
using Assets.Supervisor;

public class TestController : MonoBehaviour
{
    public Keys Keys { get; private set; }
    public Movement Movement { get; private set; }
    public Animator Animator { get; private set; }

    private const string source = nameof(TestController);
    public Character Character { get; set; }

    private void Awake()
    {
        Keys = new Keys(index: 0);
        Animator = GetComponent<Animator>();
        Movement = new Movement(maxSpeed: 5.3f, acceleration: 6f);
        Character = GetComponent<Character>();

        Movement.Direction = transform.forward;
        GameManager.Instance.GlobalFixedUpdate += Behavior;
    }

    private void Behavior()
    {
        this
            .Initialize()
            .CheckAttack()
            .CheckSkill()
            .GetDirection()
            .Turn()
            .Accelerate();

        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        this.Animator.SetFloat("speed", this.Movement.CurrentSpeed);
    }
}
