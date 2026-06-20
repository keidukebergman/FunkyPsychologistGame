using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] m_balloonPrefabs;
    [SerializeField] private Transform[] m_spawnPoints;
    [SerializeField] private float m_spawnAmount;
    [SerializeField] private Material[] m_materials;

    private void OnEnable()
    {
        SpawnBalloons();
    }

    public void SpawnBalloons()
    {
        for (int i = 0; i < m_spawnAmount; i++)
        {
            var index = i % (m_balloonPrefabs.Length - 1);
            var newBalloon = Instantiate(m_balloonPrefabs[index]).GetComponent<ClickableBalloon>();
            newBalloon.transform.position = m_spawnPoints[Random.Range(0, m_spawnPoints.Length - 1)].position;
            newBalloon.MeshRenderer.material = m_materials[Random.Range(0, m_materials.Length - 1)];
        }
    }
}
