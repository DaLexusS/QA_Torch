using NUnit.Framework;

public class TorchUnitTests
{
    private Torch _torch;

    [SetUp]
    public void Setup()
    {
        _torch = new Torch();
    }

    [Test]
    public void Torch_Starts_Unlit_And_Player_Not_Nearby()
    {
        Assert.IsFalse(_torch.IsLit, "Torch should start unlit.");
        Assert.IsFalse(_torch.IsPlayerNearby, "Player should not be nearby initially.");
    }

    [Test]
    public void Torch_WhenPlayerNearby_CanBeLit()
    {
        _torch.SetPlayerNearby(true);
        bool success = _torch.TryLight();
        
        Assert.IsTrue(success, "TryLight should return true when player is nearby.");
        Assert.IsTrue(_torch.IsLit, "Torch status should change to IsLit.");
    }

    [Test]
    public void Torch_WhenPlayerNotNearby_CannotBeLit()
    {
        _torch.SetPlayerNearby(false);
        bool success = _torch.TryLight();
        
        Assert.IsFalse(success, "TryLight should return false when player is missing.");
        Assert.IsFalse(_torch.IsLit, "Torch should remain unlit.");
    }

    [Test]
    public void Torch_WhenPlayerLeaves_StaysLit()
    {
        _torch.SetPlayerNearby(true);
        _torch.TryLight();
        
        _torch.SetPlayerNearby(false);
        
        Assert.IsTrue(_torch.IsLit, "Torch must stay lit even after player leaves.");
    }
}