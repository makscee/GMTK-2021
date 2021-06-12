using UnityEngine;

public class ShapeConnectionCheckerBase : MonoBehaviour
{
    [SerializeField] ShapeBlockConnectionChecker[] checkers = new ShapeBlockConnectionChecker[4];
    [SerializeField] ShapeBlock block;

    public void DisableDir(int dir)
    {
        checkers[dir].gameObject.SetActive(false);
    }

    public void TryToConnect()
    {
        foreach (var checker in checkers)
        {
            if (checker.dot == null) continue;
            var b = checker.dot.GetComponentInParent<Block>();
            var bind = BindMatrix.AddBind(block, b, Utils.CoordsFromDir(checker.dir), GlobalConfig.Instance.shapesBindStr);
            var visual = BindVisual.Create(block.transform, b.transform, GlobalConfig.Instance.bindShape);
            bind.visual = visual;
            block.additionalBinds.Add(bind);
        }
    }
}