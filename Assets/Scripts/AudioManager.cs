using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public AudioSource gunShot, playerHurt, enemyShot, reload, cooldown, health, step, sniper, enemyExplode;

    void Awake()
    {
        instance = this;
    }

    public void PlayGunShot()
    {
        gunShot.Stop();
        gunShot.Play();
    }

    public void PlayPlayerHurt()
    {
        playerHurt.Stop();
        playerHurt.Play();
    }

    public void PlayEnemyShot()
    {
        enemyShot.Stop();
        enemyShot.Play();
    }

    public void PlayReload()
    {
        reload.Stop();
        reload.Play();
    }

    public void PlayCooldown()
    {
        cooldown.Stop();
        cooldown.Play();
    }

    public void PlayHealth()
    {
        health.Stop();
        health.Play();
    }

    public void PlayStep()
    {
        step.Stop();
        step.Play();
    }

    public void PlaySniper()
    {
        sniper.Stop();
        sniper.Play();
    }

    public void PlayEnemyExplode()
    {
        enemyExplode.Stop();
        enemyExplode.Play();
    }

    public void StopAll()
    {
        gunShot.Stop();
        playerHurt.Stop();
        enemyShot.Stop();
        reload.Stop();
        cooldown.Stop();
        health.Stop();
        step.Stop();
        sniper.Stop();
        enemyExplode.Stop();
    }
}
