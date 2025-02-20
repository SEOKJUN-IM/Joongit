using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start �� Update �� �� ������� ���� �ű� ������ ������
    // �������� ��� public���� �������־��µ�
    // �������� public�̸� ����Ƽ���� �ν����Ϳ��� �������� ���� ������ ���� �ְ� �ȴ�.
    // public���� �������� ���� ��쿡�� �ν����Ϳ��� ������ �ʾƼ� ������ �� ���� ����.

    // Obstacle�� ���Ϸ� �̵��� �� �󸶳� �̵��� ������ ������
    public float highPosY = 1f;
    public float lowPosY = -1f;

    // top �� bottom ������ ������ �󸶳� ����� ������ ������
    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    // ������Ʈ�� ��ġ�� �� ������ ���� �󸶳� ����� ������ ������
    public Transform topObject;
    public Transform bottomObject;
    public float widthPadding = 4f;

    // ������ ������ ����, Obstacle������ ���ӸŴ����� ������ �� �ְ� �̱��� Ȱ��
    FlappyManager flappyManager;

    private void Start()
    {
        flappyManager = FlappyManager.Instance;

    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax); // Ȧ������ �ּҿ� �ִ� ������ ������ ����
        float halfHoleSize = holeSize / 2; // ����Ȧ������ = Ȧ�������� ������ ����

        topObject.localPosition = new Vector3(0, halfHoleSize); // ������ Ȧ������ �ݸ�ŭ ���� �ø�
        bottomObject.localPosition = new Vector3(0, -halfHoleSize); // bottom������Ʈ�� �ݴ�� ����
        // Ȧ�����ŭ �� ������Ʈ�� ������ ��

        // ���� �������� ���� ������Ʈ �ڿ� widthPadding��ŭ ���� ������ ���� ��ġ
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        // ���ο� ��ġ y�� ����
        placePosition.y = Random.Range(lowPosY, highPosY); // lowPosY �� highPosY ������ ������ ����

        transform.position = placePosition;

        return placePosition; // SetRandomPlace �޼��� ��ȯ��

        // localPosition �� position ���� : ������ǥ�� ������ǥ�� �����̴�
        // ������ǥ : �θ� ������Ʈ ����. ������Ʈ�� �θ� ������Ʈ���� �󸶳� �������ִ���. �θ� ������Ʈ ��ġ�� ȸ���� ����Ǹ� ������ǥ�� �ݿ�, ������� ��ġ ����
        // ������ǥ : ���� ��ü ���忡�� ��ġ. ����Ƽ ���� ��ǥ��(0, 0, 0)�� �������� ��ġ ��� ��
    }

    // ���� ������ ����, Obstacle�� BoxCollider�� ������ �ְ� �ű� Trigger�� �޷������Ƿ�
    // BoxCollider�� ��� �� = Trigger�� Exit�� �� ���� ������
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Player�� �´��� Ȯ���ϱ� ���� �÷��̾� ��������Ʈ
        Player player = collision.GetComponent<Player>();
        if (player != null) // player�� null�� �ƴ϶�� = player�� �´�
        {
            flappyManager.AddScore(1);            
        }
    }
}
