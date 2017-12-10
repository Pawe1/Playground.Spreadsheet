namespace PC.Spreadsheet
{
    public struct Cell
    {
        private string _formula;
        public string Formula { get { return _formula; } set { setFormula(value); } }
        public double? Value { get; set; }

        private void setFormula(string value)
        {
            _formula = value.Trim().ToUpper().Replace("  ", " ");
        }

        public void clear()
        {
            _formula = "";
            Value = null;
        }      
    }
}
