using UnityEngine;

public class ShapeConnectionCheckerBase : MonoBehaviour
{
    [SerializeField] ShapeBlockConnectionChecker[] checkers = new ShapeBlockConnectionChecker[4];
    public ShapeBlock block;

    public void DisableDir(int dir)
    {
        checkers[dir].gameObject.SetActive(false);
    }

    public bool TryToConnect()
    {
        var result = false;
        foreach (var checker in checkers)
        {
            if (checker.previewBind == null || !checker.previewBind.gameObject.activeSelf) continue;
            var b = checker.dot.GetComponentInParent<Block>();
            var bind = BindMatrix.AddBind(block, b, Utils.CoordsFromDir(checker.dir), GlobalConfig.Instance.shapesBindStr);
            var visual = BindVisual.Create(block, b, GlobalConfig.Instance.bindShape);
            SharedObjects.instance.soundsPlayer.PlayConnectSound();
            bind.visual = visual;
            result = true;
        }

        return result;
    }

    public void SetPreviewsEnabled(bool value)
    {
        foreach (var checker in checkers)
            checker.SetPreviewEnabled(value);
    }

    public void Destroy()
    {
        foreach (var checker in checkers)
            if (checker.previewBind != null)
                checker.previewBind.Destroy();
    }
}