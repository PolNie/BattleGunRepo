using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public Light directionalLight;
    public float[] lightIntensities;
    public Color[] lightColors;
    public Vector3[] lightPositions;
    public Vector3[] lightRotations;

    public GameObject rainEffect;

    void Start()
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty", 0);
        SetDifficulty(difficulty);
    }

    public void SetDifficulty(int difficulty)
    {
        if (difficulty >= 0 && difficulty < lightIntensities.Length)
        {
            // Change light intensity and colour
            directionalLight.intensity = lightIntensities[difficulty];
            directionalLight.color = lightColors[difficulty];

            // Change the position of the light
            directionalLight.transform.position = lightPositions[difficulty];

            // Change light rotation
            directionalLight.transform.rotation = Quaternion.Euler(lightRotations[difficulty]);

            if (rainEffect != null)
            {
                if (difficulty == 2)
                {
                    rainEffect.SetActive(true);
                }
                else
                {
                    rainEffect.SetActive(false);
                }
            }
        }
    }
}
