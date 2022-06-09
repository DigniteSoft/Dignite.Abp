
namespace Dignite.Abp.FieldCustomizing.FieldControls.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class TableRow: IHasCustomizableFields
    {
        public TableRow()
        {
            this.CustomizedFields = new CustomizeFieldDictionary();
        }

        public CustomizeFieldDictionary CustomizedFields { get; set; }
    }
}
