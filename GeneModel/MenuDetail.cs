//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace GeneModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class MenuDetail
    {
        public int Id { get; set; }
        public string SubName { get; set; }
        public string SubUrl { get; set; }
        public int MenuInfoId { get; set; }
    
        public virtual MenuInfo MenuInfo { get; set; }
    }
}
