namespace Parser.Disassemble
{
    public interface IDisassemble
    {
        string Disassemble(string[] body, int nowDepth);
    }
}