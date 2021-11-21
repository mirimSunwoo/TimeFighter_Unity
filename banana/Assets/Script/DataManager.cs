//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DataManager : MonoBehaviour
//{

//    // 유니크한 ID를 하나 만든다.
//    // todo : 서버 체크해야함
//    string FindUniqueID()
//    {
//        string id;
//        id = "Mirim" + UnityEngine.Random.Range(0, 1000000);

//        return id;
//    }

//    GameManager.Instance.GetPlugin().StorageSave("BestScore", BestScore, false, (state, message, rawData, dictionary) {
//        if (state.Equals(Configure.PN_API_STATE_SUCCESS))
//            Debug.Log("서버에 저장 완료");
//        else
//            Debug.Log("서버에 저장 실패");

//    }

//   public Plugin GetPlugin()
//    {
//        // playnanoo plugin
//        Plugin plugin = Plugin.GetInstance();
//        if (StaticData.ID == "")
//        {
//            Debug.Assert(false);
//        }
//        plugin.SetUUID(StaticData.ID);
//        plugin.SetLanguage("Configure.PN_LANG_KO");
//        return plugin;
//    }

//}
