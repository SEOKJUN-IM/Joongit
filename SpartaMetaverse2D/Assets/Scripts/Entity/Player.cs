using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator; // �ִϸ����ͷ� �ִϸ��̼� ������ �� ���̱� ������ �ִϸ����� ��������
    Rigidbody2D _rigidbody; // ������ٵ�2D�� �̵� ���� ó��, ���� Ŭ������ �̹� rigidbody��� �̸��� �־� �տ� ����� �ٿ��� ����

    public float flapForce = 6f; // �����ϴ� �� �����ֱ�
    public float forwardSpeed = 3f; // �������� �̵��ϴ� �� �����ֱ�
    public bool isDead = false; // �׾����� ��Ҵ��� �����ϱ� ���� bool, ��� ���� �߿��� false
    float deathCooldown = 0f; // �浹�ϰ� ���� �ٷ� �״� ���� �ƴ϶�, ���� �ð� �Ŀ� �׵��� ������ٿ� �����ֱ�(���߿� �����ϰ� �ٲ��� ����)

    bool isFlap = false; // ������ �پ����� �� �پ����� �����ϱ� ���� bool

    public bool godMode = false; // ������ �׽�Ʈ�ϱ� ���� bool, ����Ƽ�� Player���� God Mode�� �Ѹ� ���� �ʴ� Player�� ��

    FlappyManager flappyManager; // FlappyManager ���� �ϳ� ����
    SceneChanger sceneChanger;

    // Start is called before the first frame update
    void Start() // ������Ʈ�� Ȱ��ȭ�ǰ� ù ������ ���� �� �� ȣ��
    {
        // ���ӸŴ��� �̱��� ������ �ʱ�ȭ�ϴ� ���� Awake�̱� ������
        // Player���� Awake�� ���ð��� ������ �ϸ� ���̰� �� ���� ����
        // �׷��� �̱��� ���Ͽ� ������ �� ���� Start�� �̿����ָ� ����.
        flappyManager = FlappyManager.Instance;
        sceneChanger = SceneChanger.Instance;

        // ������Ʈ�� ���� ���� ���� ��������Ʈ�� �� ��
        // ��������Ʈ : ���� �ۼ� ���� Ŭ������ ��������Ʈ�� �ϰ� �Ǹ�
        // Ŭ������ �޷��ִ� ������Ʈ���� �츮�� ã�� �ִ� ������Ʈ�� �޷��ִ���(������ٵ�2D ��)
        // �޷��ִٸ� �װ� ��ȯ���ִ� ���
        animator = GetComponentInChildren<Animator>();  // ��������Ʈ �ڿ� ��ĥ�己 : Player Ŭ���� ���� ������Ʈ(���⼱ Model)�� �޷��ִ� �ִϸ����� ��������
        _rigidbody = GetComponent<Rigidbody2D>(); // Player Ŭ������ �޷��ִ� ������ٵ�2D ��������

        // ���� ó��
        if (animator == null) // �ִϸ����Ͱ� null�� ��
            Debug.LogError("Not Founded Animator"); // �����.�α׳� �����.�α׿����� ���� ���� ������ �ܼ�â�� ����

        if (_rigidbody == null) // ������ٵ� null�� ��
            Debug.LogError("Not Founded Rigidbody"); // �����.�α׳� �����.�α׿����� ���� ���� ������ �ܼ�â�� ����
    }

    // Update is called once per frame
    void Update() // �� �����Ӹ��� ȣ��, �ַ� �Է� ó���� ���� ������ ���
    {
        if (isDead) // �׾��� ��
        {
            if (deathCooldown <= 0) 
            {
                // �������� ���ư���
                if (Input.GetKeyDown(KeyCode.Escape)) // Esc ������
                {
                    flappyManager.SetBestScore();                    

                    sceneChanger.GoMainScene(); // ��ü������ GoMainScene �޼��� ȣ��                    
                }
            }
            else // ������ٿ� > 0 : ������ٿ��� �����ִ�
            {
                deathCooldown -= Time.deltaTime; // ��ŸŸ�� : ������Ʈ������ ��� ����, ������ �� ������� ������ �ð� ��Ʈ�� ����
            }
        }
        else // ���� �ʾ��� ��(���� ���� ��)
        {
            // Ű�Է� �޾Ƽ� ������ �� �� �ְ� �� ����
            // Ű�Է��� ��ǲ.��Ű�ٿ�(Ű�ڵ�.���ϴ�Ű)
            // ���콺�Է��� ��ǲ.�ٸ��콺��ư(int button == ��ư����)
            // ���콺 ��ư����
            // 0 = ��Ŭ��, 1 = ��Ŭ��, 2 = ��Ŭ��, 3 = �ڷΰ���, 4 = �����ΰ��� (0�� ��� ����Ʈ������ ��ġ ��ɱ��� ����)
            if (Input.GetKeyDown(KeyCode.Space)) // �����̽� Ű�� ������
            {
                isFlap = true; // ������ �ߴ�, ���⼭ �ٷ� ó���� ���� ������, ���� ó���� ���� ���� �Ƚ��������Ʈ���� ������ ������� ����
            }
        }
    }
        
    private void FixedUpdate() // ���� ������ �ʿ��� �� ������ �������� ȣ��
    {
        if (isDead) return; // �׾��ٸ�, �ƹ� �۾����� �ʰ� �׳� ����

        Vector3 velocity = _rigidbody.velocity; // ���ν�Ƽ�� ���ӵ��� �ǹ�, _������ٵ��� ���ӵ��� ������ ����
        velocity.x = forwardSpeed; // ���ν�Ƽ�� x�� ���� ���� �̵� ���ǵ�� �����ֱ�

        if (isFlap) // ������ �ߴٸ�
        {
            velocity.y += flapForce; // ���ν�Ƽ�� y�� ���� �����ϴ� ���� ������ ������ �ٲ��ֱ�
            isFlap = false ; // �����߱� ������ �ٽ� �̽��÷� false�� �ٲ��ֱ�
        }

        // _������ٵ��� ���ν�Ƽ���� ������ �� ������, _������ٵ�.���ν�Ƽ�� �ٲ� ���� �ƴ�(����3�� ��Ʈ��Ʈ(����ü)�̱� ������ ���� ���縸 �� ��)
        _rigidbody.velocity = velocity; // �׷��� �ٷ� ������ �ٲ� ����3 ���ν�Ƽ ���� �ٽ� �ѹ� _������ٵ�.���ν�Ƽ�� �־���

        // Player�� ������ �ٲ� �� �ְ� angle�� �����ֱ�
        // �ž�����.Ŭ����(���ذ�, �ּҰ�, �ִ밪)
        // ���ذ� : _������ٵ�.���ν�Ƽ.y : ���� �ö󰡴���, �Ʒ��� ����������. �ʹ� �۱� ������ x10 ����
        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f),-90, 90);
        // Ʈ�������� ������Ʈ : Ʈ�������� ���� ������Ʈ �Ӽ��� �ʹ� ���� ����ؼ� ����Ǿ� ����
        // Ʈ������ ���� �����̼� ���� �����ͼ� �������༭ ȸ���� �� �ֵ��� �Ұ���(�����̼ǰ��� ���Ͱ� �ƴ϶� ���ʹϾ��̶�� ��������� ���� ����)
        // �׷��� ���ʹϾ�.���Ϸ��� ��(���Ϸ������� �츮�� �˰� �ִ� 0��, 360�� ���� ������ �ǹ�)
        // ���ʹϾ�.���Ϸ�(x���� ������ ȸ����, y���� ������ ȸ����, z���� ������ ȸ����), �ٷ� �ľ��ϱ� ��Ʊ� ������ ����Ƽ���� �������鼭 ��� �� �����ؾߵǴ��� Ȯ��
        transform.rotation = Quaternion.Euler(0, 0, angle); // ������ ���� angle �� �˸´� �� : z�� �κп� �־���
    }

    // �浹 ���� ó��
    // ����Ƽ���� Collider���� isTrigger�� ���������� ���� �浹�ϴ� �� ��� ����(ƨ�������´ٵ���), isTrigger ���������� �뺸�� ���ٻ� ����Ѵ�
    // OnCollisionEnter: �浹 ���� �� ȣ��
    // OnCollisionStay: �浹�� �����Ǵ� ���� ȣ��
    // OnCollisionExit: �浹�� ������ �� ȣ��
    private void OnCollisionEnter2D(Collision2D collision) // �Ű������� ������ ���� �ݸ���, �浹�� ���� ���� �˷���(���, ��� ��)
    {
        if (godMode) return; // godMode true��(������� �Ѹ�) ����

        if (isDead) return; // isDead true��(�׾�����) ����

        // �̿��� ��쿡 �浹���� ��
        isDead = true; // �׾��ٰ� �������
        deathCooldown = 0.01f; // ������ٿ� = 1��(�װ� ���� �� �� �� ���� ������� �� �ְ� ������� ����)

        // �ִϸ������� Integer�� �����ش�, ���⼱ IsDie(�̸� ����Ƽ���� IsDie ���� 1�� �Ǹ� die �ִϸ��̼��� ������ �س���)
        animator.SetInteger("IsDie", 1);
        flappyManager.GameOver(); // �浹�� �߱� ������ ���ӸŴ����� GameOver �޼��� ȣ��        
    }
}
