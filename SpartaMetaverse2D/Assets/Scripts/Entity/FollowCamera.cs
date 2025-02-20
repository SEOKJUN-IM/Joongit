using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // target = Player
    float offsetX;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null) // target�� null�̸� �ƹ� ó���� �ʿ� �����Ƿ� ����
            return;
        
        // �׷��� ���� ���
        // ������X�� Ʈ������.������.x ���� �÷��̾��� x������ �� �� �ɷ� ����
        // ó�� ��ġ���� �� ī�޶�� �÷��̾� ������ �Ÿ��� ������ �� ����, �� �Ÿ��� �����ϸ鼭 �̵��ϰ� �� ��
        offsetX = transform.position.x - target.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) // target�� null�̸� �ƹ� ó���� �ʿ� �����Ƿ� ����
            return;

        Vector3 pos = transform.position; // �� ��ġ�� pos�� �־��ֱ�
        pos.x = target.position.x + offsetX; // ī�޶� ��ġ�� Ÿ���� x�����ǿ��� �Ʊ� ���� �Ÿ���ŭ �������� ��� �̵�
        transform.position = pos; // pos�� ���� ���縸 �ؼ� �����ͼ� �������� �ű� ������ �ٽ� Ʈ������.�����ǿ� ������ pos�� �־��ֱ�

        // �׳� transform.position.x ���ٰ� offsetX ���ϸ� �Ǵ°� �ƴѰ�? ������
        // transform.position.x �� ���⼭ ������ ���� ����.
        // �׷��� �׻� position�� �����ϰ� ���� ���� ������ �ϳ� �������ְ�(���⼱ Vector3 pos) �ű⿡ ���� �� ����, �������� �ٽ� �ִ� �۾����� �ϱ�
    }
}
