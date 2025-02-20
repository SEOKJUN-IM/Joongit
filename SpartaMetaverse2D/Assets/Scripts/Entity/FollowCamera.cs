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
        if (target == null) // target이 null이면 아무 처리할 필요 없으므로 리턴
            return;
        
        // 그렇지 않을 경우
        // 오프셋X를 트랜스폼.포지션.x 에서 플레이어의 x포지션 값 뺀 걸로 설정
        // 처음 배치됐을 때 카메라와 플레이어 사이의 거리가 저장이 될 것임, 이 거리를 유지하면서 이동하게 될 것
        offsetX = transform.position.x - target.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) // target이 null이면 아무 처리할 필요 없으므로 리턴
            return;

        Vector3 pos = transform.position; // 내 위치를 pos에 넣어주기
        pos.x = target.position.x + offsetX; // 카메라 위치가 타겟의 x포지션에서 아까 정한 거리만큼 떨어져서 계속 이동
        transform.position = pos; // pos에 값을 복사만 해서 가져와서 변경해준 거기 때문에 다시 트랜스폼.포지션에 변경한 pos값 넣어주기

        // 그냥 transform.position.x 에다가 offsetX 더하면 되는거 아닌가? 싶지만
        // transform.position.x 를 여기서 변경할 수가 없다.
        // 그래서 항상 position을 변조하고 싶을 때는 변수를 하나 지정해주고(여기선 Vector3 pos) 거기에 저장 후 변조, 마지막에 다시 넣는 작업으로 하기
    }
}
