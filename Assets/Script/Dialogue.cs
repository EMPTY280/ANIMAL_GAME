using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public int segNum; // 불러올 조각 번호 '0'이면 안 불러옴
    public float textTime; // 텍스트 유지 시간
    public string text; // 대사
    public int questCondition; // 퀘스트 클리어 조건
    public string addText;
}
