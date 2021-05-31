using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Bullets : MonoBehaviour
{
    // this script is not valid anymore
    public PlayerLives _playerLives = new PlayerLives();

    public int readPlayerLivesFromBoard(int commonCounter, TrackableBehaviour mTrackableBehaviour)
    {
        int maxLivesExisting = 5; // 5 at the moment -- DON'T FORGET to change this in the future when all lives are in db
        for (int i = 1; i <= maxLivesExisting; i++) {
            if (mTrackableBehaviour.TrackableName.ToString() == CustomEnums.Lives.BULLET_LIFE_ + i.ToString() && mTrackableBehaviour.CurrentStatus.ToString() == "TRACKED" && mTrackableBehaviour.CurrentStatusInfo.ToString() == "NORMAL") {
                commonCounter++;
                _playerLives.increaseLives(commonCounter, "Bullets");
            }
        }

        return commonCounter;

        //print("INTRA AICI!!!!!!!!!!!!!!!!!!!!!!!!!!!---------------------XXXXXXXXXXXXXXXXXXX " + countLivesFromBoard);
        //print("EEEEEEEE " + (CustomEnums.Lives.BULLET_LIFE_ + 1.ToString()));



        //foreach (Player player in PhotonNetwork.PlayerList)
        //{

        //print("EEEEEEEEEEEEEE " + CustomEnums.Lives.BULLET_LIFE_ + i.ToString());

        //print("DAAAA111111");


        //if (mTrackableBehaviour.TrackableName.ToString() == CustomEnums.Lives.BULLET_LIFE_1.ToString()) {
        //    // avoid counting the same life more times
        //    _playerLives.increaseLives(true);
        //}

        //_myCustomProperties["PlayerLives"] = countLivesFromBoard;
        //_playerLives.increaseLives();
        //    //print("FFFFFFFFFFFFFFF " + countLivesFromBoard);
        //    _myCustomProperties["PlayerLives"] = countLivesFromBoard;
        //    _playerLives.setLivesHUD(countLivesFromBoard);
        //    //GUIUtility.ExitGUI();
        //    countLivesFromBoard = 0; // reset the lives counter at the next player
        //}


        //if (mTrackableBehaviour.TrackableName.ToString() == CustomEnums.Lives.BULLET_LIFE_ + 1.ToString())
        //{
        //    print("FOUND LIFE 1");
        //    _playerLives.increaseLives();
        //}
        //if (mTrackableBehaviour.TrackableName.ToString() == CustomEnums.Lives.BULLET_LIFE_ + 2.ToString())
        //{
        //    print("FOUND LIFE 2");
        //    _playerLives.increaseLives();
        //}
        //if (mTrackableBehaviour.TrackableName.ToString() == CustomEnums.Lives.BULLET_LIFE_ + 3.ToString())
        //{
        //    print("FOUND LIFE 3");
        //    _playerLives.increaseLives();
        //}
        //if (mTrackableBehaviour.TrackableName.ToString() == CustomEnums.Lives.BULLET_LIFE_ + 4.ToString())
        //{
        //    print("FOUND LIFE 4");
        //    _playerLives.increaseLives();
        //}
        //if (mTrackableBehaviour.TrackableName.ToString() == CustomEnums.Lives.BULLET_LIFE_ + 5.ToString())
        //{
        //    print("FOUND LIFE 5");
        //    _playerLives.increaseLives();
        //}
        //_playerLives.setLivesHUD(countLivesFromBoard);
    }
}
