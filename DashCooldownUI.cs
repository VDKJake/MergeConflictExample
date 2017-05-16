using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCooldownUI : MonoBehaviour {

    private bool isDashActive;
    private bool isCooldownActive;
    private bool isSet;
    private float activeTime;
    private float dashMaxtime = GLOBAL_VALUES.DASH_DURATION;
    private float dashCooldownMaxTime = GLOBAL_VALUES.DASH_COOLDOWN;
    private Timer dashTimer = new Timer();

    public Timer dashCooldownTimer;
    public Image dashCooldownImage;
    public Image dashCooldownBackgroundImage;

    // Sets up the bar so that it is full
    // and sets them as inactive.
	void Start ()
    {
        dashCooldownImage.fillAmount = 1.0f;
        dashCooldownImage.gameObject.SetActive(false);
        dashCooldownBackgroundImage.gameObject.SetActive(false);
    }
	
	// Updates the timer every frame, as well as
    // checking each timer.
	void Update ()
    {
        dashTimer.Update();
        
        // If both the duration and cooldown are complete
        // then hide the slider. Else, if the image isn't active
        // (and one of the timers is running) then show the slider.
        if (dashTimer.isComplete() && dashCooldownTimer.isComplete())
        {
            //dashCooldownImage.gameObject.SetActive(false);
            //dashCooldownBackgroundImage.gameObject.SetActive(false);

            // If the alpha of the image is at 1, then run the fade function
            if (dashCooldownImage.GetComponent<CanvasRenderer>().GetAlpha() == 1.0f)
            {
                Fade();
                StartCoroutine(Expand());
            }
            
        }
        else if (!dashCooldownImage.IsActive())
        {
            dashCooldownImage.gameObject.SetActive(true);
            dashCooldownBackgroundImage.gameObject.SetActive(true);
        }
        
        // If the duration is over then tell the script it is over.
        if (dashTimer.isComplete())
        {
            isDashActive = false;
        }

        // If the cooldown timer has been set and the cooldown timer is
        // complete then tell the script that it is over.
        if (isSet == true)
        {
            if (dashCooldownTimer.isComplete())
            {
                isCooldownActive = false;
            }
        }
	}

    public void Fade()
    {
        dashCooldownImage.CrossFadeAlpha(0.0f, 0.5f, false);
        dashCooldownBackgroundImage.CrossFadeAlpha(0.0f, 0.5f, false);
    }

    IEnumerator Expand()
    {
        gameObject.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x * 1.3f, transform.localScale.y * 1.3f, transform.localScale.z * 1.3f), 0.5f);
        yield return new WaitForSeconds(0.5f);
    }

    // Starts the duration timer and sets the fill images as active.
    public void StartDashTimer()
    {
        dashTimer.Add(GLOBAL_VALUES.DASH_DURATION, true);
        isDashActive = true;
        dashCooldownImage.gameObject.SetActive(true);
        dashCooldownBackgroundImage.gameObject.SetActive(true);
    }

    // Takes in the cooldown timer and lets the script know that it
    // is active and has been set.
    public void SetTimer(Timer dTimer)
    {
        dashCooldownTimer = dTimer;
        isCooldownActive = true;
        isSet = true;
    }
}
