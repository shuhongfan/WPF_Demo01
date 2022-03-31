using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTcpServer
{
    /// <summary>处理游戏大厅中每个房间的玩家</summary>
    public class Room
    {
        private const int max = 16; //网格最大的行列数

        /// <summary>保存同一房间的两个玩家</summary>
        public User[] players { get; set; }

        /// <summary>-1：无棋子，0：黑棋，1：白棋</summary>
        private int[,] grid = new int[max, max];

        /// <summary>定时产生棋子的计时器</summary>
        public System.Timers.Timer timer { get; set; }

        /// <summary>下一步产生的棋子颜色号（0：黑棋,1：白棋）</summary>
        private int nextColor = 0;

        private Random rnd = new Random();

        public Room()
        {
            players = new User[2];
            timer = new System.Timers.Timer();
            timer.Interval = 500; //默认难度级别：4级
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = false;
            ResetGrid();
        }

        /// <summary>重置棋盘</summary>
        public void ResetGrid()
        {
            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    grid[i, j] = -1;  //-1表示无棋子
                }
            }
        }

        /// <summary>达到时间间隔时处理事件</summary>
        private void timer_Elapsed(object sender, EventArgs e)
        {
            int x, y;
            //随机产生一个格内没有棋子的单元格位置
            do
            {
                x = rnd.Next(max);  //产生一个小于max的非负整数
                y = rnd.Next(max);
            } while (grid[x, y] != -1);
            players[0].callback.GridSetDot(x, y, nextColor);
            players[1].callback.GridSetDot(x, y, nextColor);
            CheckDot(x, y, nextColor);
            nextColor = (nextColor + 1) % 2; //设定下次分发的棋子颜色
        }

        /// <summary>检查是否有相邻棋子。参数：行，列，颜色号</summary>
        private void CheckDot(int i, int j, int color)
        {
            grid[i, j] = color;
            #region 判断当前行是否有相邻点
            int k1, k2;                          //k1:循环初值，k2:循环终值
            if (i == 0) k1 = k2 = 1;              //如果是首行，只需要判断下边的点
            else if (i == max - 1) k1 = k2 = max - 2;  //如果是最后一行，只需要判断上边的点
            else k1 = i - 1; k2 = i + 1;          //如果是中间的行，上下两边的点都要判断
            for (int x = k1; x <= k2; x += 2)
            {
                if (grid[x, j] == color)
                {
                    ShowFail(color);
                    break;
                }
            }
            #endregion
            #region 判断当前列是否有相邻点
            if (j == 0) k1 = k2 = 1;
            else if (j == max -1) k1 = k2 = max - 2;
            else k1 = j - 1; k2 = j + 1;
            for (int y = k1; y <= k2; y += 2)
            {
                if (grid[i, y] == color)
                {
                    ShowFail(color);
                    break;
                }
            }
            #endregion
        }

        /// <summary>出现相邻点的颜色为dotColor</summary>
        /// <param name="color">相邻点的颜色</param>
        private void ShowFail(int color)
        {
            timer.Enabled = false;
            players[0].IsStarted = false;
            players[1].IsStarted = false;
            if (color == 0)
            {
                players[0].callback.ShowWin("白方胜（黑方出现相邻点）");
                players[1].callback.ShowWin("白方胜（黑方出现相邻点）");
            }
            else
            {
                players[0].callback.ShowWin("黑方胜（白方出现相邻点）");
                players[1].callback.ShowWin("黑方胜（白方出现相邻点）");
            }
            this.ResetGrid();
        }

        /// <summary>去掉棋子。参数：行，列</summary>
        public void UnsetGridDot(int i, int j)
        {
            grid[i, j] = -1;
            players[0].callback.GridUnsetDot(i, j);
            players[1].callback.GridUnsetDot(i, j);
        }
    }
}
