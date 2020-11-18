using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelitteSignal : MonoBehaviour
{
   public float timeRemaining = 5;
    public bool timerIsRunning = false;
    public int speed;
    public ParticleSystem system;
    public ParticleSystem.Particle[] m_Particles;
    public double length = 0.8;
    private bool beingHandled = false;
    public Transform shooterTransform;
    float modifier;

    private void Start() {
        timerIsRunning = true;
        modifier = Random.Range(0, 3);
    }

    public void Update() {
        float step = speed * Time.deltaTime;

        Vector3 target = Vector3.RotateTowards(transform.forward, shooterTransform.position, step, 0.0f);
        system.transform.rotation = Quaternion.LookRotation(target);

        if (timerIsRunning) {
            if (timeRemaining > 0){
                system.startLifetime = 1;
                timeRemaining -= Time.deltaTime;
            }
            else {
                system.startLifetime = 0.0f;
                StartCoroutine(HandleIt()); 
            }}}

    private IEnumerator HandleIt(){
    beingHandled = true;

        system.startLifetime = 0.0f;
        yield return new WaitForSeconds( 5 );
    beingHandled = false;
        timeRemaining = 1 + modifier;
    }}