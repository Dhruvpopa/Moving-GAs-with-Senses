using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    int dnaLength = 2;
    public float timeAlive;
    public float timeWalking;
    public DNA dna;
    public GameObject eyes;
    bool alive = true;
    bool seeGround = true;

    void OnCollisionEnter(Collision obj)
    {
        if(obj.gameObject.tag == "dead")
        {
            alive = false;
            timeAlive = 0;
            timeWalking = 0;
        }
    }
    public void Init()
    {
        //0 forward
        //1 left
        //2 right

        dna = new DNA(dnaLength, 3);
        timeAlive = 0;
        alive = true;
    }

    private void Update()
    {
        if (!alive) return;

        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 10,Color.red,10);
        seeGround = false;
        RaycastHit hit;
        if(Physics.Raycast(eyes.transform.position,eyes.transform.forward * 10, out hit))
        {
            if(hit.collider.gameObject.tag == "platform")
            {
                seeGround = true;
            }
        }
        timeAlive = PopulationManager.elapsed;

        float h = 0;
        float v = 0;
        if (seeGround)
        {
            if (dna.GetGene(0) == 0) { v = 1; timeWalking += 1; }
            else if (dna.GetGene(0) == 1) h = -90;
            else if (dna.GetGene(0) == 2) h = 90;
        }
        else
        {
            if (dna.GetGene(1) == 0) { v = 1; timeWalking += 1; }
            else if (dna.GetGene(1) == 1) h = -90;
            else if (dna.GetGene(1) == 2) h = 90;
        }

        this.transform.Translate(0, 0, v*0.1f);
        this.transform.Rotate(0, h, 0);
    }
}
