using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFallSimulation : MonoBehaviour
{
    public GameObject Prefab;
    public float LifeTime = 10f;
    public int WheelFalls = 0;
    public int TotalFalls = 0;
    public int UprightFalls = 0;
    public int TwoWheelFalls = 0;

    private float OnLeftWheelsTime;
    private float OnRightWheelsTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine() {
        while (true)
        {
            var spawned = Instantiate(Prefab, new Vector3(0.0f, Random.Range(3.0f, 5.0f), 0.0f) + this.transform.position, 
                Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));
            var spawnedRb = spawned.GetComponent<Rigidbody>();
            spawnedRb.sleepThreshold = 0;

            yield return new WaitForSeconds(LifeTime);

            // Somehow check collisions
            bool leftWheelTouching = Mathf.Abs(OnLeftWheelsTime - Time.realtimeSinceStartup) < 2 * Time.fixedDeltaTime;
            bool rightWheelTouching = Mathf.Abs(OnRightWheelsTime - Time.realtimeSinceStartup) < 2 * Time.fixedDeltaTime;
            if (leftWheelTouching || rightWheelTouching)
            {
                WheelFalls += 1;
            }

            if(leftWheelTouching && rightWheelTouching)
            {
                TwoWheelFalls += 1;
            }

            if(Vector3.Dot(transform.up, spawned.transform.up) > 0)
            {
                UprightFalls += 1;
            }

            Destroy(spawned);
            TotalFalls += 1;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "LeftWheel") {
            OnLeftWheelsTime = Time.realtimeSinceStartup;
        }

        if(collision.collider.tag == "RightWheel")
        {
            OnRightWheelsTime = Time.realtimeSinceStartup;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "LeftWheel")
        {
            OnLeftWheelsTime = Time.realtimeSinceStartup;
        }

        if (other.tag == "RightWheel")
        {
            OnRightWheelsTime = Time.realtimeSinceStartup;
        }
    }

    // Take a "screenshot" of a camera's Render Texture.
    Texture2D RTImage(Camera camera)
    {
        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;

        // Render the camera's view.
        camera.Render();

        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();

        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;
        return image;
    }
}
