using NUnit.Framework;

public class TorchTests
{
    [Test]
    public void Torch_ShouldStartUnlit()
    {
        var torch = new Torch();

        Assert.IsFalse(torch.IsLit);
    }

    [Test]
    public void Torch_CannotLightWithoutPlayer()
    {
        var torch = new Torch();

        var result = torch.TryLight();

        Assert.IsFalse(result);
        Assert.IsFalse(torch.IsLit);
    }

    [Test]
    public void Torch_LightsWhenPlayerNearby()
    {
        var torch = new Torch();

        torch.SetPlayerNearby(true);
        var result = torch.TryLight();

        Assert.IsTrue(result);
        Assert.IsTrue(torch.IsLit);
    }
}