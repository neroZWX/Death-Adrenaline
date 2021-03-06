﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public float speed = 800;
    public GameObject[] bulletholes;
    public float damage = 10;
    void Start()
    {
        Destroy(this.gameObject, 30);
    }
    // Update is called once per frame
    void Update () {
        Vector3 oriPos = transform.position;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Vector3 direction = transform.position - oriPos;
        float length = (transform.position - oriPos).magnitude;
        RaycastHit hitinfo;//存储碰撞信息
       bool isCollider= Physics.Raycast(oriPos, direction, out hitinfo,length);
        if (isCollider) {
         
            if (hitinfo.collider.tag == "Player")
            {
                //子弹射击到角色身上

                player player = hitinfo.collider.GetComponent<player>();
                if (player.hp > 0) {//only character's hp more than 0 then can shoot
                    player.TakeDamage(this.damage);
                }
            }
            else {
                //如果射线碰撞到物体要做的动作
                int index = Random.Range(0, 2);
                GameObject bulletHolePrefab = bulletholes[index];
                Vector3 pos = hitinfo.point;//得到碰撞的位置
                GameObject go = GameObject.Instantiate(bulletHolePrefab, pos, Quaternion.identity);
                go.transform.LookAt(hitinfo.point - hitinfo.normal);
                go.transform.Translate(Vector3.back * 0.01f);
                //hitinfo.normal;//可以得到碰撞的垂线向量，面的法线

            }
            Destroy(this.gameObject);
        }
	}
}
