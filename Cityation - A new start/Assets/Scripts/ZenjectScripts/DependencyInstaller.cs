using System;
using UnityEngine;
using Zenject;

public class DependencyInstaller : MonoInstaller
{
    public GameManager gameManager;
    public LevelManager levelManager;
    public Sprite defaultSprite;
    public override void InstallBindings()
    {
        Container.Bind<IInputController>().To<InputController>().AsSingle();
        //Container.Bind<IReplayController>().To<ReplayController>().AsTransient();
        Container.Bind<IReplayController>().FromMethod(InjectReplayController).AsTransient();
        Container.Bind<GameManager>().FromComponentOn(gameManager.gameObject).AsSingle();
        Container.Bind<LevelManager>().FromComponentOn(levelManager.gameObject).AsSingle();
        Container.Bind<Sprite>().FromMethod(() => defaultSprite);
    }

    private IReplayController InjectReplayController(InjectContext context)
    {
        {
            if (context.ObjectInstance is Component)
            {
                return new ReplayController(((Component)context.ObjectInstance).transform);
            }
            return new ReplayController();
        }
    }
}