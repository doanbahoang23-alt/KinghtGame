using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveData", menuName = "KnightGame/Wave Data")]
public class WaveData : ScriptableObject
{
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public GameObject enemyPrefab;
        public int count;
        public float spawnDelay = 1f;
    }

    public string waveName;
    public EnemySpawnInfo[] enemyGroups;
    public float timeBetweenGroups = 2f;
}