using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThomasEdisonMoveSet : MonoBehaviour {
    public Joystick actionJoystick;
    private Animator anim;
    private bool patternRecognized = false;
    private int x;
    private int y;
    private int prevx = 0;
    private int prevy = 0;
    private int moveSet;
    private int frameCount = 0;
    private ThomasEdisonAnimation edisonAnimation;
    private bool animRunning = false;
    private bool slamStrt = false;
    private bool combo1strt = false;
    private Transform enemyTransform;
    // effects
    public GameObject oneHandElectric;
    public GameObject gettingThrownEffect;
    public GameObject twoHandEffect;

    // Use this for initialization
    void Awake () {
        anim = GetComponent<Animator>();
        edisonAnimation = GetComponent<ThomasEdisonAnimation>();
        enemyTransform = GameObject.FindGameObjectWithTag(Tags.enemy).transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        defence();
        if (frameCount != 0)
        {
            moveSet = joystickPattern(frameCount);
        }       

        if (patternRecognized && !animRunning)
        {
            switch (moveSet)
            {
                case 0:
                    break;
                case 1:
                    edisonAnimation.oneHand();
                    animRunning = true;
                    break;
                case 2:
                    edisonAnimation.slam1();
                    animRunning = true;
                    break;
                case 3:
                    edisonAnimation.combo1();
                    animRunning = true;
                    break;
                default:
                    break;
            }

            
            patternRecognized = false;
            frameCount = 0;
        }


        if (Mathf.RoundToInt(10*actionJoystick.Horizontal) != 0 || Mathf.RoundToInt(10*actionJoystick.Vertical) != 0)
        {
            
            frameCount++;
            print(actionJoystick.Direction);
            print(frameCount);
        }
        else if(actionJoystick.Direction == Vector2.zero)
        {
            frameCount = 0;
        }
        
    }

    void defence()
    {
        if (actionJoystick.Vertical < -0.75)
        {
            edisonAnimation.defence(true);
        }
        else
        {
            edisonAnimation.defence(false);
        }
    }

    public int joystickPattern(int iteration)
    {
        x = Mathf.RoundToInt(10 * actionJoystick.Horizontal);
        y = Mathf.RoundToInt(10 * actionJoystick.Vertical);

        if (x != 0 || y != 0 || iteration == 1)
        {
            //----------1H--------
            if (x >= prevx && y <= 1 && y>=-1)
            {
                if (x > 8 && y <= 1 && y >= -1 && iteration > 20 && iteration < 30)
                {
                    patternRecognized = true;
                    frameCount = 0;
                }
                prevx = x;
                prevy = y;
                return 1;
            }
            //----------1H---------

            //----------Slam1------
            
            else if(((x > 8 && (y <= 2 && y >= -2 ))||(slamStrt)) && iteration < 80)
            {
                slamStrt = true;
                if (x <= 1 && y > 8 && iteration < 80)
                {
                    patternRecognized = true;
                    frameCount = 0;
                    slamStrt = false;
                    return 2;
                }

                else if((Mathf.Pow(x,2) + Mathf.Pow(y,2)) < 36)
                {
                    frameCount = 100;
                }
                prevx = x;
                prevy = y;
                return 0;
            }
            //-----------Slam1------------

            //-----------Combo1-----------
            else if(((y >= 8 && (x <= 2 && x >= -2)) || (combo1strt)) && iteration < 240)
            {
                combo1strt = true;
                if (x <= -8 && y <=2 && iteration < 240)
                {
                    patternRecognized = true;
                    frameCount = 0;
                    return 3;
                }
                return 0;
            }

            //----------Combo1-----------

            else
            {
               // frameCount = 0;
                return -1;
            }
            
        }
        else
        {
            return 0;
        }
    }

    public void animCompelete()
    {
        animRunning = false;
    }

    public void slamCheck()
    {
        //edisonAnimation.slam1cmpt();
    }

    public void comboMove()
    {
        animRunning = false;
    }

    public void slamComplete()
    {
        edisonAnimation.slam1cmpt();
        animRunning = false;
        slamStrt = false;
    }
    public void combo1Complete()
    {
        combo1strt = false;
        animRunning = false;
        edisonAnimation.slam1reset();
    }
    public void LookEnemy()
    {

    }

    public void oneHandEffect()
    {
        Vector3 v3 = new Vector3(0, 0.7f, 3f);
        Vector3 v31 = new Vector3(0, 0.7f, 0);

        if (Vector3.Distance(transform.position,enemyTransform.position)<=3f)
        {
            oneHandElectric.transform.position = enemyTransform.position + v31;
            oneHandElectric.SetActive(true);
            
        }

        else
        {
            oneHandElectric.transform.position = transform.position + v3;
            oneHandElectric.SetActive(true);
        }
    }

    public void thrownEffect()
    {
        Vector3 vector = new Vector3(0, 0.4f, 0);
        gettingThrownEffect.transform.position = transform.position + vector;
        gettingThrownEffect.SetActive(true);
    }

    public void TwoHandEffect()
    {
        twoHandEffect.transform.position = enemyTransform.position;
        twoHandEffect.SetActive(true);
    }
}


































