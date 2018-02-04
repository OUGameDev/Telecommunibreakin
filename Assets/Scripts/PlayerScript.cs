using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(VRTK_HeadsetCollision)), RequireComponent(typeof(VRTK_BodyPhysics)), RequireComponent(typeof(VRTK_HeadsetFade))]
[AddComponentMenu("VRTK/Scripts/Presence/VRTK_HeadsetCollisionFade")]
public class PlayerScript : MonoBehaviour{

    private static PlayerScript thisinst;
    private static int health = 100;

    private float lastHit = 0;

    private void Awake()
    {
        thisinst = this;
        GetComponentInChildren<VRTK_HeadsetCollision>().HeadsetCollisionDetect += new HeadsetCollisionEventHandler(OnHeadsetCollisionDetect);
        GetComponentInChildren<VRTK_BodyPhysics>().StartColliding += new BodyPhysicsEventHandler(OnBodyCollision);
    }

    public static void dealDamage(int dmg)
    {
        if (dmg > 0 && dmg <= 100)
        {
            health -= dmg;
        }

        thisinst.checkDeath();
    }

    public static void heal(int hlth)
    {
        health += hlth;
    }

    protected virtual void OnBodyCollision(object sender, BodyPhysicsEventArgs e)
    {
        if (Time.time - lastHit > 1f)
        {
            lastHit = Time.time;
            if (e.collider.name.Contains("Bullet"))
            {
                dealDamage(34);
            }
        }
    }

    protected virtual void OnHeadsetCollisionDetect(object sender, VRTK.HeadsetCollisionEventArgs e)
    {
        if (Time.time - lastHit > 1f)
        {
            lastHit = Time.time;
            if (e.collider.name.Contains("Bullet"))
            {
                dealDamage(34);
            }
        }
    }

    public void checkDeath()
    {
        if (health <= 0) //ur dead kiddo
        {
            Invoke("StartFade", 0f);
            Invoke("ResetPlayer", .2f);
            Invoke("EndFade", 3f);
        }
    }

    protected virtual void StartFade()
    {
        GetComponentInChildren<VRTK.VRTK_HeadsetFade>().Fade(Color.black, .1f);
    }

    protected virtual void ResetPlayer()
    {
        health = 100;
        VRTK.VRTK_DeviceFinder.PlayAreaTransform().position = new Vector3(-2.7f, 0f, 0f);
    }

    protected virtual void EndFade()
    {
        GetComponentInChildren<VRTK.VRTK_HeadsetFade>().Unfade(.1f);
    }

}
