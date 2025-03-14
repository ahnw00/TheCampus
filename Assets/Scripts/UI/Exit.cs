using UnityEngine;

public class Exit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void QuitGame()
    {
        // Unity 에디터에서는 Play 모드 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        // 빌드된 게임에서는 어플리케이션 종료
        Application.Quit();
    }
}
