namespace PC.Spreadsheet
{
    public struct Cell
    {
        private string _formula;
        public string formula { get { return _formula; } set { SetFormula(value); } }
        public double? value { get; set; }

        private void SetFormula(string value)
        {
            _formula = value.Trim().ToUpper().Replace("  ", " ");   // auto-sanitization
        }

        public void Clear()
        {
            _formula = "";
            value = null;
        }      
    }
}
