using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float rotationSpeed = 180f; // graus por segundo
    private Coroutine rotateCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RotatePlayer(45f);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            RotatePlayer(-45f);
        }
    }

    void RotatePlayer(float angle)
    {
        Quaternion targetRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + angle, 0);

        if (rotateCoroutine != null)
            StopCoroutine(rotateCoroutine);

        rotateCoroutine = StartCoroutine(RotateSmoothly(targetRotation));
    }

    IEnumerator RotateSmoothly(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = targetRotation;
        rotateCoroutine = null;
    }
}