using System.ComponentModel.DataAnnotations.Schema;

namespace WordsFrequency.Common.DAL.Entities
{
    [Table("SourceText")]
    public class SourceTextBase : EntityBase
    {
        public string Text { get; set; }

        public static SourceTextBase CreateInstance()
        {
            return CreateInstance<SourceTextBase>();
        }
    }    
}
