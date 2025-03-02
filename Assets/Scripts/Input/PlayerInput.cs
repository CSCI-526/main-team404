using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    public InputActions inputActions;
    public bool Jump => inputActions.GamePlay.Jump.WasPressedThisFrame();
    public bool isJumpBuffered { get; set; }
    public float jumpBufferTimeWindow;
    WaitForSeconds jumpBufferTime;
    public bool Roll => inputActions.GamePlay.Roll.WasPressedThisFrame();
    public bool isRollBuffered { get; set; }
    public float rollBufferTimeWindow;
    WaitForSeconds rollBufferTime;
    public bool Attack => inputActions.GamePlay.Attack.WasPressedThisFrame();
    public bool isAttackBuffered { get; set; }
    public float attackBufferTimeWindow;
    WaitForSeconds attackBufferTime;
    public bool Deflect => inputActions.GamePlay.Deflect.WasPressedThisFrame();
    public bool isDeflectBuffered { get; set; }
    public float deflectBufferTimeWindow;
    WaitForSeconds deflectBufferTime;
    public bool Grab => inputActions.GamePlay.Grab.WasPressedThisFrame();
    public bool isGrabBuffered { get; set; }
    public float grabBufferTimeWindow;
    WaitForSeconds grabBufferTime;
    public bool Skill => inputActions.GamePlay.Skill.WasPressedThisFrame();
    public bool isSkillBuffered { get; set; }
    public float skillBufferTimeWindow;
    WaitForSeconds skillBufferTime;
    public Vector2 AxesInput => inputActions.GamePlay.Move.ReadValue<Vector2>();
    public float Xinput => AxesInput.x;
    public float Yinput => AxesInput.y;
    public bool isUpBuffered { get; set; }
    public float upBufferTimeWindow;
    WaitForSeconds upBufferTime;
    public bool isDownBuffered { get; set; }
    public float downBufferTimeWindow;
    WaitForSeconds downBufferTime;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            inputActions = new InputActions();
            jumpBufferTime = new WaitForSeconds(jumpBufferTimeWindow);
            rollBufferTime = new WaitForSeconds(rollBufferTimeWindow);
            attackBufferTime = new WaitForSeconds(attackBufferTimeWindow);
            upBufferTime = new WaitForSeconds(upBufferTimeWindow);
            downBufferTime = new WaitForSeconds(downBufferTimeWindow);
        }


    }
    public void DisableGamePlayInputs()
    {
        inputActions.GamePlay.Disable();
    }
    public void DisableGamePlayInputs(float duration)
    {
        StartCoroutine(LoseControl(duration));
    }

    public void EnableGamePlayInputs()
    {
        inputActions.GamePlay.Enable();
    }

    public void SetJumpBufferTimer()
    {
        StopCoroutine(nameof(JumpBufferCoroutine));
        StartCoroutine(nameof(JumpBufferCoroutine));
    }
    public void SetRollBufferTimer()
    {
        StopCoroutine(nameof(RollBufferCoroutine));
        StartCoroutine(nameof(RollBufferCoroutine));
    }
    public void SetAttackBufferTimer()
    {
        StopCoroutine(nameof(AttackBufferCoroutine));
        StartCoroutine(nameof(AttackBufferCoroutine));
    }
    public void SetDeflectBufferTimer()
    {
        StopCoroutine(nameof(DeflectBufferCoroutine));
        StartCoroutine(nameof(DeflectBufferCoroutine));
    }
    public void SetGrabBufferTimer()
    {
        StopCoroutine(nameof(GrabBufferCoroutine));
        StartCoroutine(nameof(GrabBufferCoroutine));
    }
    public void SkillGrabBufferTimer()
    {
        StopCoroutine(nameof(SkillBufferCoroutine));
        StartCoroutine(nameof(SkillBufferCoroutine));
    }

    public void SetUpBufferTimer()
    {
        StopCoroutine(nameof(UpBufferCoroutine));
        StartCoroutine(nameof(UpBufferCoroutine));
    }

    public void SetDownBufferTimer()
    {
        StopCoroutine(nameof(DownBufferCoroutine));
        StartCoroutine(nameof(DownBufferCoroutine));
    }
    IEnumerator JumpBufferCoroutine()
    {
        isJumpBuffered = true;
        yield return jumpBufferTime;
        isJumpBuffered = false;
    }
    IEnumerator RollBufferCoroutine()
    {
        isRollBuffered = true;
        yield return rollBufferTime;
        isRollBuffered = false;
    }
    IEnumerator AttackBufferCoroutine()
    {
        isAttackBuffered = true;
        yield return attackBufferTime;
        isAttackBuffered = false;
    }
    IEnumerator DeflectBufferCoroutine()
    {
        isDeflectBuffered = true;
        yield return deflectBufferTime;
        isDeflectBuffered = false;
    }

    IEnumerator GrabBufferCoroutine()
    {
        isGrabBuffered = true;
        yield return grabBufferTime;
        isGrabBuffered = false;
    }
    IEnumerator SkillBufferCoroutine()
    {
        isSkillBuffered = true;
        yield return grabBufferTime;
        isSkillBuffered = false;
    }

    IEnumerator LoseControl(float duration)
    {
        DisableGamePlayInputs();
        yield return new WaitForSeconds(duration);
        EnableGamePlayInputs();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-enable gameplay inputs once the new scene is fully loaded
        EnableGamePlayInputs();
    }

    IEnumerator UpBufferCoroutine()
    {
        isUpBuffered = true;
        yield return upBufferTime;
        isUpBuffered = false;
    }

    IEnumerator DownBufferCoroutine()
    {
        isDownBuffered = true;
        yield return downBufferTime;
        isDownBuffered = false;
    }


}
