namespace project
{
    public class Cell
    {
        public string Name {get; set;}
        public string Value {get; set;}

        public Cell(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}