// ����� ��� �������� ������ ������
public class MazeCell
{
    // ���������� 
    public int X;
    public int Y;

    // ������� ����
    public bool UpW = true;
    public bool RightW = true;
    public bool BottomW = true;
    public bool LeftW = true;

    public bool start = false;

    // ��������� ������������
    public bool visited = false;

    public int numInside = -1;
}
