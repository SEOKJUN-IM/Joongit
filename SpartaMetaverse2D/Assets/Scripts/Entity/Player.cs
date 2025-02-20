using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator; // 애니메이터로 애니메이션 관리를 할 것이기 때문에 애니메이터 가져오기
    Rigidbody2D _rigidbody; // 리지드바디2D로 이동 관련 처리, 상위 클래스에 이미 rigidbody라는 이름이 있어 앞에 언더바 붙여서 구별

    public float flapForce = 6f; // 점프하는 힘 정해주기
    public float forwardSpeed = 3f; // 정면으로 이동하는 힘 정해주기
    public bool isDead = false; // 죽었는지 살았는지 구별하기 위한 bool, 평소 게임 중에는 false
    float deathCooldown = 0f; // 충돌하고 나서 바로 죽는 것이 아니라, 일정 시간 후에 죽도록 데스쿨다운 정해주기(나중에 적절하게 바꿔줄 것임)

    bool isFlap = false; // 점프를 뛰었는지 안 뛰었는지 구별하기 위한 bool

    public bool godMode = false; // 게임을 테스트하기 위한 bool, 유니티의 Player에서 God Mode를 켜면 죽지 않는 Player가 됨

    FlappyManager flappyManager; // FlappyManager 변수 하나 생성
    SceneChanger sceneChanger;

    // Start is called before the first frame update
    void Start() // 오브젝트가 활성화되고 첫 프레임 전에 한 번 호출
    {
        // 게임매니저 싱글턴 패턴을 초기화하는 것이 Awake이기 때문에
        // Player에서 Awake에 동시간대 접근을 하면 꼬이게 될 수도 있음
        // 그래서 싱글턴 패턴에 접근을 할 때는 Start를 이용해주면 좋다.
        flappyManager = FlappyManager.Instance;
        sceneChanger = SceneChanger.Instance;

        // 컴포넌트를 붙일 때는 이제 겟컴포넌트를 쓸 것
        // 겟컴포넌트 : 현재 작성 중인 클래스가 겟컴포넌트를 하게 되면
        // 클래스에 달려있는 오브젝트한테 우리가 찾고 있는 컴포넌트가 달려있는지(리지드바디2D 등)
        // 달려있다면 그걸 반환해주는 기능
        animator = GetComponentInChildren<Animator>();  // 겟컴포넌트 뒤에 인칠드런 : Player 클래스 하위 오브젝트(여기선 Model)에 달려있는 애니메이터 가져오기
        _rigidbody = GetComponent<Rigidbody2D>(); // Player 클래스에 달려있는 리지드바디2D 가져오기

        // 예외 처리
        if (animator == null) // 애니메이터가 null일 때
            Debug.LogError("Not Founded Animator"); // 디버그.로그나 디버그.로그에러를 쓰면 뒤의 문구가 콘솔창에 나옴

        if (_rigidbody == null) // 리지드바디가 null일 때
            Debug.LogError("Not Founded Rigidbody"); // 디버그.로그나 디버그.로그에러를 쓰면 뒤의 문구가 콘솔창에 나옴
    }

    // Update is called once per frame
    void Update() // 매 프레임마다 호출, 주로 입력 처리와 게임 로직에 사용
    {
        if (isDead) // 죽었을 때
        {
            if (deathCooldown <= 0) 
            {
                // 메인으로 돌아가기
                if (Input.GetKeyDown(KeyCode.Escape)) // Esc 누르면
                {
                    flappyManager.SetBestScore();                    

                    sceneChanger.GoMainScene(); // 씬체인저의 GoMainScene 메서드 호출                    
                }
            }
            else // 데스쿨다운 > 0 : 데스쿨다운이 남아있다
            {
                deathCooldown -= Time.deltaTime; // 델타타임 : 업데이트에서만 사용 가능, 프레임 수 상관없이 동일한 시간 컨트롤 가능
            }
        }
        else // 죽지 않았을 때(게임 중일 때)
        {
            // 키입력 받아서 점프를 뛸 수 있게 할 것임
            // 키입력은 인풋.겟키다운(키코드.원하는키)
            // 마우스입력은 잇풋.겟마우스버튼(int button == 버튼숫자)
            // 마우스 버튼숫자
            // 0 = 좌클릭, 1 = 우클릭, 2 = 휠클릭, 3 = 뒤로가기, 4 = 앞으로가기 (0번 경우 스마트폰에서 터치 기능까지 포함)
            if (Input.GetKeyDown(KeyCode.Space)) // 스페이스 키를 누르면
            {
                isFlap = true; // 점프를 했다, 여기서 바로 처리할 수도 있지만, 물리 처리를 위해 밑의 픽스드업데이트에서 로직을 만들어줄 것임
            }
        }
    }
        
    private void FixedUpdate() // 물리 연산이 필요할 때 일정한 간격으로 호출
    {
        if (isDead) return; // 죽었다면, 아무 작업하지 않고 그냥 리턴

        Vector3 velocity = _rigidbody.velocity; // 벨로시티는 가속도를 의미, _리지드바디의 가속도를 가져올 것임
        velocity.x = forwardSpeed; // 벨로시티의 x축 값을 정면 이동 스피드로 정해주기

        if (isFlap) // 점프를 했다면
        {
            velocity.y += flapForce; // 벨로시티의 y축 값을 점프하는 힘을 더해준 것으로 바꿔주기
            isFlap = false ; // 점프했기 때문에 다시 이스플랩 false로 바꿔주기
        }

        // _리지드바디의 벨로시티값을 가지고만 온 것이지, _리지드바디.벨로시티가 바뀐 것은 아님(벡터3는 스트럭트(구조체)이기 때문에 값을 복사만 한 것)
        _rigidbody.velocity = velocity; // 그래서 바로 위에서 바꾼 벡터3 벨로시티 값을 다시 한번 _리지드바디.벨로시티에 넣어줌

        // Player가 각도도 바뀔 수 있게 angle값 정해주기
        // 매쓰에프.클램프(기준값, 최소값, 최대값)
        // 기준값 : _리지드바디.벨로시티.y : 위로 올라가는지, 아래로 떨어지는지. 너무 작기 때문에 x10 해줌
        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f),-90, 90);
        // 트랜스폼도 컴포넌트 : 트랜스폼과 게임 오브젝트 속성은 너무 자주 사용해서 내장되어 있음
        // 트랜스폼 안의 로테이션 값을 가져와서 지정해줘서 회전할 수 있도록 할것임(로테이션값은 벡터가 아니라 쿼터니언이라는 사원수값을 쓰고 있음)
        // 그래서 쿼터니언.오일러를 씀(오일러각도가 우리가 알고 있는 0도, 360도 등의 각도를 의미)
        // 쿼터니언.오일러(x축을 축으로 회전값, y축을 축으로 회전값, z축을 축으로 회전값), 바로 파악하기 어렵기 때문에 유니티에서 돌려보면서 어느 값 조정해야되는지 확인
        transform.rotation = Quaternion.Euler(0, 0, angle); // 위에서 정한 angle 값 알맞는 축 : z축 부분에 넣어줌
    }

    // 충돌 관련 처리
    // 유니티에서 Collider안의 isTrigger가 꺼져있으면 실제 충돌하는 것 모두 구현(튕겨져나온다든지), isTrigger 켜져있으면 통보만 해줄뿐 통과한다
    // OnCollisionEnter: 충돌 시작 시 호출
    // OnCollisionStay: 충돌이 유지되는 동안 호출
    // OnCollisionExit: 충돌이 끝났을 때 호출
    private void OnCollisionEnter2D(Collision2D collision) // 매개변수로 들어오는 값은 콜리젼, 충돌에 대한 정보 알려줌(어디서, 어떻게 등)
    {
        if (godMode) return; // godMode true면(무적모드 켜면) 리턴

        if (isDead) return; // isDead true면(죽었으면) 리턴

        // 이외의 경우에 충돌했을 때
        isDead = true; // 죽었다고 만들어줌
        deathCooldown = 0.01f; // 데스쿨다운 = 1초(죽고 나면 몇 초 뒤 게임 재시작할 수 있게 만들어줄 것임)

        // 애니메이터의 Integer를 정해준다, 여기선 IsDie(미리 유니티에서 IsDie 값이 1이 되면 die 애니매이션이 나오게 해놓음)
        animator.SetInteger("IsDie", 1);
        flappyManager.GameOver(); // 충돌을 했기 때문에 게임매니저의 GameOver 메서드 호출        
    }
}
