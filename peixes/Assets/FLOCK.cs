using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLOCK : MonoBehaviour
{
    public FLOCKMANAGER myManager;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(myManager.minSpeed,
            myManager.maxSpeed);
    }
    void ApplyRules()//aplica regras
    {
        GameObject[] gos;
        gos = myManager.allFish;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        foreach(GameObject go in gos)
        {
            if(go!= this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if(nDistance <= myManager.neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if(nDistance < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    FLOCK anotherFlock = go.GetComponent<FLOCK>();
                    gSpeed = gSpeed + anotherFlock.speed;

                }
                    
            }
            if(groupSize>0)
            {
                vcentre = vcentre / groupSize;
                speed = gSpeed / groupSize;

                Vector3 direction = (vcentre + vavoid) - transform.position;

                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp
                    (
                        transform.rotation,
                        Quaternion.LookRotation(direction),
                        myManager.rotatioSpeed * .0005f /*Time.deltaTime*/ //Multipliquei por um valor menor pra segurar a rotação espasmática muito louca rave.
                    );

                    //TESTE
                    //print(Time.deltaTime); -> Retornando + ou - 0.3f. os peixes rodam espasmáticamente muito louco rave.
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        ApplyRules();

        transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
