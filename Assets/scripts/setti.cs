using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setti : MonoBehaviour
{
    GameObject masu;
    GameObject ddmasu;
    GameObject ablemasu;
    GameObject[,] mp = new GameObject[8, 8];
    int turn;
    int state = 0;
    int[,] field = new int[8,8];


    // Start is called before the first frame update
    void Start()
    {
        masu = (GameObject)Resources.Load("normal");
        ddmasu = (GameObject)Resources.Load("damaged");
        ablemasu = (GameObject)Resources.Load("black");
        int i;
        int j;
       
        for(i=0; i<8; i++)
        {
            for(j=0; j<8; j++)
            {
                mp[i, j] = Instantiate(masu, new Vector3((i - 4) * 32+16, (j - 4) * 32+16,0),Quaternion.identity);
                field[i, j] = 0;

            }
        }
        mp[3, 3] = Instantiate(ddmasu, new Vector3((3 - 4) * 32 + 16, (3 - 4) * 32 + 16, 0), Quaternion.identity);
        mp[3, 4] = Instantiate(ddmasu, new Vector3((3 - 4) * 32 + 16, (4 - 4) * 32 + 16, 0), Quaternion.identity);
        mp[4, 3] = Instantiate(ddmasu, new Vector3((4 - 4) * 32 + 16, (3 - 4) * 32 + 16, 0), Quaternion.identity);
        mp[4, 4] = Instantiate(ddmasu, new Vector3((4 - 4) * 32 + 16, (4 - 4) * 32 + 16, 0), Quaternion.identity);
        state = 1;


        //normal=0, me=1, enermey=2
        field[3, 3] = 1;
        field[4, 4] = 1;
        field[3, 4] = 2;
        field[4, 3] = 2;

        turn = 1;//my_turn=1, enermy_turn=0

    }

        bool checkfield(int x,int y)
    {
        int[] px = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };//対象のマスから８方向へのx座標
        int[] py = new int[] { 1, 1, 1, 0, 0, -1, -1, -1};//対象のマスから８方向へのy座標
        bool checkPut = false;

        int k;
        for(k=0; k<8; k++)
        {
            if (x + px[k] > 0 && x + px[k] < 8 && y + py[k] > 0 && y + py[k]<8)
            {
                if (field[x, y] == 0 && field[x + px[k], y + py[k]] != turn && field[x + px[k], y + py[k]] != 0)//置くマスが空マスか？,周囲の色は自分と違うか？,それが空マスでないか？
                {
                    int nx = px[k] + x;//周囲のマスのx座標
                    int ny = py[k] + y;//周囲のマスのy座標
                    
                    while (true)
                    {
                        nx += px[k];
                        ny += py[k];
                        if (nx < 0 || nx >= 8 || ny < 0 || ny >= 8)//端っこに着いたら終わる
                        { break; }

                        if (field[nx, ny] == turn && field[nx, ny] !=0)
                        {
                            checkPut = true;
                            break;
                        }
                    }
                }
            }
        }
        return checkPut;
    }


    void Update()
    {
        if(state==1)
        {
            for(int i=0; i<8; i++)
            {
                for(int j=0; j<8; j++)
                {
                    if (checkfield(i, j))
                    {
                        mp[i, j] = Instantiate(ablemasu, new Vector3((i - 4) * 32 + 16, (j - 4) * 32 + 16, 0), Quaternion.identity);
                    }
                }
            }
        }
    }
}
