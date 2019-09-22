using Assets.Fundamentals;
using UnityEngine;
using static UnityEngine.Input;
using static UnityEngine.Time;
using Assets.CharacterInfo;
using static Assets.CharacterInfo.Character;

namespace Assets.Scripts.Extensions
{
    public static class TestControllersExtensions
    {
        public static TestController CheckAttack(this TestController @this)
        {
            PlayerStats stats = @this.Character.Stats;
            if (Time.time < stats.NextAvailabeAttack)
                return @this;

            bool isAttacking = GetButtonDown(@this.Keys[KeyNames.AttackKey]);
            if (isAttacking)
            {
                @this.Animator.SetTrigger("attack");
                @this.Movement.CanMove = false;
                @this.Movement.CurrentSpeed *= 0.3f;
                stats.NextAvailabeAttack = Time.time + stats.CoolTime;
            }

            return @this;
        }

        public static TestController CheckSkill(this TestController @this)
        {
            if (GetButtonDown(@this.Keys[KeyNames.ThrowWeaponKey]))
                Skills.ThrowWeapon.Use();
            else if (GetButtonDown(@this.Keys[KeyNames.RollKey]))
                Skills.Roll.Use();

            return @this;
        }

        public static TestController GetDirection(this TestController @this)
        {
            var horizontal = GetAxis(@this.Keys[KeyNames.HorizontalKey]);
            var vertical = GetAxis(@this.Keys[KeyNames.VerticalKey]);
            
            var dir = new Vector3(horizontal, 0, vertical);
            if (dir == Vector3.zero)
                return @this;

            @this.Movement.Direction = dir;

            return @this;
        }

        public static TestController Accelerate(this TestController @this)
        {
            var conditions = (PlayerAnimations.None | PlayerAnimations.TakingHit);
            if ((@this.Character.CurrentAnimation & conditions) == 0)
                return @this;

            var movement = @this.Movement;

            if (!@this.ThereIsInput())
                return @this;


            bool canBeFullyAccelerated = (movement.CurrentSpeed + movement.Acceleration * fixedDeltaTime) < movement.MaxSpeed;
            if (canBeFullyAccelerated){
                if (@this.Character.CurrentAnimation == PlayerAnimations.TakingHit)
                    movement.CurrentSpeed += movement.Acceleration / 2 * fixedDeltaTime;
                else
                    movement.CurrentSpeed += movement.Acceleration * fixedDeltaTime;
            }
            else
                movement.CurrentSpeed = movement.MaxSpeed;

            return @this;
        }

        public static TestController Turn(this TestController @this)
        {
            var conditions = (PlayerAnimations.None | PlayerAnimations.TakingHit);
            if ((@this.Character.CurrentAnimation & conditions) == 0)
                return @this;

            #region variables
            var movement = @this.Movement;
            var direction = movement.Direction;
            var targetRotation = Quaternion.LookRotation(direction);
            #endregion

            @this.transform.rotation = targetRotation;
            return @this;
        }

        public static TestController Initialize(this TestController @this)
        {
            #region Variables
            float smoothFactor = 15 * fixedDeltaTime;
            var movement = @this.Movement;
            var currentSpeed = movement.CurrentSpeed;
            #endregion

            if (!GetButton(@this.Keys[KeyNames.HorizontalKey]) && !GetButton(@this.Keys[KeyNames.VerticalKey]))
                movement.CurrentSpeed = Mathf.SmoothStep(currentSpeed, 0, smoothFactor);

            return @this;
        }

        private static TestController AnimateMovement(this TestController @this)
        {
            #region variables
            var movementVector = @this.Movement.MovementVector;
            #endregion

            @this.Animator.SetFloat("speed", movementVector.magnitude);

            return @this;
        }

        private static bool ThereIsInput(this TestController @this)
        {
            return GetButton(@this.Keys[KeyNames.HorizontalKey]) || GetButton(@this.Keys[KeyNames.VerticalKey]);
        }
    }
}
