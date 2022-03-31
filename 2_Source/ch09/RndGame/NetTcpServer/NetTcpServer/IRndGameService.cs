using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NetTcpServer
{
    //需要服务端实现的协定
    [ServiceContract(Namespace = "RandomGameExample",
        //SessionMode = SessionMode.Required,
        CallbackContract = typeof(IRndGameServiceCallback))]
    public interface IRndGameService
    {
        [OperationContract(IsOneWay = true)]
        void Login(string userName);

        [OperationContract(IsOneWay = true)]
        void Logout(string userName);

        [OperationContract(IsOneWay = true)]
        void SitDown(string userName, int index, int side);

        [OperationContract(IsOneWay = true)]
        void GetUp(int index, int side);

        [OperationContract(IsOneWay = true)]
        void Start(string userName, int index, int side);

        [OperationContract(IsOneWay = true)]
        void SetLevel(int index, int level);

        [OperationContract(IsOneWay = true)]
        void UnsetDot(int index, int row, int col);

        [OperationContract(IsOneWay = true)]
        void Talk(int index, string userName, string message);
    }

    //需要客户端实现的接口
    public interface IRndGameServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void ShowLogin(string loginUserName, int maxTables);

        [OperationContract(IsOneWay = true)]
        void ShowLogout(string userName);

        [OperationContract(IsOneWay = true)]
        void ShowSitDown(string userName, int side);

        [OperationContract(IsOneWay = true)]
        void ShowGetUp(string userName, int side);

        [OperationContract(IsOneWay = true)]
        void ShowStart(int side);

        [OperationContract(IsOneWay = true)]
        void ShowTalk(string userName, string message);

        [OperationContract(IsOneWay = true)]
        void GameStart();

        [OperationContract(IsOneWay = true)]
        void ShowWin(string message);

        [OperationContract(IsOneWay = true)]
        void UpdateTablesInfo(string tablesInfo, int userCount);

        [OperationContract(IsOneWay = true)]
        void UpdateLevel(int lavel);

        [OperationContract(IsOneWay = true)]
        void GridSetDot(int row, int col, int color);

        [OperationContract(IsOneWay = true)]
        void GridUnsetDot(int row, int col);

    }
}
