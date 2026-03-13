using System.Text;

namespace TileProfile.Library.Models;

public class WindowProfile
{
    public string Name { get; set; }
    public int MainWindowWidth { get; set; }
    public int MainWindowHeight { get; set; }
    public List<WindowBox> WindowBoxes { get; set; }

    public string GetBoxesAsWmCtrlString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var box in WindowBoxes)
        {
            sb.AppendLine(box.GetTextAsWmCtrl());
        }
        return sb.ToString();
    }
}

public class WindowBox
{
    public int Index { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public string GetTextAsWmCtrl()
    {
        return $"{X},{Y},{Width},{Height}";
    }
}