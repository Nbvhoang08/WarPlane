using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
     
    public Rigidbody2D Rb;
    private float horizontal;
    private float vertical;
    private Vector2 joystickVector;
    public float rotationSpeed = 1;
    public float velocity = 1;
    public float controllerDeadzone = 0;
    public float hitRotationAmount = 0;
    public GameObject pointerRotator;
    public GameObject pointer;
    public Quaternion targetQuat;
    private bool giveUserControl = true;
    public float smoothTime = 0.1f;
    public float maxSpeed = 1;
    private bool isFreeFalling = false; // Biến để kiểm tra trạng thái rơi tự do

    private bool canDash = true;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    public GameObject DashTrail;
    public GameObject TrailL;
    public GameObject TrailR;

    // Thêm biến cho Joystick
    public Joystick joystick;

    // Biến để theo dõi trạng thái quay đầu
    [SerializeField] private bool isTurningBack = false;
    private Vector3 initialTurnBackPosition;

    // Biến để theo dõi camera
    private Camera mainCamera;
    private Vector2 screenBounds;
    public Vector3 viewPos ;
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        if(joystick==null){
            joystick = FindObjectOfType<Joystick>();
        }
    }

    Vector2 velo = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        if(joystick==null){
            joystick = FindObjectOfType<Joystick>();
        }
        // Lấy đầu vào từ Joystick
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
        joystickVector = new Vector2(horizontal, vertical).normalized;

        pointerRotator.transform.position = transform.position;

        if (giveUserControl && joystickVector.magnitude > 0.1f)
            targetQuat = Quaternion.Euler(0, 0, Mathf.Atan2(joystickVector.y, joystickVector.x) * Mathf.Rad2Deg);

        if (joystickVector != Vector2.zero)
            pointerRotator.transform.rotation = Quaternion.Slerp(pointerRotator.transform.rotation, targetQuat, 0.1f);

        Vector2 pointerDamp = Vector2.SmoothDamp(pointer.transform.localPosition, new Vector2(joystickVector.magnitude * 0.3f, 0), ref velo, 0.05f);

        pointer.transform.localPosition = pointerDamp;

        // Dash input handling
        if (Input.GetKeyDown(KeyCode.R) && canDash)
        {
            StartCoroutine(Dash());
        }
        
    }
    public void Launch()
    {
        if (canDash)
        {
            StartCoroutine(Dash());
            SoundManager.Instance.PlayVFXSound(1);
        }
    }

    void SetDashTrail(bool isDash)
    {
        DashTrail.SetActive(isDash);
    }

    void SetSideTrail(bool isDash)
    {
        TrailL.SetActive(isDash);
        TrailR.SetActive(isDash);
    }

    private void FixedUpdate()
    {
        if (!giveUserControl || (giveUserControl && joystickVector.magnitude > controllerDeadzone) && canDash)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetQuat, 0.06f);
            Rb.velocity = transform.right * velocity;
            isFreeFalling = false; // Đặt lại trạng thái rơi tự do
            SetSideTrail(true);
        }
        else if (!isFreeFalling && !isTurningBack)
        {
            // Khi joystickVector = Vector2.zero và chưa rơi tự do, bắt đầu Coroutine
            StartCoroutine(FreeFall());
        }

        // Kiểm tra xem nhân vật có vượt ra khỏi tầm nhìn của camera hay không
        viewPos = mainCamera.WorldToViewportPoint(transform.position);
        bool outOfBounds = viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1;
         // Giới hạn tọa độ X và Y của nhân vật
        // Vector3 position = transform.position;
        // position.x = Mathf.Clamp(position.x, -65f, 65f);
        // position.y = Mathf.Clamp(position.y, -15f, 50f);
        // transform.position = position;
        if (outOfBounds && !isTurningBack) { 
            StartCoroutine(TurnBack()); 
        }
    }

    private IEnumerator TurnBack()
    {
        isTurningBack = true;
        initialTurnBackPosition = transform.position;
        giveUserControl = false;

        if(viewPos.y< -0)
        {
            targetQuat = Quaternion.Euler(0, 0, 90);
            
        }else if(viewPos.y > 1){
            targetQuat = Quaternion.Euler(0, 0, -90);
         
        }else if(viewPos.x< -0)
        {
            targetQuat = Quaternion.Euler(0, 0,0);
            
        }else if(viewPos.x > 1f){
            targetQuat = Quaternion.Euler(0, 0, 180);
            
        }
        while (Vector3.Distance(transform.position, initialTurnBackPosition) < 10f)
        {
                Rb.velocity = transform.right * velocity; 
                yield return null;
        }


        Rb.velocity = Vector2.zero;
        giveUserControl = true;
        isTurningBack = false;
    }

    private IEnumerator FreeFall()
    {
        isFreeFalling = true; // Đặt trạng thái rơi tự do
        float duration = 0.5f; // Thời gian để giảm vận tốc về 0
        float elapsed = 0f;
        SetSideTrail(false);
        // SetDashTrail(false);
        Vector2 initialVelocity = Rb.velocity;

        while (elapsed <= duration && Rb.velocity.y > 0)
        {
            elapsed += Time.fixedDeltaTime;
            Rb.velocity = Vector2.Lerp(initialVelocity, Vector2.zero, elapsed / duration);

            yield return new WaitForFixedUpdate();
        }

        while (isFreeFalling && !isTurningBack)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.fixedDeltaTime); // Xoay máy bay để tạo cảm giác rơi tự do

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        float startTime = Time.time;
        SetSideTrail(false);
        SetDashTrail(true);
        while (Time.time < startTime + dashDuration)
        {
            Rb.velocity = transform.right * dashSpeed;
            yield return null;
        }
        canDash = true;
        Rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashCooldown);

        SetDashTrail(false);
    }

   

}
