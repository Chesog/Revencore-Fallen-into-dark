using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
   [Header("Character SetUps")]

   #region EXPOSED_FIELDS 
   [SerializeField] public HealthComponent character_Health_Component;
   #endregion

   #region PUBLIC_FIELDS
   public float speed;
   public float initialSpeed;
   public float jumpForce;
   public Vector3 movement;
   public Animator anim;
   public Rigidbody rigidbody;
   [Header("Character Jump Timers")]
   public float jumpBufferTime;
   public float jumpBufferTimeCounter;
   #endregion

   #region UNITY_CALLS
   private void OnEnable()
   {
       if (character_Health_Component == null)
       {
           character_Health_Component = GetComponent<HealthComponent>();
       }
       if (!character_Health_Component)
       {
           Debug.LogError(message: $"{name}: (logError){nameof(character_Health_Component)} is null");
           enabled = false;
       }

       if (rigidbody == null)
       {
           rigidbody = GetComponent<Rigidbody>();
       }
       if (!rigidbody)
       {
           Debug.LogError(message: $"{name}: (logError){nameof(rigidbody)} is null");
           enabled = false;
       }

       if (anim == null)
       {
           anim = GetComponent<Animator>();
       }
       if (!anim)
       {
           Debug.LogError(message: $"{name}: (logError){nameof(anim)} is null");
           enabled = false;
       }
   }
   #endregion

   #region PUBLIC_FIELDS
   /// <summary>
   /// Set The Character Base Variables
   /// </summary>
   /// <param name="_health"></param>
   /// <param name="speed"></param>
   /// <param name="initialSpeed"></param>
   /// <param name="jumpForce"></param>
   /// <param name="anim"></param>
   /// <param name="rigidbody"></param>
   /// <param name="jumpBufferTime"></param>
   public void SetCharacter_Component(HealthComponent _health, float speed, float initialSpeed, float jumpForce, Animator anim, Rigidbody rigidbody, float jumpBufferTime)
   {
       character_Health_Component = _health;
       this.speed = speed;
       this.initialSpeed = initialSpeed;
       this.jumpForce = jumpForce;
       this.anim = anim;
       this.rigidbody = rigidbody;
       this.jumpBufferTime = jumpBufferTime;
   }
   #endregion
}
