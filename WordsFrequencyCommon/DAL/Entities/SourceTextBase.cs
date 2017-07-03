using System.ComponentModel.DataAnnotations.Schema;

namespace WordsFrequency.Common.DAL.Entities
{
    [Table("SourceText")]
    public class SourceTextBase : EntityBase, ISourceText
    {
        public string Text { get; set; }
    }
}
