using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public ColorControllor colorControllor;
    public float speed;

    Vector3 rgb,rgbs;
    Color color;

	// Use this for initialization
	void Start () {
        color.a = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        colorControllor.SetColor(color);
	}

    void FixedUpdate()
    {
        RandomColor();
        gameObject.transform.Translate(Vector3.right * -speed * Time.fixedDeltaTime);
        Debug.DrawRay(transform.position, gameObject.transform.right);
    }

    void RandomColor() {
        rgbs += Random.insideUnitSphere;
        rgbs.x = rgbs.x < -1f ? -1f : rgbs.x;
        rgbs.x = rgbs.x > 1f ? 1f : rgbs.x;

        rgbs.y = rgbs.y < -1f ? -1f : rgbs.y;
        rgbs.y = rgbs.y > 1f ? 1f : rgbs.y;

        rgb += rgbs*Time.fixedDeltaTime;
        rgb.x = rgb.x < 0.0f ? 0.0f : rgb.x;
        rgb.y = rgb.y < 0.3f ? 0.3f : rgb.y;

        rgb.x = rgb.x > 1f ? 1f : rgb.x;
        rgb.y = rgb.y > 0.8f ? 0.8f : rgb.y;

        color = Color.HSVToRGB(rgb.x, rgb.y, 1f);
    }
}
