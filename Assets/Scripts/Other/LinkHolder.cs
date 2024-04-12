using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkHolder : MonoBehaviour
{
    public static Transform Point1 { get; private set; }
    public static Transform Point2 { get; private set; }
    public static Transform EnemyAttackPoint { get; private set; }
    public static GameObject BulletObject { get; private set; }

    [SerializeField] private Transform point1, point2, enemyAttackPoint;
    [SerializeField] private GameObject bulletObject;

    private void Start()
    {
        Point1 = point1;
        Point2 = point2;
        EnemyAttackPoint = enemyAttackPoint;

        BulletObject = bulletObject;
    }
}
