using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : System.Object {
    private static SSDirector _instance;
    public ISceneController currentSceneController { get; set; }
    public static SSDirector getInstance() {
        if (_instance == null) {
            _instance = new SSDirector();
        }
        return _instance;
    }

    public FirstController.State isMoving() {
        return gameobject.state;
    }
    public void setMoving(FirstController.State state) {
        gameobject.state = state;
    }
    private FirstController gameobject;

    public FirstController getGameObject()
    {
        return gameobject;
    }
    public void setGameObject(FirstController other)
    {
        if (gameobject == null)
            gameobject = other;
    }
}