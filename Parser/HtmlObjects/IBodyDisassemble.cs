namespace Parser.HtmlObjects
{
    internal interface IBodyDisassemble
    {
        string Disa { get; }

        void SetData(ref string[] body);
        string Disassemble(int nowDepth);
    }
}