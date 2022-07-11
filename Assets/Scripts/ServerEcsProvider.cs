using GameLogic;
using RiptideNetworking;

public class ServerEcsProvider : BaseEcsProvider
{
    public Server Server
    {
        set
        {
            systems.Inject(value);
            systems.Init();
        }
    }
}