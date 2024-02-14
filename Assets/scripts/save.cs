using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public GameObject score_object = null; 
    public int score_num = 0;

    // ���������̏���
    void Start()
    {
        // �X�R�A�̃��[�h
        score_num = PlayerPrefs.GetInt("SCORE", 0);
    }
    // �폜���̏���
    void OnDestroy()
    {
        // �X�R�A��ۑ�
        PlayerPrefs.SetInt("SCORE", score_num);
        PlayerPrefs.Save();
    }

    // �X�V
    void Update()
    {
        // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
        Text score_text = score_object.GetComponent<Text>();
        // �e�L�X�g�̕\�������ւ���
        score_text.text = "Score:" + score_num;

        score_num += 1; // �Ƃ肠����1���Z�������Ă݂�
    }
}