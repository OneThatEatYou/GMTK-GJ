using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NGHelper : MonoBehaviour
{
    public io.newgrounds.core core;

    public int winMedalId;
    public int scoreboardId;

    private void Start()
    {
        core.onReady(() =>
        {
            core.checkLogin((bool logged_in) =>
            {
                if (logged_in)
                {
                    OnLoggedIn();
                }
                else
                {
                    RequestLogin();
                }
            });
        });
    }

    void OnLoggedIn()
    {
        io.newgrounds.objects.user player = core.current_user;
    }

    void OnLoginFailed()
    {
        io.newgrounds.objects.error error = core.login_error;
    }

    void OnLoginCancelled()
    {
        
    }

    void RequestLogin()
    {
        core.requestLogin(OnLoggedIn, OnLoginFailed, OnLoginCancelled);
    }

    void UnlockMedal(int medal_Id)
    {
        io.newgrounds.components.Medal.unlock medal_Unlock = new io.newgrounds.components.Medal.unlock();
        medal_Unlock.id = medal_Id;
        medal_Unlock.callWith(core);

        Debug.Log("Sent a message to sever to unlock medal");
    }

    void SubmitScore(int scoreboard_id, int score)
    {
        io.newgrounds.components.ScoreBoard.postScore submitScore = new io.newgrounds.components.ScoreBoard.postScore();
        submitScore.id = scoreboard_id;
        submitScore.value = score;
        submitScore.callWith(core);

        Debug.Log("Sent score to sever");
    }

    void SubmitEvent(string eventName)
    {
        io.newgrounds.components.Event.logEvent clearEvent = new io.newgrounds.components.Event.logEvent();
        clearEvent.event_name = eventName;
        clearEvent.callWith(core);

        Debug.Log("Submitted event " + eventName + " to sever");
    }

    public void OnWinGame()
    {
        UnlockMedal(winMedalId);

        float elapsedTime = Time.time - GameManager.Instance.startTime;
        int ms = Mathf.RoundToInt(elapsedTime * 100);
        SubmitScore(scoreboardId, ms);
    }
}
