using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour
{
    public float movementSpeed = 4.0f;

    public float maxHp = 100;
    public float invulnerabilityWindow = 0.5f;
    public float fireRate = 1.0f;
    public int maxAmmo = 12;

    public GameObject projectilePrefab;
    public GameObject overdriveOverlay;

    [HideInInspector]
    public float hp { get { return currentHp; }}
    float currentHp;

    [HideInInspector]
    public float ammo { get { return currentAmmo; }}
    float currentAmmo;

    bool isInvulnerable;
    float lastInvulnerableTime;
    float lastLaunchTime;

    bool overdrive;
    float lastOverdriveTime;
    float overdriveDuration;
    float overdriveSpeedMod = 1f;
    float overdriveFireRateMod = 1f;

    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHp = maxHp;
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        isInvulnerable = Time.time < lastInvulnerableTime + invulnerabilityWindow;
        overdrive = Time.time < lastOverdriveTime + overdriveDuration;

        if (overdrive)
        {
            overdriveSpeedMod = 1.5f;
            overdriveFireRateMod = 0.25f;
        }
        else
        {
            overdriveSpeedMod = 1f;
            overdriveFireRateMod = 1f;
            
            if (overdriveOverlay.activeInHierarchy)
            {
                overdriveOverlay.SetActive(false);
            }
        }

        if (Input.GetButtonDown("Launch"))
        {
            if ((Time.time > lastLaunchTime + ((1/fireRate) * overdriveFireRateMod)) && currentAmmo > 0)
            {
                lastLaunchTime = Time.time;
                if (!overdrive)
                {
                    currentAmmo--;
                    UiAmmoSlider.instance.SetSize(currentAmmo/(float)maxAmmo);
                }
                Launch();
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + movementSpeed * horizontal * Time.deltaTime * overdriveSpeedMod;
        position.y = position.y + movementSpeed * vertical * Time.deltaTime * overdriveSpeedMod;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHP(float change)
    {
        if (change < 0)
        {
            if (isInvulnerable) return;
            
            animator.SetTrigger("Hit");

            lastInvulnerableTime = Time.time;
        }

        currentHp = Mathf.Clamp(currentHp + change, 0, maxHp);
        UiHpSlider.instance.SetSize(currentHp/maxHp);
        
        if (currentHp == 0)
        {
            SceneManager.LoadScene("Main");
        }
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }

    public void GiveAmmo(int count)
    {
        if (count > 0)
        {
            currentAmmo = Mathf.Clamp(currentAmmo + count, 0, maxAmmo);
            UiAmmoSlider.instance.SetSize(currentAmmo/(float)maxAmmo);
        }
    }

    public void Overdrive(float duration)
    {
        lastOverdriveTime = Time.time;
        overdriveDuration = duration;
        overdriveOverlay.SetActive(true);
    }
}
