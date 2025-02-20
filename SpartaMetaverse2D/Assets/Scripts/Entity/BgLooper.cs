using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5;
    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero; // Vector3.zero = (0, 0, 0)

    // Start is called before the first frame update
    void Start()
    {
        // Start���� obstacle���� ���� ã�ƿ� ����
        // public���� ������� �� �����ϴ� �͵� ����������, GameObject�� Ŭ������ ����غ���
        // GameObject���� FindObjectsOfType<>�� �ִµ�, �� ���� ��ü�� ���ƴٴϸ� <>���� ��(���⼱ Obstacle)�� �޷��ִ� ���� ã�ƿ��� �޼���
        // ������ �ʱ� ������ Start�� Awake���� �ѹ��� ������ �� �ְ� ���ִ� ���� ����
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleLastPosition = obstacles[0].transform.position; // ù ��° obstacles ���������� ����
        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacleCount; i++) // �ݺ������� obstacle ���� ��ġ
        {
            // �̸� �������� SetRandomPlace�� obstacleLastPosition�� obstacleCount�� �־��ָ� ����
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
            // ������ ��ġ�� ������ ��ġ�� ��ġ�� �޾ƿͼ� �� ���� obstacle�� ��ġ�� ���� ��������
        }
    }
    // �̷��� �ϸ� ó���� ��� ��ֹ����� ã��, �� ��ֹ����� ó�� ��ֹ����� �� ��ֹ����� ��ġ�� ����

    // �浹 ó�� : BgLooper�� Trigger �浹�� �ϴ� ��ֹ����� �� �ڷ� ���� ����
    // �׷��� ��Ʈ���ſ���2D ��� : ���� �浹 ȿ���� ���� ���� �ƴ� �浹�� ���� �뺸�� ����
    // �׷��� ��Ʈ���ſ���2D �� �浹�� ���� ������ �� �� ����, �浹ü�� ���� ������ �ְ� ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ������Ʈ ���� ���
        if (collision.CompareTag("BackGround")) // ����Ƽ���� �̸� background ������Ʈ�鿡 �ٿ����� Tag : BackGround �� ���� �ֵ�� ��
        {
            // ������ ������Ʈ���� ���� ����� ã�ƿ� ����
            // ���� ������� Collider�� �̹� �˰� �ְ�, ������Ʈ�鿡 BoxCollider2D�� �޾Ƴ��� ������ ������ �� �ִ�.
            // �� BoxCollider2D�� collision�̶�� �ϴ� Collider�̴�.
            // Collider2D�� ��� Collider���� �θ� Ŭ����, ������ �� Ŭ������ ������ �ϴ� �� �ƴ϶� BoxCollider2D�� ����� ������ �� ����.
            // �׷��� collision�� ��� BoxCollider2D�� �ٲ���
            float widthOfBgOgject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position; // �浹�� ������Ʈ ��ġ ������

            pos.x += widthOfBgOgject * numBgCount; // x�����ǰ��� ���� ������x���ī��Ʈ�� �����ֱ�
            collision.transform.position = pos; // ������ pos�� �ٽ� �־��ֱ�
            return;
            // �̹� BackGround�� ó���� �ߴٸ� Obstacle�� �ƴϱ� ������ return�� �ؼ� �� �̻� �������� �ʰ� ���ָ� ����.
        }

        // ������Ʈ �ִ� ���
        // �浹ü���� ��������Ʈ �غ��� : Obstacle�� �ִ���
        // �浹�� ������Ʈ�� � ������Ʈ���� �˰� ���� �� ��������Ʈ ���
        // Obstacle�� �����ͺôµ� �� �޷��ִٸ� ���� �˰� �ִ� Obstacle�� �ƴ� ��
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle) // obstacle�� �޷��ִٸ� = obstacle != null �̶��
        {
            // �׷��ٸ� �츮�� �˰� �ִ� �ɽ�ŸŬ ������Ʈ�� �Ȱ��� ����
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
}
