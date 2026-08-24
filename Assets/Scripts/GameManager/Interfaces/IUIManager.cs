using UnityEngine;

public interface IUIManager
{
    bool Navigating { get; }
    bool Confirm { get; }
    bool Start { get; }
    void SetConfirmAction();
    void SetScoreDisplay(int points);
    void SetTimerDisplay(string time);
    void SetAirJumpPowerUpUI(bool isInfinity);
    void SetDashPowerUpUI(bool isInfinity);
    void SetWallMovePowerUpUI(bool isInfinity);
    void SetHourglassDisplay(int count);
    void SetOvertimeAirJumpPowerUpUI(string clip, float value, CharacterPowerUpManager characterPowerUpManager);
    void SetOvertimeDashPowerUpUI(string clip, float value, CharacterPowerUpManager characterPowerUpManager);
    void SetOvertimeWallMovePowerUpUI(string clip, float value, CharacterPowerUpManager characterPowerUpManager);
    void SetHubUIObjects();
    void SetLevelUIObjects();
    void SetTrophyUIPosition();
    void SetGemScoreText(string text);
    void SetHourglassText(string text);
    void SetTimeScoreText(string text);
    void SetMasterScoreText(string text);
    void SetFillAmount(float value);
    void ResetBrassTrophy();
    void ResetSilverTrophy();
    void ResetGoldTrophy();
    void ResetScoreUI();
    void ShowScorePanel();
    void HideScorePanel();
}