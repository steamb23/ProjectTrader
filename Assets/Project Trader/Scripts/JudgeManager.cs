using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using TMPro;
using ProjectTrader;

/// <summary>
/// 심사 연출 관리자
/// </summary>
public class JudgeManager : MonoBehaviour
{
    [Tooltip("심사 위원")]
    [SerializeField] JudgeAi[] judgePrefabs;
    [SerializeField] PathNodeManager pathNodeManager;
    [SerializeField] JudgeResultWindow resultWindow;

    List<JudgeAi> judgeInstances = new List<JudgeAi>();

    bool isDirecting;

    public bool IsDirecting => isDirecting;

    public void Start()
    {
        pathNodeManager = FindObjectOfType<PathNodeManager>();
    }

    /// <summary>
    /// 연출 시작
    /// </summary>
    public void StartDirecting()
    {
        StartCoroutine(Coroutine());

        IEnumerator Coroutine()
        {
            isDirecting = true;
            var waitObject = new WaitForSeconds(0.5f);

            #region 심사위원 생성 및 이동
            int i = 0;
            foreach (var judgePrefab in judgePrefabs.OrderBy((item) => Random.Range(0, int.MaxValue)))
            {
                var judgeInstance = Instantiate(judgePrefab);
                judgeInstance.transform.position = pathNodeManager.exitNode.transform.position;
                judgeInstance.SetTarget(pathNodeManager.judgeNodes[i].transform);

                judgeInstances.Add(judgeInstance);

                i++;
                yield return waitObject;
            }
            #endregion

            #region 심사위원이 위치할때까지 대기
            bool isWait;
            do
            {
                isWait = false;
                foreach (var judgeInstance in judgeInstances)
                {
                    // 심사위원이 아직 위치에 도착하지 않았으면 계속 대기
                    if (judgeInstance.targetTransform != null)
                    {
                        isWait = true;
                        break;
                    }
                    //// 도착했으면 정면을 바라봄
                    //else
                    //{
                    //    judgeInstance.RandomDirection();
                    //}
                }
                if (isWait)
                    yield return null;
            } while (isWait);
            #endregion

            #region 위치했으면 텍스트 띄우기
            var text = FloatingText.Create(judgeInstances[judgeInstances.Count - 1].transform, new Vector3(1, 1), "");

            waitObject = new WaitForSeconds(1);
            // 귀찮으므로 하드코딩
            {
                text.Text = "심사숙고중";
                yield return waitObject;
                text.Text = "심사숙고중.";
                yield return waitObject;
                text.Text = "심사숙고중..";
                yield return waitObject;
                text.Text = "심사숙고중...";
                yield return waitObject;
                text.Text = "한눈 파는 중";
                yield return waitObject;
                text.Text = "한눈 파는 중.";
                yield return waitObject;
                text.Text = "한눈 파는 중..";
                yield return waitObject;
                text.Text = "한눈 파는 중...";
                yield return waitObject;
                text.Text = "쓰레기 확인 중";
                yield return waitObject;
                text.Text = "쓰레기 확인 중.";
                yield return waitObject;
                text.Text = "쓰레기 확인 중..";
                yield return waitObject;
                text.Text = "쓰레기 확인 중...";
                yield return waitObject;
                text.Text = "물 마시는 중";
                yield return waitObject;
                text.Text = "물 마시는 중.";
                yield return waitObject;
                text.Text = "물 마시는 중..";
                yield return waitObject;
                text.Text = "물 마시는 중...";
                yield return waitObject;
                text.Text = "의논 중";
                yield return waitObject;
                text.Text = "의논 중.";
                yield return waitObject;
                text.Text = "의논 중..";
                yield return waitObject;
                text.Text = "의논 중...";
                yield return waitObject;
            }
            Destroy(text.gameObject);
            #endregion

            #region 심사위원 퇴장
            foreach (var judgeInstance in judgeInstances)
            {
                judgeInstance.SetTarget(pathNodeManager.exitNode.transform);
            }
            #endregion

            #region 심사 결과창
            // 목표 인지도 설정
            int goal = 0;
            int successReward = 0;
            int failReward = 0;
            switch (PlayData.CurrentData.Level)
            {
                case 1:
                    goal = 1000;
                    successReward = 30000;
                    failReward = 5000;
                    break;
                case 2:
                    goal = 2500;
                    successReward = 50000;
                    failReward = 5000;
                    break;
                default:
                    goal = int.MaxValue;
                    successReward = 0;
                    failReward = 0;
                    break;
            }

            if (PlayData.CurrentData.Awareness > goal)
            {
                // 상금 지급
                PlayData.CurrentData.Money += successReward;
                // 등급 업 처리
                PlayData.CurrentData.Level += 1;
                resultWindow.ShowSucess(successReward);
            }
            else
            {
                PlayData.CurrentData.Money += failReward;
                resultWindow.ShowFail(failReward);
            }
            isDirecting = false;
            #endregion

            #region 심사위원 나가기 체크
            do
            {
                isWait = false;
                foreach (var judgeInstance in judgeInstances)
                {
                    // 심사위원이 아직 위치에 도착하지 않았으면 계속 대기
                    if (judgeInstance != null)
                    {
                        if (judgeInstance.targetTransform != null)
                        {
                            isWait = true;
                            //break;
                        }
                        // 도착했으면 삭제
                        else
                        {
                            Destroy(judgeInstance.gameObject);
                        }
                    }
                }
                if (isWait)
                    yield return null;
            } while (isWait);
            judgeInstances.Clear();
            #endregion
        }
    }
}