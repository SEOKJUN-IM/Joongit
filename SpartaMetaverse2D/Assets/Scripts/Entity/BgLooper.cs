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
        // Start에서 obstacle들을 전부 찾아올 것임
        // public으로 열어놓고 다 연결하는 것도 가능하지만, GameObject의 클래스를 사용해보기
        // GameObject에는 FindObjectsOfType<>이 있는데, 이 씬을 전체를 돌아다니며 <>안의 것(여기선 Obstacle)이 달려있는 것을 찾아오는 메서드
        // 가볍지 않기 때문에 Start나 Awake에서 한번만 동작할 수 있게 해주는 것이 좋음
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleLastPosition = obstacles[0].transform.position; // 첫 번째 obstacles 포지션으로 보냄
        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacleCount; i++) // 반복문으로 obstacle 랜덤 배치
        {
            // 미리 만들어놓은 SetRandomPlace에 obstacleLastPosition과 obstacleCount를 넣어주면 동작
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
            // 각각의 배치가 끝나면 배치한 위치를 받아와서 그 다음 obstacle이 배치될 곳을 전달해줌
        }
    }
    // 이렇게 하면 처음에 모든 장애물들을 찾고, 그 장애물들을 처음 장애물부터 끝 장애물까지 배치를 해줌

    // 충돌 처리 : BgLooper랑 Trigger 충돌을 하는 장애물들을 다 뒤로 보낼 것임
    // 그래서 온트리거엔터2D 사용 : 실제 충돌 효과가 나는 것이 아닌 충돌에 대한 통보만 해줌
    // 그래서 온트리거엔터2D 는 충돌에 대한 정보를 줄 순 없고, 충돌체에 대한 정보만 주게 됨
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 컴포넌트 없는 경우
        if (collision.CompareTag("BackGround")) // 유니티에서 미리 background 오브젝트들에 붙여놓은 Tag : BackGround 를 가진 애들과 비교
        {
            // 각각의 오브젝트들의 가로 사이즈를 찾아올 것임
            // 가로 사이즈는 Collider가 이미 알고 있고, 오브젝트들에 BoxCollider2D를 달아놨기 때문에 가져올 수 있다.
            // 그 BoxCollider2D는 collision이라고 하는 Collider이다.
            // Collider2D는 모든 Collider들의 부모 클래스, 실제로 그 클래스로 접근을 하는 게 아니라 BoxCollider2D의 사이즈를 가져올 수 없다.
            // 그래서 collision을 잠시 BoxCollider2D로 바꿔줌
            float widthOfBgOgject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position; // 충돌한 오브젝트 위치 가져옴

            pos.x += widthOfBgOgject * numBgCount; // x포지션값에 가로 사이즈x배경카운트수 더해주기
            collision.transform.position = pos; // 변조한 pos값 다시 넣어주기
            return;
            // 이미 BackGround로 처리를 했다면 Obstacle이 아니기 때문에 return을 해서 더 이상 동작하지 않게 해주면 좋다.
        }

        // 컴포넌트 있는 경우
        // 충돌체에서 겟컴포넌트 해본다 : Obstacle이 있는지
        // 충돌한 오브젝트가 어떤 오브젝트인지 알고 싶을 때 겟컴포넌트 사용
        // Obstacle을 가져와봤는데 안 달려있다면 내가 알고 있는 Obstacle이 아닌 것
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle) // obstacle이 달려있다면 = obstacle != null 이라면
        {
            // 그렇다면 우리가 알고 있는 옵스타클 오브젝트임 똑같이 진행
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
}
