using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    public enum TargetState { Ready, Spawn, Run, End }

    private Rigidbody2D rb;
    private float pushingPower = 15f;         // 위로 올리는 힘
    private float increaseGravityTime = 0.1f; // 중력 늘어나는 속도
    private float maxGravity = 5f;            // 최대 중력

    private float activeRunTime = 3f;         // 스폰 시, 게임 시작되는 시간

    [SerializeField] private float limitDropSpeed;     // 낙하 한계속도
    [SerializeField] private GameObject targetCompass;
    [SerializeField] private TextMeshProUGUI textTarget;

    public TargetState targetState = TargetState.Ready;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetCompass.SetActive(false);
    }
    private void Update()
    {
        switch (targetState)
        {
            case TargetState.Ready:
                textTarget.text = "Push Here";
                TargetReady();
                break;
            case TargetState.Spawn:
                TargetReady();
                activeRunTime -= Time.deltaTime;
                textTarget.text = Mathf.CeilToInt(activeRunTime).ToString();
                if (activeRunTime < 0f) targetState = TargetState.Run;
                break;
            case TargetState.Run:
                if(rb.gravityScale < 1f) rb.gravityScale = 1f;
                if(textTarget.gameObject.activeInHierarchy== true) textTarget.gameObject.SetActive(false);
                IncreaseGravity();
               // Physics2D.IgnoreLayerCollision(6, 6, false);
                break;
        }
    }
    private void TargetReady()
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        // Physics2D.IgnoreLayerCollision(6, 6, true);
    }
    private void FixedUpdate()
    {
        if(rb == null)
        {
            Debug.Log("Rigidbody is null");
        }
        if (rb.velocity.y < -limitDropSpeed)
            rb.velocity = new Vector2(rb.velocity.x, -limitDropSpeed);
    }
    private void LateUpdate()
    {
        SetCompassTransform();
    }
    private void OnMouseDown()
    {
        if(targetState == TargetState.Ready || targetState == TargetState.Spawn)
        {
            targetState = TargetState.Run;
        }
        if(GameManager.Instance.gameState == GameManager.GameState.NotRun)
        {
            GameManager.Instance.gameState = GameManager.GameState.Run;
        }

        if(targetState == TargetState.Run)
            PushUp();
    }
    private void PushUp() 
    {
        var xForce = Random.Range(-0.2f, 0.2f);

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(xForce * 10f, 1 * pushingPower), ForceMode2D.Impulse);

        GameManager.Audio.SetAudioSFXClip(GameManager.Audio.SourceSFX, AudioManager.AudioClipSFXAddress.Target);
    }
    private void IncreaseGravity()
    {
        rb.gravityScale += (Time.deltaTime * increaseGravityTime);
        if (rb.gravityScale > maxGravity) rb.gravityScale = maxGravity;
    }

    private void SetCompassTransform()
    {
        Vector3 thisPosition = this.transform.position;
        Vector3 cameraPosition = new Vector3(0, Camera.main.orthographicSize,  0);

        if(thisPosition.y > cameraPosition.y)
        {
            targetCompass.SetActive(true);
            targetCompass.transform.position = new Vector3(thisPosition.x, cameraPosition.y -1 , 0);
        }
        else
        {
            targetCompass.SetActive(false);
        }
    }
    // ================================================================================================================

    public void SetGreenItem()
    {
        rb.gravityScale = 1f;
        rb.velocity = Vector2.zero;
    }
}