using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public bool canShoot;
    [SerializeField] private Transform bullet_ShootPoint = default;
    [SerializeField] private ParticleSystem shootFX = default;
    private float horizontal, touchStartPositionX, touchEndPositionX;
    private readonly float x_Speed = 5f;
    private readonly float z_Speed = 25f;
    private readonly float rotationSpeed = 10f;
    private readonly float maxAngle = 10f;
    private Rigidbody myBody;
    private Touch touch;
    private Vector3 speed;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        speed = Vector3.forward * z_Speed;
        canShoot = true;
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            TouchInput();
        } 
        else
        {
            KeyboardInput();
        }
        
        ChangeRotation();
    }

    void FixedUpdate()
    {
        myBody.MovePosition(myBody.position + (speed * Time.fixedDeltaTime));        //Moves the tank - Mueve el tanque
    }

    private void ChangeRotation()
    {
        if (speed.x > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else if (speed.x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, -maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * rotationSpeed);
        }
    }

    //------------------------------ Keyboard Input Section ------------------------------
    private void KeyboardInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        speed.x = x_Speed * horizontal;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }
    }

    //------------------------------ Mobile Input Section ------------------------------
    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPositionX = touch.position.x;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                touchEndPositionX = touch.position.x;

                if (touchStartPositionX > touchEndPositionX)
                {
                    speed.x = -x_Speed;
                }
                else if (touchStartPositionX < touchEndPositionX)
                {
                    speed.x = x_Speed;
                }
            } 
            else if (touch.phase == TouchPhase.Ended)
            {
                speed.x = 0f;
            }
        }
    }

    //Called when "Shoot" Button is pressed for Mobile Input, and from "Update" for Keyboard input
    public void ShootBullet()
    {
        if (Time.timeScale != 0 && canShoot)
        {
            GameObject bullet = ObjectPool.instance.GetPooledBullet();
            if(bullet != null)
            {
                bullet.transform.position = bullet_ShootPoint.position;
                bullet.transform.rotation = Quaternion.identity;
                bullet.SetActive(true);
            }
            shootFX.Play();
            SFXManager.instance.PlaySXF(1);
            UIManager.instance.ShootEnergy();
        }
    }
}