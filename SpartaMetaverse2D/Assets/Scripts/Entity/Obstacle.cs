using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start 와 Update 둘 다 사용하지 않을 거기 때문에 지워줌
    // 변수들을 모두 public으로 선언해주었는데
    // 변수들이 public이면 유니티에서 인스펙터에서 변수들을 직접 수정할 수가 있게 된다.
    // public으로 선언하지 않은 경우에는 인스펙터에서 보이지 않아서 수정을 할 수가 없다.

    // Obstacle이 상하로 이동할 때 얼마나 이동할 것인지 정해줌
    public float highPosY = 1f;
    public float lowPosY = -1f;

    // top 과 bottom 사이의 공간을 얼마나 잡아줄 것인지 정해줌
    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    // 오브젝트들 배치할 때 사이의 폭을 얼마나 잡아줄 것인지 정해줌
    public Transform topObject;
    public Transform bottomObject;
    public float widthPadding = 4f;

    // 점수가 오르게 구현, Obstacle에서도 게임매니저에 접근할 수 있게 싱글턴 활용
    FlappyManager flappyManager;

    private void Start()
    {
        flappyManager = FlappyManager.Instance;

    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax); // 홀사이즈 최소와 최대 사이의 랜덤값 지정
        float halfHoleSize = holeSize / 2; // 하프홀사이즈 = 홀사이즈의 반으로 지정

        topObject.localPosition = new Vector3(0, halfHoleSize); // 정해진 홀사이즈 반만큼 위로 올림
        bottomObject.localPosition = new Vector3(0, -halfHoleSize); // bottom오브젝트는 반대로 내림
        // 홀사이즈만큼 두 오브젝트를 벌려준 것

        // 가장 마지막에 놓인 오브젝트 뒤에 widthPadding만큼 더한 값으로 새로 배치
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        // 새로운 배치 y값 지정
        placePosition.y = Random.Range(lowPosY, highPosY); // lowPosY 와 highPosY 사이의 랜덤값 지정

        transform.position = placePosition;

        return placePosition; // SetRandomPlace 메서드 반환값

        // localPosition 과 position 차이 : 로컬좌표와 월드좌표의 차이이다
        // 로컬좌표 : 부모 오브젝트 기준. 오브젝트가 부모 오브젝트에서 얼마나 떨어져있는지. 부모 오브젝트 위치나 회전이 변경되면 로컬좌표에 반영, 상대적인 위치 유지
        // 월드좌표 : 게임 전체 월드에서 위치. 유니티 전역 좌표계(0, 0, 0)을 기준으로 위치 잡는 것
    }

    // 점수 오르게 구현, Obstacle이 BoxCollider를 가지고 있고 거기 Trigger도 달려있으므로
    // BoxCollider를 벗어날 때 = Trigger를 Exit할 때 점수 오르게
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Player가 맞는지 확인하기 위해 플레이어 겟컴포넌트
        Player player = collision.GetComponent<Player>();
        if (player != null) // player가 null이 아니라면 = player가 맞다
        {
            flappyManager.AddScore(1);            
        }
    }
}
